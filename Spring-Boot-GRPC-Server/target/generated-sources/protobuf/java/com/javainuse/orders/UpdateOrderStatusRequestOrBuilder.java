// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: order-service.proto

package com.javainuse.orders;

public interface UpdateOrderStatusRequestOrBuilder extends
    // @@protoc_insertion_point(interface_extends:orders.UpdateOrderStatusRequest)
    com.google.protobuf.MessageOrBuilder {

  /**
   * <code>int32 order_id = 1;</code>
   */
  int getOrderId();

  /**
   * <code>.orders.OrderStatus new_status = 2;</code>
   */
  int getNewStatusValue();
  /**
   * <code>.orders.OrderStatus new_status = 2;</code>
   */
  com.javainuse.orders.OrderStatus getNewStatus();
}
