# Expense-Payment System

## About The Project

The Expense-Payment System is designed for field employees and offers a comprehensive expense management solution. This application enables field workers to instantly record their expenses, allowing employers to monitor these expenses and quickly approve them. The system serves two different user roles: field personnel and system administrators.

### Key Features

- **Expense Entry and Tracking:** Field personnel can quickly enter their expenses into the application and request reimbursements.
- **Expense Approval:** Administrators can view expense claims, approve or reject them.
- **Instant Payment:** Approved payments are transferred via bank integration directly into the employee's account through an EFT.
- **Explanation for Rejected Claims:** There is a provision for an explanation field for rejected expense claims, enabling employees to understand why their claim was denied.


## Technology Stack

The Expense-Payment System is developed using a combination of modern and reliable technologies. Key technologies used in the project include:

- **Database:** Microsoft SQL Server - Employed for its robust and reliable data management capabilities.
- **JWT Token:** For Authentication - Utilizes JSON Web Tokens for secure user authentication and authorization processes.
- **EF Repository:** Entity Framework - An ORM (Object-Relational Mapping) tool used for database operations.
- **Swagger:** API Documentation and Testing Tool - Facilitates easy understanding and testing of the API.
- **Redis:** An open-source database used for caching and message queuing.
- **CQRS Pattern:** Command Query Responsibility Segregation - This design pattern is used to separate the read and write operations, enhancing performance and scalability.

## Key Enumerations

The Expense-Payment System uses several enumerations to define key concepts within the application. These include:

### ExpenseStatus
- `PendingApproval` (0): The expense is awaiting approval.
- `Approved` (1): The expense has been approved.
- `Rejected` (2): The expense has been rejected.

### PaymentStatus
- `Successful` (1): The payment was successful.
- `Failed` (2): The payment failed.

### RoleTypes
- `Personel` (1): A regular staff member.
- `Admin` (2): An administrative user with elevated privileges.


# Getting Started
**This guide provides step-by-step instructions for setting up and running the "InterestCalculation.WebApi" project.**

# Prerequisites
**Ensure that you have the .NET SDK installed on your computer.**

### 1.Clone the Repository:
**Clone the project from GitHub:**
```bash
https://github.com/yavuzislam/Expense_Management_Platform.git
```

### 2.Update the Connection String:
**Edit the connection string in the appsettings.json file according to your environment.**
```bash
/appsettings.json
```

### 3. Navigate to Data Project
```bash
cd /Expense_Management_Data
```

### 4.Apply Database Migrations:
**Apply the Entity Framework Core migrations:**
```bash
dotnet ef database update --project "./Expense_Management_Data" --startup-project "./Expense_Management_Api"
```

### 5.Navigate to the Startup Project:
**Use the command line or terminal to go to the Expense_Management_Api directory:**
```bash
cd Expense_Management_Api
```
### 6.Run the Project:
**Start the project with watch mode (automatic reloading):**
```bash
dotnet run
```

