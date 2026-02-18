LoginApp – ASP.NET Core MVC Web Application
📌 Overview

LoginApp is an ASP.NET Core MVC web application built using Entity Framework Core and SQL Server that provides:

User Registration & Login

Secure Password Hashing (SHA256)

Session-Based Authentication

Incident Reporting System

Volunteer Task Management

This project demonstrates core web development concepts including authentication, CRUD operations, session management, and database integration using modern ASP.NET Core practices.

🚀 Features
🔐 Authentication System

Handled in AccountController:

User Registration with duplicate email validation

Secure password hashing using SHA256

Login verification with password hash comparison

Session storage (UserID, UserEmail, UserName)

Logout functionality

Protected Dashboard page

📋 Incident Reports

Handled in IncidentReportsController:

View logged-in user's incident reports

Create new incident reports

Automatically associates reports with logged-in user

Stores report date automatically

🤝 Volunteer Tasks

Handled in VolunteerTasksController:

View personal volunteer tasks

Create new tasks

Model validation with error handling

User-specific task filtering

🏠 Home Controller

Basic navigation pages:

Index

Privacy

Error handling with request tracking

🛠 Technologies Used

ASP.NET Core MVC

Entity Framework Core

SQL Server

Session State Management

SHA256 Cryptography

C#

Razor Views
