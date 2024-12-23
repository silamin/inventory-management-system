package com.javainuse.sep03.service;
import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.annotation.JsonDeserialize;
import com.google.protobuf.Empty;
import com.javainuse.user.*;
import org.springframework.http.MediaType;
import org.springframework.web.reactive.function.client.WebClient;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import reactor.netty.http.client.HttpClient;
import io.netty.handler.ssl.SslContextBuilder;
import io.netty.handler.ssl.util.InsecureTrustManagerFactory;
import org.springframework.http.client.reactive.ReactorClientHttpConnector;

import java.io.IOException;
import java.time.Duration;
import java.time.Instant;

@GrpcService
public class UserService extends UserServiceGrpc.UserServiceImplBase {

    private static final Logger logger = LoggerFactory.getLogger(UserService.class);

    WebClient webClient = WebClient.builder()
            .baseUrl("https://localhost:7211/Users")
            .clientConnector(new ReactorClientHttpConnector(
                    HttpClient.create().secure(sslContextSpec ->
                            sslContextSpec.sslContext(SslContextBuilder.forClient().trustManager(InsecureTrustManagerFactory.INSTANCE))
                    )
            ))
            .build();

    // Retrieve the token from the gRPC context
    private String getTokenFromContext() {
        String token = AuthInterceptor.AUTH_CONTEXT_KEY.get();
        logger.info("Retrieved token: {}", token);
        return token;
    }


    @Override
    public void addUser(CreateUser request, StreamObserver<GetUser> responseObserver) {
        logger.info("STARTING addUser request for user: {}", request.getUserName());
        Instant startTime = Instant.now();

        String token = getTokenFromContext();
        if (token == null) {
            logger.error("Authorization token is missing");
            responseObserver.onError(new RuntimeException("Authorization token is missing"));
            return;
        }

        UserData restRequest = new UserData(
                request.getUserName(),
                request.getPassword(),
                request.getUserRole().getNumber()
        );
        logger.info("Sending POST request to /Users with body: {}", restRequest);

        webClient.post()
                .uri("/")
                .headers(headers -> headers.setBearerAuth(token)) // Set Bearer token
                .contentType(MediaType.APPLICATION_JSON)
                .bodyValue(restRequest)
                .retrieve()
                .bodyToMono(UserData.class)
                .doOnSuccess(userResponse -> {
                    logger.info("POST /Users SUCCESS. Response: {}", userResponse);
                    GetUser userDTO = GetUser.newBuilder()
                            .setUserId(userResponse.getUserId())
                            .setUserName(userResponse.getUserName())
                            .setUserRole(UserRole.forNumber(userResponse.getUserRole()))
                            .build();

                    responseObserver.onNext(userDTO);
                    responseObserver.onCompleted();
                })
                .doOnError(error -> {
                    logger.error("POST /Users FAILED. Error: {}", error.getMessage(), error);
                    responseObserver.onError(new RuntimeException("Failed to create user: " + error.getMessage()));
                })
                .subscribe();
    }

    @Override
    public void editUser(User request, StreamObserver<Empty> responseObserver) {
        logger.info("STARTING editUser request for userId: {}", request.getUserId());
        Instant startTime = Instant.now();

        String token = getTokenFromContext();
        if (token == null) {
            logger.error("Authorization token is missing");
            responseObserver.onError(new RuntimeException("Authorization token is missing"));
            return;
        }

        UserData restRequest = new UserData(
                request.getUsername(),
                request.getPassword(),
                request.getUserRole().getNumber()
        );
        logger.info("Sending PUT request to /Users/{} with body: {}", request.getUserId(), restRequest);

        webClient.put()
                .uri("/{id}", request.getUserId())
                .headers(headers -> headers.setBearerAuth(token)) // Set Bearer token
                .contentType(MediaType.APPLICATION_JSON)
                .bodyValue(restRequest)
                .retrieve()
                .toBodilessEntity()
                .doOnSuccess(response -> {
                    logger.info("PUT /Users/{} SUCCESS. Response: {}", request.getUserId(), response);
                    responseObserver.onNext(Empty.getDefaultInstance());
                    responseObserver.onCompleted();
                })
                .doOnError(error -> {
                    logger.error("PUT /Users/{} FAILED. Error: {}", request.getUserId(), error.getMessage(), error);
                    responseObserver.onError(new RuntimeException("Failed to update user: " + error.getMessage()));
                })
                .subscribe();
    }

    @Override
    public void deleteUser(DeleteUser request, StreamObserver<Empty> responseObserver) {
        logger.info("STARTING deleteUser request for userId: {}", request.getUserId());
        Instant startTime = Instant.now();

        String token = getTokenFromContext();
        if (token == null) {
            logger.error("Authorization token is missing");
            responseObserver.onError(new RuntimeException("Authorization token is missing"));
            return;
        }

        webClient.delete()
                .uri("/{id}", request.getUserId())
                .headers(headers -> headers.setBearerAuth(token)) // Set Bearer token
                .retrieve()
                .toBodilessEntity()
                .doOnSuccess(response -> {
                    logger.info("DELETE /Users/{} SUCCESS. Response: {}", request.getUserId(), response);
                    responseObserver.onNext(Empty.getDefaultInstance());
                    responseObserver.onCompleted();
                })
                .doOnError(error -> {
                    logger.error("DELETE /Users/{} FAILED. Error: {}", request.getUserId(), error.getMessage(), error);
                    responseObserver.onError(new RuntimeException("Failed to delete user: " + error.getMessage()));
                })
                .subscribe();
    }

    @Override
    public void getUsers(Role request, StreamObserver<UserList> responseObserver) {
        logger.info("STARTING getUsers request for role: {}", request.getUserRole());
        Instant startTime = Instant.now();

        String token = getTokenFromContext();
        if (token == null) {
            logger.error("Authorization token is missing");
            responseObserver.onError(new RuntimeException("Authorization token is missing"));
            return;
        }

        // Map the gRPC UserRole enum to the string expected by the REST API
        String userRole = request.getUserRole().toString();

        webClient.get()
                .uri("/role/{userRole}", userRole)
                .headers(headers -> headers.setBearerAuth(token)) // Set Bearer token
                .accept(MediaType.APPLICATION_JSON)
                .retrieve()
                .bodyToFlux(UserData.class)
                .collectList()
                .doOnSuccess(userResponseList -> {
                    logger.info("GET /Users/role/{} SUCCESS. Total users: {}, Users: {}", userRole, userResponseList.size(), userResponseList);

                    UserList.Builder userListBuilder = UserList.newBuilder();
                    userResponseList.stream().map(userResponse ->
                            GetUser.newBuilder()
                                    .setUserId(userResponse.getUserId())
                                    .setUserName(userResponse.getUserName())
                                    .setUserRole(UserRole.forNumber(userResponse.getUserRole()))
                                    .build()
                    ).forEach(userListBuilder::addUsers);

                    responseObserver.onNext(userListBuilder.build());
                    responseObserver.onCompleted();
                })
                .doOnError(error -> {
                    logger.error("GET /Users/role/{} FAILED. Error: {}", userRole, error.getMessage(), error);
                    responseObserver.onError(new RuntimeException("Failed to fetch users by role: " + error.getMessage()));
                })
                .subscribe();
    }

    // Custom DTO for Unified User
    public static class UserData {
        private int userId;
        private String userName;
        private String password;

        @JsonDeserialize(using = UserRoleDeserializer.class)
        private int userRole; // Role stays an INT, but the deserializer will convert from string

        public UserData() {}

        public UserData(String userName, String password, int userRole) {
            this.userName = userName;
            this.password = password;
            this.userRole = userRole;
        }

        public int getUserId() { return userId; }
        public String getUserName() { return userName; }
        public String getPassword() { return password; }
        public int getUserRole() { return userRole; }

        @Override
        public String toString() {
            return "User{" +
                    "userId=" + userId +
                    ", userName='" + userName + '\'' +
                    "password=" + password +
                    ", userRole=" + userRole +
                    '}';
        }
    }

    // Custom Deserializer for UserRole
    public static class UserRoleDeserializer extends JsonDeserializer<Integer> {
        @Override
        public Integer deserialize(JsonParser p, DeserializationContext ctxt) throws IOException {
            String roleName = p.getText(); // Get the string from the JSON response
            switch (roleName) {
                case "INVENTORY_MANAGER":
                    return 0;
                case "WAREHOUSE_WORKER":
                    return 1;
                default:
                    throw new IllegalArgumentException("Unknown role: " + roleName);
            }
        }
    }
}
