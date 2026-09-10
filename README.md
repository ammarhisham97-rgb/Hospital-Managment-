# Hospital Management System

A production-style Hospital Management API built with ASP.NET Core 10, demonstrating clean architecture, SOLID principles, and industry best practices for managing patients, doctors, departments, appointments, and medical records.

## Overview

This is a comprehensive backend service for hospital operations. It provides role-based access control, appointment conflict detection, comprehensive patient medical tracking, and administrative dashboards. The system is designed to handle real-world healthcare workflows with proper separation of concerns, input validation, error handling, and structured logging.

## Key Features

### Authentication & Authorization
- User registration system with role-based role assignment (Admin, Doctor, Receptionist, Patient)
- JWT token-based authentication with configurable expiry
- Role-based access control (RBAC) enforced on all protected endpoints
- Secure password hashing with ASP.NET Core Identity
- Password change functionality with current password verification
- User profile management with address, contact, and date of birth

### Patient Management
- Complete CRUD operations with pagination support
- Unique patient identification (PatientNumber)
- Medical profile tracking: blood group and allergy information
- Emergency contact information storage
- Insurance details management
- Medical history tracking
- Search and filter capabilities

### Doctor Management
- Full doctor profiles with specialization and licensing
- Department assignment and management
- Consultation fee configuration
- Years of experience tracking
- Availability status management
- Doctor schedule management for availability windows
- Search doctors by specialization or department

### Appointment System
- Book appointments between patients and doctors
- Conflict detection: prevents double-booking of doctors
- Appointment status tracking (Scheduled, Completed, Cancelled, Rescheduled)
- Configurable appointment duration
- Reschedule capabilities
- Appointment notes for doctor feedback
- Appointment type classification (Consultation, Follow-up, etc.)

### Medical Records & Prescriptions
- Create comprehensive medical records with diagnosis information
- Track symptoms, physical examination findings, and visit notes
- Link records to specific appointments
- Prescriptions with full medication details:
  - Medication name, dosage, frequency
  - Duration of course
  - Special instructions
  - Dosage quantity tracking

### Dashboard & Analytics
- Real-time statistics: total patients, doctors, appointments, departments
- Today's appointment count and listing
- Upcoming appointments tracking
- Administrative overview for admins and receptionists

### Department Management
- Create and manage hospital departments
- Department contact information
- Associate doctors with departments
- Retrieve department details with doctor counts

## Technology Stack

### Backend
- **ASP.NET Core 10** Web API
- **C# 13** with nullable reference types and implicit usings
- **Entity Framework Core 9** with SQL Server

### Database
- **SQL Server LocalDB** (default) or SQL Server
- **EF Core Migrations** for schema versioning and deployment
- **ASP.NET Core Identity** for user and role management

### Authentication & Authorization
- **JWT (JSON Web Tokens)** for stateless authentication
- **System.IdentityModel.Tokens.Jwt** for token handling
- **ASP.NET Core Identity** for user/role management
- **Role-Based Access Control (RBAC)** via `[Authorize(Roles = "...")]`

### Architecture & Patterns
- **Unit of Work Pattern** for coordinating multiple repositories
- **Repository Pattern** with generic base implementation
- **Dependency Injection** (built-in ASP.NET Core DI container)
- **AutoMapper** for entity-to-DTO and DTO-to-entity mapping
- **FluentValidation** for input validation rules
- **Layered Architecture**: Controllers → Services → Repositories → Database

### Validation & Error Handling
- **FluentValidation** fluent rules engine for all DTOs
- **Centralized Exception Handling Middleware** with consistent API responses
- **Custom error response formatting** via `ApiResponseDto<T>`
- **Structured validation error aggregation**

### Logging
- **Serilog** structured logging framework
- **File-based logging** with daily rolling intervals
- **Console output** for development environments
- **Contextual enrichment** with application name and timestamp
- **Request logging** middleware for HTTP activity tracking

### API Documentation
- **Microsoft.AspNetCore.OpenApi** for OpenAPI/Swagger generation
- **Scalar.AspNetCore** for interactive API documentation UI (Development)
- **XML documentation comments** on all public methods
- **Response type declarations** via `[ProducesResponseType]`

### CORS
- **AllowAll policy** for local development (configurable for production)

## Architecture

The project follows a clean, layered architecture with clear separation of concerns:

```
Hospital Management System (Web API)
│
├── Controllers (API Endpoints)
│   ├── AuthController (Authentication/Authorization)
│   ├── PatientsController (Patient operations)
│   ├── DoctorsController (Doctor operations)
│   ├── AppointmentsController (Appointment management)
│   ├── MedicalRecordsController (Medical records)
│   ├── DepartmentsController (Department management)
│   └── DashboardController (Analytics/Statistics)
│
├── Services (Business Logic)
│   ├── AuthService
│   ├── PatientService
│   ├── DoctorService
│   ├── AppointmentService (conflict detection logic)
│   ├── MedicalRecordService
│   ├── DepartmentService
│   └── DashboardService (statistics queries)
│
├── Repositories (Data Access)
│   ├── IUnitOfWork (Coordinates all repositories)
│   ├── IRepository<T> (Generic CRUD operations)
│   ├── IPatientRepository (Patient-specific queries)
│   ├── IDoctorRepository (Doctor-specific queries)
│   ├── IAppointmentRepository (Appointment-specific queries including conflict detection)
│   ├── IMedicalRecordRepository
│   └── IDepartmentRepository
│
├── Models (Domain Entities)
│   ├── User (from ASP.NET Core Identity)
│   ├── Patient
│   ├── Doctor
│   ├── Appointment
│   ├── MedicalRecord
│   ├── Prescription
│   ├── Department
│   ├── DoctorSchedule
│   └── BaseEntity (common properties: Id, CreatedAt, UpdatedAt)
│
├── DTOs (Data Transfer Objects)
│   └── Separate DTOs for each entity with request/response variants
│
├── Validators (FluentValidation Rules)
│   ├── PatientValidator
│   ├── DoctorValidator
│   ├── AppointmentValidator
│   ├── AuthValidator
│   ├── MedicalRecordValidator
│   └── DepartmentValidator
│
├── Mapping (AutoMapper Profiles)
│   └── Entity ↔ DTO transformations
│
├── Middleware
│   └── ExceptionHandlingMiddleware (Global error handling)
│
└── Data (EF Core)
	└── AppDbContext (Entity configuration and relationships)
```

## Project Structure

```
Hospital Managment system/
├── Controllers/                    # API endpoint definitions
│   ├── AuthController.cs
│   ├── PatientsController.cs
│   ├── DoctorsController.cs
│   ├── AppointmentsController.cs
│   ├── MedicalRecordsController.cs
│   ├── DepartmentsController.cs
│   └── DashboardController.cs
│
├── Services/                       # Business logic implementation
│   ├── AuthService.cs
│   ├── PatientService.cs
│   ├── DoctorService.cs
│   ├── AppointmentService.cs
│   ├── MedicalRecordService.cs
│   ├── DepartmentService.cs
│   └── DashboardService.cs
│
├── Repositories/                   # Data access layer
│   ├── Repository.cs (Generic base)
│   ├── UnitOfWork.cs
│   ├── PatientRepository.cs
│   ├── DoctorRepository.cs
│   ├── AppointmentRepository.cs
│   ├── MedicalRecordRepository.cs
│   └── DepartmentRepository.cs
│
├── Interfaces/                     # Contracts for DI
│   ├── IUnitOfWork.cs
│   ├── IRepository.cs
│   ├── IAuthService.cs
│   ├── IPatientService.cs
│   ├── IDoctorService.cs
│   ├── IAppointmentService.cs
│   ├── IMedicalRecordService.cs
│   ├── IDashboardService.cs
│   └── [repository interfaces]
│
├── Models/                         # Domain entities
│   ├── BaseEntity.cs
│   ├── User.cs
│   ├── Patient.cs
│   ├── Doctor.cs
│   ├── Appointment.cs
│   ├── MedicalRecord.cs
│   ├── Prescription.cs
│   ├── Department.cs
│   └── DoctorSchedule.cs
│
├── DTOs/                          # Data Transfer Objects
│   ├── AuthDto.cs
│   ├── PatientDto.cs
│   ├── DoctorDto.cs
│   ├── AppointmentDto.cs
│   ├── MedicalRecordDto.cs
│   ├── DepartmentDto.cs
│   └── PrescriptionDto.cs
│
├── Validators/                    # FluentValidation rules
│   ├── AuthValidator.cs
│   ├── PatientValidator.cs
│   ├── DoctorValidator.cs
│   ├── AppointmentValidator.cs
│   ├── MedicalRecordValidator.cs
│   └── DepartmentValidator.cs
│
├── Mapping/                       # AutoMapper profiles
│   └── MappingProfile.cs
│
├── Middleware/                    # HTTP middleware
│   └── ExceptionHandlingMiddleware.cs
│
├── Helpers/                       # Utility functions
│   ├── ClaimsHelper.cs (JWT claims extraction)
│   └── PaginationHelper.cs
│
├── Data/                          # EF Core context
│   ├── AppDbContext.cs
│   └── AppDbContextFactory.cs
│
├── Migrations/                    # EF Core schema migrations
│   └── [migration files]
│
├── Program.cs                     # Startup configuration
├── appsettings.json              # Configuration
├── appsettings.Development.json  # Development overrides
└── Hospital Managment system.csproj  # Project file
```

## Key Engineering Decisions

### 1. **Unit of Work with Repository Pattern**
The codebase implements the Unit of Work pattern via `IUnitOfWork` to coordinate multiple repositories and ensure transactional consistency. Each repository (`IPatientRepository`, `IDoctorRepository`, etc.) inherits from the generic `IRepository<T>` base, providing standard CRUD operations while allowing specialized queries. This approach:
- Ensures atomic operations across multiple entities
- Provides `SaveChangesAsync()` and `BeginTransactionAsync()` for transaction control
- Decouples business logic from EF Core implementation details
- Simplifies unit testing through interface-based design

### 2. **Role-Based Access Control (RBAC)**
Authentication is handled via JWT tokens with roles embedded as claims. Controllers enforce role-based access using `[Authorize(Roles = "Admin,Doctor")]` attributes at the controller and action method levels. This ensures:
- Fine-grained permission control per endpoint
- Different operations restricted to different user types
- Admin-only operations (department creation, doctor management)
- Self-service patient operations

### 3. **Appointment Conflict Detection**
The `AppointmentService.BookAppointmentAsync()` method validates against double-booking via `_unitOfWork.Appointments.HasConflictAsync()`, which checks if a doctor has overlapping appointments based on appointment datetime and duration. This prevents scheduling errors at the business logic layer before database persistence, ensuring data consistency.

### 4. **FluentValidation Rules**
Input validation is centralized using FluentValidation fluent API. Each DTO has a dedicated validator (e.g., `PatientValidator`, `AppointmentValidator`) with rules for:
- Required field checks
- Format validation (phone numbers, blood groups)
- Length constraints on strings
- Conditional validation rules
- Clear, user-friendly error messages

This approach separates validation logic from controllers and enables reusability.

### 5. **AutoMapper for Entity-DTO Separation**
The codebase uses AutoMapper profiles to map between domain entities and DTOs, decoupling the API contract from database schemas. This allows:
- Selective property exposure in API responses
- Transformation logic (e.g., calculating doctor count in a department)
- Consistent mapping across all services
- Easier API versioning in the future

### 6. **Centralized Exception Handling Middleware**
A custom `ExceptionHandlingMiddleware` catches all unhandled exceptions and returns consistent `ApiResponseDto<T>` responses with appropriate HTTP status codes. This ensures:
- Consistent error response format across all endpoints
- Internal exceptions are not exposed to clients
- Detailed server-side logging via Serilog
- Graceful error recovery

### 7. **Structured Logging with Serilog**
Serilog is configured to write logs to both console (development) and rolling daily files with contextual information (application name, timestamp, enrichment). This enables:
- Audit trail for authentication events
- Debugging production issues
- Structured querying of logs
- Automatic log rotation by date

### 8. **Dependency Injection Container**
Services, repositories, and validators are registered in `Program.cs` using ASP.NET Core's built-in DI container with `AddScoped<IInterface, Implementation>()`. This promotes:
- Loose coupling between layers
- Testability through interface mocking
- Centralized configuration
- Lifecycle management (per-request scope)

### 9. **Generic Repository Base Implementation**
The `Repository<T>` base class implements common CRUD operations (`GetAllAsync()`, `GetByIdAsync()`, `AddAsync()`, `UpdateAsync()`, `DeleteAsync()`) for all entities. Specialized repositories override these as needed to add domain-specific queries (e.g., `GetPatientAppointmentsAsync()` in `AppointmentRepository`). This:
- Reduces code duplication
- Provides a consistent data access API
- Allows rapid entity addition without re-implementing CRUD

### 10. **Pagination Helper for Consistent Paging**
The `PaginationHelper` utility validates page numbers/sizes and calculates pagination metadata (`totalPages`, `skip count`). All list endpoints support pagination with `pageNumber` and `pageSize` query parameters, ensuring:
- Consistent pagination behavior across endpoints
- Protection against invalid page parameters
- Efficient database queries on large datasets

## Authentication & Authorization

### Registration & Login
**POST /api/auth/register**
- Registers a new user with email, password, and role
- Password hashing is handled by ASP.NET Core Identity
- Returns JWT access token and refresh token info upon success

**POST /api/auth/login**
- Authenticates user by email and password
- Issues JWT token with user claims (UserId, Email, Roles)
- Token includes expiry (default 60 minutes from `appsettings.json`)

### JWT Configuration
Tokens are signed using a symmetric key (configurable via `JwtSettings:SecretKey` in appsettings). Token validation includes:
- Signature verification
- Issuer validation (default: "HospitalManagementSystem")
- Audience validation (default: "HospitalManagementUsers")
- Lifetime validation with configurable expiry
- No clock skew allowed

### Authorization
Protected endpoints require a valid Bearer token in the `Authorization` header:
```
Authorization: Bearer <JWT_TOKEN>
```

Role-based authorization is enforced via `[Authorize(Roles = "...")]` attributes. For example:
- **Admin only**: Department creation/update/delete, doctor creation
- **Admin + Doctor**: Medical record creation, appointment notes
- **Admin + Receptionist**: Dashboard statistics
- **All authenticated users**: View own profile, book/view appointments

### Password Management
Users can change their password via **POST /api/auth/change-password** while authenticated. The endpoint requires the current password and validates the new password against configured complexity rules (minimum 8 characters, uppercase, lowercase, digit, special character).

## Database

### Technology
- **SQL Server LocalDB** (default for development)
- **Entity Framework Core 9** with SQL Server provider
- **ASP.NET Core Identity** for user/role management

### Connection String
Default connection string (from `appsettings.json`):
```
Server=(localdb)\mssqllocaldb;Database=HospitalManagementDb;Trusted_Connection=true;TrustServerCertificate=true;
```

### Key Entities & Relationships

**User** (ASP.NET Core Identity - IdentityUser)
- 1:1 → Patient (optional)
- 1:1 → Doctor (optional)
- Roles assigned via AspNetUserRoles table

**Patient**
- 1:1 ← User
- 1:N → Appointment
- 1:N → MedicalRecord
- Contains: patientNumber, bloodGroup, allergies, medicalHistory, emergencyContact, insurance details

**Doctor**
- 1:1 ← User
- N:1 → Department
- 1:N → Appointment
- 1:N → MedicalRecord
- 1:N → DoctorSchedule
- Contains: licenseNumber, specialization, yearsOfExperience, qualifications, consultationFee, isAvailable

**Appointment**
- N:1 → Patient
- N:1 → Doctor
- 1:1 ← MedicalRecord (optional)
- Contains: appointmentDateTime, status, reasonForVisit, notes, appointmentType, durationInMinutes

**MedicalRecord**
- N:1 → Patient
- N:1 → Doctor
- 0:1 → Appointment (nullable)
- 1:N → Prescription
- Contains: diagnosis, visitNotes, symptoms, physicalExamFindings, treatmentPlan

**Prescription**
- N:1 → MedicalRecord
- Contains: medicationName, dosage, frequency, duration, quantity, specialInstructions

**Department**
- 1:N → Doctor
- Contains: name, description, contactNumber, email

**DoctorSchedule**
- N:1 → Doctor
- Contains: dayOfWeek, startTime, endTime (defines doctor availability windows)

### Migrations
EF Core migrations are version-controlled in the `Migrations/` folder. The initial migration (`20260704162246_Initial.cs`) creates all tables, relationships, and constraints.

On application startup (in `Program.cs`), the migration is automatically applied:
```csharp
dbContext.Database.Migrate();
```

### Seed Data
On first run, the application seeds:
- System roles: Admin, Doctor, Patient, Receptionist
- Default admin user: `admin@hospital.com` / `Admin@123456`
- Five default departments: Cardiology, Neurology, Orthopedics, Pediatrics, General Surgery
- Sample doctors with appropriate roles and departments

## API

The API is RESTful and organized by resource. All endpoints return a consistent `ApiResponseDto<T>` structure:

```json
{
  "success": true,
  "message": "Operation successful",
  "data": { /* entity or list */ },
  "statusCode": 200,
  "error": null
}
```

### Key Endpoints

**Authentication**
- `POST /api/auth/register` - Register a new user
- `POST /api/auth/login` - Login and get JWT token
- `POST /api/auth/change-password` - Change password (authenticated)

**Patients**
- `GET /api/patients` `[Admin, Doctor, Receptionist]` - List patients (paginated)
- `GET /api/patients/{id}` - Get patient details
- `POST /api/patients` `[Admin, Receptionist]` - Create patient
- `PUT /api/patients/{id}` `[Admin, Doctor, Receptionist]` - Update patient
- `DELETE /api/patients/{id}` `[Admin, Receptionist]` - Delete patient
- `GET /api/patients/{id}/medical-history` - Get patient's medical records

**Doctors**
- `GET /api/doctors` - List doctors (paginated)
- `GET /api/doctors/{id}` - Get doctor details
- `POST /api/doctors` `[Admin]` - Create doctor
- `PUT /api/doctors/{id}` `[Admin, Doctor]` - Update doctor
- `DELETE /api/doctors/{id}` `[Admin]` - Delete doctor
- `GET /api/doctors/search` - Search doctors by specialization/name

**Appointments**
- `POST /api/appointments` - Book appointment (includes conflict detection)
- `GET /api/appointments` `[Admin, Doctor, Receptionist]` - List all appointments (paginated)
- `GET /api/appointments/{id}` - Get appointment details
- `PUT /api/appointments/{id}/reschedule` `[Admin, Doctor]` - Reschedule appointment
- `PUT /api/appointments/{id}/cancel` `[Admin, Doctor, Receptionist]` - Cancel appointment
- `GET /api/appointments/patient/{patientId}` - Get patient's appointments
- `GET /api/appointments/doctor/{doctorId}` `[Admin, Doctor, Receptionist]` - Get doctor's appointments

**Medical Records**
- `POST /api/medical-records` `[Admin, Doctor]` - Create medical record
- `GET /api/medical-records/{id}` - Get medical record
- `GET /api/medical-records/patient/{patientId}` `[Admin, Doctor, Receptionist]` - Get patient's records
- `PUT /api/medical-records/{id}` `[Admin, Doctor]` - Update medical record

**Departments**
- `GET /api/departments` - List departments (paginated)
- `GET /api/departments/{id}` - Get department details
- `POST /api/departments` `[Admin]` - Create department
- `PUT /api/departments/{id}` `[Admin]` - Update department
- `DELETE /api/departments/{id}` `[Admin]` - Delete department

**Dashboard** `[Admin, Receptionist]`
- `GET /api/dashboard/statistics` - Get summary statistics
- `GET /api/dashboard/total-patients` - Total patient count
- `GET /api/dashboard/total-doctors` - Total doctor count
- `GET /api/dashboard/today-appointments` - Today's count and list
- `GET /api/dashboard/upcoming-appointments` - Upcoming appointments

### OpenAPI Documentation
In development environment, interactive API documentation is available:
- **Scalar UI**: Access at `https://localhost:5001/scalar/v1` (after running the application)
- Shows all endpoints with request/response schemas
- Includes authentication mechanism for testing

All public endpoints have XML documentation comments explaining parameters, return types, and authorization requirements.

## Validation

Input validation is enforced via FluentValidation rules applied before business logic execution. Examples:

**PatientValidator**
- Blood group must be valid (A+, A-, B+, B-, O+, O-, AB+, AB-)
- Emergency contact number format validation (regex)
- Maximum lengths for text fields

**AppointmentValidator**
- Appointment datetime must be in the future
- Duration must be positive integer
- Patient and doctor IDs must exist

**AuthValidator (Register)**
- Email format validation
- Password complexity requirements (implemented in Identity)
- Phone number format optional but validated when present

Validation errors are returned as HTTP 400 with detailed error messages aggregated from all failed rules.

## Running Locally

### Prerequisites
- **.NET 10 SDK** installed ([download](https://dotnet.microsoft.com/download))
- **SQL Server LocalDB** (included with Visual Studio or [downloadable separately](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb))
- **Visual Studio 2022** (recommended) or any .NET-capable editor

### Setup Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/ammarhisham97-rgb/Hospital-Managment-.git
   cd "Hospital Managment system"
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Apply database migrations** (automatic on startup, or manual via)
   ```bash
   dotnet ef database update
   ```
   Or from Package Manager Console in Visual Studio:
   ```powershell
   Update-Database
   ```

4. **Update appsettings if needed**
   - Edit `appsettings.json` to change connection string, JWT secret, or other settings
   - Development-specific settings can go in `appsettings.Development.json`

5. **Run the application**
   ```bash
   dotnet run
   ```
   Or press `F5` in Visual Studio.

6. **Access the API**
   - API runs on: `https://localhost:5001` (HTTPS) or `http://localhost:5000` (HTTP)
   - Test via provided HTTP requests file: `Hospital Managment system.http`
   - Or use Postman/Insomnia to test endpoints (see **API Documentation** section below)

### Seed Data
The application automatically creates on first run:
- System roles (Admin, Doctor, Patient, Receptionist)
- Admin user: `admin@hospital.com` / `Admin@123456`
- Five departments

### Database Reset
To reset the database:
1. Delete the `HospitalManagementDb.mdf` file from your LocalDB instance folder
2. Re-run the application to recreate the database with seed data

Or via command line:
```bash
dotnet ef database drop
dotnet ef database update
```

## Configuration

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
	"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=HospitalManagementDb;Trusted_Connection=true;TrustServerCertificate=true;"
  },
  "JwtSettings": {
	"SecretKey": "your-super-secret-key-that-is-at-least-32-characters-long-for-security",
	"Issuer": "HospitalManagementSystem",
	"Audience": "HospitalManagementUsers",
	"ExpiryMinutes": 60
  },
  "AllowedHosts": "*"
}
```

### Key Configuration Points

| Setting | Purpose | Default | Notes |
|---------|---------|---------|-------|
| `ConnectionStrings:DefaultConnection` | Database connection | LocalDB | Update for SQL Server |
| `JwtSettings:SecretKey` | JWT signing key | (placeholder) | **Change in production** (min 32 chars) |
| `JwtSettings:Issuer` | Token issuer claim | HospitalManagementSystem | Affects token validation |
| `JwtSettings:Audience` | Token audience claim | HospitalManagementUsers | Affects token validation |
| `JwtSettings:ExpiryMinutes` | Token lifetime | 60 | In minutes |
| `Logging:LogLevel:Default` | Default log level | Information | Set to Debug for verbose logging |

### Environment-Specific Configuration
Create `appsettings.Production.json` for production overrides:
- Use production database server
- Change JWT secret to a secure value
- Update CORS policy from `AllowAll`
- Disable detailed error messages

## API Documentation

### Interactive Documentation (Development)
The application includes **Scalar** (modern OpenAPI UI) available at:
```
https://localhost:5001/scalar/v1
```

Features:
- Browse all endpoints with full documentation
- See request/response schemas
- Test endpoints directly with authentication
- View HTTP status codes and error responses

### Testing Endpoints
Use the included `.http` file or any REST client:

**Postman / Insomnia**
1. Create a request to `POST https://localhost:5001/api/auth/login`
2. Body (JSON):
   ```json
   {
	 "email": "admin@hospital.com",
	 "password": "Admin@123456"
   }
   ```
3. Copy the `accessToken` from the response
4. Set header `Authorization: Bearer <TOKEN>` on all subsequent requests

**Visual Studio Code (.http file)**
- Open `Hospital Managment system.http`
- Click "Send Request" on any endpoint (requires REST Client extension)

## Engineering Focus

This project demonstrates:

1. **Clean Architecture** - Clear separation between Controllers, Services, Repositories, and Entities
2. **SOLID Principles**:
   - **S**ingleton/Scoped lifetimes for dependency injection
   - **O**pen for extension via interfaces and inheritance
   - **L**iskov substitution via consistent repository contracts
   - **I**nterface segregation with focused interfaces
   - **D**ependency inversion through DI container registration

3. **Data Access Patterns** - Unit of Work and Repository patterns for testable, maintainable data access

4. **Input Validation** - Fluent, centralized validation rules separate from controllers

5. **Exception Handling** - Global middleware for consistent error responses and operational visibility

6. **Security** - Role-based access control, password hashing, JWT authentication with configurable expiry

7. **Logging & Observability** - Structured logging with Serilog, file rotation, contextual enrichment

8. **Scalability Considerations** - Pagination support, conflict detection logic at service layer, query optimization via repository-specific methods

9. **Real-World Business Logic** - Appointment double-booking prevention, medical record association, prescription management

## Author

**Ammar Hisham** — Backend .NET Developer  
[GitHub](https://github.com/ammarhisham97-rgb) | [Repository](https://github.com/ammarhisham97-rgb/Hospital-Managment-)
