# Organization Employees API

## Overview

Organization Employees API is a simple CRUD API developed using .NET Core for managing an organization's employees. This project is part of an assessment for Zimozi Solutions, a leading software development company with a global presence in Singapore, India, and Australia. The API allows for the creation, retrieval, updating, and deletion of employee records, and uses PostgreSQL as the database.

## Table of Contents

- [Requirements](#requirements)
- [Endpoints](#endpoints)
- [Data Model](#data-model)
- [Database Setup](#database-setup)
- [Validation](#validation)
- [Error Handling](#error-handling)
- [Testing](#testing)
- [Running the Application](#running-the-application)

## Requirements

- .NET Core 8.0
- PostgreSQL

## Endpoints

- **GET /api/Employees**: Return a list of all employees.
- **GET /api/Employees/{id}**: Get a specific employee by ID.
- **POST /api/Employees**: Add a new employee.
- **PUT /api/Employees/{id}**: Update an existing employee.
- **DELETE /api/Employees/{id}**: Delete an employee.

## Data Model

- **EmployeeID** (int, primary key)
- **Name** (string, max length 100)
- **Department** (string, max length 100)
- **Salary** (decimal)

## Database Setup

1. Install PostgreSQL if you haven't already.
2. Update the connection string in `appsettings.json` if necessary

4. Run the following command in the project directory to apply migrations:

```
dotnet ef database update
```

## Validation

- The Name and Department fields are validated to ensure they are not empty.
- Salary is validated to be a positive number.

## Error Handling

The API implements proper error handling, including cases where an employee is not found for update or deletion. A global exception handling middleware is used to catch and format errors consistently.

## Testing

- Unit tests for the service layer are implemented in the `organizationEmployeesAPI.Tests` project.
- Integration tests for the API endpoints are also included in the same project.

## Running the Application

1. Clone the repository:
   ```
   git clone https://github.com/your-username/organizationEmployeesAPI.git
   ```

2. Navigate to the project directory:
   ```
   cd organizationEmployeesAPI
   ```

3. Restore dependencies:
   ```
   dotnet restore
   ```

4. Update the database (as mentioned in the Database Setup section).

5. Run the application:
   ```
   dotnet run --project organizationEmployeesAPI
   ```

6. The API will be available at `https://localhost:5001` or `http://localhost:5000`.

7. You can use Swagger UI to test the API endpoints by navigating to `https://localhost:5001/swagger` in your browser.
