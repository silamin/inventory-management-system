package com.javainuse.user;

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
    comments = "Source: user-service.proto")
public final class UserServiceGrpc {

  private UserServiceGrpc() {}

  public static final String SERVICE_NAME = "users.UserService";

  // Static method descriptors that strictly reflect the proto.
  private static volatile io.grpc.MethodDescriptor<com.javainuse.user.CreateUser,
      com.javainuse.user.GetUser> getAddUserMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "addUser",
      requestType = com.javainuse.user.CreateUser.class,
      responseType = com.javainuse.user.GetUser.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<com.javainuse.user.CreateUser,
      com.javainuse.user.GetUser> getAddUserMethod() {
    io.grpc.MethodDescriptor<com.javainuse.user.CreateUser, com.javainuse.user.GetUser> getAddUserMethod;
    if ((getAddUserMethod = UserServiceGrpc.getAddUserMethod) == null) {
      synchronized (UserServiceGrpc.class) {
        if ((getAddUserMethod = UserServiceGrpc.getAddUserMethod) == null) {
          UserServiceGrpc.getAddUserMethod = getAddUserMethod = 
              io.grpc.MethodDescriptor.<com.javainuse.user.CreateUser, com.javainuse.user.GetUser>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(
                  "users.UserService", "addUser"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  com.javainuse.user.CreateUser.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  com.javainuse.user.GetUser.getDefaultInstance()))
                  .setSchemaDescriptor(new UserServiceMethodDescriptorSupplier("addUser"))
                  .build();
          }
        }
     }
     return getAddUserMethod;
  }

  private static volatile io.grpc.MethodDescriptor<com.javainuse.user.User,
      com.google.protobuf.Empty> getEditUserMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "editUser",
      requestType = com.javainuse.user.User.class,
      responseType = com.google.protobuf.Empty.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<com.javainuse.user.User,
      com.google.protobuf.Empty> getEditUserMethod() {
    io.grpc.MethodDescriptor<com.javainuse.user.User, com.google.protobuf.Empty> getEditUserMethod;
    if ((getEditUserMethod = UserServiceGrpc.getEditUserMethod) == null) {
      synchronized (UserServiceGrpc.class) {
        if ((getEditUserMethod = UserServiceGrpc.getEditUserMethod) == null) {
          UserServiceGrpc.getEditUserMethod = getEditUserMethod = 
              io.grpc.MethodDescriptor.<com.javainuse.user.User, com.google.protobuf.Empty>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(
                  "users.UserService", "editUser"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  com.javainuse.user.User.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  com.google.protobuf.Empty.getDefaultInstance()))
                  .setSchemaDescriptor(new UserServiceMethodDescriptorSupplier("editUser"))
                  .build();
          }
        }
     }
     return getEditUserMethod;
  }

  private static volatile io.grpc.MethodDescriptor<com.javainuse.user.DeleteUser,
      com.google.protobuf.Empty> getDeleteUserMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "deleteUser",
      requestType = com.javainuse.user.DeleteUser.class,
      responseType = com.google.protobuf.Empty.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<com.javainuse.user.DeleteUser,
      com.google.protobuf.Empty> getDeleteUserMethod() {
    io.grpc.MethodDescriptor<com.javainuse.user.DeleteUser, com.google.protobuf.Empty> getDeleteUserMethod;
    if ((getDeleteUserMethod = UserServiceGrpc.getDeleteUserMethod) == null) {
      synchronized (UserServiceGrpc.class) {
        if ((getDeleteUserMethod = UserServiceGrpc.getDeleteUserMethod) == null) {
          UserServiceGrpc.getDeleteUserMethod = getDeleteUserMethod = 
              io.grpc.MethodDescriptor.<com.javainuse.user.DeleteUser, com.google.protobuf.Empty>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(
                  "users.UserService", "deleteUser"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  com.javainuse.user.DeleteUser.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  com.google.protobuf.Empty.getDefaultInstance()))
                  .setSchemaDescriptor(new UserServiceMethodDescriptorSupplier("deleteUser"))
                  .build();
          }
        }
     }
     return getDeleteUserMethod;
  }

  private static volatile io.grpc.MethodDescriptor<com.javainuse.user.Role,
      com.javainuse.user.UserList> getGetUsersMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "getUsers",
      requestType = com.javainuse.user.Role.class,
      responseType = com.javainuse.user.UserList.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<com.javainuse.user.Role,
      com.javainuse.user.UserList> getGetUsersMethod() {
    io.grpc.MethodDescriptor<com.javainuse.user.Role, com.javainuse.user.UserList> getGetUsersMethod;
    if ((getGetUsersMethod = UserServiceGrpc.getGetUsersMethod) == null) {
      synchronized (UserServiceGrpc.class) {
        if ((getGetUsersMethod = UserServiceGrpc.getGetUsersMethod) == null) {
          UserServiceGrpc.getGetUsersMethod = getGetUsersMethod = 
              io.grpc.MethodDescriptor.<com.javainuse.user.Role, com.javainuse.user.UserList>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(
                  "users.UserService", "getUsers"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  com.javainuse.user.Role.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  com.javainuse.user.UserList.getDefaultInstance()))
                  .setSchemaDescriptor(new UserServiceMethodDescriptorSupplier("getUsers"))
                  .build();
          }
        }
     }
     return getGetUsersMethod;
  }

  /**
   * Creates a new async stub that supports all call types for the service
   */
  public static UserServiceStub newStub(io.grpc.Channel channel) {
    return new UserServiceStub(channel);
  }

  /**
   * Creates a new blocking-style stub that supports unary and streaming output calls on the service
   */
  public static UserServiceBlockingStub newBlockingStub(
      io.grpc.Channel channel) {
    return new UserServiceBlockingStub(channel);
  }

  /**
   * Creates a new ListenableFuture-style stub that supports unary calls on the service
   */
  public static UserServiceFutureStub newFutureStub(
      io.grpc.Channel channel) {
    return new UserServiceFutureStub(channel);
  }

  /**
   */
  public static abstract class UserServiceImplBase implements io.grpc.BindableService {

    /**
     */
    public void addUser(com.javainuse.user.CreateUser request,
        io.grpc.stub.StreamObserver<com.javainuse.user.GetUser> responseObserver) {
      asyncUnimplementedUnaryCall(getAddUserMethod(), responseObserver);
    }

    /**
     */
    public void editUser(com.javainuse.user.User request,
        io.grpc.stub.StreamObserver<com.google.protobuf.Empty> responseObserver) {
      asyncUnimplementedUnaryCall(getEditUserMethod(), responseObserver);
    }

    /**
     */
    public void deleteUser(com.javainuse.user.DeleteUser request,
        io.grpc.stub.StreamObserver<com.google.protobuf.Empty> responseObserver) {
      asyncUnimplementedUnaryCall(getDeleteUserMethod(), responseObserver);
    }

    /**
     */
    public void getUsers(com.javainuse.user.Role request,
        io.grpc.stub.StreamObserver<com.javainuse.user.UserList> responseObserver) {
      asyncUnimplementedUnaryCall(getGetUsersMethod(), responseObserver);
    }

    @java.lang.Override public final io.grpc.ServerServiceDefinition bindService() {
      return io.grpc.ServerServiceDefinition.builder(getServiceDescriptor())
          .addMethod(
            getAddUserMethod(),
            asyncUnaryCall(
              new MethodHandlers<
                com.javainuse.user.CreateUser,
                com.javainuse.user.GetUser>(
                  this, METHODID_ADD_USER)))
          .addMethod(
            getEditUserMethod(),
            asyncUnaryCall(
              new MethodHandlers<
                com.javainuse.user.User,
                com.google.protobuf.Empty>(
                  this, METHODID_EDIT_USER)))
          .addMethod(
            getDeleteUserMethod(),
            asyncUnaryCall(
              new MethodHandlers<
                com.javainuse.user.DeleteUser,
                com.google.protobuf.Empty>(
                  this, METHODID_DELETE_USER)))
          .addMethod(
            getGetUsersMethod(),
            asyncUnaryCall(
              new MethodHandlers<
                com.javainuse.user.Role,
                com.javainuse.user.UserList>(
                  this, METHODID_GET_USERS)))
          .build();
    }
  }

  /**
   */
  public static final class UserServiceStub extends io.grpc.stub.AbstractStub<UserServiceStub> {
    private UserServiceStub(io.grpc.Channel channel) {
      super(channel);
    }

    private UserServiceStub(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @java.lang.Override
    protected UserServiceStub build(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      return new UserServiceStub(channel, callOptions);
    }

    /**
     */
    public void addUser(com.javainuse.user.CreateUser request,
        io.grpc.stub.StreamObserver<com.javainuse.user.GetUser> responseObserver) {
      asyncUnaryCall(
          getChannel().newCall(getAddUserMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void editUser(com.javainuse.user.User request,
        io.grpc.stub.StreamObserver<com.google.protobuf.Empty> responseObserver) {
      asyncUnaryCall(
          getChannel().newCall(getEditUserMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void deleteUser(com.javainuse.user.DeleteUser request,
        io.grpc.stub.StreamObserver<com.google.protobuf.Empty> responseObserver) {
      asyncUnaryCall(
          getChannel().newCall(getDeleteUserMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void getUsers(com.javainuse.user.Role request,
        io.grpc.stub.StreamObserver<com.javainuse.user.UserList> responseObserver) {
      asyncUnaryCall(
          getChannel().newCall(getGetUsersMethod(), getCallOptions()), request, responseObserver);
    }
  }

  /**
   */
  public static final class UserServiceBlockingStub extends io.grpc.stub.AbstractStub<UserServiceBlockingStub> {
    private UserServiceBlockingStub(io.grpc.Channel channel) {
      super(channel);
    }

    private UserServiceBlockingStub(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @java.lang.Override
    protected UserServiceBlockingStub build(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      return new UserServiceBlockingStub(channel, callOptions);
    }

    /**
     */
    public com.javainuse.user.GetUser addUser(com.javainuse.user.CreateUser request) {
      return blockingUnaryCall(
          getChannel(), getAddUserMethod(), getCallOptions(), request);
    }

    /**
     */
    public com.google.protobuf.Empty editUser(com.javainuse.user.User request) {
      return blockingUnaryCall(
          getChannel(), getEditUserMethod(), getCallOptions(), request);
    }

    /**
     */
    public com.google.protobuf.Empty deleteUser(com.javainuse.user.DeleteUser request) {
      return blockingUnaryCall(
          getChannel(), getDeleteUserMethod(), getCallOptions(), request);
    }

    /**
     */
    public com.javainuse.user.UserList getUsers(com.javainuse.user.Role request) {
      return blockingUnaryCall(
          getChannel(), getGetUsersMethod(), getCallOptions(), request);
    }
  }

  /**
   */
  public static final class UserServiceFutureStub extends io.grpc.stub.AbstractStub<UserServiceFutureStub> {
    private UserServiceFutureStub(io.grpc.Channel channel) {
      super(channel);
    }

    private UserServiceFutureStub(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @java.lang.Override
    protected UserServiceFutureStub build(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      return new UserServiceFutureStub(channel, callOptions);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<com.javainuse.user.GetUser> addUser(
        com.javainuse.user.CreateUser request) {
      return futureUnaryCall(
          getChannel().newCall(getAddUserMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<com.google.protobuf.Empty> editUser(
        com.javainuse.user.User request) {
      return futureUnaryCall(
          getChannel().newCall(getEditUserMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<com.google.protobuf.Empty> deleteUser(
        com.javainuse.user.DeleteUser request) {
      return futureUnaryCall(
          getChannel().newCall(getDeleteUserMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<com.javainuse.user.UserList> getUsers(
        com.javainuse.user.Role request) {
      return futureUnaryCall(
          getChannel().newCall(getGetUsersMethod(), getCallOptions()), request);
    }
  }

  private static final int METHODID_ADD_USER = 0;
  private static final int METHODID_EDIT_USER = 1;
  private static final int METHODID_DELETE_USER = 2;
  private static final int METHODID_GET_USERS = 3;

  private static final class MethodHandlers<Req, Resp> implements
      io.grpc.stub.ServerCalls.UnaryMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.ServerStreamingMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.ClientStreamingMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.BidiStreamingMethod<Req, Resp> {
    private final UserServiceImplBase serviceImpl;
    private final int methodId;

    MethodHandlers(UserServiceImplBase serviceImpl, int methodId) {
      this.serviceImpl = serviceImpl;
      this.methodId = methodId;
    }

    @java.lang.Override
    @java.lang.SuppressWarnings("unchecked")
    public void invoke(Req request, io.grpc.stub.StreamObserver<Resp> responseObserver) {
      switch (methodId) {
        case METHODID_ADD_USER:
          serviceImpl.addUser((com.javainuse.user.CreateUser) request,
              (io.grpc.stub.StreamObserver<com.javainuse.user.GetUser>) responseObserver);
          break;
        case METHODID_EDIT_USER:
          serviceImpl.editUser((com.javainuse.user.User) request,
              (io.grpc.stub.StreamObserver<com.google.protobuf.Empty>) responseObserver);
          break;
        case METHODID_DELETE_USER:
          serviceImpl.deleteUser((com.javainuse.user.DeleteUser) request,
              (io.grpc.stub.StreamObserver<com.google.protobuf.Empty>) responseObserver);
          break;
        case METHODID_GET_USERS:
          serviceImpl.getUsers((com.javainuse.user.Role) request,
              (io.grpc.stub.StreamObserver<com.javainuse.user.UserList>) responseObserver);
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

  private static abstract class UserServiceBaseDescriptorSupplier
      implements io.grpc.protobuf.ProtoFileDescriptorSupplier, io.grpc.protobuf.ProtoServiceDescriptorSupplier {
    UserServiceBaseDescriptorSupplier() {}

    @java.lang.Override
    public com.google.protobuf.Descriptors.FileDescriptor getFileDescriptor() {
      return com.javainuse.user.UserServiceOuterClass.getDescriptor();
    }

    @java.lang.Override
    public com.google.protobuf.Descriptors.ServiceDescriptor getServiceDescriptor() {
      return getFileDescriptor().findServiceByName("UserService");
    }
  }

  private static final class UserServiceFileDescriptorSupplier
      extends UserServiceBaseDescriptorSupplier {
    UserServiceFileDescriptorSupplier() {}
  }

  private static final class UserServiceMethodDescriptorSupplier
      extends UserServiceBaseDescriptorSupplier
      implements io.grpc.protobuf.ProtoMethodDescriptorSupplier {
    private final String methodName;

    UserServiceMethodDescriptorSupplier(String methodName) {
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
      synchronized (UserServiceGrpc.class) {
        result = serviceDescriptor;
        if (result == null) {
          serviceDescriptor = result = io.grpc.ServiceDescriptor.newBuilder(SERVICE_NAME)
              .setSchemaDescriptor(new UserServiceFileDescriptorSupplier())
              .addMethod(getAddUserMethod())
              .addMethod(getEditUserMethod())
              .addMethod(getDeleteUserMethod())
              .addMethod(getGetUsersMethod())
              .build();
        }
      }
    }
    return result;
  }
}
