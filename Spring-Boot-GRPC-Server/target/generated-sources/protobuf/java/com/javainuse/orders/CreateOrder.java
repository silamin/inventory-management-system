// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: order-service.proto

package com.javainuse.orders;

/**
 * Protobuf type {@code orders.CreateOrder}
 */
public  final class CreateOrder extends
    com.google.protobuf.GeneratedMessageV3 implements
    // @@protoc_insertion_point(message_implements:orders.CreateOrder)
    CreateOrderOrBuilder {
private static final long serialVersionUID = 0L;
  // Use CreateOrder.newBuilder() to construct.
  private CreateOrder(com.google.protobuf.GeneratedMessageV3.Builder<?> builder) {
    super(builder);
  }
  private CreateOrder() {
    orderItems_ = java.util.Collections.emptyList();
    createdBy_ = 0;
  }

  @java.lang.Override
  public final com.google.protobuf.UnknownFieldSet
  getUnknownFields() {
    return this.unknownFields;
  }
  private CreateOrder(
      com.google.protobuf.CodedInputStream input,
      com.google.protobuf.ExtensionRegistryLite extensionRegistry)
      throws com.google.protobuf.InvalidProtocolBufferException {
    this();
    if (extensionRegistry == null) {
      throw new java.lang.NullPointerException();
    }
    int mutable_bitField0_ = 0;
    com.google.protobuf.UnknownFieldSet.Builder unknownFields =
        com.google.protobuf.UnknownFieldSet.newBuilder();
    try {
      boolean done = false;
      while (!done) {
        int tag = input.readTag();
        switch (tag) {
          case 0:
            done = true;
            break;
          case 10: {
            if (!((mutable_bitField0_ & 0x00000001) == 0x00000001)) {
              orderItems_ = new java.util.ArrayList<com.javainuse.orders.CreateOrderItem>();
              mutable_bitField0_ |= 0x00000001;
            }
            orderItems_.add(
                input.readMessage(com.javainuse.orders.CreateOrderItem.parser(), extensionRegistry));
            break;
          }
          case 18: {
            com.google.protobuf.Timestamp.Builder subBuilder = null;
            if (deliveryDate_ != null) {
              subBuilder = deliveryDate_.toBuilder();
            }
            deliveryDate_ = input.readMessage(com.google.protobuf.Timestamp.parser(), extensionRegistry);
            if (subBuilder != null) {
              subBuilder.mergeFrom(deliveryDate_);
              deliveryDate_ = subBuilder.buildPartial();
            }

            break;
          }
          case 24: {

            createdBy_ = input.readInt32();
            break;
          }
          default: {
            if (!parseUnknownFieldProto3(
                input, unknownFields, extensionRegistry, tag)) {
              done = true;
            }
            break;
          }
        }
      }
    } catch (com.google.protobuf.InvalidProtocolBufferException e) {
      throw e.setUnfinishedMessage(this);
    } catch (java.io.IOException e) {
      throw new com.google.protobuf.InvalidProtocolBufferException(
          e).setUnfinishedMessage(this);
    } finally {
      if (((mutable_bitField0_ & 0x00000001) == 0x00000001)) {
        orderItems_ = java.util.Collections.unmodifiableList(orderItems_);
      }
      this.unknownFields = unknownFields.build();
      makeExtensionsImmutable();
    }
  }
  public static final com.google.protobuf.Descriptors.Descriptor
      getDescriptor() {
    return com.javainuse.orders.OrderServiceOuterClass.internal_static_orders_CreateOrder_descriptor;
  }

  @java.lang.Override
  protected com.google.protobuf.GeneratedMessageV3.FieldAccessorTable
      internalGetFieldAccessorTable() {
    return com.javainuse.orders.OrderServiceOuterClass.internal_static_orders_CreateOrder_fieldAccessorTable
        .ensureFieldAccessorsInitialized(
            com.javainuse.orders.CreateOrder.class, com.javainuse.orders.CreateOrder.Builder.class);
  }

  private int bitField0_;
  public static final int ORDER_ITEMS_FIELD_NUMBER = 1;
  private java.util.List<com.javainuse.orders.CreateOrderItem> orderItems_;
  /**
   * <pre>
   * For creating, we only send item_ids
   * </pre>
   *
   * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
   */
  public java.util.List<com.javainuse.orders.CreateOrderItem> getOrderItemsList() {
    return orderItems_;
  }
  /**
   * <pre>
   * For creating, we only send item_ids
   * </pre>
   *
   * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
   */
  public java.util.List<? extends com.javainuse.orders.CreateOrderItemOrBuilder> 
      getOrderItemsOrBuilderList() {
    return orderItems_;
  }
  /**
   * <pre>
   * For creating, we only send item_ids
   * </pre>
   *
   * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
   */
  public int getOrderItemsCount() {
    return orderItems_.size();
  }
  /**
   * <pre>
   * For creating, we only send item_ids
   * </pre>
   *
   * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
   */
  public com.javainuse.orders.CreateOrderItem getOrderItems(int index) {
    return orderItems_.get(index);
  }
  /**
   * <pre>
   * For creating, we only send item_ids
   * </pre>
   *
   * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
   */
  public com.javainuse.orders.CreateOrderItemOrBuilder getOrderItemsOrBuilder(
      int index) {
    return orderItems_.get(index);
  }

  public static final int DELIVERY_DATE_FIELD_NUMBER = 2;
  private com.google.protobuf.Timestamp deliveryDate_;
  /**
   * <code>.google.protobuf.Timestamp delivery_date = 2;</code>
   */
  public boolean hasDeliveryDate() {
    return deliveryDate_ != null;
  }
  /**
   * <code>.google.protobuf.Timestamp delivery_date = 2;</code>
   */
  public com.google.protobuf.Timestamp getDeliveryDate() {
    return deliveryDate_ == null ? com.google.protobuf.Timestamp.getDefaultInstance() : deliveryDate_;
  }
  /**
   * <code>.google.protobuf.Timestamp delivery_date = 2;</code>
   */
  public com.google.protobuf.TimestampOrBuilder getDeliveryDateOrBuilder() {
    return getDeliveryDate();
  }

  public static final int CREATED_BY_FIELD_NUMBER = 3;
  private int createdBy_;
  /**
   * <code>int32 created_by = 3;</code>
   */
  public int getCreatedBy() {
    return createdBy_;
  }

  private byte memoizedIsInitialized = -1;
  @java.lang.Override
  public final boolean isInitialized() {
    byte isInitialized = memoizedIsInitialized;
    if (isInitialized == 1) return true;
    if (isInitialized == 0) return false;

    memoizedIsInitialized = 1;
    return true;
  }

  @java.lang.Override
  public void writeTo(com.google.protobuf.CodedOutputStream output)
                      throws java.io.IOException {
    for (int i = 0; i < orderItems_.size(); i++) {
      output.writeMessage(1, orderItems_.get(i));
    }
    if (deliveryDate_ != null) {
      output.writeMessage(2, getDeliveryDate());
    }
    if (createdBy_ != 0) {
      output.writeInt32(3, createdBy_);
    }
    unknownFields.writeTo(output);
  }

  @java.lang.Override
  public int getSerializedSize() {
    int size = memoizedSize;
    if (size != -1) return size;

    size = 0;
    for (int i = 0; i < orderItems_.size(); i++) {
      size += com.google.protobuf.CodedOutputStream
        .computeMessageSize(1, orderItems_.get(i));
    }
    if (deliveryDate_ != null) {
      size += com.google.protobuf.CodedOutputStream
        .computeMessageSize(2, getDeliveryDate());
    }
    if (createdBy_ != 0) {
      size += com.google.protobuf.CodedOutputStream
        .computeInt32Size(3, createdBy_);
    }
    size += unknownFields.getSerializedSize();
    memoizedSize = size;
    return size;
  }

  @java.lang.Override
  public boolean equals(final java.lang.Object obj) {
    if (obj == this) {
     return true;
    }
    if (!(obj instanceof com.javainuse.orders.CreateOrder)) {
      return super.equals(obj);
    }
    com.javainuse.orders.CreateOrder other = (com.javainuse.orders.CreateOrder) obj;

    boolean result = true;
    result = result && getOrderItemsList()
        .equals(other.getOrderItemsList());
    result = result && (hasDeliveryDate() == other.hasDeliveryDate());
    if (hasDeliveryDate()) {
      result = result && getDeliveryDate()
          .equals(other.getDeliveryDate());
    }
    result = result && (getCreatedBy()
        == other.getCreatedBy());
    result = result && unknownFields.equals(other.unknownFields);
    return result;
  }

  @java.lang.Override
  public int hashCode() {
    if (memoizedHashCode != 0) {
      return memoizedHashCode;
    }
    int hash = 41;
    hash = (19 * hash) + getDescriptor().hashCode();
    if (getOrderItemsCount() > 0) {
      hash = (37 * hash) + ORDER_ITEMS_FIELD_NUMBER;
      hash = (53 * hash) + getOrderItemsList().hashCode();
    }
    if (hasDeliveryDate()) {
      hash = (37 * hash) + DELIVERY_DATE_FIELD_NUMBER;
      hash = (53 * hash) + getDeliveryDate().hashCode();
    }
    hash = (37 * hash) + CREATED_BY_FIELD_NUMBER;
    hash = (53 * hash) + getCreatedBy();
    hash = (29 * hash) + unknownFields.hashCode();
    memoizedHashCode = hash;
    return hash;
  }

  public static com.javainuse.orders.CreateOrder parseFrom(
      java.nio.ByteBuffer data)
      throws com.google.protobuf.InvalidProtocolBufferException {
    return PARSER.parseFrom(data);
  }
  public static com.javainuse.orders.CreateOrder parseFrom(
      java.nio.ByteBuffer data,
      com.google.protobuf.ExtensionRegistryLite extensionRegistry)
      throws com.google.protobuf.InvalidProtocolBufferException {
    return PARSER.parseFrom(data, extensionRegistry);
  }
  public static com.javainuse.orders.CreateOrder parseFrom(
      com.google.protobuf.ByteString data)
      throws com.google.protobuf.InvalidProtocolBufferException {
    return PARSER.parseFrom(data);
  }
  public static com.javainuse.orders.CreateOrder parseFrom(
      com.google.protobuf.ByteString data,
      com.google.protobuf.ExtensionRegistryLite extensionRegistry)
      throws com.google.protobuf.InvalidProtocolBufferException {
    return PARSER.parseFrom(data, extensionRegistry);
  }
  public static com.javainuse.orders.CreateOrder parseFrom(byte[] data)
      throws com.google.protobuf.InvalidProtocolBufferException {
    return PARSER.parseFrom(data);
  }
  public static com.javainuse.orders.CreateOrder parseFrom(
      byte[] data,
      com.google.protobuf.ExtensionRegistryLite extensionRegistry)
      throws com.google.protobuf.InvalidProtocolBufferException {
    return PARSER.parseFrom(data, extensionRegistry);
  }
  public static com.javainuse.orders.CreateOrder parseFrom(java.io.InputStream input)
      throws java.io.IOException {
    return com.google.protobuf.GeneratedMessageV3
        .parseWithIOException(PARSER, input);
  }
  public static com.javainuse.orders.CreateOrder parseFrom(
      java.io.InputStream input,
      com.google.protobuf.ExtensionRegistryLite extensionRegistry)
      throws java.io.IOException {
    return com.google.protobuf.GeneratedMessageV3
        .parseWithIOException(PARSER, input, extensionRegistry);
  }
  public static com.javainuse.orders.CreateOrder parseDelimitedFrom(java.io.InputStream input)
      throws java.io.IOException {
    return com.google.protobuf.GeneratedMessageV3
        .parseDelimitedWithIOException(PARSER, input);
  }
  public static com.javainuse.orders.CreateOrder parseDelimitedFrom(
      java.io.InputStream input,
      com.google.protobuf.ExtensionRegistryLite extensionRegistry)
      throws java.io.IOException {
    return com.google.protobuf.GeneratedMessageV3
        .parseDelimitedWithIOException(PARSER, input, extensionRegistry);
  }
  public static com.javainuse.orders.CreateOrder parseFrom(
      com.google.protobuf.CodedInputStream input)
      throws java.io.IOException {
    return com.google.protobuf.GeneratedMessageV3
        .parseWithIOException(PARSER, input);
  }
  public static com.javainuse.orders.CreateOrder parseFrom(
      com.google.protobuf.CodedInputStream input,
      com.google.protobuf.ExtensionRegistryLite extensionRegistry)
      throws java.io.IOException {
    return com.google.protobuf.GeneratedMessageV3
        .parseWithIOException(PARSER, input, extensionRegistry);
  }

  @java.lang.Override
  public Builder newBuilderForType() { return newBuilder(); }
  public static Builder newBuilder() {
    return DEFAULT_INSTANCE.toBuilder();
  }
  public static Builder newBuilder(com.javainuse.orders.CreateOrder prototype) {
    return DEFAULT_INSTANCE.toBuilder().mergeFrom(prototype);
  }
  @java.lang.Override
  public Builder toBuilder() {
    return this == DEFAULT_INSTANCE
        ? new Builder() : new Builder().mergeFrom(this);
  }

  @java.lang.Override
  protected Builder newBuilderForType(
      com.google.protobuf.GeneratedMessageV3.BuilderParent parent) {
    Builder builder = new Builder(parent);
    return builder;
  }
  /**
   * Protobuf type {@code orders.CreateOrder}
   */
  public static final class Builder extends
      com.google.protobuf.GeneratedMessageV3.Builder<Builder> implements
      // @@protoc_insertion_point(builder_implements:orders.CreateOrder)
      com.javainuse.orders.CreateOrderOrBuilder {
    public static final com.google.protobuf.Descriptors.Descriptor
        getDescriptor() {
      return com.javainuse.orders.OrderServiceOuterClass.internal_static_orders_CreateOrder_descriptor;
    }

    @java.lang.Override
    protected com.google.protobuf.GeneratedMessageV3.FieldAccessorTable
        internalGetFieldAccessorTable() {
      return com.javainuse.orders.OrderServiceOuterClass.internal_static_orders_CreateOrder_fieldAccessorTable
          .ensureFieldAccessorsInitialized(
              com.javainuse.orders.CreateOrder.class, com.javainuse.orders.CreateOrder.Builder.class);
    }

    // Construct using com.javainuse.orders.CreateOrder.newBuilder()
    private Builder() {
      maybeForceBuilderInitialization();
    }

    private Builder(
        com.google.protobuf.GeneratedMessageV3.BuilderParent parent) {
      super(parent);
      maybeForceBuilderInitialization();
    }
    private void maybeForceBuilderInitialization() {
      if (com.google.protobuf.GeneratedMessageV3
              .alwaysUseFieldBuilders) {
        getOrderItemsFieldBuilder();
      }
    }
    @java.lang.Override
    public Builder clear() {
      super.clear();
      if (orderItemsBuilder_ == null) {
        orderItems_ = java.util.Collections.emptyList();
        bitField0_ = (bitField0_ & ~0x00000001);
      } else {
        orderItemsBuilder_.clear();
      }
      if (deliveryDateBuilder_ == null) {
        deliveryDate_ = null;
      } else {
        deliveryDate_ = null;
        deliveryDateBuilder_ = null;
      }
      createdBy_ = 0;

      return this;
    }

    @java.lang.Override
    public com.google.protobuf.Descriptors.Descriptor
        getDescriptorForType() {
      return com.javainuse.orders.OrderServiceOuterClass.internal_static_orders_CreateOrder_descriptor;
    }

    @java.lang.Override
    public com.javainuse.orders.CreateOrder getDefaultInstanceForType() {
      return com.javainuse.orders.CreateOrder.getDefaultInstance();
    }

    @java.lang.Override
    public com.javainuse.orders.CreateOrder build() {
      com.javainuse.orders.CreateOrder result = buildPartial();
      if (!result.isInitialized()) {
        throw newUninitializedMessageException(result);
      }
      return result;
    }

    @java.lang.Override
    public com.javainuse.orders.CreateOrder buildPartial() {
      com.javainuse.orders.CreateOrder result = new com.javainuse.orders.CreateOrder(this);
      int from_bitField0_ = bitField0_;
      int to_bitField0_ = 0;
      if (orderItemsBuilder_ == null) {
        if (((bitField0_ & 0x00000001) == 0x00000001)) {
          orderItems_ = java.util.Collections.unmodifiableList(orderItems_);
          bitField0_ = (bitField0_ & ~0x00000001);
        }
        result.orderItems_ = orderItems_;
      } else {
        result.orderItems_ = orderItemsBuilder_.build();
      }
      if (deliveryDateBuilder_ == null) {
        result.deliveryDate_ = deliveryDate_;
      } else {
        result.deliveryDate_ = deliveryDateBuilder_.build();
      }
      result.createdBy_ = createdBy_;
      result.bitField0_ = to_bitField0_;
      onBuilt();
      return result;
    }

    @java.lang.Override
    public Builder clone() {
      return (Builder) super.clone();
    }
    @java.lang.Override
    public Builder setField(
        com.google.protobuf.Descriptors.FieldDescriptor field,
        java.lang.Object value) {
      return (Builder) super.setField(field, value);
    }
    @java.lang.Override
    public Builder clearField(
        com.google.protobuf.Descriptors.FieldDescriptor field) {
      return (Builder) super.clearField(field);
    }
    @java.lang.Override
    public Builder clearOneof(
        com.google.protobuf.Descriptors.OneofDescriptor oneof) {
      return (Builder) super.clearOneof(oneof);
    }
    @java.lang.Override
    public Builder setRepeatedField(
        com.google.protobuf.Descriptors.FieldDescriptor field,
        int index, java.lang.Object value) {
      return (Builder) super.setRepeatedField(field, index, value);
    }
    @java.lang.Override
    public Builder addRepeatedField(
        com.google.protobuf.Descriptors.FieldDescriptor field,
        java.lang.Object value) {
      return (Builder) super.addRepeatedField(field, value);
    }
    @java.lang.Override
    public Builder mergeFrom(com.google.protobuf.Message other) {
      if (other instanceof com.javainuse.orders.CreateOrder) {
        return mergeFrom((com.javainuse.orders.CreateOrder)other);
      } else {
        super.mergeFrom(other);
        return this;
      }
    }

    public Builder mergeFrom(com.javainuse.orders.CreateOrder other) {
      if (other == com.javainuse.orders.CreateOrder.getDefaultInstance()) return this;
      if (orderItemsBuilder_ == null) {
        if (!other.orderItems_.isEmpty()) {
          if (orderItems_.isEmpty()) {
            orderItems_ = other.orderItems_;
            bitField0_ = (bitField0_ & ~0x00000001);
          } else {
            ensureOrderItemsIsMutable();
            orderItems_.addAll(other.orderItems_);
          }
          onChanged();
        }
      } else {
        if (!other.orderItems_.isEmpty()) {
          if (orderItemsBuilder_.isEmpty()) {
            orderItemsBuilder_.dispose();
            orderItemsBuilder_ = null;
            orderItems_ = other.orderItems_;
            bitField0_ = (bitField0_ & ~0x00000001);
            orderItemsBuilder_ = 
              com.google.protobuf.GeneratedMessageV3.alwaysUseFieldBuilders ?
                 getOrderItemsFieldBuilder() : null;
          } else {
            orderItemsBuilder_.addAllMessages(other.orderItems_);
          }
        }
      }
      if (other.hasDeliveryDate()) {
        mergeDeliveryDate(other.getDeliveryDate());
      }
      if (other.getCreatedBy() != 0) {
        setCreatedBy(other.getCreatedBy());
      }
      this.mergeUnknownFields(other.unknownFields);
      onChanged();
      return this;
    }

    @java.lang.Override
    public final boolean isInitialized() {
      return true;
    }

    @java.lang.Override
    public Builder mergeFrom(
        com.google.protobuf.CodedInputStream input,
        com.google.protobuf.ExtensionRegistryLite extensionRegistry)
        throws java.io.IOException {
      com.javainuse.orders.CreateOrder parsedMessage = null;
      try {
        parsedMessage = PARSER.parsePartialFrom(input, extensionRegistry);
      } catch (com.google.protobuf.InvalidProtocolBufferException e) {
        parsedMessage = (com.javainuse.orders.CreateOrder) e.getUnfinishedMessage();
        throw e.unwrapIOException();
      } finally {
        if (parsedMessage != null) {
          mergeFrom(parsedMessage);
        }
      }
      return this;
    }
    private int bitField0_;

    private java.util.List<com.javainuse.orders.CreateOrderItem> orderItems_ =
      java.util.Collections.emptyList();
    private void ensureOrderItemsIsMutable() {
      if (!((bitField0_ & 0x00000001) == 0x00000001)) {
        orderItems_ = new java.util.ArrayList<com.javainuse.orders.CreateOrderItem>(orderItems_);
        bitField0_ |= 0x00000001;
       }
    }

    private com.google.protobuf.RepeatedFieldBuilderV3<
        com.javainuse.orders.CreateOrderItem, com.javainuse.orders.CreateOrderItem.Builder, com.javainuse.orders.CreateOrderItemOrBuilder> orderItemsBuilder_;

    /**
     * <pre>
     * For creating, we only send item_ids
     * </pre>
     *
     * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
     */
    public java.util.List<com.javainuse.orders.CreateOrderItem> getOrderItemsList() {
      if (orderItemsBuilder_ == null) {
        return java.util.Collections.unmodifiableList(orderItems_);
      } else {
        return orderItemsBuilder_.getMessageList();
      }
    }
    /**
     * <pre>
     * For creating, we only send item_ids
     * </pre>
     *
     * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
     */
    public int getOrderItemsCount() {
      if (orderItemsBuilder_ == null) {
        return orderItems_.size();
      } else {
        return orderItemsBuilder_.getCount();
      }
    }
    /**
     * <pre>
     * For creating, we only send item_ids
     * </pre>
     *
     * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
     */
    public com.javainuse.orders.CreateOrderItem getOrderItems(int index) {
      if (orderItemsBuilder_ == null) {
        return orderItems_.get(index);
      } else {
        return orderItemsBuilder_.getMessage(index);
      }
    }
    /**
     * <pre>
     * For creating, we only send item_ids
     * </pre>
     *
     * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
     */
    public Builder setOrderItems(
        int index, com.javainuse.orders.CreateOrderItem value) {
      if (orderItemsBuilder_ == null) {
        if (value == null) {
          throw new NullPointerException();
        }
        ensureOrderItemsIsMutable();
        orderItems_.set(index, value);
        onChanged();
      } else {
        orderItemsBuilder_.setMessage(index, value);
      }
      return this;
    }
    /**
     * <pre>
     * For creating, we only send item_ids
     * </pre>
     *
     * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
     */
    public Builder setOrderItems(
        int index, com.javainuse.orders.CreateOrderItem.Builder builderForValue) {
      if (orderItemsBuilder_ == null) {
        ensureOrderItemsIsMutable();
        orderItems_.set(index, builderForValue.build());
        onChanged();
      } else {
        orderItemsBuilder_.setMessage(index, builderForValue.build());
      }
      return this;
    }
    /**
     * <pre>
     * For creating, we only send item_ids
     * </pre>
     *
     * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
     */
    public Builder addOrderItems(com.javainuse.orders.CreateOrderItem value) {
      if (orderItemsBuilder_ == null) {
        if (value == null) {
          throw new NullPointerException();
        }
        ensureOrderItemsIsMutable();
        orderItems_.add(value);
        onChanged();
      } else {
        orderItemsBuilder_.addMessage(value);
      }
      return this;
    }
    /**
     * <pre>
     * For creating, we only send item_ids
     * </pre>
     *
     * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
     */
    public Builder addOrderItems(
        int index, com.javainuse.orders.CreateOrderItem value) {
      if (orderItemsBuilder_ == null) {
        if (value == null) {
          throw new NullPointerException();
        }
        ensureOrderItemsIsMutable();
        orderItems_.add(index, value);
        onChanged();
      } else {
        orderItemsBuilder_.addMessage(index, value);
      }
      return this;
    }
    /**
     * <pre>
     * For creating, we only send item_ids
     * </pre>
     *
     * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
     */
    public Builder addOrderItems(
        com.javainuse.orders.CreateOrderItem.Builder builderForValue) {
      if (orderItemsBuilder_ == null) {
        ensureOrderItemsIsMutable();
        orderItems_.add(builderForValue.build());
        onChanged();
      } else {
        orderItemsBuilder_.addMessage(builderForValue.build());
      }
      return this;
    }
    /**
     * <pre>
     * For creating, we only send item_ids
     * </pre>
     *
     * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
     */
    public Builder addOrderItems(
        int index, com.javainuse.orders.CreateOrderItem.Builder builderForValue) {
      if (orderItemsBuilder_ == null) {
        ensureOrderItemsIsMutable();
        orderItems_.add(index, builderForValue.build());
        onChanged();
      } else {
        orderItemsBuilder_.addMessage(index, builderForValue.build());
      }
      return this;
    }
    /**
     * <pre>
     * For creating, we only send item_ids
     * </pre>
     *
     * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
     */
    public Builder addAllOrderItems(
        java.lang.Iterable<? extends com.javainuse.orders.CreateOrderItem> values) {
      if (orderItemsBuilder_ == null) {
        ensureOrderItemsIsMutable();
        com.google.protobuf.AbstractMessageLite.Builder.addAll(
            values, orderItems_);
        onChanged();
      } else {
        orderItemsBuilder_.addAllMessages(values);
      }
      return this;
    }
    /**
     * <pre>
     * For creating, we only send item_ids
     * </pre>
     *
     * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
     */
    public Builder clearOrderItems() {
      if (orderItemsBuilder_ == null) {
        orderItems_ = java.util.Collections.emptyList();
        bitField0_ = (bitField0_ & ~0x00000001);
        onChanged();
      } else {
        orderItemsBuilder_.clear();
      }
      return this;
    }
    /**
     * <pre>
     * For creating, we only send item_ids
     * </pre>
     *
     * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
     */
    public Builder removeOrderItems(int index) {
      if (orderItemsBuilder_ == null) {
        ensureOrderItemsIsMutable();
        orderItems_.remove(index);
        onChanged();
      } else {
        orderItemsBuilder_.remove(index);
      }
      return this;
    }
    /**
     * <pre>
     * For creating, we only send item_ids
     * </pre>
     *
     * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
     */
    public com.javainuse.orders.CreateOrderItem.Builder getOrderItemsBuilder(
        int index) {
      return getOrderItemsFieldBuilder().getBuilder(index);
    }
    /**
     * <pre>
     * For creating, we only send item_ids
     * </pre>
     *
     * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
     */
    public com.javainuse.orders.CreateOrderItemOrBuilder getOrderItemsOrBuilder(
        int index) {
      if (orderItemsBuilder_ == null) {
        return orderItems_.get(index);  } else {
        return orderItemsBuilder_.getMessageOrBuilder(index);
      }
    }
    /**
     * <pre>
     * For creating, we only send item_ids
     * </pre>
     *
     * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
     */
    public java.util.List<? extends com.javainuse.orders.CreateOrderItemOrBuilder> 
         getOrderItemsOrBuilderList() {
      if (orderItemsBuilder_ != null) {
        return orderItemsBuilder_.getMessageOrBuilderList();
      } else {
        return java.util.Collections.unmodifiableList(orderItems_);
      }
    }
    /**
     * <pre>
     * For creating, we only send item_ids
     * </pre>
     *
     * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
     */
    public com.javainuse.orders.CreateOrderItem.Builder addOrderItemsBuilder() {
      return getOrderItemsFieldBuilder().addBuilder(
          com.javainuse.orders.CreateOrderItem.getDefaultInstance());
    }
    /**
     * <pre>
     * For creating, we only send item_ids
     * </pre>
     *
     * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
     */
    public com.javainuse.orders.CreateOrderItem.Builder addOrderItemsBuilder(
        int index) {
      return getOrderItemsFieldBuilder().addBuilder(
          index, com.javainuse.orders.CreateOrderItem.getDefaultInstance());
    }
    /**
     * <pre>
     * For creating, we only send item_ids
     * </pre>
     *
     * <code>repeated .orders.CreateOrderItem order_items = 1;</code>
     */
    public java.util.List<com.javainuse.orders.CreateOrderItem.Builder> 
         getOrderItemsBuilderList() {
      return getOrderItemsFieldBuilder().getBuilderList();
    }
    private com.google.protobuf.RepeatedFieldBuilderV3<
        com.javainuse.orders.CreateOrderItem, com.javainuse.orders.CreateOrderItem.Builder, com.javainuse.orders.CreateOrderItemOrBuilder> 
        getOrderItemsFieldBuilder() {
      if (orderItemsBuilder_ == null) {
        orderItemsBuilder_ = new com.google.protobuf.RepeatedFieldBuilderV3<
            com.javainuse.orders.CreateOrderItem, com.javainuse.orders.CreateOrderItem.Builder, com.javainuse.orders.CreateOrderItemOrBuilder>(
                orderItems_,
                ((bitField0_ & 0x00000001) == 0x00000001),
                getParentForChildren(),
                isClean());
        orderItems_ = null;
      }
      return orderItemsBuilder_;
    }

    private com.google.protobuf.Timestamp deliveryDate_ = null;
    private com.google.protobuf.SingleFieldBuilderV3<
        com.google.protobuf.Timestamp, com.google.protobuf.Timestamp.Builder, com.google.protobuf.TimestampOrBuilder> deliveryDateBuilder_;
    /**
     * <code>.google.protobuf.Timestamp delivery_date = 2;</code>
     */
    public boolean hasDeliveryDate() {
      return deliveryDateBuilder_ != null || deliveryDate_ != null;
    }
    /**
     * <code>.google.protobuf.Timestamp delivery_date = 2;</code>
     */
    public com.google.protobuf.Timestamp getDeliveryDate() {
      if (deliveryDateBuilder_ == null) {
        return deliveryDate_ == null ? com.google.protobuf.Timestamp.getDefaultInstance() : deliveryDate_;
      } else {
        return deliveryDateBuilder_.getMessage();
      }
    }
    /**
     * <code>.google.protobuf.Timestamp delivery_date = 2;</code>
     */
    public Builder setDeliveryDate(com.google.protobuf.Timestamp value) {
      if (deliveryDateBuilder_ == null) {
        if (value == null) {
          throw new NullPointerException();
        }
        deliveryDate_ = value;
        onChanged();
      } else {
        deliveryDateBuilder_.setMessage(value);
      }

      return this;
    }
    /**
     * <code>.google.protobuf.Timestamp delivery_date = 2;</code>
     */
    public Builder setDeliveryDate(
        com.google.protobuf.Timestamp.Builder builderForValue) {
      if (deliveryDateBuilder_ == null) {
        deliveryDate_ = builderForValue.build();
        onChanged();
      } else {
        deliveryDateBuilder_.setMessage(builderForValue.build());
      }

      return this;
    }
    /**
     * <code>.google.protobuf.Timestamp delivery_date = 2;</code>
     */
    public Builder mergeDeliveryDate(com.google.protobuf.Timestamp value) {
      if (deliveryDateBuilder_ == null) {
        if (deliveryDate_ != null) {
          deliveryDate_ =
            com.google.protobuf.Timestamp.newBuilder(deliveryDate_).mergeFrom(value).buildPartial();
        } else {
          deliveryDate_ = value;
        }
        onChanged();
      } else {
        deliveryDateBuilder_.mergeFrom(value);
      }

      return this;
    }
    /**
     * <code>.google.protobuf.Timestamp delivery_date = 2;</code>
     */
    public Builder clearDeliveryDate() {
      if (deliveryDateBuilder_ == null) {
        deliveryDate_ = null;
        onChanged();
      } else {
        deliveryDate_ = null;
        deliveryDateBuilder_ = null;
      }

      return this;
    }
    /**
     * <code>.google.protobuf.Timestamp delivery_date = 2;</code>
     */
    public com.google.protobuf.Timestamp.Builder getDeliveryDateBuilder() {
      
      onChanged();
      return getDeliveryDateFieldBuilder().getBuilder();
    }
    /**
     * <code>.google.protobuf.Timestamp delivery_date = 2;</code>
     */
    public com.google.protobuf.TimestampOrBuilder getDeliveryDateOrBuilder() {
      if (deliveryDateBuilder_ != null) {
        return deliveryDateBuilder_.getMessageOrBuilder();
      } else {
        return deliveryDate_ == null ?
            com.google.protobuf.Timestamp.getDefaultInstance() : deliveryDate_;
      }
    }
    /**
     * <code>.google.protobuf.Timestamp delivery_date = 2;</code>
     */
    private com.google.protobuf.SingleFieldBuilderV3<
        com.google.protobuf.Timestamp, com.google.protobuf.Timestamp.Builder, com.google.protobuf.TimestampOrBuilder> 
        getDeliveryDateFieldBuilder() {
      if (deliveryDateBuilder_ == null) {
        deliveryDateBuilder_ = new com.google.protobuf.SingleFieldBuilderV3<
            com.google.protobuf.Timestamp, com.google.protobuf.Timestamp.Builder, com.google.protobuf.TimestampOrBuilder>(
                getDeliveryDate(),
                getParentForChildren(),
                isClean());
        deliveryDate_ = null;
      }
      return deliveryDateBuilder_;
    }

    private int createdBy_ ;
    /**
     * <code>int32 created_by = 3;</code>
     */
    public int getCreatedBy() {
      return createdBy_;
    }
    /**
     * <code>int32 created_by = 3;</code>
     */
    public Builder setCreatedBy(int value) {
      
      createdBy_ = value;
      onChanged();
      return this;
    }
    /**
     * <code>int32 created_by = 3;</code>
     */
    public Builder clearCreatedBy() {
      
      createdBy_ = 0;
      onChanged();
      return this;
    }
    @java.lang.Override
    public final Builder setUnknownFields(
        final com.google.protobuf.UnknownFieldSet unknownFields) {
      return super.setUnknownFieldsProto3(unknownFields);
    }

    @java.lang.Override
    public final Builder mergeUnknownFields(
        final com.google.protobuf.UnknownFieldSet unknownFields) {
      return super.mergeUnknownFields(unknownFields);
    }


    // @@protoc_insertion_point(builder_scope:orders.CreateOrder)
  }

  // @@protoc_insertion_point(class_scope:orders.CreateOrder)
  private static final com.javainuse.orders.CreateOrder DEFAULT_INSTANCE;
  static {
    DEFAULT_INSTANCE = new com.javainuse.orders.CreateOrder();
  }

  public static com.javainuse.orders.CreateOrder getDefaultInstance() {
    return DEFAULT_INSTANCE;
  }

  private static final com.google.protobuf.Parser<CreateOrder>
      PARSER = new com.google.protobuf.AbstractParser<CreateOrder>() {
    @java.lang.Override
    public CreateOrder parsePartialFrom(
        com.google.protobuf.CodedInputStream input,
        com.google.protobuf.ExtensionRegistryLite extensionRegistry)
        throws com.google.protobuf.InvalidProtocolBufferException {
      return new CreateOrder(input, extensionRegistry);
    }
  };

  public static com.google.protobuf.Parser<CreateOrder> parser() {
    return PARSER;
  }

  @java.lang.Override
  public com.google.protobuf.Parser<CreateOrder> getParserForType() {
    return PARSER;
  }

  @java.lang.Override
  public com.javainuse.orders.CreateOrder getDefaultInstanceForType() {
    return DEFAULT_INSTANCE;
  }

}

