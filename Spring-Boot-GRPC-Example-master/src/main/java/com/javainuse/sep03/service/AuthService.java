package com.javainuse.sep03.service;

import com.javainuse.authentication.AuthServiceGrpc;
import com.javainuse.authentication.LoginRequest;
import com.javainuse.authentication.LoginResponse;
import io.grpc.stub.StreamObserver;
import io.netty.handler.ssl.SslContextBuilder;
import io.netty.handler.ssl.util.InsecureTrustManagerFactory;
import net.devh.boot.grpc.server.service.GrpcService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.http.client.reactive.ReactorClientHttpConnector;
import org.springframework.web.reactive.function.client.WebClient;
import reactor.netty.http.client.HttpClient;

import java.util.Map;

@GrpcService
public class AuthService extends AuthServiceGrpc.AuthServiceImplBase {

    private static final Logger logger = LoggerFactory.getLogger(AuthService.class);

    WebClient webClient = WebClient.builder()
            .baseUrl("https://localhost:7211/api/auth")
            .clientConnector(new ReactorClientHttpConnector(
                    HttpClient.create().secure(sslContextSpec ->
                            sslContextSpec.sslContext(SslContextBuilder.forClient().trustManager(InsecureTrustManagerFactory.INSTANCE))
                    )
            ))
            .build();

    @Override
    public void login(LoginRequest request, StreamObserver<LoginResponse> responseObserver) {
        logger.info("Login attempt: Username - {} Password - {}", request.getUsername(), request.getPassword());

        Map<String, String> requestBody = Map.of(
                "username", request.getUsername(),
                "password", request.getPassword()
        );

        try {
            webClient.post()
                    .uri("/login")
                    .bodyValue(requestBody)
                    .retrieve()
                    .bodyToMono(String.class) // Assuming the response is a String JSON response
                    .doOnNext(responseString -> {
                        logger.info("Response body from REST API: {}", responseString);
                    })
                    .map(this::extractTokenFromResponse) // Extract the token from the response
                    .doOnNext(token -> {
                        logger.info("Extracted Token: {}", token);
                    })
                    .doOnError(throwable -> {
                        logger.error("Error during login request: {}", throwable.getMessage(), throwable);
                        responseObserver.onError(throwable);
                    })
                    .subscribe(token -> {
                        LoginResponse response = LoginResponse.newBuilder()
                                .setToken(token)
                                .build();

                        responseObserver.onNext(response);
                        responseObserver.onCompleted();
                    });
        } catch (Exception e) {
            logger.error("Unexpected error during login: {}", e.getMessage(), e);
            responseObserver.onError(e);
        }
    }

    /**
     * Extracts the token from the response JSON.
     * Example response: {"token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxOSIsInVuaXF1ZV9uYW1lIjoiYWRtaW4iLCJyb2xlIjoiSU5WRU5UT1JZX01BTkFHRVIiLCJqdGkiOiJlMzM5N2UwNS0yOTM0LTQ0ODMtYWFiMy03ODA0ZTM1N2EwYjMiLCJuYmYiOjE3MzQyNDkwODQsImV4cCI6MTczNDI1MjY4NCwiaWF0IjoxNzM0MjQ5MDg0fQ.7JuGwezrmbMgTfECJRaw2xKcimHJegPxPyKwR6HVM7c" }
     */
    private String extractTokenFromResponse(String response) {
        try {
            int tokenStartIndex = response.indexOf("\"token\":\"") + 9; // Position after "token":"
            int tokenEndIndex = response.indexOf("\"", tokenStartIndex);
            if (tokenStartIndex != -1 && tokenEndIndex != -1) {
                String token = response.substring(tokenStartIndex, tokenEndIndex);
                logger.info("Successfully extracted token from response: {}", token);
                return token;
            }
            throw new RuntimeException("Failed to extract token from response");
        } catch (Exception e) {
            logger.error("Error while extracting token from response: {}", e.getMessage(), e);
            throw new RuntimeException("Error while extracting token from response: " + e.getMessage());
        }
    }
}
