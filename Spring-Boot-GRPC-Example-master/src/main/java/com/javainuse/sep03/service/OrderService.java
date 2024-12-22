package com.javainuse.sep03.service;

import com.fasterxml.jackson.annotation.JsonInclude;
import com.fasterxml.jackson.annotation.JsonProperty;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.google.protobuf.Empty;
import com.javainuse.orders.*;
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

import java.time.Instant;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.stream.Collectors;

@GrpcService
public class OrderService extends OrderServiceGrpc.OrderServiceImplBase {

    private static final Logger logger = LoggerFactory.getLogger(OrderService.class);

    private final WebClient webClient = WebClient.builder()
            .baseUrl("https://localhost:7211/Orders")
            .clientConnector(new ReactorClientHttpConnector(
                    HttpClient.create().secure(sslContextSpec ->
                            sslContextSpec.sslContext(SslContextBuilder.forClient().trustManager(InsecureTrustManagerFactory.INSTANCE))
                    )
            ))
            .build();

    // Retrieve the token from the gRPC context
    private String getTokenFromContext() {
        return AuthInterceptor.AUTH_CONTEXT_KEY.get();
    }

    @Override
    public void createOrder(CreateOrder request, StreamObserver<CreateOrderResponse> responseObserver) {
        logger.info("Received request to create order");

        String token = getTokenFromContext();
        if (token == null) {
            logger.error("Authorization token is missing");
            responseObserver.onError(new RuntimeException("Authorization token is missing"));
            return;
        }

        try {
            String deliveryDate = Instant.ofEpochSecond(request.getDeliveryDate().getSeconds(), request.getDeliveryDate().getNanos()).toString();

            OrderData createOrderDTO = new OrderData(
                    deliveryDate,
                    request.getOrderItemsList().stream().map(orderItem ->
                            new OrderItemData(orderItem.getItemId(), orderItem.getTotalQuantity())
                    ).collect(Collectors.toList()),
                    request.getCreatedBy(),
                    Instant.now().toString()
            );

            logger.info("Sending payload to REST API: {}", new ObjectMapper().writeValueAsString(createOrderDTO));

            webClient.post()
                    .uri("/")
                    .headers(headers -> headers.setBearerAuth(token)) // Set Bearer token
                    .contentType(MediaType.APPLICATION_JSON)
                    .body(Mono.just(createOrderDTO), OrderData.class)
                    .retrieve()
                    .onStatus(HttpStatus::isError, response -> {
                        logger.error("Error from REST API with status: {}", response.statusCode());
                        return response.bodyToMono(String.class)
                                .doOnNext(body -> logger.error("Error response from REST API: {}", body))
                                .flatMap(errorBody -> Mono.error(new RuntimeException("Error calling REST API: " + errorBody)));
                    })
                    .bodyToMono(Boolean.class)
                    .subscribe(
                            success -> {
                                logger.info("Order created successfully");
                                CreateOrderResponse response = CreateOrderResponse.newBuilder().setSuccess(success).build();
                                responseObserver.onNext(response);
                                responseObserver.onCompleted();
                            },
                            error -> {
                                logger.error("Error occurred in createOrder: ", error);
                                responseObserver.onError(error);
                            }
                    );
        } catch (Exception e) {
            logger.error("Exception occurred while processing createOrder", e);
            responseObserver.onError(e);
        }
    }

    @Override
    public void getOrders(Status request, StreamObserver<OrderList> responseObserver) {
        String status = request.getOrderStatus().name();
        logger.info("Fetching orders with status: {}", status);

        String token = getTokenFromContext();
        if (token == null) {
            logger.error("Authorization token is missing");
            responseObserver.onError(new RuntimeException("Authorization token is missing"));
            return;
        }

        webClient.get()
                .uri(uriBuilder -> uriBuilder
                        .path("/status/{status}")
                        .build(status))
                .headers(headers -> headers.setBearerAuth(token)) // Set Bearer token
                .accept(MediaType.APPLICATION_JSON)
                .retrieve()
                .bodyToMono(List.class)
                .subscribe(
                        orders -> {
                            try {
                                List<Order> grpcOrderList = mapOrdersFromApiResponse(orders);
                                OrderList orderList = OrderList.newBuilder().addAllOrders(grpcOrderList).build();
                                responseObserver.onNext(orderList);
                                responseObserver.onCompleted();
                            } catch (Exception e) {
                                logger.error("Error processing orders from REST API response", e);
                                responseObserver.onError(e);
                            }
                        },
                        error -> {
                            logger.error("Error occurred while fetching orders by status from REST API", error);
                            responseObserver.onError(error);
                        }
                );
    }

    private List<Order> mapOrdersFromApiResponse(Object apiResponse) {
        return ((List<?>) apiResponse).stream().map(order -> {
            try {
                Map<String, Object> orderMap = (Map<String, Object>) order;

                List<GetOrderItem> orderItems = ((List<?>) orderMap.getOrDefault("orderItems", List.of()))
                        .stream().map(orderItem -> {
                            Map<String, Object> orderItemMap = (Map<String, Object>) orderItem;
                            return GetOrderItem.newBuilder()
                                    .setItemName(Optional.ofNullable((String) orderItemMap.get("itemName")).orElse("Unknown"))
                                    .setQuantityToPick(((Number) orderItemMap.getOrDefault("quantityToPick", 0)).intValue())
                                    .setTotalQuantity(((Number) orderItemMap.getOrDefault("totalQuantity", 0)).intValue())
                                    .build();
                        }).collect(Collectors.toList());

                int orderId = ((Number) orderMap.getOrDefault("orderId", 0)).intValue();
                String assignedUser = Optional.ofNullable((String) orderMap.get("assignedUser")).orElse("Unassigned");
                String createdBy = Optional.ofNullable((String) orderMap.get("createdBy")).orElse("Unknown");

                Instant deliveryDate = Optional.ofNullable(orderMap.get("deliveryDate"))
                        .map(dateStr -> Instant.parse((String) dateStr))
                        .orElse(null);

                Instant createdAt = Optional.ofNullable(orderMap.get("createdAt"))
                        .map(dateStr -> Instant.parse((String) dateStr))
                        .orElse(null);

                Instant completedAt = Optional.ofNullable(orderMap.get("completedAt"))
                        .map(dateStr -> Instant.parse((String) dateStr))
                        .orElse(null);

                return Order.newBuilder()
                        .setOrderId(orderId)
                        .addAllOrderItems(orderItems)
                        .setAssignedUser(assignedUser)
                        .setCreatedByUser(createdBy)
                        .setOrderStatus(OrderStatus.valueOf(orderMap.get("orderStatus").toString()))
                        .setDeliveryDate(deliveryDate != null ?
                                com.google.protobuf.Timestamp.newBuilder()
                                        .setSeconds(deliveryDate.getEpochSecond())
                                        .setNanos(deliveryDate.getNano())
                                        .build() :
                                com.google.protobuf.Timestamp.getDefaultInstance())
                        .setCreatedAt(createdAt != null ?
                                com.google.protobuf.Timestamp.newBuilder()
                                        .setSeconds(createdAt.getEpochSecond())
                                        .setNanos(createdAt.getNano())
                                        .build() :
                                com.google.protobuf.Timestamp.getDefaultInstance())
                        .setCompletedAt(completedAt != null ?
                                com.google.protobuf.Timestamp.newBuilder()
                                        .setSeconds(completedAt.getEpochSecond())
                                        .setNanos(completedAt.getNano())
                                        .build() :
                                com.google.protobuf.Timestamp.getDefaultInstance())
                        .build();
            } catch (Exception e) {
                logger.error("Error while processing order: {}", order, e);
                throw e;
            }
        }).collect(Collectors.toList());
    }

    /**
     * Data structure for posting orders
     */
    public static class OrderData {
        @JsonProperty("deliveryDate")
        private String deliveryDate;

        @JsonProperty("orderItems")
        private List<OrderItemData> orderItems;

        @JsonProperty("createdBy")
        private int createdBy;

        @JsonProperty("createdAt")
        private String createdAt;

        @JsonProperty("completedAt")
        @JsonInclude(JsonInclude.Include.NON_NULL)
        private String completedAt;

        public OrderData(String deliveryDate, List<OrderItemData> orderItems, int createdBy, String createdAt) {
            this.deliveryDate = deliveryDate;
            this.orderItems = orderItems;
            this.createdBy = createdBy;
            this.createdAt = createdAt;
        }
    }

    /**
     * Data structure for order items
     */
    public static class OrderItemData {
        @JsonProperty("itemId")
        private int itemId;

        @JsonProperty("totalQuantity")
        private int totalQuantity;

        @JsonProperty("quantityToPick")
        @JsonInclude(JsonInclude.Include.NON_NULL)
        private Integer quantityToPick;

        public OrderItemData(int itemId, int totalQuantity) {
            this.itemId = itemId;
            this.totalQuantity = totalQuantity;
        }

        public OrderItemData(int itemId, int totalQuantity, int quantityToPick) {
            this.itemId = itemId;
            this.totalQuantity = totalQuantity;
            this.quantityToPick = quantityToPick;
        }
    }
}
