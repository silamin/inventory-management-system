// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: order-service.proto

package com.javainuse.orders;

public interface OrderListOrBuilder extends
    // @@protoc_insertion_point(interface_extends:orders.OrderList)
    com.google.protobuf.MessageOrBuilder {

  /**
   * <code>repeated .orders.Order orders = 1;</code>
   */
  java.util.List<com.javainuse.orders.Order> 
      getOrdersList();
  /**
   * <code>repeated .orders.Order orders = 1;</code>
   */
  com.javainuse.orders.Order getOrders(int index);
  /**
   * <code>repeated .orders.Order orders = 1;</code>
   */
  int getOrdersCount();
  /**
   * <code>repeated .orders.Order orders = 1;</code>
   */
  java.util.List<? extends com.javainuse.orders.OrderOrBuilder> 
      getOrdersOrBuilderList();
  /**
   * <code>repeated .orders.Order orders = 1;</code>
   */
  com.javainuse.orders.OrderOrBuilder getOrdersOrBuilder(
      int index);
}
