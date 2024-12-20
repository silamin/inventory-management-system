# System Architecture for SEP3 Project - Group 4

## Overview

This document provides an overview of the system architecture for the SEP3 project, which is an **Inventory Management System**. 
The architecture follows a **three-tier structure** with communication channels and components designed to support effective interaction and separation of concerns.

## Architecture Description

### 1. Presentation Tier
- **Technology:** Blazor frontend using C#
- **Description:** This is the layer users interact with directly. It consists of a user interface that allows users to input and view data and serves as a gRPC client for communication with the backend.
- **Communication Protocols:** gRPC for sending requests and receiving responses from the Application Tier.

### 2. Application Tier
- **Technology:** Java backend using Spring Boot
- **Description:** The Application Tier serves as the business logic layer. It processes requests from the Presentation Tier and communicates with the Data Access Layer using REST. It is responsible for ensuring data integrity and business rule enforcement.
- **Communication Protocols:** 
  - gRPC for communicating with the Presentation Tier.
  - REST for interacting with the Data Tier.

### 3. Data Tier
- **Technology:** .NET-based Data Access Layer using Entity Framework and a PostgreSQL database
- **Description:** This layer handles data persistence and retrieval. It interacts with the PostgreSQL database using Entity Framework for database management and CRUD operations.
- **Communication Protocols:** REST API interactions with the Application Tier.

### Communication Channels
- **gRPC (Presentation ⇔ Application Tier)**: Enables efficient and fast communication for client requests and responses between the Blazor frontend and the Java backend.
- **REST (Application ⇔ Data Tier)**: Used for data exchange and interactions between the Application Tier and the Data Access Layer.
- **Entity Framework (Database Interaction)**: Manages data queries and persistence in the PostgreSQL database.
- **RabbitMQ (Asynchronous Communication)**: Utilized as a message broker to enable asynchronous messaging between different application layers.
- **SignalR (Real-Time Communication)**: Provides real-time updates and notifications across layers, enhancing user experience.

EOD
