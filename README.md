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

