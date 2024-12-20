// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: item-service.proto

package com.javainuse.item;

/**
 * Protobuf type {@code items.Item}
 */
public  final class Item extends
    com.google.protobuf.GeneratedMessageV3 implements
    // @@protoc_insertion_point(message_implements:items.Item)
    ItemOrBuilder {
private static final long serialVersionUID = 0L;
  // Use Item.newBuilder() to construct.
  private Item(com.google.protobuf.GeneratedMessageV3.Builder<?> builder) {
    super(builder);
  }
  private Item() {
    itemId_ = 0;
    itemName_ = "";
    description_ = "";
    quantityInStore_ = 0;
  }

  @java.lang.Override
  public final com.google.protobuf.UnknownFieldSet
  getUnknownFields() {
    return this.unknownFields;
  }
  private Item(
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
          case 8: {

            itemId_ = input.readInt32();
            break;
          }
          case 18: {
            java.lang.String s = input.readStringRequireUtf8();

            itemName_ = s;
            break;
          }
          case 26: {
            java.lang.String s = input.readStringRequireUtf8();

            description_ = s;
            break;
          }
          case 32: {

            quantityInStore_ = input.readInt32();
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
      this.unknownFields = unknownFields.build();
      makeExtensionsImmutable();
    }
  }
  public static final com.google.protobuf.Descriptors.Descriptor
      getDescriptor() {
    return com.javainuse.item.ItemServiceOuterClass.internal_static_items_Item_descriptor;
  }

  @java.lang.Override
  protected com.google.protobuf.GeneratedMessageV3.FieldAccessorTable
      internalGetFieldAccessorTable() {
    return com.javainuse.item.ItemServiceOuterClass.internal_static_items_Item_fieldAccessorTable
        .ensureFieldAccessorsInitialized(
            com.javainuse.item.Item.class, com.javainuse.item.Item.Builder.class);
  }

  public static final int ITEMID_FIELD_NUMBER = 1;
  private int itemId_;
  /**
   * <code>int32 itemId = 1;</code>
   */
  public int getItemId() {
    return itemId_;
  }

  public static final int ITEMNAME_FIELD_NUMBER = 2;
  private volatile java.lang.Object itemName_;
  /**
   * <code>string itemName = 2;</code>
   */
  public java.lang.String getItemName() {
    java.lang.Object ref = itemName_;
    if (ref instanceof java.lang.String) {
      return (java.lang.String) ref;
    } else {
      com.google.protobuf.ByteString bs = 
          (com.google.protobuf.ByteString) ref;
      java.lang.String s = bs.toStringUtf8();
      itemName_ = s;
      return s;
    }
  }
  /**
   * <code>string itemName = 2;</code>
   */
  public com.google.protobuf.ByteString
      getItemNameBytes() {
    java.lang.Object ref = itemName_;
    if (ref instanceof java.lang.String) {
      com.google.protobuf.ByteString b = 
          com.google.protobuf.ByteString.copyFromUtf8(
              (java.lang.String) ref);
      itemName_ = b;
      return b;
    } else {
      return (com.google.protobuf.ByteString) ref;
    }
  }

  public static final int DESCRIPTION_FIELD_NUMBER = 3;
  private volatile java.lang.Object description_;
  /**
   * <code>string description = 3;</code>
   */
  public java.lang.String getDescription() {
    java.lang.Object ref = description_;
    if (ref instanceof java.lang.String) {
      return (java.lang.String) ref;
    } else {
      com.google.protobuf.ByteString bs = 
          (com.google.protobuf.ByteString) ref;
      java.lang.String s = bs.toStringUtf8();
      description_ = s;
      return s;
    }
  }
  /**
   * <code>string description = 3;</code>
   */
  public com.google.protobuf.ByteString
      getDescriptionBytes() {
    java.lang.Object ref = description_;
    if (ref instanceof java.lang.String) {
      com.google.protobuf.ByteString b = 
          com.google.protobuf.ByteString.copyFromUtf8(
              (java.lang.String) ref);
      description_ = b;
      return b;
    } else {
      return (com.google.protobuf.ByteString) ref;
    }
  }

  public static final int QUANTITY_IN_STORE_FIELD_NUMBER = 4;
  private int quantityInStore_;
  /**
   * <code>int32 quantity_in_store = 4;</code>
   */
  public int getQuantityInStore() {
    return quantityInStore_;
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
    if (itemId_ != 0) {
      output.writeInt32(1, itemId_);
    }
    if (!getItemNameBytes().isEmpty()) {
      com.google.protobuf.GeneratedMessageV3.writeString(output, 2, itemName_);
    }
    if (!getDescriptionBytes().isEmpty()) {
      com.google.protobuf.GeneratedMessageV3.writeString(output, 3, description_);
    }
    if (quantityInStore_ != 0) {
      output.writeInt32(4, quantityInStore_);
    }
    unknownFields.writeTo(output);
  }

  @java.lang.Override
  public int getSerializedSize() {
    int size = memoizedSize;
    if (size != -1) return size;

    size = 0;
    if (itemId_ != 0) {
      size += com.google.protobuf.CodedOutputStream
        .computeInt32Size(1, itemId_);
    }
    if (!getItemNameBytes().isEmpty()) {
      size += com.google.protobuf.GeneratedMessageV3.computeStringSize(2, itemName_);
    }
    if (!getDescriptionBytes().isEmpty()) {
      size += com.google.protobuf.GeneratedMessageV3.computeStringSize(3, description_);
    }
    if (quantityInStore_ != 0) {
      size += com.google.protobuf.CodedOutputStream
        .computeInt32Size(4, quantityInStore_);
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
    if (!(obj instanceof com.javainuse.item.Item)) {
      return super.equals(obj);
    }
    com.javainuse.item.Item other = (com.javainuse.item.Item) obj;

    boolean result = true;
    result = result && (getItemId()
        == other.getItemId());
    result = result && getItemName()
        .equals(other.getItemName());
    result = result && getDescription()
        .equals(other.getDescription());
    result = result && (getQuantityInStore()
        == other.getQuantityInStore());
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
    hash = (37 * hash) + ITEMID_FIELD_NUMBER;
    hash = (53 * hash) + getItemId();
    hash = (37 * hash) + ITEMNAME_FIELD_NUMBER;
    hash = (53 * hash) + getItemName().hashCode();
    hash = (37 * hash) + DESCRIPTION_FIELD_NUMBER;
    hash = (53 * hash) + getDescription().hashCode();
    hash = (37 * hash) + QUANTITY_IN_STORE_FIELD_NUMBER;
    hash = (53 * hash) + getQuantityInStore();
    hash = (29 * hash) + unknownFields.hashCode();
    memoizedHashCode = hash;
    return hash;
  }

  public static com.javainuse.item.Item parseFrom(
      java.nio.ByteBuffer data)
      throws com.google.protobuf.InvalidProtocolBufferException {
    return PARSER.parseFrom(data);
  }
  public static com.javainuse.item.Item parseFrom(
      java.nio.ByteBuffer data,
      com.google.protobuf.ExtensionRegistryLite extensionRegistry)
      throws com.google.protobuf.InvalidProtocolBufferException {
    return PARSER.parseFrom(data, extensionRegistry);
  }
  public static com.javainuse.item.Item parseFrom(
      com.google.protobuf.ByteString data)
      throws com.google.protobuf.InvalidProtocolBufferException {
    return PARSER.parseFrom(data);
  }
  public static com.javainuse.item.Item parseFrom(
      com.google.protobuf.ByteString data,
      com.google.protobuf.ExtensionRegistryLite extensionRegistry)
      throws com.google.protobuf.InvalidProtocolBufferException {
    return PARSER.parseFrom(data, extensionRegistry);
  }
  public static com.javainuse.item.Item parseFrom(byte[] data)
      throws com.google.protobuf.InvalidProtocolBufferException {
    return PARSER.parseFrom(data);
  }
  public static com.javainuse.item.Item parseFrom(
      byte[] data,
      com.google.protobuf.ExtensionRegistryLite extensionRegistry)
      throws com.google.protobuf.InvalidProtocolBufferException {
    return PARSER.parseFrom(data, extensionRegistry);
  }
  public static com.javainuse.item.Item parseFrom(java.io.InputStream input)
      throws java.io.IOException {
    return com.google.protobuf.GeneratedMessageV3
        .parseWithIOException(PARSER, input);
  }
  public static com.javainuse.item.Item parseFrom(
      java.io.InputStream input,
      com.google.protobuf.ExtensionRegistryLite extensionRegistry)
      throws java.io.IOException {
    return com.google.protobuf.GeneratedMessageV3
        .parseWithIOException(PARSER, input, extensionRegistry);
  }
  public static com.javainuse.item.Item parseDelimitedFrom(java.io.InputStream input)
      throws java.io.IOException {
    return com.google.protobuf.GeneratedMessageV3
        .parseDelimitedWithIOException(PARSER, input);
  }
  public static com.javainuse.item.Item parseDelimitedFrom(
      java.io.InputStream input,
      com.google.protobuf.ExtensionRegistryLite extensionRegistry)
      throws java.io.IOException {
    return com.google.protobuf.GeneratedMessageV3
        .parseDelimitedWithIOException(PARSER, input, extensionRegistry);
  }
  public static com.javainuse.item.Item parseFrom(
      com.google.protobuf.CodedInputStream input)
      throws java.io.IOException {
    return com.google.protobuf.GeneratedMessageV3
        .parseWithIOException(PARSER, input);
  }
  public static com.javainuse.item.Item parseFrom(
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
  public static Builder newBuilder(com.javainuse.item.Item prototype) {
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
   * Protobuf type {@code items.Item}
   */
  public static final class Builder extends
      com.google.protobuf.GeneratedMessageV3.Builder<Builder> implements
      // @@protoc_insertion_point(builder_implements:items.Item)
      com.javainuse.item.ItemOrBuilder {
    public static final com.google.protobuf.Descriptors.Descriptor
        getDescriptor() {
      return com.javainuse.item.ItemServiceOuterClass.internal_static_items_Item_descriptor;
    }

    @java.lang.Override
    protected com.google.protobuf.GeneratedMessageV3.FieldAccessorTable
        internalGetFieldAccessorTable() {
      return com.javainuse.item.ItemServiceOuterClass.internal_static_items_Item_fieldAccessorTable
          .ensureFieldAccessorsInitialized(
              com.javainuse.item.Item.class, com.javainuse.item.Item.Builder.class);
    }

    // Construct using com.javainuse.item.Item.newBuilder()
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
      }
    }
    @java.lang.Override
    public Builder clear() {
      super.clear();
      itemId_ = 0;

      itemName_ = "";

      description_ = "";

      quantityInStore_ = 0;

      return this;
    }

    @java.lang.Override
    public com.google.protobuf.Descriptors.Descriptor
        getDescriptorForType() {
      return com.javainuse.item.ItemServiceOuterClass.internal_static_items_Item_descriptor;
    }

    @java.lang.Override
    public com.javainuse.item.Item getDefaultInstanceForType() {
      return com.javainuse.item.Item.getDefaultInstance();
    }

    @java.lang.Override
    public com.javainuse.item.Item build() {
      com.javainuse.item.Item result = buildPartial();
      if (!result.isInitialized()) {
        throw newUninitializedMessageException(result);
      }
      return result;
    }

    @java.lang.Override
    public com.javainuse.item.Item buildPartial() {
      com.javainuse.item.Item result = new com.javainuse.item.Item(this);
      result.itemId_ = itemId_;
      result.itemName_ = itemName_;
      result.description_ = description_;
      result.quantityInStore_ = quantityInStore_;
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
      if (other instanceof com.javainuse.item.Item) {
        return mergeFrom((com.javainuse.item.Item)other);
      } else {
        super.mergeFrom(other);
        return this;
      }
    }

    public Builder mergeFrom(com.javainuse.item.Item other) {
      if (other == com.javainuse.item.Item.getDefaultInstance()) return this;
      if (other.getItemId() != 0) {
        setItemId(other.getItemId());
      }
      if (!other.getItemName().isEmpty()) {
        itemName_ = other.itemName_;
        onChanged();
      }
      if (!other.getDescription().isEmpty()) {
        description_ = other.description_;
        onChanged();
      }
      if (other.getQuantityInStore() != 0) {
        setQuantityInStore(other.getQuantityInStore());
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
      com.javainuse.item.Item parsedMessage = null;
      try {
        parsedMessage = PARSER.parsePartialFrom(input, extensionRegistry);
      } catch (com.google.protobuf.InvalidProtocolBufferException e) {
        parsedMessage = (com.javainuse.item.Item) e.getUnfinishedMessage();
        throw e.unwrapIOException();
      } finally {
        if (parsedMessage != null) {
          mergeFrom(parsedMessage);
        }
      }
      return this;
    }

    private int itemId_ ;
    /**
     * <code>int32 itemId = 1;</code>
     */
    public int getItemId() {
      return itemId_;
    }
    /**
     * <code>int32 itemId = 1;</code>
     */
    public Builder setItemId(int value) {
      
      itemId_ = value;
      onChanged();
      return this;
    }
    /**
     * <code>int32 itemId = 1;</code>
     */
    public Builder clearItemId() {
      
      itemId_ = 0;
      onChanged();
      return this;
    }

    private java.lang.Object itemName_ = "";
    /**
     * <code>string itemName = 2;</code>
     */
    public java.lang.String getItemName() {
      java.lang.Object ref = itemName_;
      if (!(ref instanceof java.lang.String)) {
        com.google.protobuf.ByteString bs =
            (com.google.protobuf.ByteString) ref;
        java.lang.String s = bs.toStringUtf8();
        itemName_ = s;
        return s;
      } else {
        return (java.lang.String) ref;
      }
    }
    /**
     * <code>string itemName = 2;</code>
     */
    public com.google.protobuf.ByteString
        getItemNameBytes() {
      java.lang.Object ref = itemName_;
      if (ref instanceof String) {
        com.google.protobuf.ByteString b = 
            com.google.protobuf.ByteString.copyFromUtf8(
                (java.lang.String) ref);
        itemName_ = b;
        return b;
      } else {
        return (com.google.protobuf.ByteString) ref;
      }
    }
    /**
     * <code>string itemName = 2;</code>
     */
    public Builder setItemName(
        java.lang.String value) {
      if (value == null) {
    throw new NullPointerException();
  }
  
      itemName_ = value;
      onChanged();
      return this;
    }
    /**
     * <code>string itemName = 2;</code>
     */
    public Builder clearItemName() {
      
      itemName_ = getDefaultInstance().getItemName();
      onChanged();
      return this;
    }
    /**
     * <code>string itemName = 2;</code>
     */
    public Builder setItemNameBytes(
        com.google.protobuf.ByteString value) {
      if (value == null) {
    throw new NullPointerException();
  }
  checkByteStringIsUtf8(value);
      
      itemName_ = value;
      onChanged();
      return this;
    }

    private java.lang.Object description_ = "";
    /**
     * <code>string description = 3;</code>
     */
    public java.lang.String getDescription() {
      java.lang.Object ref = description_;
      if (!(ref instanceof java.lang.String)) {
        com.google.protobuf.ByteString bs =
            (com.google.protobuf.ByteString) ref;
        java.lang.String s = bs.toStringUtf8();
        description_ = s;
        return s;
      } else {
        return (java.lang.String) ref;
      }
    }
    /**
     * <code>string description = 3;</code>
     */
    public com.google.protobuf.ByteString
        getDescriptionBytes() {
      java.lang.Object ref = description_;
      if (ref instanceof String) {
        com.google.protobuf.ByteString b = 
            com.google.protobuf.ByteString.copyFromUtf8(
                (java.lang.String) ref);
        description_ = b;
        return b;
      } else {
        return (com.google.protobuf.ByteString) ref;
      }
    }
    /**
     * <code>string description = 3;</code>
     */
    public Builder setDescription(
        java.lang.String value) {
      if (value == null) {
    throw new NullPointerException();
  }
  
      description_ = value;
      onChanged();
      return this;
    }
    /**
     * <code>string description = 3;</code>
     */
    public Builder clearDescription() {
      
      description_ = getDefaultInstance().getDescription();
      onChanged();
      return this;
    }
    /**
     * <code>string description = 3;</code>
     */
    public Builder setDescriptionBytes(
        com.google.protobuf.ByteString value) {
      if (value == null) {
    throw new NullPointerException();
  }
  checkByteStringIsUtf8(value);
      
      description_ = value;
      onChanged();
      return this;
    }

    private int quantityInStore_ ;
    /**
     * <code>int32 quantity_in_store = 4;</code>
     */
    public int getQuantityInStore() {
      return quantityInStore_;
    }
    /**
     * <code>int32 quantity_in_store = 4;</code>
     */
    public Builder setQuantityInStore(int value) {
      
      quantityInStore_ = value;
      onChanged();
      return this;
    }
    /**
     * <code>int32 quantity_in_store = 4;</code>
     */
    public Builder clearQuantityInStore() {
      
      quantityInStore_ = 0;
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


    // @@protoc_insertion_point(builder_scope:items.Item)
  }

  // @@protoc_insertion_point(class_scope:items.Item)
  private static final com.javainuse.item.Item DEFAULT_INSTANCE;
  static {
    DEFAULT_INSTANCE = new com.javainuse.item.Item();
  }

  public static com.javainuse.item.Item getDefaultInstance() {
    return DEFAULT_INSTANCE;
  }

  private static final com.google.protobuf.Parser<Item>
      PARSER = new com.google.protobuf.AbstractParser<Item>() {
    @java.lang.Override
    public Item parsePartialFrom(
        com.google.protobuf.CodedInputStream input,
        com.google.protobuf.ExtensionRegistryLite extensionRegistry)
        throws com.google.protobuf.InvalidProtocolBufferException {
      return new Item(input, extensionRegistry);
    }
  };

  public static com.google.protobuf.Parser<Item> parser() {
    return PARSER;
  }

  @java.lang.Override
  public com.google.protobuf.Parser<Item> getParserForType() {
    return PARSER;
  }

  @java.lang.Override
  public com.javainuse.item.Item getDefaultInstanceForType() {
    return DEFAULT_INSTANCE;
  }

}

