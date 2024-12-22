// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: order-service.proto

package com.javainuse.orders;

public final class OrderServiceOuterClass {
  private OrderServiceOuterClass() {}
  public static void registerAllExtensions(
      com.google.protobuf.ExtensionRegistryLite registry) {
  }

  public static void registerAllExtensions(
      com.google.protobuf.ExtensionRegistry registry) {
    registerAllExtensions(
        (com.google.protobuf.ExtensionRegistryLite) registry);
  }
  static final com.google.protobuf.Descriptors.Descriptor
    internal_static_orders_Status_descriptor;
  static final 
    com.google.protobuf.GeneratedMessageV3.FieldAccessorTable
      internal_static_orders_Status_fieldAccessorTable;
  static final com.google.protobuf.Descriptors.Descriptor
    internal_static_orders_CreateOrderResponse_descriptor;
  static final 
    com.google.protobuf.GeneratedMessageV3.FieldAccessorTable
      internal_static_orders_CreateOrderResponse_fieldAccessorTable;
  static final com.google.protobuf.Descriptors.Descriptor
    internal_static_orders_CreateOrder_descriptor;
  static final 
    com.google.protobuf.GeneratedMessageV3.FieldAccessorTable
      internal_static_orders_CreateOrder_fieldAccessorTable;
  static final com.google.protobuf.Descriptors.Descriptor
    internal_static_orders_CreateOrderItem_descriptor;
  static final 
    com.google.protobuf.GeneratedMessageV3.FieldAccessorTable
      internal_static_orders_CreateOrderItem_fieldAccessorTable;
  static final com.google.protobuf.Descriptors.Descriptor
    internal_static_orders_Order_descriptor;
  static final 
    com.google.protobuf.GeneratedMessageV3.FieldAccessorTable
      internal_static_orders_Order_fieldAccessorTable;
  static final com.google.protobuf.Descriptors.Descriptor
    internal_static_orders_UpdateOrderStatusRequest_descriptor;
  static final 
    com.google.protobuf.GeneratedMessageV3.FieldAccessorTable
      internal_static_orders_UpdateOrderStatusRequest_fieldAccessorTable;
  static final com.google.protobuf.Descriptors.Descriptor
    internal_static_orders_GetOrderItem_descriptor;
  static final 
    com.google.protobuf.GeneratedMessageV3.FieldAccessorTable
      internal_static_orders_GetOrderItem_fieldAccessorTable;
  static final com.google.protobuf.Descriptors.Descriptor
    internal_static_orders_OrderList_descriptor;
  static final 
    com.google.protobuf.GeneratedMessageV3.FieldAccessorTable
      internal_static_orders_OrderList_fieldAccessorTable;

  public static com.google.protobuf.Descriptors.FileDescriptor
      getDescriptor() {
    return descriptor;
  }
  private static  com.google.protobuf.Descriptors.FileDescriptor
      descriptor;
  static {
    java.lang.String[] descriptorData = {
      "\n\023order-service.proto\022\006orders\032\037google/pr" +
      "otobuf/timestamp.proto\032\033google/protobuf/" +
      "empty.proto\032\022user-service.proto\032\022item-se" +
      "rvice.proto\"3\n\006Status\022)\n\014order_status\030\001 " +
      "\001(\0162\023.orders.OrderStatus\"&\n\023CreateOrderR" +
      "esponse\022\017\n\007success\030\001 \001(\010\"\202\001\n\013CreateOrder" +
      "\022,\n\013order_items\030\001 \003(\0132\027.orders.CreateOrd" +
      "erItem\0221\n\rdelivery_date\030\002 \001(\0132\032.google.p" +
      "rotobuf.Timestamp\022\022\n\ncreated_by\030\003 \001(\005\":\n" +
      "\017CreateOrderItem\022\017\n\007item_id\030\001 \001(\005\022\026\n\016tot" +
      "al_quantity\030\002 \001(\005\"\264\002\n\005Order\022\020\n\010order_id\030" +
      "\001 \001(\005\022)\n\013order_items\030\002 \003(\0132\024.orders.GetO" +
      "rderItem\022.\n\ncreated_at\030\003 \001(\0132\032.google.pr" +
      "otobuf.Timestamp\022\025\n\rassigned_user\030\004 \001(\t\022" +
      "\027\n\017created_by_user\030\005 \001(\t\022)\n\014order_status" +
      "\030\006 \001(\0162\023.orders.OrderStatus\0221\n\rdelivery_" +
      "date\030\007 \001(\0132\032.google.protobuf.Timestamp\0220" +
      "\n\014completed_at\030\010 \001(\0132\032.google.protobuf.T" +
      "imestamp\"U\n\030UpdateOrderStatusRequest\022\020\n\010" +
      "order_id\030\001 \001(\005\022\'\n\nnew_status\030\002 \001(\0162\023.ord" +
      "ers.OrderStatus\"i\n\014GetOrderItem\022\025\n\rorder" +
      "_item_id\030\001 \001(\005\022\020\n\010itemName\030\002 \001(\t\022\030\n\020quan" +
      "tity_to_pick\030\003 \001(\005\022\026\n\016total_quantity\030\004 \001" +
      "(\005\"*\n\tOrderList\022\035\n\006orders\030\001 \003(\0132\r.orders" +
      ".Order*>\n\013OrderStatus\022\017\n\013NOT_STARTED\020\000\022\r" +
      "\n\tCOMPLETED\020\001\022\017\n\013IN_PROGRESS\020\0022\316\001\n\014Order" +
      "Service\022?\n\013createOrder\022\023.orders.CreateOr" +
      "der\032\033.orders.CreateOrderResponse\022.\n\tgetO" +
      "rders\022\016.orders.Status\032\021.orders.OrderList" +
      "\022M\n\021updateOrderStatus\022 .orders.UpdateOrd" +
      "erStatusRequest\032\026.google.protobuf.EmptyB" +
      "\030\n\024com.javainuse.ordersP\001b\006proto3"
    };
    com.google.protobuf.Descriptors.FileDescriptor.InternalDescriptorAssigner assigner =
        new com.google.protobuf.Descriptors.FileDescriptor.    InternalDescriptorAssigner() {
          public com.google.protobuf.ExtensionRegistry assignDescriptors(
              com.google.protobuf.Descriptors.FileDescriptor root) {
            descriptor = root;
            return null;
          }
        };
    com.google.protobuf.Descriptors.FileDescriptor
      .internalBuildGeneratedFileFrom(descriptorData,
        new com.google.protobuf.Descriptors.FileDescriptor[] {
          com.google.protobuf.TimestampProto.getDescriptor(),
          com.google.protobuf.EmptyProto.getDescriptor(),
          com.javainuse.user.UserServiceOuterClass.getDescriptor(),
          com.javainuse.item.ItemServiceOuterClass.getDescriptor(),
        }, assigner);
    internal_static_orders_Status_descriptor =
      getDescriptor().getMessageTypes().get(0);
    internal_static_orders_Status_fieldAccessorTable = new
      com.google.protobuf.GeneratedMessageV3.FieldAccessorTable(
        internal_static_orders_Status_descriptor,
        new java.lang.String[] { "OrderStatus", });
    internal_static_orders_CreateOrderResponse_descriptor =
      getDescriptor().getMessageTypes().get(1);
    internal_static_orders_CreateOrderResponse_fieldAccessorTable = new
      com.google.protobuf.GeneratedMessageV3.FieldAccessorTable(
        internal_static_orders_CreateOrderResponse_descriptor,
        new java.lang.String[] { "Success", });
    internal_static_orders_CreateOrder_descriptor =
      getDescriptor().getMessageTypes().get(2);
    internal_static_orders_CreateOrder_fieldAccessorTable = new
      com.google.protobuf.GeneratedMessageV3.FieldAccessorTable(
        internal_static_orders_CreateOrder_descriptor,
        new java.lang.String[] { "OrderItems", "DeliveryDate", "CreatedBy", });
    internal_static_orders_CreateOrderItem_descriptor =
      getDescriptor().getMessageTypes().get(3);
    internal_static_orders_CreateOrderItem_fieldAccessorTable = new
      com.google.protobuf.GeneratedMessageV3.FieldAccessorTable(
        internal_static_orders_CreateOrderItem_descriptor,
        new java.lang.String[] { "ItemId", "TotalQuantity", });
    internal_static_orders_Order_descriptor =
      getDescriptor().getMessageTypes().get(4);
    internal_static_orders_Order_fieldAccessorTable = new
      com.google.protobuf.GeneratedMessageV3.FieldAccessorTable(
        internal_static_orders_Order_descriptor,
        new java.lang.String[] { "OrderId", "OrderItems", "CreatedAt", "AssignedUser", "CreatedByUser", "OrderStatus", "DeliveryDate", "CompletedAt", });
    internal_static_orders_UpdateOrderStatusRequest_descriptor =
      getDescriptor().getMessageTypes().get(5);
    internal_static_orders_UpdateOrderStatusRequest_fieldAccessorTable = new
      com.google.protobuf.GeneratedMessageV3.FieldAccessorTable(
        internal_static_orders_UpdateOrderStatusRequest_descriptor,
        new java.lang.String[] { "OrderId", "NewStatus", });
    internal_static_orders_GetOrderItem_descriptor =
      getDescriptor().getMessageTypes().get(6);
    internal_static_orders_GetOrderItem_fieldAccessorTable = new
      com.google.protobuf.GeneratedMessageV3.FieldAccessorTable(
        internal_static_orders_GetOrderItem_descriptor,
        new java.lang.String[] { "OrderItemId", "ItemName", "QuantityToPick", "TotalQuantity", });
    internal_static_orders_OrderList_descriptor =
      getDescriptor().getMessageTypes().get(7);
    internal_static_orders_OrderList_fieldAccessorTable = new
      com.google.protobuf.GeneratedMessageV3.FieldAccessorTable(
        internal_static_orders_OrderList_descriptor,
        new java.lang.String[] { "Orders", });
    com.google.protobuf.TimestampProto.getDescriptor();
    com.google.protobuf.EmptyProto.getDescriptor();
    com.javainuse.user.UserServiceOuterClass.getDescriptor();
    com.javainuse.item.ItemServiceOuterClass.getDescriptor();
  }

  // @@protoc_insertion_point(outer_class_scope)
}
