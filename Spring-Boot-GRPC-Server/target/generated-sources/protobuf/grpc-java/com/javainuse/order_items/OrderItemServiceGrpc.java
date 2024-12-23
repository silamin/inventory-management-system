package com.javainuse.order_items;

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
    comments = "Source: order-item-service.proto")
public final class OrderItemServiceGrpc {

  private OrderItemServiceGrpc() {}

  public static final String SERVICE_NAME = "order_items.OrderItemService";

  // Static method descriptors that strictly reflect the proto.
  private static volatile io.grpc.MethodDescriptor<com.javainuse.order_items.UpdateOrderItemRequest,
      com.google.protobuf.Empty> getUpdateOrderItemMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "updateOrderItem",
      requestType = com.javainuse.order_items.UpdateOrderItemRequest.class,
      responseType = com.google.protobuf.Empty.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<com.javainuse.order_items.UpdateOrderItemRequest,
      com.google.protobuf.Empty> getUpdateOrderItemMethod() {
    io.grpc.MethodDescriptor<com.javainuse.order_items.UpdateOrderItemRequest, com.google.protobuf.Empty> getUpdateOrderItemMethod;
    if ((getUpdateOrderItemMethod = OrderItemServiceGrpc.getUpdateOrderItemMethod) == null) {
      synchronized (OrderItemServiceGrpc.class) {
        if ((getUpdateOrderItemMethod = OrderItemServiceGrpc.getUpdateOrderItemMethod) == null) {
          OrderItemServiceGrpc.getUpdateOrderItemMethod = getUpdateOrderItemMethod = 
              io.grpc.MethodDescriptor.<com.javainuse.order_items.UpdateOrderItemRequest, com.google.protobuf.Empty>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(
                  "order_items.OrderItemService", "updateOrderItem"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  com.javainuse.order_items.UpdateOrderItemRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  com.google.protobuf.Empty.getDefaultInstance()))
                  .setSchemaDescriptor(new OrderItemServiceMethodDescriptorSupplier("updateOrderItem"))
                  .build();
          }
        }
     }
     return getUpdateOrderItemMethod;
  }

  /**
   * Creates a new async stub that supports all call types for the service
   */
  public static OrderItemServiceStub newStub(io.grpc.Channel channel) {
    return new OrderItemServiceStub(channel);
  }

  /**
   * Creates a new blocking-style stub that supports unary and streaming output calls on the service
   */
  public static OrderItemServiceBlockingStub newBlockingStub(
      io.grpc.Channel channel) {
    return new OrderItemServiceBlockingStub(channel);
  }

  /**
   * Creates a new ListenableFuture-style stub that supports unary calls on the service
   */
  public static OrderItemServiceFutureStub newFutureStub(
      io.grpc.Channel channel) {
    return new OrderItemServiceFutureStub(channel);
  }

  /**
   */
  public static abstract class OrderItemServiceImplBase implements io.grpc.BindableService {

    /**
     */
    public void updateOrderItem(com.javainuse.order_items.UpdateOrderItemRequest request,
        io.grpc.stub.StreamObserver<com.google.protobuf.Empty> responseObserver) {
      asyncUnimplementedUnaryCall(getUpdateOrderItemMethod(), responseObserver);
    }

    @java.lang.Override public final io.grpc.ServerServiceDefinition bindService() {
      return io.grpc.ServerServiceDefinition.builder(getServiceDescriptor())
          .addMethod(
            getUpdateOrderItemMethod(),
            asyncUnaryCall(
              new MethodHandlers<
                com.javainuse.order_items.UpdateOrderItemRequest,
                com.google.protobuf.Empty>(
                  this, METHODID_UPDATE_ORDER_ITEM)))
          .build();
    }
  }

  /**
   */
  public static final class OrderItemServiceStub extends io.grpc.stub.AbstractStub<OrderItemServiceStub> {
    private OrderItemServiceStub(io.grpc.Channel channel) {
      super(channel);
    }

    private OrderItemServiceStub(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @java.lang.Override
    protected OrderItemServiceStub build(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      return new OrderItemServiceStub(channel, callOptions);
    }

    /**
     */
    public void updateOrderItem(com.javainuse.order_items.UpdateOrderItemRequest request,
        io.grpc.stub.StreamObserver<com.google.protobuf.Empty> responseObserver) {
      asyncUnaryCall(
          getChannel().newCall(getUpdateOrderItemMethod(), getCallOptions()), request, responseObserver);
    }
  }

  /**
   */
  public static final class OrderItemServiceBlockingStub extends io.grpc.stub.AbstractStub<OrderItemServiceBlockingStub> {
    private OrderItemServiceBlockingStub(io.grpc.Channel channel) {
      super(channel);
    }

    private OrderItemServiceBlockingStub(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @java.lang.Override
    protected OrderItemServiceBlockingStub build(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      return new OrderItemServiceBlockingStub(channel, callOptions);
    }

    /**
     */
    public com.google.protobuf.Empty updateOrderItem(com.javainuse.order_items.UpdateOrderItemRequest request) {
      return blockingUnaryCall(
          getChannel(), getUpdateOrderItemMethod(), getCallOptions(), request);
    }
  }

  /**
   */
  public static final class OrderItemServiceFutureStub extends io.grpc.stub.AbstractStub<OrderItemServiceFutureStub> {
    private OrderItemServiceFutureStub(io.grpc.Channel channel) {
      super(channel);
    }

    private OrderItemServiceFutureStub(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @java.lang.Override
    protected OrderItemServiceFutureStub build(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      return new OrderItemServiceFutureStub(channel, callOptions);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<com.google.protobuf.Empty> updateOrderItem(
        com.javainuse.order_items.UpdateOrderItemRequest request) {
      return futureUnaryCall(
          getChannel().newCall(getUpdateOrderItemMethod(), getCallOptions()), request);
    }
  }

  private static final int METHODID_UPDATE_ORDER_ITEM = 0;

  private static final class MethodHandlers<Req, Resp> implements
      io.grpc.stub.ServerCalls.UnaryMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.ServerStreamingMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.ClientStreamingMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.BidiStreamingMethod<Req, Resp> {
    private final OrderItemServiceImplBase serviceImpl;
    private final int methodId;

    MethodHandlers(OrderItemServiceImplBase serviceImpl, int methodId) {
      this.serviceImpl = serviceImpl;
      this.methodId = methodId;
    }

    @java.lang.Override
    @java.lang.SuppressWarnings("unchecked")
    public void invoke(Req request, io.grpc.stub.StreamObserver<Resp> responseObserver) {
      switch (methodId) {
        case METHODID_UPDATE_ORDER_ITEM:
          serviceImpl.updateOrderItem((com.javainuse.order_items.UpdateOrderItemRequest) request,
              (io.grpc.stub.StreamObserver<com.google.protobuf.Empty>) responseObserver);
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

  private static abstract class OrderItemServiceBaseDescriptorSupplier
      implements io.grpc.protobuf.ProtoFileDescriptorSupplier, io.grpc.protobuf.ProtoServiceDescriptorSupplier {
    OrderItemServiceBaseDescriptorSupplier() {}

    @java.lang.Override
    public com.google.protobuf.Descriptors.FileDescriptor getFileDescriptor() {
      return com.javainuse.order_items.OrderItemServiceOuterClass.getDescriptor();
    }

    @java.lang.Override
    public com.google.protobuf.Descriptors.ServiceDescriptor getServiceDescriptor() {
      return getFileDescriptor().findServiceByName("OrderItemService");
    }
  }

  private static final class OrderItemServiceFileDescriptorSupplier
      extends OrderItemServiceBaseDescriptorSupplier {
    OrderItemServiceFileDescriptorSupplier() {}
  }

  private static final class OrderItemServiceMethodDescriptorSupplier
      extends OrderItemServiceBaseDescriptorSupplier
      implements io.grpc.protobuf.ProtoMethodDescriptorSupplier {
    private final String methodName;

    OrderItemServiceMethodDescriptorSupplier(String methodName) {
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
      synchronized (OrderItemServiceGrpc.class) {
        result = serviceDescriptor;
        if (result == null) {
          serviceDescriptor = result = io.grpc.ServiceDescriptor.newBuilder(SERVICE_NAME)
              .setSchemaDescriptor(new OrderItemServiceFileDescriptorSupplier())
              .addMethod(getUpdateOrderItemMethod())
              .build();
        }
      }
    }
    return result;
  }
}
