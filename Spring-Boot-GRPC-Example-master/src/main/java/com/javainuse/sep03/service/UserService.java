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

    @Override
    public void addUser(CreateUser request, StreamObserver<GetUser> responseObserver) {
        logger.info("STARTING addUser request for user: {}", request.getUserName());
        Instant startTime = Instant.now();

        UserData restRequest = new UserData(
                request.getUserName(),
                request.getPassword(),
                request.getUserRole().getNumber()
        );
        logger.info("Sending POST request to /Users with body: {}", restRequest);

        webClient.post()
                .uri("/")
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
                    logger.info("FINISHED addUser request for user: {} in {} ms", request.getUserName(), Duration.between(startTime, Instant.now()).toMillis());
                })
                .doOnError(error -> {
                    logger.error("POST /Users FAILED. Request Body: {}. Error: {}", restRequest, error.getMessage(), error);
                    responseObserver.onError(new RuntimeException("Failed to create user: " + error.getMessage()));
                })
                .subscribe();
    }

    @Override
    public void editUser(User request, StreamObserver<Empty> responseObserver) {
        logger.info("STARTING editUser request for userId: {}", request.getUserId());
        Instant startTime = Instant.now();

        UserData restRequest = new UserData(
                request.getUsername(),
                request.getPassword(),
                request.getUserRole().getNumber()
        );
        logger.info("Sending PUT request to /Users/{} with body: {}", request.getUserId(), restRequest);

        webClient.put()
                .uri("/{id}", request.getUserId())
                .contentType(MediaType.APPLICATION_JSON)
                .bodyValue(restRequest)
                .retrieve()
                .toBodilessEntity()
                .doOnSuccess(response -> {
                    logger.info("PUT /Users/{} SUCCESS. Response: {}", request.getUserId(), response);
                    responseObserver.onNext(Empty.getDefaultInstance());
                    responseObserver.onCompleted();
                    logger.info("FINISHED editUser request for userId: {} in {} ms", request.getUserId(), Duration.between(startTime, Instant.now()).toMillis());
                })
                .doOnError(error -> {
                    logger.error("PUT /Users/{} FAILED. Request Body: {}. Error: {}", request.getUserId(), restRequest, error.getMessage(), error);
                    responseObserver.onError(new RuntimeException("Failed to update user: " + error.getMessage()));
                })
                .subscribe();
    }

    @Override
    public void deleteUser(DeleteUser request, StreamObserver<Empty> responseObserver) {
        logger.info("STARTING deleteUser request for userId: {}", request.getUserId());
        Instant startTime = Instant.now();

        webClient.delete()
                .uri("/{id}", request.getUserId())
                .retrieve()
                .toBodilessEntity()
                .doOnSuccess(response -> {
                    logger.info("DELETE /Users/{} SUCCESS. Response: {}", request.getUserId(), response);
                    responseObserver.onNext(Empty.getDefaultInstance());
                    responseObserver.onCompleted();
                    logger.info("FINISHED deleteUser request for userId: {} in {} ms", request.getUserId(), Duration.between(startTime, Instant.now()).toMillis());
                })
                .doOnError(error -> {
                    logger.error("DELETE /Users/{} FAILED. Error: {}", request.getUserId(), error.getMessage(), error);
                    responseObserver.onError(new RuntimeException("Failed to delete user: " + error.getMessage()));
                })
                .subscribe();
    }

    @Override
    public void getAllUsers(Empty request, StreamObserver<UserList> responseObserver) {
        logger.info("STARTING getAllUsers request...");
        Instant startTime = Instant.now();

        webClient.get()
                .uri("/")
                .accept(MediaType.APPLICATION_JSON)
                .retrieve()
                .bodyToFlux(UserData.class)
                .collectList()
                .doOnSuccess(userResponseList -> {
                    logger.info("GET /Users SUCCESS. Total users: {}, Users: {}", userResponseList.size(), userResponseList);
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
                    logger.info("FINISHED getAllUsers request in {} ms", Duration.between(startTime, Instant.now()).toMillis());
                })
                .doOnError(error -> {
                    logger.error("GET /Users FAILED. Error: {}", error.getMessage(), error);
                    responseObserver.onError(new RuntimeException("Failed to fetch users: " + error.getMessage()));
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
