# UmDoisTresVendas

UmDoisTresVendas is a back-end application designed to manage sales operations efficiently. Built with C# and .NET 8, this project utilizes various design patterns and best practices to ensure maintainability, scalability, and performance.

## Highlights

- API-Driven: The application exposes RESTful APIs for handling sales operations, including creating, updating, retrieving, and canceling sales.
- Validation: Utilizes FluentValidation for comprehensive input validation to ensure data integrity.
- Logging: Integrated logging using Serilog to track important events and errors throughout the application lifecycle.
- AutoMapper: Employs AutoMapper for seamless DTO-to-Entity mapping, promoting clean and maintainable code.
- Unit Testing: Designed with testability in mind, leveraging XUnit and FluentAssertions for unit testing to ensure reliable functionality.
- Asynchronous Programming: Implements asynchronous methods for non-blocking operations, enhancing performance under load.

## Getting Started

Follow the steps below to run the UmDoisTresVendas project locally:

### Prerequisites

Ensure you have the following installed:

- .NET 8 SDK
- Visual Studio Code or any other preferred IDE
- SQL Server

### Installation

1. Navigate to the Project Directory

   Open your terminal and navigate to the directory where you have the project.

   ```
   cd UmDoisTresVendas
   ```

2. Restore NuGet Packages

   Restore the required NuGet packages using the following command:

   ```
   dotnet restore
   ```

3. Set Up the Database

   Ensure your database is set up correctly. Update the connection string in appsettings.json to point to your database. It may look something like this:

   ```
   "ConnectionStrings": {
   "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
   }
   ```

4. Run Database Migrations

   If you have Entity Framework Core set up, you can apply migrations using:

   ```
   cd UmDoisTresVendas.Infrastructure
   dotnet ef database update -s ../UmDoisTresVendas.Api
   ```

5. Run the Application

   Start the application with the following command:

   ```
   dotnet run
   ```

   The application will start running on http://localhost:5000 or another specified port.

### Usage

You can now test the available API endpoints using tools like Postman or cURL.

Here are some example endpoints:

- Get Sale by ID: GET /api/sales/{saleId}
- Create Sale: POST /api/sales
- Update Sale: PUT /api/sales/{saleId}
- Cancel Sale: POST /api/sales/{saleId}/cancel

#### To facilitate testing and interacting with the API, you can download the Postman collection:

- [Download Postman Collection](./Docs/123Vendas.postman_collection.json)

Simply import this collection into Postman to start testing the API endpoints.

### Contributing

Contributions are welcome! Please fork the repository and submit a pull request for any improvements or features you would like to add.

This project was created to practice various programming patterns and practices, so feel free to use the code or contribute to it as you see fit.

### License

The code in this project is freely available for anyone to use, modify, and distribute. There are no restrictions on its use; just make sure to give credit where it's due.

## Contact

For any inquiries or suggestions, please reach out to matheusrocha7802@gmail.com
