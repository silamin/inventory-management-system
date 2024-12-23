package com.javainuse.item;

import static io.grpc.MethodDescriptor.generateFullMethodName;
import static io.grpc.stub.ClientCalls.asyncBidiStreamingCall;
import static io.grpc.stub.ClientCalls.asyncClientStreamingCall;
import static io.grpc.stub.ClientCalls.asyncServerStreamingCall;
import static io.grpc.stub.ClientCalls.asyncUnaryCall;
import static io.grpc.stub.ClientCalls.blockingServerStreamingCall;
import static io.grpc.stub.ClientCalls.blockingUnaryCall;
import static io.grpc.stub.ClientCalls.futureUnaryCall;
import static io.grpc.stub.ServerCalls.asyncBidiStreamingCall;
import static io.grpc.stub.ServerCalls.asyncClientStreamingCall;
import static io.grpc.stub.ServerCalls.asyncServerStreamingCall;
import static io.grpc.stub.ServerCalls.asyncUnaryCall;
import static io.grpc.stub.ServerCalls.asyncUnimplementedStreamingCall;
import static io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall;

/**
 */
@javax.annotation.Generated(
    value = "by gRPC proto compiler (version 1.22.1)",
    comments = "Source: item-service.proto")
public final class ItemServiceGrpc {

  private ItemServiceGrpc() {}

  public static final String SERVICE_NAME = "items.ItemService";

  // Static method descriptors that strictly reflect the proto.
  private static volatile io.grpc.MethodDescriptor<com.javainuse.item.CreateItem,
      com.javainuse.item.Item> getCreateItemMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "createItem",
      requestType = com.javainuse.item.CreateItem.class,
      responseType = com.javainuse.item.Item.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<com.javainuse.item.CreateItem,
      com.javainuse.item.Item> getCreateItemMethod() {
    io.grpc.MethodDescriptor<com.javainuse.item.CreateItem, com.javainuse.item.Item> getCreateItemMethod;
    if ((getCreateItemMethod = ItemServiceGrpc.getCreateItemMethod) == null) {
      synchronized (ItemServiceGrpc.class) {
        if ((getCreateItemMethod = ItemServiceGrpc.getCreateItemMethod) == null) {
          ItemServiceGrpc.getCreateItemMethod = getCreateItemMethod = 
              io.grpc.MethodDescriptor.<com.javainuse.item.CreateItem, com.javainuse.item.Item>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(
                  "items.ItemService", "createItem"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  com.javainuse.item.CreateItem.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  com.javainuse.item.Item.getDefaultInstance()))
                  .setSchemaDescriptor(new ItemServiceMethodDescriptorSupplier("createItem"))
                  .build();
          }
        }
     }
     return getCreateItemMethod;
  }

  private static volatile io.grpc.MethodDescriptor<com.javainuse.item.Item,
      com.google.protobuf.Empty> getEditItemMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "editItem",
      requestType = com.javainuse.item.Item.class,
      responseType = com.google.protobuf.Empty.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<com.javainuse.item.Item,
      com.google.protobuf.Empty> getEditItemMethod() {
    io.grpc.MethodDescriptor<com.javainuse.item.Item, com.google.protobuf.Empty> getEditItemMethod;
    if ((getEditItemMethod = ItemServiceGrpc.getEditItemMethod) == null) {
      synchronized (ItemServiceGrpc.class) {
        if ((getEditItemMethod = ItemServiceGrpc.getEditItemMethod) == null) {
          ItemServiceGrpc.getEditItemMethod = getEditItemMethod = 
              io.grpc.MethodDescriptor.<com.javainuse.item.Item, com.google.protobuf.Empty>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(
                  "items.ItemService", "editItem"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  com.javainuse.item.Item.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  com.google.protobuf.Empty.getDefaultInstance()))
                  .setSchemaDescriptor(new ItemServiceMethodDescriptorSupplier("editItem"))
                  .build();
          }
        }
     }
     return getEditItemMethod;
  }

  private static volatile io.grpc.MethodDescriptor<com.javainuse.item.DeleteItem,
      com.google.protobuf.Empty> getDeleteItemMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "deleteItem",
      requestType = com.javainuse.item.DeleteItem.class,
      responseType = com.google.protobuf.Empty.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<com.javainuse.item.DeleteItem,
      com.google.protobuf.Empty> getDeleteItemMethod() {
    io.grpc.MethodDescriptor<com.javainuse.item.DeleteItem, com.google.protobuf.Empty> getDeleteItemMethod;
    if ((getDeleteItemMethod = ItemServiceGrpc.getDeleteItemMethod) == null) {
      synchronized (ItemServiceGrpc.class) {
        if ((getDeleteItemMethod = ItemServiceGrpc.getDeleteItemMethod) == null) {
          ItemServiceGrpc.getDeleteItemMethod = getDeleteItemMethod = 
              io.grpc.MethodDescriptor.<com.javainuse.item.DeleteItem, com.google.protobuf.Empty>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(
                  "items.ItemService", "deleteItem"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  com.javainuse.item.DeleteItem.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  com.google.protobuf.Empty.getDefaultInstance()))
                  .setSchemaDescriptor(new ItemServiceMethodDescriptorSupplier("deleteItem"))
                  .build();
          }
        }
     }
     return getDeleteItemMethod;
  }

  private static volatile io.grpc.MethodDescriptor<com.google.protobuf.Empty,
      com.javainuse.item.ItemList> getGetAllItemsMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "getAllItems",
      requestType = com.google.protobuf.Empty.class,
      responseType = com.javainuse.item.ItemList.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<com.google.protobuf.Empty,
      com.javainuse.item.ItemList> getGetAllItemsMethod() {
    io.grpc.MethodDescriptor<com.google.protobuf.Empty, com.javainuse.item.ItemList> getGetAllItemsMethod;
    if ((getGetAllItemsMethod = ItemServiceGrpc.getGetAllItemsMethod) == null) {
      synchronized (ItemServiceGrpc.class) {
        if ((getGetAllItemsMethod = ItemServiceGrpc.getGetAllItemsMethod) == null) {
          ItemServiceGrpc.getGetAllItemsMethod = getGetAllItemsMethod = 
              io.grpc.MethodDescriptor.<com.google.protobuf.Empty, com.javainuse.item.ItemList>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(
                  "items.ItemService", "getAllItems"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  com.google.protobuf.Empty.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  com.javainuse.item.ItemList.getDefaultInstance()))
                  .setSchemaDescriptor(new ItemServiceMethodDescriptorSupplier("getAllItems"))
                  .build();
          }
        }
     }
     return getGetAllItemsMethod;
  }

  /**
   * Creates a new async stub that supports all call types for the service
   */
  public static ItemServiceStub newStub(io.grpc.Channel channel) {
    return new ItemServiceStub(channel);
  }

  /**
   * Creates a new blocking-style stub that supports unary and streaming output calls on the service
   */
  public static ItemServiceBlockingStub newBlockingStub(
      io.grpc.Channel channel) {
    return new ItemServiceBlockingStub(channel);
  }

  /**
   * Creates a new ListenableFuture-style stub that supports unary calls on the service
   */
  public static ItemServiceFutureStub newFutureStub(
      io.grpc.Channel channel) {
    return new ItemServiceFutureStub(channel);
  }

  /**
   */
  public static abstract class ItemServiceImplBase implements io.grpc.BindableService {

    /**
     */
    public void createItem(com.javainuse.item.CreateItem request,
        io.grpc.stub.StreamObserver<com.javainuse.item.Item> responseObserver) {
      asyncUnimplementedUnaryCall(getCreateItemMethod(), responseObserver);
    }

    /**
     */
    public void editItem(com.javainuse.item.Item request,
        io.grpc.stub.StreamObserver<com.google.protobuf.Empty> responseObserver) {
      asyncUnimplementedUnaryCall(getEditItemMethod(), responseObserver);
    }

    /**
     */
    public void deleteItem(com.javainuse.item.DeleteItem request,
        io.grpc.stub.StreamObserver<com.google.protobuf.Empty> responseObserver) {
      asyncUnimplementedUnaryCall(getDeleteItemMethod(), responseObserver);
    }

    /**
     */
    public void getAllItems(com.google.protobuf.Empty request,
        io.grpc.stub.StreamObserver<com.javainuse.item.ItemList> responseObserver) {
      asyncUnimplementedUnaryCall(getGetAllItemsMethod(), responseObserver);
    }

    @java.lang.Override public final io.grpc.ServerServiceDefinition bindService() {
      return io.grpc.ServerServiceDefinition.builder(getServiceDescriptor())
          .addMethod(
            getCreateItemMethod(),
            asyncUnaryCall(
              new MethodHandlers<
                com.javainuse.item.CreateItem,
                com.javainuse.item.Item>(
                  this, METHODID_CREATE_ITEM)))
          .addMethod(
            getEditItemMethod(),
            asyncUnaryCall(
              new MethodHandlers<
                com.javainuse.item.Item,
                com.google.protobuf.Empty>(
                  this, METHODID_EDIT_ITEM)))
          .addMethod(
            getDeleteItemMethod(),
            asyncUnaryCall(
              new MethodHandlers<
                com.javainuse.item.DeleteItem,
                com.google.protobuf.Empty>(
                  this, METHODID_DELETE_ITEM)))
          .addMethod(
            getGetAllItemsMethod(),
            asyncUnaryCall(
              new MethodHandlers<
                com.google.protobuf.Empty,
                com.javainuse.item.ItemList>(
                  this, METHODID_GET_ALL_ITEMS)))
          .build();
    }
  }

  /**
   */
  public static final class ItemServiceStub extends io.grpc.stub.AbstractStub<ItemServiceStub> {
    private ItemServiceStub(io.grpc.Channel channel) {
      super(channel);
    }

    private ItemServiceStub(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @java.lang.Override
    protected ItemServiceStub build(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      return new ItemServiceStub(channel, callOptions);
    }

    /**
     */
    public void createItem(com.javainuse.item.CreateItem request,
        io.grpc.stub.StreamObserver<com.javainuse.item.Item> responseObserver) {
      asyncUnaryCall(
          getChannel().newCall(getCreateItemMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void editItem(com.javainuse.item.Item request,
        io.grpc.stub.StreamObserver<com.google.protobuf.Empty> responseObserver) {
      asyncUnaryCall(
          getChannel().newCall(getEditItemMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void deleteItem(com.javainuse.item.DeleteItem request,
        io.grpc.stub.StreamObserver<com.google.protobuf.Empty> responseObserver) {
      asyncUnaryCall(
          getChannel().newCall(getDeleteItemMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void getAllItems(com.google.protobuf.Empty request,
        io.grpc.stub.StreamObserver<com.javainuse.item.ItemList> responseObserver) {
      asyncUnaryCall(
          getChannel().newCall(getGetAllItemsMethod(), getCallOptions()), request, responseObserver);
    }
  }

  /**
   */
  public static final class ItemServiceBlockingStub extends io.grpc.stub.AbstractStub<ItemServiceBlockingStub> {
    private ItemServiceBlockingStub(io.grpc.Channel channel) {
      super(channel);
    }

    private ItemServiceBlockingStub(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @java.lang.Override
    protected ItemServiceBlockingStub build(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      return new ItemServiceBlockingStub(channel, callOptions);
    }

    /**
     */
    public com.javainuse.item.Item createItem(com.javainuse.item.CreateItem request) {
      return blockingUnaryCall(
          getChannel(), getCreateItemMethod(), getCallOptions(), request);
    }

    /**
     */
    public com.google.protobuf.Empty editItem(com.javainuse.item.Item request) {
      return blockingUnaryCall(
          getChannel(), getEditItemMethod(), getCallOptions(), request);
    }

    /**
     */
    public com.google.protobuf.Empty deleteItem(com.javainuse.item.DeleteItem request) {
      return blockingUnaryCall(
          getChannel(), getDeleteItemMethod(), getCallOptions(), request);
    }

    /**
     */
    public com.javainuse.item.ItemList getAllItems(com.google.protobuf.Empty request) {
      return blockingUnaryCall(
          getChannel(), getGetAllItemsMethod(), getCallOptions(), request);
    }
  }

  /**
   */
  public static final class ItemServiceFutureStub extends io.grpc.stub.AbstractStub<ItemServiceFutureStub> {
    private ItemServiceFutureStub(io.grpc.Channel channel) {
      super(channel);
    }

    private ItemServiceFutureStub(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @java.lang.Override
    protected ItemServiceFutureStub build(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      return new ItemServiceFutureStub(channel, callOptions);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<com.javainuse.item.Item> createItem(
        com.javainuse.item.CreateItem request) {
      return futureUnaryCall(
          getChannel().newCall(getCreateItemMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<com.google.protobuf.Empty> editItem(
        com.javainuse.item.Item request) {
      return futureUnaryCall(
          getChannel().newCall(getEditItemMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<com.google.protobuf.Empty> deleteItem(
        com.javainuse.item.DeleteItem request) {
      return futureUnaryCall(
          getChannel().newCall(getDeleteItemMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<com.javainuse.item.ItemList> getAllItems(
        com.google.protobuf.Empty request) {
      return futureUnaryCall(
          getChannel().newCall(getGetAllItemsMethod(), getCallOptions()), request);
    }
  }

  private static final int METHODID_CREATE_ITEM = 0;
  private static final int METHODID_EDIT_ITEM = 1;
  private static final int METHODID_DELETE_ITEM = 2;
  private static final int METHODID_GET_ALL_ITEMS = 3;

  private static final class MethodHandlers<Req, Resp> implements
      io.grpc.stub.ServerCalls.UnaryMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.ServerStreamingMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.ClientStreamingMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.BidiStreamingMethod<Req, Resp> {
    private final ItemServiceImplBase serviceImpl;
    private final int methodId;

    MethodHandlers(ItemServiceImplBase serviceImpl, int methodId) {
      this.serviceImpl = serviceImpl;
      this.methodId = methodId;
    }

    @java.lang.Override
    @java.lang.SuppressWarnings("unchecked")
    public void invoke(Req request, io.grpc.stub.StreamObserver<Resp> responseObserver) {
      switch (methodId) {
        case METHODID_CREATE_ITEM:
          serviceImpl.createItem((com.javainuse.item.CreateItem) request,
              (io.grpc.stub.StreamObserver<com.javainuse.item.Item>) responseObserver);
          break;
        case METHODID_EDIT_ITEM:
          serviceImpl.editItem((com.javainuse.item.Item) request,
              (io.grpc.stub.StreamObserver<com.google.protobuf.Empty>) responseObserver);
          break;
        case METHODID_DELETE_ITEM:
          serviceImpl.deleteItem((com.javainuse.item.DeleteItem) request,
              (io.grpc.stub.StreamObserver<com.google.protobuf.Empty>) responseObserver);
          break;
        case METHODID_GET_ALL_ITEMS:
          serviceImpl.getAllItems((com.google.protobuf.Empty) request,
              (io.grpc.stub.StreamObserver<com.javainuse.item.ItemList>) responseObserver);
          break;
        default:
          throw new AssertionError();
      }
    }

    @java.lang.Override
    @java.lang.SuppressWarnings("unchecked")
    public io.grpc.stub.StreamObserver<Req> invoke(
        io.grpc.stub.StreamObserver<Resp> responseObserver) {
      switch (methodId) {
        default:
          throw new AssertionError();
      }
    }
  }

  private static abstract class ItemServiceBaseDescriptorSupplier
      implements io.grpc.protobuf.ProtoFileDescriptorSupplier, io.grpc.protobuf.ProtoServiceDescriptorSupplier {
    ItemServiceBaseDescriptorSupplier() {}

    @java.lang.Override
    public com.google.protobuf.Descriptors.FileDescriptor getFileDescriptor() {
      return com.javainuse.item.ItemServiceOuterClass.getDescriptor();
    }

    @java.lang.Override
    public com.google.protobuf.Descriptors.ServiceDescriptor getServiceDescriptor() {
      return getFileDescriptor().findServiceByName("ItemService");
    }
  }

  private static final class ItemServiceFileDescriptorSupplier
      extends ItemServiceBaseDescriptorSupplier {
    ItemServiceFileDescriptorSupplier() {}
  }

  private static final class ItemServiceMethodDescriptorSupplier
      extends ItemServiceBaseDescriptorSupplier
      implements io.grpc.protobuf.ProtoMethodDescriptorSupplier {
    private final String methodName;

    ItemServiceMethodDescriptorSupplier(String methodName) {
      this.methodName = methodName;
    }

    @java.lang.Override
    public com.google.protobuf.Descriptors.MethodDescriptor getMethodDescriptor() {
      return getServiceDescriptor().findMethodByName(methodName);
    }
  }

  private static volatile io.grpc.ServiceDescriptor serviceDescriptor;

  public static io.grpc.ServiceDescriptor getServiceDescriptor() {
    io.grpc.ServiceDescriptor result = serviceDescriptor;
    if (result == null) {
      synchronized (ItemServiceGrpc.class) {
        result = serviceDescriptor;
        if (result == null) {
          serviceDescriptor = result = io.grpc.ServiceDescriptor.newBuilder(SERVICE_NAME)
              .setSchemaDescriptor(new ItemServiceFileDescriptorSupplier())
              .addMethod(getCreateItemMethod())
              .addMethod(getEditItemMethod())
              .addMethod(getDeleteItemMethod())
              .addMethod(getGetAllItemsMethod())
              .build();
        }
      }
    }
    return result;
  }
}
