# Inventory Management System

An advanced inventory management system with role-based functionality designed to streamline inventory and order processing for businesses. The system supports two types of users: **Inventory Managers** and **Warehouse Workers**. The architecture follows a 3-tier approach with Blazor UI (gRPC Client), a Spring Boot gRPC Server, and a .NET Web API.

## Features

### Inventory Manager
- Manage inventory: Add, edit, and delete items.
- User management: Add and manage users.
- Create orders and view order history.

### Warehouse Worker
- Assign an order to themselves.
- Pick items and update stock.
- Mark orders as completed.
- View own orders (assigned, unassigned, completed by self).

## System Architecture

This system is built with the following architecture:

1. **Blazor UI**: A gRPC client application for interacting with the system via a user-friendly interface.
2. **Spring Boot gRPC Server**: Acts as a middleware service layer, exposing gRPC endpoints. It receives requests from the Blazor UI and delegates business logic calls to the .NET Web API.
3. **.NET Web API**: Backend logic where core inventory management logic, database interaction, and authorization checks reside.

### Authentication and Authorization
- **Token Generation**: Upon successful login, the system issues a JWT (or similar token).
- **Password Security**: All passwords are securely hashed before storage to protect user data.
- **Token Storage**: The token is stored in the browser’s Local Storage.
- **Bearer Header**: For protected endpoints, the Blazor UI attaches the token in the `Authorization: Bearer <token>` header for each request.
- **Role-Based Access**:
  - Inventory Manager vs. Warehouse Worker roles are checked server-side in the .NET API.
  - Unauthorized requests return `401 Unauthorized` or `403 Forbidden` based on the scenario.

---

## Quick Setup

### Prerequisites
- **.NET SDK**: For building and running the Web API.
- **Java & Spring Boot**: For setting up the gRPC server.
- **Node.js & npm**: For running Blazor UI.
- **Docker** (optional): For containerized deployment.

### Installation

1. Clone the repository:
    ```bash
    git clone <repository_url>
    cd inventory-management-system
    ```

2. Set up the **.NET Web API**:
    - Update the `appsettings.json` file with database configuration.
    - Apply migrations and update the database:
      ```bash
      dotnet ef database update
      ```
    - Set `ClearDbAndSeedDb` to `true` in `appsettings.json` to seed the database using the script.
    - Build and run the Web API:
      ```bash
      dotnet build
      dotnet run
      ```

3. Set up the **Spring Boot gRPC Server**:
    - Compile using Maven to generate code:
      ```bash
      ./mvnw compile
      ./mvnw spring-boot:run
      ```

4. Set up the **Blazor UI**:
    - Configure the gRPC server URL in the application settings.
    - Build and run the UI:
      ```bash
      dotnet build
      dotnet run
      ```

---

## Usage

### Role-Based Features

- **Inventory Manager**:
  1. Log in to the Blazor UI.
  2. Navigate to the Inventory section to manage items.
  3. Use the sidebar to switch between:
     - **Inventory Dashboard**: View and manage inventory items.
     - **Manage Users**: Add, edit, or delete user accounts.
     - **Order History**: View the history of all orders created.
  4. Use the Orders section to create and view order history.

- **Warehouse Worker**:
  1. Log in to the Blazor UI.
  2. Go to the **Pick-Up Management** page for order handling.
  3. Use the three toggle buttons to switch views:
     - **Unassigned Orders**: View all unassigned orders that can be claimed.
     - **Assigned Orders**: View orders assigned to the logged-in worker.
     - **Completed Orders**: View a history of orders completed by the worker.
  4. Pick items and update stock while fulfilling orders.
  5. Mark orders as completed.

---

## API Details

### Authentication
- **Endpoint**: `/api/auth/login`
- **Method**: POST
- **Headers**: None
- **Body**:
  ```json
  {
    "username": "string",
    "password": "string"
  }
