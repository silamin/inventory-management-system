package com.javainuse.sep03.service;

import com.google.protobuf.Empty;
import com.javainuse.orders.UpdateOrderItemRequest;
import com.javainuse.orders.OrderItemServiceGrpc;
import io.grpc.stub.StreamObserver;
import io.netty.handler.ssl.SslContextBuilder;
import io.netty.handler.ssl.util.InsecureTrustManagerFactory;
import net.devh.boot.grpc.server.service.GrpcService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.client.reactive.ReactorClientHttpConnector;
import org.springframework.web.reactive.function.client.WebClient;
import reactor.core.publisher.Mono;
import reactor.netty.http.client.HttpClient;

@GrpcService
public class OrderItemService extends OrderItemServiceGrpc.OrderItemServiceImplBase {

    private static final Logger logger = LoggerFactory.getLogger(OrderItemService.class);

    private final WebClient webClient = WebClient.builder()
            .baseUrl("https://localhost:7211/OrderItems")
            .clientConnector(new ReactorClientHttpConnector(
                    HttpClient.create().secure(sslContextSpec ->
                            sslContextSpec.sslContext(SslContextBuilder.forClient()
                                    .trustManager(InsecureTrustManagerFactory.INSTANCE))
                    )
            ))
            .build();

    // Retrieve the token from the gRPC context
    private String getTokenFromContext() {
        return AuthInterceptor.AUTH_CONTEXT_KEY.get();
    }

    @Override
    public void updateOrderItem(UpdateOrderItemRequest request, StreamObserver<Empty> responseObserver) {
        logger.info("Received request to update order item: {}", request.getOrderItemId());

        String token = getTokenFromContext();
        if (token == null) {
            logger.error("Authorization token is missing");
            responseObserver.onError(new RuntimeException("Authorization token is missing"));
            return;
        }

        // Construct the REST API request payload
        int quantityToPick = request.getQuantityToPick();
        logger.info("Updating QuantityToPick to: {}", quantityToPick);

        webClient.put()
                .uri("/{id}", request.getOrderItemId())
                .headers(headers -> headers.setBearerAuth(token)) // Set Bearer token
                .contentType(MediaType.APPLICATION_JSON)
                .bodyValue(quantityToPick)
                .retrieve()
                .onStatus(HttpStatus::isError, response -> {
                    logger.error("Error from REST API with status: {}", response.statusCode());
                    return response.bodyToMono(String.class)
                            .doOnNext(body -> logger.error("Error response from REST API: {}", body))
                            .flatMap(errorBody -> Mono.error(new RuntimeException("Error calling REST API: " + errorBody)));
                })
                .toBodilessEntity()
                .subscribe(
                        success -> {
                            logger.info("Order item updated successfully");
                            responseObserver.onNext(Empty.newBuilder().build());
                            responseObserver.onCompleted();
                        },
                        error -> {
                            logger.error("Error occurred while updating order item", error);
                            responseObserver.onError(error);
                        }
                );
    }
}
