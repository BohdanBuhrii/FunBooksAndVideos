﻿# FunBooksAndVideos e-commerce shop

## Overview
This is an **ASP.NET Core** API that provides various endpoints for managing e-shop resources. The API is documented using **Swagger** and uses **SQL Server** as the database.

## Features
- Create purchase order with available products
- Create, view and modify products
- View memberships

## Prerequisites
- .NET 8.0 (or later)
- SQL Server
- Visual Studio / VS Code

## Getting Started

### 1. Clone the Repository
```sh
git clone https://github.com/BohdanBuhrii/FunBooksAndVideos.git
```

### 2. Configure Database
1. Create a SQL Server database.
2. Run the initialization script available at `./EShop/Database/Sql/Init.sql`:
   ```sh
   sqlcmd -S <YourServer> -d <YourDatabase> -i ./EShop/Database/Sql/Init.sql
   ```
3. Update `appsettings.Development.json` with your database connection string:
   ```json
   "EShopConnectionString": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
   ```

### 3. Run the API
```sh
dotnet run
```
The API will start and be available at `http://localhost:5117`.

### 4. Access Swagger UI
Once the API is running, open **Swagger UI** in your browser:
```
http://localhost:5117/swagger/index.html
```
Swagger provides interactive API documentation where you can test the endpoints directly.

## API Endpoints
Refer to Swagger UI for the complete list of available endpoints.
