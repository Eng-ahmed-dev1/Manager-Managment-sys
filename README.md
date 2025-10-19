# Task Management System

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![WPF](https://img.shields.io/badge/WPF-0C54C2?style=for-the-badge&logo=windows&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-512BD4?style=for-the-badge&logo=nuget&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![XAML](https://img.shields.io/badge/XAML-0C54C2?style=for-the-badge&logo=xaml&logoColor=white)

A comprehensive desktop application for managing tasks and employees, built with WPF (Windows Presentation Foundation) and Entity Framework Core. The system provides role-based access control with separate interfaces for managers and employees.

## 📋 Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Database Configuration](#database-configuration)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [User Roles](#user-roles)
- [Screenshots](#screenshots)
- [Contributing](#contributing)
- [License](#license)

## ✨ Features

### Manager Dashboard
- **Task Management**: Create, edit, and delete tasks
- **Employee Assignment**: Assign tasks to employees
- **Status Tracking**: Monitor task status (Pending, In Progress, Completed)
- **Employee Management**: Add new employees automatically when assigning tasks
- **Real-time Updates**: View all tasks with employee information in a data grid
- **Task Details**: View comprehensive task information including title, description, and due dates

### Employee Dashboard
- **Task View**: Separate views for active and completed tasks
- **Status Updates**: Update task status (Pending → In Progress → Completed)
- **Personal Dashboard**: View only assigned tasks
- **Task Completion**: Mark tasks as completed with automatic timestamp

### Authentication System
- Secure login with username and password validation
- Role-based access control (Manager/Employee)
- User-friendly error messages

## 🛠️ Technologies Used

- **Framework**: .NET with WPF (Windows Presentation Foundation)
- **Language**: C# 
- **UI Markup**: XAML
- **ORM**: Entity Framework Core
- **Database**: Microsoft SQL Server
- **Design Pattern**: MVVM-inspired architecture
- **Data Binding**: WPF data binding with LINQ queries

## 📦 Prerequisites

Before running this application, ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (Version 6.0 or higher)
- [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express or higher)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (Community, Professional, or Enterprise)
- SQL Server Management Studio (SSMS) - Optional but recommended

## 🚀 Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/task-management-system.git
   cd task-management-system
   ```

2. **Open the solution**
   - Open `ManagerManagmentsys.sln` in Visual Studio 2022

3. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

4. **Update the connection string**
   - Open `ManagmentDB.cs`
   - Update the connection string in the `OnConfiguring` method to match your SQL Server instance:
   ```csharp
   optionsBuilder.UseSqlServer("Data Source=YOUR_SERVER_NAME;Initial Catalog=TasksDB;Integrated Security=True;Trust Server Certificate=True");
   ```

5. **Create the database**
   - Open Package Manager Console in Visual Studio
   - Run the following commands:
   ```powershell
   Add-Migration InitialCreate
   Update-Database
   ```

6. **Build and run**
   ```bash
   dotnet build
   dotnet run
   ```

## 🗄️ Database Configuration

### Database Schema

**User Table**
- `UserID` (int, Primary Key)
- `Name` (string, 250 chars)
- `Email` (string, 250 chars)
- `Password` (string, 250 chars)
- `Role` (string, 250 chars) - "Manager" or "Employee"

**Tasks Table**
- `TaskId` (int, Primary Key)
- `Title` (string, 250 chars)
- `Description` (string, max)
- `Status` (string, 250 chars)
- `DueDate` (DateTime)
- `Userid` (int, Foreign Key)

### Initial Setup

To add a default manager account, run the following SQL script in SSMS:

```sql
USE TasksDB;
GO

INSERT INTO [User] (UserID, Name, Email, Password, Role)
VALUES (1, 'admin', 'admin@company.com', '123', 'Manager');
```

## 💻 Usage

### Login Credentials

**Default Manager Account:**
- Username: `admin`
- Password: `123`

### Manager Workflow

1. **Login** with manager credentials
2. **Add Task**: 
   - Enter Task ID (unique number)
   - Fill in Title and Description
   - Enter Employee Name and ID
   - Select Status from dropdown
   - Click "Add"
3. **Edit Task**: 
   - Select task from the data grid
   - Modify details
   - Click "Edit"
4. **Delete Task**: 
   - Select task from the data grid
   - Click "Delete"

### Employee Workflow

1. **Login** with employee credentials
2. **View Tasks**: 
   - Upper grid shows Pending/In Progress tasks
   - Lower grid shows Completed tasks
3. **Update Status**:
   - Enter Task ID
   - Select new status from dropdown
   - Click "Save"

## 📁 Project Structure

```
ManagerManagmentsys/
├── Model/
│   ├── ManagmentDB.cs          # DbContext configuration
│   ├── Tasks.cs                # Task entity model
│   └── User.cs                 # User entity model
├── Views/
│   ├── ManagerDashBoard.xaml   # Manager UI
│   ├── ManagerDashBoard.xaml.cs
│   ├── UserDashBaord.xaml      # Employee UI
│   └── UserDashBaord.xaml.cs
├── MainWindow.xaml             # Login UI
├── MainWindow.xaml.cs          # Login logic
└── App.xaml                    # Application configuration
```

## 👥 User Roles

### Manager
- Full CRUD operations on tasks
- Employee assignment capabilities
- Overview of all tasks and employees
- Administrative dashboard access

### Employee
- View assigned tasks only
- Update task status
- Mark tasks as completed
- Personal task dashboard

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🐛 Known Issues

- Employee accounts are automatically created when managers assign tasks
- Task ID must be manually managed (no auto-increment in UI)
- Password storage is not encrypted (for production, implement proper password hashing)

## 🔮 Future Enhancements

- [ ] Password encryption and security improvements
- [ ] Auto-increment Task ID generation
- [ ] Email notifications for task assignments
- [ ] Task priority levels
- [ ] Due date reminders
- [ ] Task comments and attachments
- [ ] Export functionality (PDF, Excel)
- [ ] User profile management
- [ ] Task filtering and search functionality
- [ ] Dashboard analytics and reporting

## 📧 Contact

For questions or support, please open an issue in the GitHub repository.

---

**Made with ❤️ using WPF and Entity Framework Core**
