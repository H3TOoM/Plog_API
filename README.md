# Blog API

A RESTful API built with ASP.NET Core for a blogging platform that allows users to create, read, update, and delete blog posts, categories, comments, and likes.

## Features

- User authentication and authorization using JWT
- CRUD operations for blog posts (Plogs)
- Category management
- Comment system
- Like functionality
- RESTful API design
- Swagger/OpenAPI documentation
- Entity Framework Core for data access
- Repository pattern implementation
- Dependency Injection

## Technologies Used

- ASP.NET Core 8.0+
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger/OpenAPI
- AutoMapper (based on DTOs structure)

## Project Structure

```
Blog_API/
├── Controllers/       # API controllers
├── DTOs/             # Data Transfer Objects
├── Data/             # Database context and configurations
├── Migrations/       # Database migrations
├── Models/           # Domain models
├── Repoistries/      # Repository implementations
├── Services/         # Business logic services
├── Program.cs        # Application entry point and configuration
└── appsettings.json  # Application settings
```

## Getting Started

### Prerequisites

- .NET 8.0 SDK 
- SQL Server (LocalDB)
- Visual Studio 2022 or VS Code with C# extensions

### Installation

1. Clone the repository:
   ```bash
   git clone [repository-url]
   ```

2. Update the connection string in `appsettings.json` to point to your SQL Server instance:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=your_server;Database=BlogDB;Trusted_Connection=True;"
     }
   }
   ```

3. Run the database migrations:
   ```bash
   dotnet ef database update
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

5. Access the Swagger documentation at `https://localhost:PORT/swagger`

## API Endpoints

### Authentication
- `POST /api/account/register` - Register a new user
- `POST /api/account/login` - Authenticate and get JWT token

### Blog Posts (Plogs)
- `GET /api/plogs` - Get all blog posts
- `GET /api/plogs/{id}` - Get a specific blog post
- `POST /api/plogs` - Create a new blog post (requires authentication)
- `PUT /api/plogs/{id}` - Update a blog post (requires authentication)
- `DELETE /api/plogs/{id}` - Delete a blog post (requires authentication)

### Categories
- `GET /api/categories` - Get all categories
- `GET /api/categories/{id}` - Get a specific category
- `POST /api/categories` - Create a new category (requires authentication)
- `PUT /api/categories/{id}` - Update a category (requires authentication)
- `DELETE /api/categories/{id}` - Delete a category (requires authentication)

### Comments
- `GET /api/comments` - Get all comments
- `GET /api/comments/{id}` - Get a specific comment
- `POST /api/comments` - Create a new comment (requires authentication)
- `PUT /api/comments/{id}` - Update a comment (requires authentication)
- `DELETE /api/comments/{id}` - Delete a comment (requires authentication)

### Likes
- `POST /api/likes` - Like a blog post (requires authentication)
- `DELETE /api/likes/{id}` - Remove a like (requires authentication)

## Authentication

The API uses JWT (JSON Web Tokens) for authentication. To access protected endpoints:

1. Register a new user at `/api/account/register`
2. Log in at `/api/account/login` to get a JWT token
3. Include the token in the `Authorization` header for subsequent requests:
   ```
   Authorization: Bearer your-jwt-token
   ```

## Development

### Running Tests
(Add test instructions if tests are implemented)

### Code Style
- Follow C# coding conventions
- Use meaningful variable and method names
- Add XML documentation for public APIs

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/)
- [JWT Authentication](https://jwt.io/)
