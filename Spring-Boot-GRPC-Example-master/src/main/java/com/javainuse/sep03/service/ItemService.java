package com.javainuse.sep03.service;

import com.javainuse.item.CreateItem;
import com.javainuse.item.DeleteItem;
import com.javainuse.item.Item;
import com.javainuse.item.ItemList;
import com.javainuse.item.ItemServiceGrpc;
import com.google.protobuf.Empty;
import io.grpc.Context;
import io.grpc.Metadata;
import io.netty.handler.ssl.SslContextBuilder;
import io.netty.handler.ssl.util.InsecureTrustManagerFactory;
import net.devh.boot.grpc.server.service.GrpcService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.http.client.reactive.ReactorClientHttpConnector;
import org.springframework.web.reactive.function.client.WebClient;
import io.grpc.stub.StreamObserver;
import reactor.netty.http.client.HttpClient;

import java.util.List;

import static com.javainuse.sep03.service.AuthInterceptor.AUTH_CONTEXT_KEY;

@GrpcService
public class ItemService extends ItemServiceGrpc.ItemServiceImplBase {

    private static final Logger logger = LoggerFactory.getLogger(ItemService.class);

    WebClient webClient = WebClient.builder()
            .baseUrl("https://localhost:7211/Items")
            .clientConnector(new ReactorClientHttpConnector(
                    HttpClient.create().secure(sslContextSpec ->
                            sslContextSpec.sslContext(SslContextBuilder.forClient().trustManager(InsecureTrustManagerFactory.INSTANCE))
                    )
            ))
            .build();

    // Retrieve the Authorization token from the gRPC context
    private String getTokenFromContext() {
        return AUTH_CONTEXT_KEY.get();
    }

    @Override
    public void createItem(CreateItem request, StreamObserver<Item> responseObserver) {
        logger.info("Received create request: name={}, description={}, quantityInStore={}",
                request.getName(), request.getDescription(), request.getQuantityInStore());

        String token = getTokenFromContext();

        if (token == null) {
            responseObserver.onError(new RuntimeException("Authorization token is missing"));
            return;
        }

        ItemData restCreateItem = new ItemData();
        restCreateItem.setItemName(request.getName());
        restCreateItem.setDescription(request.getDescription());
        restCreateItem.setQuantityInStore(request.getQuantityInStore());

        try {
            ItemData createdItem = webClient.post()
                    .uri("/")
                    .headers(headers -> headers.setBearerAuth(token))
                    .bodyValue(restCreateItem)
                    .retrieve()
                    .bodyToMono(ItemData.class)
                    .block();

            if (createdItem == null) {
                responseObserver.onError(new RuntimeException("Failed to create item"));
                return;
            }

            Item grpcItem = Item.newBuilder()
                    .setItemId(createdItem.getItemId())
                    .setItemName(createdItem.getItemName())
                    .setDescription(createdItem.getDescription() == null ? "" : createdItem.getDescription())
                    .setQuantityInStore(createdItem.getQuantityInStore())
                    .build();

            logger.info("Item created successfully: itemId={}, itemName={}, description={}, quantityInStore={}",
                    grpcItem.getItemId(), grpcItem.getItemName(), grpcItem.getDescription(), grpcItem.getQuantityInStore());

            responseObserver.onNext(grpcItem);
            responseObserver.onCompleted();
        } catch (Exception e) {
            logger.error("Error occurred during create REST API call: {}", e.getMessage(), e);
            responseObserver.onError(e);
        }
    }

    @Override
    public void editItem(Item request, StreamObserver<Empty> responseObserver) {
        logger.info("Received edit request: itemId={}, itemName={}, description={}, quantityInStore={}",
                request.getItemId(), request.getItemName(), request.getDescription(), request.getQuantityInStore());

        String token = getTokenFromContext();

        if (token == null) {
            responseObserver.onError(new RuntimeException("Authorization token is missing"));
            return;
        }

        int itemId = request.getItemId();

        ItemData restItem = new ItemData();
        restItem.setItemId(itemId);
        restItem.setItemName(request.getItemName());
        restItem.setDescription(request.getDescription());
        restItem.setQuantityInStore(request.getQuantityInStore());

        try {
            webClient.put()
                    .uri("/{id}", itemId)
                    .headers(headers -> headers.setBearerAuth(token))
                    .bodyValue(restItem)
                    .retrieve()
                    .toBodilessEntity()
                    .block();

            logger.info("Item updated successfully: itemId={}, itemName={}, description={}, quantityInStore={}",
                    restItem.getItemId(), restItem.getItemName(), restItem.getDescription(), restItem.getQuantityInStore());

            responseObserver.onNext(Empty.getDefaultInstance());
            responseObserver.onCompleted();
        } catch (Exception e) {
            logger.error("Error occurred during edit REST API call: {}", e.getMessage(), e);
            responseObserver.onError(e);
        }
    }

    @Override
    public void deleteItem(DeleteItem request, StreamObserver<Empty> responseObserver) {
        int itemId = request.getItemId();
        logger.info("Received delete request for itemId: {}", itemId);

        String token = getTokenFromContext();

        if (token == null) {
            responseObserver.onError(new RuntimeException("Authorization token is missing"));
            return;
        }

        try {
            webClient.delete()
                    .uri("/{id}", itemId)
                    .headers(headers -> headers.setBearerAuth(token))
                    .retrieve()
                    .toBodilessEntity()
                    .block();

            logger.info("Item deleted successfully with itemId: {}", itemId);
            responseObserver.onNext(Empty.getDefaultInstance());
            responseObserver.onCompleted();
        } catch (Exception e) {
            logger.error("Error occurred during delete REST API call: {}", e.getMessage(), e);
            responseObserver.onError(e);
        }
    }

    @Override
    public void getAllItems(Empty request, StreamObserver<ItemList> responseObserver) {
        logger.info("Received request to get all items");

        String token = getTokenFromContext();

        if (token == null) {
            logger.error("Authorization token is missing. Aborting the request.");
            responseObserver.onError(new RuntimeException("Authorization token is missing"));
            return;
        }

        logger.info("Token to be used for API call: {}", token);

        try {
            List<ItemData> restItems = webClient.get()
                    .uri("/")
                    .headers(headers -> headers.setBearerAuth(token))
                    .retrieve()
                    .bodyToFlux(ItemData.class)
                    .collectList()
                    .block();

            if (restItems == null) {
                logger.error("No items returned from the API");
                responseObserver.onError(new RuntimeException("No items returned"));
                return;
            }

            ItemList.Builder itemListBuilder = ItemList.newBuilder();
            for (ItemData ri : restItems) {
                Item grpcItem = Item.newBuilder()
                        .setItemId(ri.getItemId())
                        .setItemName(ri.getItemName())
                        .setDescription(ri.getDescription() == null ? "" : ri.getDescription())
                        .setQuantityInStore(ri.getQuantityInStore())
                        .build();
                itemListBuilder.addItems(grpcItem);
            }

            logger.info("Items retrieved successfully: count={}", itemListBuilder.getItemsList().size());

            responseObserver.onNext(itemListBuilder.build());
            responseObserver.onCompleted();
        } catch (Exception e) {
            logger.error("Error occurred during get all items REST API call: {}", e.getMessage(), e);
            responseObserver.onError(e);
        }
    }



    public static class ItemData {
        private int itemId;
        private String itemName;
        private String description;
        private int quantityInStore;

        public int getItemId() {
            return itemId;
        }

        public void setItemId(int itemId) {
            this.itemId = itemId;
        }

        public String getItemName() {
            return itemName;
        }

        public void setItemName(String itemName) {
            this.itemName = itemName;
        }

        public String getDescription() {
            return description;
        }

        public void setDescription(String description) {
            this.description = description;
        }

        public int getQuantityInStore() {
            return quantityInStore;
        }

        public void setQuantityInStore(int quantityInStore) {
            this.quantityInStore = quantityInStore;
        }

        @Override
        public String toString() {
            return "ItemData{" +
                    "itemId=" + itemId +
                    ", itemName='" + itemName + '\'' +
                    ", description='" + description + '\'' +
                    ", quantityInStore=" + quantityInStore +
                    '}';
        }
    }
}
