# BasicAuth

## Project Overview

BasicAuth is a .NET-based web API project implementing a basic authentication and user management system. It follows the CQRS (Command Query Responsibility Segregation) architectural pattern, separating read and write operations into distinct layers for better maintainability and scalability.

### Key Features

- **User Authentication**: JWT-based login and signup functionality
- **User Management**: CRUD operations for user accounts (Create, Read, Update, Delete)
- **Protected Endpoints**: Example protected API endpoint for fetching dad jokes
- **API Documentation**: Integrated Swagger UI for testing and exploring the API

### Technology Stack

- **Framework**: .NET 10.0
- **Web Framework**: ASP.NET Core
- **Database**: SQLite (with Entity Framework Core)
- **Authentication**: JWT Bearer Tokens
- **Architecture**: CQRS with MediatR
- **API Documentation**: Swashbuckle (Swagger)
- **Environment Management**: DotNetEnv for configuration

### Project Structure

The solution is organized into multiple projects:

- **BasicAuth.Web**: The main web API application
  - Controllers for handling HTTP requests
  - Program.cs for application configuration
  - DTOs for API responses

- **BasicAuth.Commands**: Handles write operations (commands)
  - Auth commands (Login, Signup)
  - User commands (Create, Update, Delete)

- **BasicAuth.Queries**: Handles read operations (queries)
  - User queries (GetUser, GetUsers, GetUserId)

- **BasicAuth.Data**: Data access layer
  - Entity Framework Core DbContext
  - Database models (User entity)
  - Migrations for database schema

### API Endpoints

- `POST /auth/login` - User login
- `POST /auth/signup` - User registration
- `GET /users` - List all users
- `GET /users/{id}` - Get user by ID
- `POST /users` - Create new user
- `PATCH /users` - Update user
- `DELETE /users/{id}` - Delete user
- `GET /jokes` - Fetch a dad joke (requires authentication)

### Getting Started

1. **Prerequisites**:
   - .NET 10.0 SDK
   - SQLite (included with EF Core)

2. **Environment Setup**:
   - Create a `.env` file in the `BasicAuth.Web` directory with the following variables:
     ```
     DbConnection=Data Source=BasicAuth.db
     JWT_SECRET_KEY=your-secret-key-here
     X-Api-Key=your-api-ninjas-key-here
     ```

3. **Running the Application**:
   - Navigate to the `BasicAuth.Web` directory
   - Run `dotnet run` or use Visual Studio/VS Code to launch
   - The API will be available at `https://localhost:7140`
   - Swagger UI: `https://localhost:7140/swagger`

4. **Database Migration**:
   - The application automatically migrates the database on startup

### Development Notes

- The project uses MediatR for implementing the CQRS pattern
- JWT tokens are required for accessing protected endpoints
- CORS is configured to allow requests from `https://localhost:7140`
- Database migrations are handled automatically in development