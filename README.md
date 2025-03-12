
---

### Travel Alert Bangladesh - Server Side

This repository contains the server-side implementation of the Travel Alert Bangladesh (TAB) project, built using **ASP.NET Core 8.0 LTS**. The server-side handles API services for the TAB platform, providing secure, scalable, and efficient data handling, user authentication, and interaction between the client-side and the backend services.

## Features

- **ASP.NET Core 8.0 LTS** for building robust APIs.
- **Entity Framework Core** for database interaction.
- **CORS** support to enable cross-origin resource sharing.
- **ASP.NET Core Identity** for user authentication and authorization.
- **MS SQL Server** for database management.
- **Modular Architecture** for scalability and maintainability.

## Prerequisites

- **.NET Core SDK 8.0 (LTS)**
- **Visual Studio 2022** or **Visual Studio Code**
- **MS SQL Server**

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/yousuf-mansur/travel-alert-bangladesh-server-side.git
   ```

2. Open the solution file in Visual Studio:

   ```bash
   TravelUpdateApi.sln
   ```

3. Restore NuGet packages (if necessary):

   ```bash
   dotnet restore
   ```

4. Set up the database:

   In **Package Manager Console**, run the following command to apply migrations and create the database:

   ```bash
   Update-Database
   ```

## Running the Application

1. Press `F5` or click the Start button in Visual Studio to run the application.
2. The API will be available at the base URL (default `https://localhost:5001/`).

## API Documentation

The server-side project includes Swagger UI for easy testing and exploration of the API. You can access it at the following URL after running the application:

```url
https://localhost:5001/swagger
```

## Project Structure

- `Controllers/` - Contains the API controllers responsible for handling requests and sending responses.
- `Models/` - Contains data models and Identity models used by Entity Framework Core.
- `Data/` - Handles DbContext and Identity setup.
- `Services/` - Contains business logic and various services (e.g., user management, data handling).
  
## Packages Used

The following NuGet packages are used in this project:

```xml
<PackageReference Include="Microsoft.AspNetCore.Cors" Version="2.2.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.10" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.10" />
<PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="8.0.10" />
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.10" />
<PackageReference Include="Microsoft.AspNetCore.Http.Features" Version="5.0.17" />
```

## Contributing

1. Fork the repository.
2. Create your feature branch (`git checkout -b feature/AmazingFeature`).
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`).
4. Push to the branch (`git push origin feature/AmazingFeature`).
5. Open a pull request.

## About Us

Worked on the Travel Alert Bangladesh platform under the consultation of **Syed Zahidul Hassan, Consultant: Show & Tell Consulting Ltd.
(IsDB-BISEW IT Scholarship Programme)**. Developed the backend services using the latest technologies in **ASP.NET Core**, ensuring scalability and high performance for the travel agency.

Contact: [Md. Yousuf Mansur](mailto:mansurmdyousuf@gmail.com)

---
