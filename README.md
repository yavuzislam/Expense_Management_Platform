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


## API Endpoint
**Screenshot of the single API endpoint used in the project:**

Below are some screenshots of the key endpoints and features of the Expense-Payment System, along with brief descriptions of their functionalities:

### Expense Management
![Expense Management](https://github.com/yavuzislam/Expense_Management_Platform/assets/65170205/f4301be2-11cc-45f7-8669-5d3c77d0cddd)
This interface allows field personnel to enter and submit their expenses for approval. It provides an overview of all submitted expenses, their statuses, and details.

### Notifications
![Notifications](https://github.com/yavuzislam/Expense_Management_Platform/assets/65170205/3f599057-3d65-4c00-8b80-eb8279c013df)

### Payments
![Payments](https://github.com/yavuzislam/Expense_Management_Platform/assets/65170205/6514630e-e994-435d-a01e-504c8422d1b9)
This section shows the payment status of approved expenses, enabling users to track the transaction status and history.

### Reports
![Reports](https://github.com/yavuzislam/Expense_Management_Platform/assets/65170205/780b1c32-ebab-4339-b343-66869200ff12)
Users can generate and view various expense reports, providing insights into spending patterns and expense management efficiency.

### Token Management
![Token](https://github.com/yavuzislam/Expense_Management_Platform/assets/65170205/1b2ec542-d371-47ea-8d0f-7fec5c6b6b20)
This feature deals with the authentication tokens for secure access to the application.

### User Management
![Users](https://github.com/yavuzislam/Expense_Management_Platform/assets/65170205/0de10f0d-1302-4155-aafb-a805a53c714f)
Here, administrators can manage user accounts, roles, and permissions within the application.

### Expense Categories
![Category](https://github.com/yavuzislam/Expense_Management_Platform/assets/65170205/c5b0a321-9c34-42f3-a5f1-4ee8f81eae50)
This section allows for the categorization of expenses, helping in organizing and reporting expenses more effectively.

### EFT Details
![EFT Detail](https://github.com/yavuzislam/Expense_Management_Platform/assets/65170205/ad45f5c2-bede-4a4e-b512-85720c19d094)
Displays the details of Electronic Fund Transfers for approved expense payments.


