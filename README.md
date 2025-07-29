# 👤 UserRegistrationApp

[![.NET 8](https://img.shields.io/badge/.NET-8.0-purple?logo=dotnet)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
A modern ASP.NET Core MVC web app for user registration and login, built using **Entity Framework Core (Database-First)** and styled with **Bootstrap 5**.

---

## 🚀 Key Features

✅ User Registration with input validation  
✅ Secure Login & Logout using cookie authentication  
✅ Paging support using `X.PagedList`  
✅ MVC architectural pattern  
✅ Clean UI with Bootstrap 5  
✅ Database-First EF Core integration  

---

## 🛠️ Tech Stack

| Layer       | Technology                    |
|-------------|-------------------------------|
| Framework   | ASP.NET Core MVC (.NET 8)     |
| Backend     | C#, Entity Framework Core     |
| Frontend    | Razor Views, HTML, Bootstrap 5|
| Database    | SQL Server                    |
| NuGet Tools | `X.PagedList.Mvc.Core`        |

---

⚙️ Getting Started
Follow these steps to run the project locally:

🧱 Prerequisites
.NET 8 SDK

SQL Server / LocalDB

Visual Studio 2022+

🔧 Setup Instructions
Clone the repository

git clone https://github.com/Sriram110603/UserRegistrationApp.git

cd UserRegistrationApp


Update DB connection string in appsettings.json



"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=UserAppDb;Trusted_Connection=True;"
}


Build & Run the app
dotnet build


dotnet run

Visit the site
https://localhost:7027


🧪 Database Setup
This project follows the Database-First approach using an existing SQL Server DB.


⚠️ Ensure that the UserAppDb database exists with the required tables (User, etc.).

## 📁 Project Structure

```plaintext
📦 UserRegistrationApp
├── 📂 Controllers
│   └── AccountController.cs
├── 📂 Models
│   └── User.cs
├── 📂 Views
│   ├── 📂 Account
│   │   ├── Register.cshtml
│   │   └── Login.cshtml
│   └── 📂 Shared
│       └── _Layout.cshtml
├── 📂 Data
│   └── ApplicationDbContext.cs
├── 📂 wwwroot
│   └── (static files)
├── appsettings.json
├── Program.cs
└── UserRegistrationApp.csproj
