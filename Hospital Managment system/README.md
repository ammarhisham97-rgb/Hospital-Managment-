# Hospital Management System - ASP.NET Core 9 Web API

A comprehensive, production-style Hospital Management System built with ASP.NET Core 9 Web API. This project demonstrates industry best practices and is suitable for a junior .NET backend developer's portfolio.

## Table of Contents

- [Features](#features)
- [Technology Stack](#technology-stack)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Database Design](#database-design)
- [API Endpoints](#api-endpoints)
- [Authentication & Authorization](#authentication--authorization)
- [Error Handling](#error-handling)
- [Logging](#logging)
- [Deployment](#deployment)

## Features

### Authentication & Authorization
- User registration with role-based assignment (Admin, Doctor, Receptionist, Patient)
- JWT token-based authentication
- Role-based access control (RBAC)
- Secure password hashing with ASP.NET Core Identity
- Password change functionality
- User profile management

### Patient Management
- CRUD operations for patients
- Patient profile with medical history
- Blood group and allergy tracking
- Emergency contact information
- Insurance details
- Patient search functionality
- Medical history retrieval

### Doctor Management
- CRUD operations for doctors
- Doctor profile with specialization
- Department assignment
- Consultation fee management
- Availability status
- Doctor schedule management
- Search doctors by specialization or name

### Appointment System
- Book appointments between patients and doctors
- Prevent double booking (conflict detection)
- Reschedule appointments
- Cancel appointments
- View appointment history
- Today's appointments list
- Upcoming appointments list
- Appointment status tracking

### Medical Records
- Create and manage medical records
- Diagnosis and treatment planning
- Lab test results tracking
- Prescription management
- Visit notes and symptoms documentation
- Link records to appointments

### Department Management
- Create and manage departments
- Department-doctor relationships
- Search departments
- View doctors by department

### Dashboard Statistics
- Total patients count
- Total doctors count
- Total appointments count
- Total departments count
- Today's appointments list and count
- Upcoming appointments list and count

## Technology Stack

- **Framework**: ASP.NET Core 9
- **Language**: C# 12
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: JWT with ASP.NET Core Identity
- **ORM**: Entity Framework Core 9.0
- **API Documentation**: Swagger/OpenAPI
- **Validation**: FluentValidation
- **Logging**: Serilog
- **Mapping**: AutoMapper
- **Architecture**: Repository Pattern with Unit of Work

## Architecture

The project follows clean architecture principles with proper separation of concerns:

```
Hospital Managment system/
├── Controllers/           # API endpoints
├── Models/               # Entity classes
├── DTOs/                 # Data Transfer Objects
├── Data/                 # DbContext and configurations
├── Repositories/         # Data access layer
├── Services/             # Business logic layer
├── Interfaces/           # Service and repository contracts
├── Mapping/              # AutoMapper profiles
├── Validators/           # FluentValidation rules
├── Middleware/           # Custom middleware (exception handling)
├── Helpers/              # Utility classes
├── Program.cs            # Application configuration
└── appsettings.json      # Configuration settings
```

## Getting Started

### Prerequisites

- .NET 10 SDK
- SQL Server (Local or Express)
- Visual Studio 2022+ or any C# IDE
- Git

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd "Hospital Managment system"
   ```

2. **Install dependencies**
   ```bash
   dotnet restore
   ```

3. **Configure the database connection**

   Update `appsettings.json` with your SQL Server connection string:
   ```json
   "ConnectionStrings": {
	 "DefaultConnection": "Server=YOUR_SERVER;Database=HospitalManagementDb;Trusted_Connection=true;TrustServerCertificate=true;"
   }
   ```

4. **Update JWT settings**

   In `appsettings.json`, update the JWT secret key (should be at least 32 characters):
   ```json
   "JwtSettings": {
	 "SecretKey": "your-super-secret-key-that-is-at-least-32-characters-long",
	 "Issuer": "HospitalManagementSystem",
	 "Audience": "HospitalManagementUsers",
	 "ExpiryMinutes": 60
   }
   ```

5. **Create the database**

   Apply Entity Framework Core migrations:
   ```bash
   dotnet ef database update
   ```

6. **Run the application**
   ```bash
   dotnet run
   ```

   The API will be available at `https://localhost:5001` (or the configured port)

## Project Structure

### Models
- **User**: Inherits from IdentityUser, represents authenticated users
- **Patient**: Patient demographic and medical information
- **Doctor**: Doctor details, specialization, and department
- **Department**: Hospital departments
- **Appointment**: Patient-Doctor appointments with status tracking
- **MedicalRecord**: Clinical documentation for patient visits
- **Prescription**: Medication prescriptions from medical records
- **DoctorSchedule**: Doctor availability schedules

### DTOs (Data Transfer Objects)
Separate DTOs for different operations ensure proper API contracts:
- **AuthDto**: Registration, login, password change
- **PatientDto**: Patient create/update operations
- **DoctorDto**: Doctor create/update operations
- **AppointmentDto**: Appointment operations
- **MedicalRecordDto**: Medical record operations
- **PrescriptionDto**: Prescription operations
- **DepartmentDto**: Department operations

### Services
Business logic is encapsulated in services:
- **AuthService**: Authentication operations
- **PatientService**: Patient management
- **DoctorService**: Doctor management
- **AppointmentService**: Appointment management with conflict detection
- **MedicalRecordService**: Medical record and prescription management
- **DepartmentService**: Department management
- **DashboardService**: Statistics and metrics

## Database Design

### Entity Relationships

**One-to-Many Relationships:**
- Department → Doctors
- Patient → Appointments
- Patient → Medical Records
- Doctor → Appointments
- Doctor → Medical Records
- Doctor → Schedules
- Medical Record → Prescriptions

**One-to-One Relationships:**
- User → Patient
- User → Doctor
- Appointment → Medical Record

### Key Features

- **Soft Deletes**: All entities support soft deletion with `IsDeleted` flag
- **Timestamps**: `CreatedAt` and `UpdatedAt` tracking on all entities
- **Auditing**: Track when entities are modified
- **Query Filters**: Automatically exclude soft-deleted entities in queries
- **Indexes**: Strategic indexes on frequently searched fields

## API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login user
- `GET /api/auth/me` - Get current user profile [Authorized]
- `POST /api/auth/change-password` - Change password [Authorized]

### Patients
- `GET /api/patients` - Get all patients with pagination [Admin, Doctor, Receptionist]
- `GET /api/patients/{id}` - Get patient by ID
- `PUT /api/patients/{id}` - Update patient
- `DELETE /api/patients/{id}` - Delete patient [Admin, Receptionist]
- `GET /api/patients/search/{searchTerm}` - Search patients [Admin, Doctor, Receptionist]
- `GET /api/patients/{id}/medical-history` - Get patient medical history

### Doctors
- `GET /api/doctors` - Get all doctors with pagination
- `GET /api/doctors/{id}` - Get doctor by ID
- `PUT /api/doctors/{id}` - Update doctor [Admin, Doctor]
- `DELETE /api/doctors/{id}` - Delete doctor [Admin]
- `GET /api/doctors/specialization/{specialization}` - Get doctors by specialization
- `GET /api/doctors/available/list` - Get available doctors
- `GET /api/doctors/search/{searchTerm}` - Search doctors
- `GET /api/doctors/{id}/schedule` - Get doctor schedule
- `POST /api/doctors/{id}/schedule` - Add doctor schedule [Admin, Doctor]

### Appointments
- `POST /api/appointments` - Book appointment
- `GET /api/appointments` - Get all appointments with pagination [Admin, Doctor, Receptionist]
- `GET /api/appointments/{id}` - Get appointment by ID
- `PUT /api/appointments/{id}/reschedule` - Reschedule appointment
- `PUT /api/appointments/{id}/cancel` - Cancel appointment
- `GET /api/appointments/today/list` - Get today's appointments [Admin, Doctor, Receptionist]
- `GET /api/appointments/upcoming/list` - Get upcoming appointments [Admin, Doctor, Receptionist]
- `GET /api/appointments/patient/{patientId}` - Get patient appointments
- `GET /api/appointments/doctor/{doctorId}` - Get doctor appointments [Admin, Doctor, Receptionist]
- `PUT /api/appointments/{id}/complete` - Mark appointment as completed [Admin, Doctor]

### Medical Records
- `POST /api/medicalrecords` - Create medical record [Admin, Doctor]
- `GET /api/medicalrecords` - Get all medical records with pagination [Admin, Doctor, Receptionist]
- `GET /api/medicalrecords/{id}` - Get medical record by ID [Admin, Doctor, Receptionist]
- `PUT /api/medicalrecords/{id}` - Update medical record [Admin, Doctor]
- `DELETE /api/medicalrecords/{id}` - Delete medical record [Admin]
- `GET /api/medicalrecords/patient/{patientId}` - Get patient records
- `POST /api/medicalrecords/{recordId}/prescriptions` - Add prescription [Admin, Doctor]
- `GET /api/medicalrecords/{recordId}/prescriptions` - Get prescriptions [Admin, Doctor, Receptionist]

### Departments
- `POST /api/departments` - Create department [Admin]
- `GET /api/departments` - Get all departments with pagination
- `GET /api/departments/{id}` - Get department by ID
- `PUT /api/departments/{id}` - Update department [Admin]
- `DELETE /api/departments/{id}` - Delete department [Admin]
- `GET /api/departments/search/{searchTerm}` - Search departments

### Dashboard
- `GET /api/dashboard/statistics` - Get all statistics [Admin, Receptionist]
- `GET /api/dashboard/total-patients` - Total patients count [Admin, Receptionist]
- `GET /api/dashboard/total-doctors` - Total doctors count [Admin, Receptionist]
- `GET /api/dashboard/total-appointments` - Total appointments count [Admin, Receptionist]
- `GET /api/dashboard/today-appointments` - Today's appointments [Admin, Receptionist]
- `GET /api/dashboard/upcoming-appointments` - Upcoming appointments [Admin, Receptionist]

## Authentication & Authorization

### Roles

1. **Admin** - Full system access, user management, department management
2. **Doctor** - Manage appointments, create medical records, manage schedule
3. **Receptionist** - Manage patient registrations, appointments, view statistics
4. **Patient** - Book appointments, view medical records

### JWT Token

The API uses JWT (JSON Web Token) for authentication. Include the token in the Authorization header:

```
Authorization: Bearer <your_jwt_token>
```

### Token Claims

- `sub` (Subject): User ID
- `email`: User email
- `name`: User full name
- `role`: User role(s)

## Error Handling

The API implements global exception handling middleware that catches all unhandled exceptions and returns consistent error responses:

```json
{
  "success": false,
  "message": "An error occurred",
  "error": "Error details",
  "statusCode": 400
}
```

### Common HTTP Status Codes

- `200 OK` - Successful request
- `201 Created` - Resource created successfully
- `400 Bad Request` - Invalid request data
- `401 Unauthorized` - Authentication required
- `403 Forbidden` - Insufficient permissions
- `404 Not Found` - Resource not found
- `500 Internal Server Error` - Server error

## Logging

The application uses Serilog for structured logging with multiple sinks:

- **Console**: Real-time log output in the console
- **File**: Daily rolling file logs in the `logs/` directory

### Log Levels

- **Debug** - Detailed diagnostic information
- **Information** - General operational information
- **Warning** - Warning messages for potentially harmful situations
- **Error** - Error messages for failures
- **Fatal** - Critical failures

Access logs will be created in `logs/hospital-system-YYYY-MM-DD.txt`

## Validation

FluentValidation is used for request DTO validation:

- Email format validation
- Password strength requirements (length, uppercase, lowercase, digits, special characters)
- Phone number format validation
- Blood group validation
- Numeric range validation

Validation errors are returned as:

```json
{
  "success": false,
  "message": "Validation failed",
  "error": "Field 1 error, Field 2 error",
  "statusCode": 400
}
```

## Seeding

The application comes with initial seed data:

**Default Admin User:**
- Email: `admin@hospital.com`
- Password: `Admin@123456`
- Role: Admin

**Default Departments:**
- Cardiology
- Neurology
- Orthopedics

## Development Practices

### Best Practices Implemented

1. **Clean Code**: Meaningful naming, single responsibility principle
2. **SOLID Principles**: Dependency injection, loose coupling
3. **Async/Await**: Async programming throughout the application
4. **Pagination**: Efficient data retrieval with page numbers and size
5. **DTOs**: Separation of database models from API contracts
6. **Repository Pattern**: Centralized data access logic
7. **Unit of Work Pattern**: Transaction management across repositories
8. **Error Handling**: Comprehensive exception handling with custom middleware
9. **Security**: JWT authentication, role-based authorization
10. **Documentation**: XML comments on all public members

### Testing

To test the API, use tools like:

- **Swagger UI**: Available at `/swagger` when running in development
- **Postman**: Import the API endpoints
- **REST Client**: VS Code extension

## Deployment

### Docker Deployment

Create a `Dockerfile`:

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:10 AS runtime
WORKDIR /app
COPY --from=builder /app/bin/Release/net10.0/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Hospital Managment system.dll"]
```

### Azure Deployment

The project can be deployed to:

- Azure App Service
- Azure Container Instances
- Azure Kubernetes Service (AKS)

### Production Considerations

1. Update JWT secret key to a strong value
2. Use environment-specific configuration
3. Enable HTTPS
4. Configure CORS appropriately
5. Set up database backups
6. Monitor logs and errors
7. Use a reverse proxy (nginx/IIS)
8. Configure rate limiting
9. Implement caching strategies
10. Use connection pooling

## Configuration Files

### appsettings.json

```json
{
  "Logging": {
	"LogLevel": {
	  "Default": "Information",
	  "Microsoft.EntityFrameworkCore.Database.Command": "Debug"
	}
  },
  "ConnectionStrings": {
	"DefaultConnection": "Server=(local);Database=HospitalManagementDb;Trusted_Connection=true;"
  },
  "JwtSettings": {
	"SecretKey": "your-secret-key",
	"Issuer": "HospitalManagementSystem",
	"Audience": "HospitalManagementUsers",
	"ExpiryMinutes": 60
  }
}
```

## Troubleshooting

### Common Issues

**Issue**: Database connection fails
- **Solution**: Check connection string in `appsettings.json`, ensure SQL Server is running

**Issue**: Migration fails
- **Solution**: Delete the database and run migrations again, or check EF Core version compatibility

**Issue**: Authentication fails
- **Solution**: Verify JWT settings, ensure token is properly formatted in Authorization header

**Issue**: Swagger UI not loading
- **Solution**: Ensure application is running in Development environment

## Contributing

When contributing to this project:

1. Follow the existing code style
2. Add XML comments to public members
3. Write meaningful commit messages
4. Test your changes thoroughly
5. Update documentation as needed

## License

This project is provided as-is for educational and portfolio purposes.

## Support

For issues or questions:

1. Check the existing code comments and documentation
2. Review the architecture pattern used
3. Consult the Swagger documentation at `/swagger`

## Future Enhancements

Potential improvements for this system:

- [ ] Email notifications for appointments
- [ ] SMS notifications
- [ ] Payment integration
- [ ] Prescription printing/PDF export
- [ ] Advanced reporting and analytics
- [ ] Telemedicine support
- [ ] Real-time appointment scheduling
- [ ] Insurance verification
- [ ] Lab integration
- [ ] Patient portal UI
