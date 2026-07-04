# Hospital Management System - Frontend Integration Guide

## Table of Contents
1. [API Base URL & Configuration](#api-base-url--configuration)
2. [Authentication Endpoints](#authentication-endpoints)
3. [Patient Endpoints](#patient-endpoints)
4. [Doctor Endpoints](#doctor-endpoints)
5. [Department Endpoints](#department-endpoints)
6. [Appointment Endpoints](#appointment-endpoints)
7. [Medical Records Endpoints](#medical-records-endpoints)
8. [Dashboard Endpoints](#dashboard-endpoints)
9. [Response Format](#response-format)
10. [Error Handling](#error-handling)
11. [JavaScript Client Implementation](#javascript-client-implementation)

---

## API Base URL & Configuration

### Development Environment
```
Base URL: http://localhost:5000
API Version: v1
```

### API Response Header
```
Content-Type: application/json
Authorization: Bearer {token} (for protected endpoints)
```

---

## Authentication Endpoints

### 1. Register New User

**Endpoint:** `POST /api/auth/register`

**Description:** Create a new user account (Patient, Doctor, or Admin)

**Request Body:**
```json
{
  "fullName": "John Doe",
  "email": "john@example.com",
  "phoneNumber": "+1234567890",
  "password": "SecurePass123!",
  "confirmPassword": "SecurePass123!",
  "dateOfBirth": "1990-01-15",
  "address": "123 Main Street",
  "city": "New York",
  "state": "NY",
  "postalCode": "10001",
  "country": "USA",
  "role": "Patient"
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Registration successful",
  "data": {
	"userId": "abc123def456",
	"token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
	"refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
	"email": "john@example.com",
	"fullName": "John Doe",
	"role": "Patient"
  },
  "statusCode": 200
}
```

**Error Response (400 Bad Request):**
```json
{
  "success": false,
  "message": "Validation failed",
  "error": "Email already exists, Password must be at least 8 characters",
  "statusCode": 400
}
```

---

### 2. Login User

**Endpoint:** `POST /api/auth/login`

**Description:** Authenticate user and receive JWT token

**Request Body:**
```json
{
  "email": "john@example.com",
  "password": "SecurePass123!"
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Login successful",
  "data": {
	"userId": "abc123def456",
	"token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
	"refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
	"email": "john@example.com",
	"fullName": "John Doe",
	"role": "Patient"
  },
  "statusCode": 200
}
```

**Error Response (401 Unauthorized):**
```json
{
  "success": false,
  "message": "Login failed",
  "error": "Invalid email or password",
  "statusCode": 401
}
```

---

### 3. Refresh Token

**Endpoint:** `POST /api/auth/refresh`

**Description:** Get a new JWT token using refresh token

**Request Body:**
```json
{
  "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Token refreshed successfully",
  "data": {
	"token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
	"refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
  },
  "statusCode": 200
}
```

---

### 4. Logout

**Endpoint:** `POST /api/auth/logout`

**Description:** Invalidate user session

**Headers:**
```
Authorization: Bearer {token}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Logout successful",
  "statusCode": 200
}
```

---

### 5. Change Password

**Endpoint:** `POST /api/auth/change-password`

**Description:** Change user password

**Headers:**
```
Authorization: Bearer {token}
```

**Request Body:**
```json
{
  "currentPassword": "OldPass123!",
  "newPassword": "NewPass123!",
  "confirmPassword": "NewPass123!"
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Password changed successfully",
  "statusCode": 200
}
```

---

## Patient Endpoints

### 1. Get All Patients (Admin/Doctor/Receptionist)

**Endpoint:** `GET /api/patients?pageNumber=1&pageSize=10`

**Description:** Retrieve all patients with pagination

**Headers:**
```
Authorization: Bearer {token}
```

**Query Parameters:**
- `pageNumber` (integer, default: 1): Page number for pagination
- `pageSize` (integer, default: 10): Number of records per page

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Patients retrieved successfully",
  "data": {
	"pageNumber": 1,
	"pageSize": 10,
	"totalCount": 50,
	"totalPages": 5,
	"items": [
	  {
		"id": 1,
		"userId": "user123",
		"patientNumber": "PAT001",
		"fullName": "John Doe",
		"email": "john@example.com",
		"phoneNumber": "+1234567890",
		"dateOfBirth": "1990-01-15",
		"bloodGroup": "O+",
		"allergies": "Penicillin",
		"medicalHistory": "Asthma, Hypertension",
		"emergencyContactName": "Jane Doe",
		"emergencyContactPhone": "+1987654321",
		"insuranceProvider": "BlueCross",
		"insurancePolicyNumber": "BC123456",
		"address": "123 Main Street",
		"city": "New York",
		"state": "NY",
		"postalCode": "10001",
		"country": "USA",
		"createdAt": "2024-01-15T10:00:00Z",
		"updatedAt": "2024-01-15T10:00:00Z"
	  }
	]
  },
  "statusCode": 200
}
```

---

### 2. Get Patient By ID

**Endpoint:** `GET /api/patients/{id}`

**Description:** Retrieve specific patient details

**Headers:**
```
Authorization: Bearer {token}
```

**URL Parameters:**
- `id` (integer): Patient ID

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Patient retrieved successfully",
  "data": {
	"id": 1,
	"userId": "user123",
	"patientNumber": "PAT001",
	"fullName": "John Doe",
	"email": "john@example.com",
	"phoneNumber": "+1234567890",
	"dateOfBirth": "1990-01-15",
	"bloodGroup": "O+",
	"allergies": "Penicillin",
	"medicalHistory": "Asthma, Hypertension",
	"emergencyContactName": "Jane Doe",
	"emergencyContactPhone": "+1987654321",
	"insuranceProvider": "BlueCross",
	"insurancePolicyNumber": "BC123456",
	"address": "123 Main Street",
	"city": "New York",
	"state": "NY",
	"postalCode": "10001",
	"country": "USA",
	"createdAt": "2024-01-15T10:00:00Z",
	"updatedAt": "2024-01-15T10:00:00Z"
  },
  "statusCode": 200
}
```

---

### 3. Update Patient Information

**Endpoint:** `PUT /api/patients/{id}`

**Description:** Update patient details

**Headers:**
```
Authorization: Bearer {token}
```

**URL Parameters:**
- `id` (integer): Patient ID

**Request Body:**
```json
{
  "bloodGroup": "AB+",
  "allergies": "Penicillin, Aspirin",
  "medicalHistory": "Asthma, Hypertension, Diabetes",
  "emergencyContactName": "Jane Doe",
  "emergencyContactPhone": "+1987654321",
  "insuranceProvider": "Aetna",
  "insurancePolicyNumber": "AET654321"
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Patient updated successfully",
  "data": {
	"id": 1,
	"userId": "user123",
	"patientNumber": "PAT001",
	"fullName": "John Doe",
	"email": "john@example.com",
	"phoneNumber": "+1234567890",
	"dateOfBirth": "1990-01-15",
	"bloodGroup": "AB+",
	"allergies": "Penicillin, Aspirin",
	"medicalHistory": "Asthma, Hypertension, Diabetes",
	"emergencyContactName": "Jane Doe",
	"emergencyContactPhone": "+1987654321",
	"insuranceProvider": "Aetna",
	"insurancePolicyNumber": "AET654321",
	"address": "123 Main Street",
	"city": "New York",
	"state": "NY",
	"postalCode": "10001",
	"country": "USA",
	"createdAt": "2024-01-15T10:00:00Z",
	"updatedAt": "2024-01-15T11:30:00Z"
  },
  "statusCode": 200
}
```

---

### 4. Get Patient's Appointments

**Endpoint:** `GET /api/patients/{id}/appointments`

**Description:** Retrieve all appointments for a patient

**Headers:**
```
Authorization: Bearer {token}
```

**URL Parameters:**
- `id` (integer): Patient ID

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Appointments retrieved successfully",
  "data": [
	{
	  "id": 5,
	  "patientId": 1,
	  "patientName": "John Doe",
	  "doctorId": 2,
	  "doctorName": "Dr. Smith",
	  "appointmentDateTime": "2024-02-20T14:30:00Z",
	  "reasonForVisit": "Regular checkup",
	  "status": "Scheduled",
	  "notes": "Patient has diabetes",
	  "appointmentType": "Clinical",
	  "durationInMinutes": 30,
	  "createdAt": "2024-01-15T10:00:00Z",
	  "updatedAt": "2024-01-15T10:00:00Z"
	}
  ],
  "statusCode": 200
}
```

---

### 5. Get Patient's Medical Records

**Endpoint:** `GET /api/patients/{id}/medical-records`

**Description:** Retrieve all medical records for a patient

**Headers:**
```
Authorization: Bearer {token}
```

**URL Parameters:**
- `id` (integer): Patient ID

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Medical records retrieved successfully",
  "data": [
	{
	  "id": 3,
	  "patientId": 1,
	  "patientName": "John Doe",
	  "doctorId": 2,
	  "doctorName": "Dr. Smith",
	  "appointmentId": 5,
	  "diagnosis": "Type 2 Diabetes Mellitus",
	  "visitNotes": "Patient presents with elevated blood sugar",
	  "symptoms": "Fatigue, Increased thirst",
	  "physicalExamination": "Weight: 85kg, BP: 145/90",
	  "labTestResults": "HbA1c: 8.2%",
	  "treatmentPlan": "Start Metformin 500mg twice daily",
	  "prescriptions": [
		{
		  "id": 1,
		  "medicalRecordId": 3,
		  "medicationName": "Metformin",
		  "dosage": "500mg",
		  "frequency": "Twice daily",
		  "duration": "30 days",
		  "instructions": "Take with meals",
		  "quantity": 60,
		  "refills": 3,
		  "createdAt": "2024-01-15T10:00:00Z"
		}
	  ],
	  "createdAt": "2024-01-15T10:00:00Z",
	  "updatedAt": "2024-01-15T10:00:00Z"
	}
  ],
  "statusCode": 200
}
```

---

### 6. Delete Patient (Admin Only)

**Endpoint:** `DELETE /api/patients/{id}`

**Description:** Soft delete a patient (mark as deleted)

**Headers:**
```
Authorization: Bearer {token}
```

**URL Parameters:**
- `id` (integer): Patient ID

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Patient deleted successfully",
  "statusCode": 200
}
```

---

## Doctor Endpoints

### 1. Get All Doctors

**Endpoint:** `GET /api/doctors?pageNumber=1&pageSize=10`

**Description:** Retrieve all doctors with pagination (no authentication required)

**Query Parameters:**
- `pageNumber` (integer, default: 1): Page number for pagination
- `pageSize` (integer, default: 10): Number of records per page

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Doctors retrieved successfully",
  "data": {
	"pageNumber": 1,
	"pageSize": 10,
	"totalCount": 25,
	"totalPages": 3,
	"items": [
	  {
		"id": 2,
		"userId": "doc123",
		"fullName": "Dr. Michael Smith",
		"email": "smith@hospital.com",
		"phoneNumber": "+1234567890",
		"licenseNumber": "MD123456",
		"specialization": "Cardiology",
		"yearsOfExperience": 15,
		"departmentId": 1,
		"departmentName": "Cardiology",
		"qualifications": "MD, Board Certified",
		"consultationFee": 150.00,
		"isAvailable": true,
		"address": "456 Medical Plaza",
		"city": "New York",
		"state": "NY",
		"postalCode": "10001",
		"country": "USA",
		"createdAt": "2024-01-01T10:00:00Z",
		"updatedAt": "2024-01-15T10:00:00Z"
	  }
	]
  },
  "statusCode": 200
}
```

---

### 2. Get Doctor By ID

**Endpoint:** `GET /api/doctors/{id}`

**Description:** Retrieve specific doctor details (no authentication required)

**URL Parameters:**
- `id` (integer): Doctor ID

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Doctor retrieved successfully",
  "data": {
	"id": 2,
	"userId": "doc123",
	"fullName": "Dr. Michael Smith",
	"email": "smith@hospital.com",
	"phoneNumber": "+1234567890",
	"licenseNumber": "MD123456",
	"specialization": "Cardiology",
	"yearsOfExperience": 15,
	"departmentId": 1,
	"departmentName": "Cardiology",
	"qualifications": "MD, Board Certified",
	"consultationFee": 150.00,
	"isAvailable": true,
	"address": "456 Medical Plaza",
	"city": "New York",
	"state": "NY",
	"postalCode": "10001",
	"country": "USA",
	"createdAt": "2024-01-01T10:00:00Z",
	"updatedAt": "2024-01-15T10:00:00Z"
  },
  "statusCode": 200
}
```

---

### 3. Update Doctor Information (Admin/Doctor)

**Endpoint:** `PUT /api/doctors/{id}`

**Description:** Update doctor details

**Headers:**
```
Authorization: Bearer {token}
```

**URL Parameters:**
- `id` (integer): Doctor ID

**Request Body:**
```json
{
  "licenseNumber": "MD123456",
  "specialization": "Cardiology",
  "yearsOfExperience": 16,
  "departmentId": 1,
  "qualifications": "MD, Board Certified, Interventional Cardiology",
  "consultationFee": 175.00,
  "isAvailable": true
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Doctor updated successfully",
  "data": {
	"id": 2,
	"userId": "doc123",
	"fullName": "Dr. Michael Smith",
	"email": "smith@hospital.com",
	"phoneNumber": "+1234567890",
	"licenseNumber": "MD123456",
	"specialization": "Cardiology",
	"yearsOfExperience": 16,
	"departmentId": 1,
	"departmentName": "Cardiology",
	"qualifications": "MD, Board Certified, Interventional Cardiology",
	"consultationFee": 175.00,
	"isAvailable": true,
	"address": "456 Medical Plaza",
	"city": "New York",
	"state": "NY",
	"postalCode": "10001",
	"country": "USA",
	"createdAt": "2024-01-01T10:00:00Z",
	"updatedAt": "2024-01-15T11:30:00Z"
  },
  "statusCode": 200
}
```

---

### 4. Get Doctors By Department

**Endpoint:** `GET /api/doctors/department/{departmentId}`

**Description:** Get all doctors in a specific department

**URL Parameters:**
- `departmentId` (integer): Department ID

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Doctors retrieved successfully",
  "data": [
	{
	  "id": 2,
	  "userId": "doc123",
	  "fullName": "Dr. Michael Smith",
	  "email": "smith@hospital.com",
	  "phoneNumber": "+1234567890",
	  "licenseNumber": "MD123456",
	  "specialization": "Cardiology",
	  "yearsOfExperience": 15,
	  "departmentId": 1,
	  "departmentName": "Cardiology",
	  "qualifications": "MD, Board Certified",
	  "consultationFee": 150.00,
	  "isAvailable": true,
	  "address": "456 Medical Plaza",
	  "city": "New York",
	  "state": "NY",
	  "postalCode": "10001",
	  "country": "USA",
	  "createdAt": "2024-01-01T10:00:00Z",
	  "updatedAt": "2024-01-15T10:00:00Z"
	}
  ],
  "statusCode": 200
}
```

---

### 5. Get Doctor's Schedule

**Endpoint:** `GET /api/doctors/{id}/schedule`

**Description:** Get doctor's availability schedule

**Headers:**
```
Authorization: Bearer {token}
```

**URL Parameters:**
- `id` (integer): Doctor ID

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Schedule retrieved successfully",
  "data": [
	{
	  "id": 1,
	  "doctorId": 2,
	  "dayOfWeek": "Monday",
	  "startTime": "09:00",
	  "endTime": "17:00",
	  "isAvailable": true,
	  "createdAt": "2024-01-01T10:00:00Z"
	}
  ],
  "statusCode": 200
}
```

---

### 6. Delete Doctor (Admin Only)

**Endpoint:** `DELETE /api/doctors/{id}`

**Description:** Soft delete a doctor (mark as deleted)

**Headers:**
```
Authorization: Bearer {token}
```

**URL Parameters:**
- `id` (integer): Doctor ID

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Doctor deleted successfully",
  "statusCode": 200
}
```

---

## Department Endpoints

### 1. Get All Departments

**Endpoint:** `GET /api/departments?pageNumber=1&pageSize=10`

**Description:** Retrieve all departments (no authentication required)

**Query Parameters:**
- `pageNumber` (integer, default: 1): Page number for pagination
- `pageSize` (integer, default: 10): Number of records per page

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Departments retrieved successfully",
  "data": {
	"pageNumber": 1,
	"pageSize": 10,
	"totalCount": 8,
	"totalPages": 1,
	"items": [
	  {
		"id": 1,
		"name": "Cardiology",
		"description": "Heart and cardiovascular system specialization",
		"contactNumber": "+1234567890",
		"email": "cardiology@hospital.com",
		"doctorCount": 5,
		"createdAt": "2024-01-01T10:00:00Z",
		"updatedAt": "2024-01-15T10:00:00Z"
	  }
	]
  },
  "statusCode": 200
}
```

---

### 2. Get Department By ID

**Endpoint:** `GET /api/departments/{id}`

**Description:** Retrieve specific department details

**URL Parameters:**
- `id` (integer): Department ID

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Department retrieved successfully",
  "data": {
	"id": 1,
	"name": "Cardiology",
	"description": "Heart and cardiovascular system specialization",
	"contactNumber": "+1234567890",
	"email": "cardiology@hospital.com",
	"doctorCount": 5,
	"createdAt": "2024-01-01T10:00:00Z",
	"updatedAt": "2024-01-15T10:00:00Z"
  },
  "statusCode": 200
}
```

---

### 3. Create New Department (Admin Only)

**Endpoint:** `POST /api/departments`

**Description:** Create a new department

**Headers:**
```
Authorization: Bearer {token}
```

**Request Body:**
```json
{
  "name": "Orthopedics",
  "description": "Bone, joint and muscle specialization",
  "contactNumber": "+1234567890",
  "email": "orthopedics@hospital.com"
}
```

**Response (201 Created):**
```json
{
  "success": true,
  "message": "Department created successfully",
  "data": {
	"id": 9,
	"name": "Orthopedics",
	"description": "Bone, joint and muscle specialization",
	"contactNumber": "+1234567890",
	"email": "orthopedics@hospital.com",
	"doctorCount": 0,
	"createdAt": "2024-01-15T12:00:00Z",
	"updatedAt": "2024-01-15T12:00:00Z"
  },
  "statusCode": 201
}
```

---

### 4. Update Department (Admin Only)

**Endpoint:** `PUT /api/departments/{id}`

**Description:** Update department details

**Headers:**
```
Authorization: Bearer {token}
```

**URL Parameters:**
- `id` (integer): Department ID

**Request Body:**
```json
{
  "name": "Cardiology",
  "description": "Advanced heart and cardiovascular system specialization",
  "contactNumber": "+1234567890",
  "email": "cardiology@hospital.com"
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Department updated successfully",
  "data": {
	"id": 1,
	"name": "Cardiology",
	"description": "Advanced heart and cardiovascular system specialization",
	"contactNumber": "+1234567890",
	"email": "cardiology@hospital.com",
	"doctorCount": 5,
	"createdAt": "2024-01-01T10:00:00Z",
	"updatedAt": "2024-01-15T12:30:00Z"
  },
  "statusCode": 200
}
```

---

### 5. Delete Department (Admin Only)

**Endpoint:** `DELETE /api/departments/{id}`

**Description:** Soft delete a department

**Headers:**
```
Authorization: Bearer {token}
```

**URL Parameters:**
- `id` (integer): Department ID

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Department deleted successfully",
  "statusCode": 200
}
```

---

## Appointment Endpoints

### 1. Book Appointment

**Endpoint:** `POST /api/appointments`

**Description:** Create a new appointment

**Headers:**
```
Authorization: Bearer {token}
```

**Request Body:**
```json
{
  "patientId": 1,
  "doctorId": 2,
  "appointmentDateTime": "2024-02-20T14:30:00Z",
  "reasonForVisit": "Regular checkup and diabetes monitoring",
  "appointmentType": "Clinical",
  "durationInMinutes": 30
}
```

**Response (201 Created):**
```json
{
  "success": true,
  "message": "Appointment booked successfully",
  "data": {
	"id": 5,
	"patientId": 1,
	"patientName": "John Doe",
	"doctorId": 2,
	"doctorName": "Dr. Michael Smith",
	"appointmentDateTime": "2024-02-20T14:30:00Z",
	"reasonForVisit": "Regular checkup and diabetes monitoring",
	"status": "Scheduled",
	"notes": null,
	"appointmentType": "Clinical",
	"durationInMinutes": 30,
	"createdAt": "2024-01-15T10:00:00Z",
	"updatedAt": "2024-01-15T10:00:00Z"
  },
  "statusCode": 201
}
```

---

### 2. Get All Appointments (Admin/Doctor/Receptionist)

**Endpoint:** `GET /api/appointments?pageNumber=1&pageSize=10`

**Description:** Retrieve all appointments with pagination

**Headers:**
```
Authorization: Bearer {token}
```

**Query Parameters:**
- `pageNumber` (integer, default: 1): Page number for pagination
- `pageSize` (integer, default: 10): Number of records per page

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Appointments retrieved successfully",
  "data": {
	"pageNumber": 1,
	"pageSize": 10,
	"totalCount": 45,
	"totalPages": 5,
	"items": [
	  {
		"id": 5,
		"patientId": 1,
		"patientName": "John Doe",
		"doctorId": 2,
		"doctorName": "Dr. Michael Smith",
		"appointmentDateTime": "2024-02-20T14:30:00Z",
		"reasonForVisit": "Regular checkup and diabetes monitoring",
		"status": "Scheduled",
		"notes": null,
		"appointmentType": "Clinical",
		"durationInMinutes": 30,
		"createdAt": "2024-01-15T10:00:00Z",
		"updatedAt": "2024-01-15T10:00:00Z"
	  }
	]
  },
  "statusCode": 200
}
```

---

### 3. Get Appointment By ID

**Endpoint:** `GET /api/appointments/{id}`

**Description:** Retrieve specific appointment details

**Headers:**
```
Authorization: Bearer {token}
```

**URL Parameters:**
- `id` (integer): Appointment ID

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Appointment retrieved successfully",
  "data": {
	"id": 5,
	"patientId": 1,
	"patientName": "John Doe",
	"doctorId": 2,
	"doctorName": "Dr. Michael Smith",
	"appointmentDateTime": "2024-02-20T14:30:00Z",
	"reasonForVisit": "Regular checkup and diabetes monitoring",
	"status": "Scheduled",
	"notes": null,
	"appointmentType": "Clinical",
	"durationInMinutes": 30,
	"createdAt": "2024-01-15T10:00:00Z",
	"updatedAt": "2024-01-15T10:00:00Z"
  },
  "statusCode": 200
}
```

---

### 4. Update Appointment

**Endpoint:** `PUT /api/appointments/{id}`

**Description:** Update appointment details or status

**Headers:**
```
Authorization: Bearer {token}
```

**URL Parameters:**
- `id` (integer): Appointment ID

**Request Body:**
```json
{
  "appointmentDateTime": "2024-02-21T15:00:00Z",
  "reasonForVisit": "Follow-up consultation",
  "status": "Scheduled",
  "notes": "Patient was instructed to fast before appointment",
  "durationInMinutes": 45
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Appointment updated successfully",
  "data": {
	"id": 5,
	"patientId": 1,
	"patientName": "John Doe",
	"doctorId": 2,
	"doctorName": "Dr. Michael Smith",
	"appointmentDateTime": "2024-02-21T15:00:00Z",
	"reasonForVisit": "Follow-up consultation",
	"status": "Scheduled",
	"notes": "Patient was instructed to fast before appointment",
	"appointmentType": "Clinical",
	"durationInMinutes": 45,
	"createdAt": "2024-01-15T10:00:00Z",
	"updatedAt": "2024-01-15T13:00:00Z"
  },
  "statusCode": 200
}
```

---

### 5. Cancel Appointment

**Endpoint:** `PUT /api/appointments/{id}/cancel`

**Description:** Cancel an appointment

**Headers:**
```
Authorization: Bearer {token}
```

**URL Parameters:**
- `id` (integer): Appointment ID

**Request Body:**
```json
{
  "cancellationReason": "Doctor emergency"
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Appointment cancelled successfully",
  "statusCode": 200
}
```

---

### 6. Get Doctor's Available Slots

**Endpoint:** `GET /api/appointments/doctor/{doctorId}/available-slots?date=2024-02-20`

**Description:** Get available appointment slots for a doctor on a specific date

**URL Parameters:**
- `doctorId` (integer): Doctor ID

**Query Parameters:**
- `date` (string, format: YYYY-MM-DD): Date to check availability

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Available slots retrieved successfully",
  "data": [
	{
	  "time": "09:00",
	  "available": true
	},
	{
	  "time": "09:30",
	  "available": true
	},
	{
	  "time": "10:00",
	  "available": false
	},
	{
	  "time": "10:30",
	  "available": true
	}
  ],
  "statusCode": 200
}
```

---

### 7. Delete Appointment

**Endpoint:** `DELETE /api/appointments/{id}`

**Description:** Soft delete an appointment

**Headers:**
```
Authorization: Bearer {token}
```

**URL Parameters:**
- `id` (integer): Appointment ID

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Appointment deleted successfully",
  "statusCode": 200
}
```

---

## Medical Records Endpoints

### 1. Get All Medical Records (Admin/Doctor)

**Endpoint:** `GET /api/medicalrecords?pageNumber=1&pageSize=10`

**Description:** Retrieve all medical records with pagination

**Headers:**
```
Authorization: Bearer {token}
```

**Query Parameters:**
- `pageNumber` (integer, default: 1): Page number for pagination
- `pageSize` (integer, default: 10): Number of records per page

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Medical records retrieved successfully",
  "data": {
	"pageNumber": 1,
	"pageSize": 10,
	"totalCount": 30,
	"totalPages": 3,
	"items": [
	  {
		"id": 3,
		"patientId": 1,
		"patientName": "John Doe",
		"doctorId": 2,
		"doctorName": "Dr. Michael Smith",
		"appointmentId": 5,
		"diagnosis": "Type 2 Diabetes Mellitus",
		"visitNotes": "Patient presents with elevated blood sugar levels",
		"symptoms": "Fatigue, Increased thirst, Frequent urination",
		"physicalExamination": "Weight: 85kg, Blood Pressure: 145/90, Heart Rate: 78",
		"labTestResults": "HbA1c: 8.2%, Fasting Glucose: 145 mg/dL",
		"treatmentPlan": "Start Metformin 500mg twice daily, Lifestyle modifications",
		"createdAt": "2024-01-15T10:00:00Z",
		"updatedAt": "2024-01-15T10:00:00Z"
	  }
	]
  },
  "statusCode": 200
}
```

---

### 2. Get Medical Record By ID

**Endpoint:** `GET /api/medicalrecords/{id}`

**Description:** Retrieve specific medical record details

**Headers:**
```
Authorization: Bearer {token}
```

**URL Parameters:**
- `id` (integer): Medical Record ID

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Medical record retrieved successfully",
  "data": {
	"id": 3,
	"patientId": 1,
	"patientName": "John Doe",
	"doctorId": 2,
	"doctorName": "Dr. Michael Smith",
	"appointmentId": 5,
	"diagnosis": "Type 2 Diabetes Mellitus",
	"visitNotes": "Patient presents with elevated blood sugar levels",
	"symptoms": "Fatigue, Increased thirst, Frequent urination",
	"physicalExamination": "Weight: 85kg, Blood Pressure: 145/90, Heart Rate: 78",
	"labTestResults": "HbA1c: 8.2%, Fasting Glucose: 145 mg/dL",
	"treatmentPlan": "Start Metformin 500mg twice daily, Lifestyle modifications",
	"prescriptions": [
	  {
		"id": 1,
		"medicalRecordId": 3,
		"medicationName": "Metformin",
		"dosage": "500mg",
		"frequency": "Twice daily",
		"duration": "30 days",
		"instructions": "Take with meals to reduce side effects",
		"quantity": 60,
		"refills": 3,
		"createdAt": "2024-01-15T10:00:00Z"
	  }
	],
	"createdAt": "2024-01-15T10:00:00Z",
	"updatedAt": "2024-01-15T10:00:00Z"
  },
  "statusCode": 200
}
```

---

### 3. Create Medical Record (Doctor Only)

**Endpoint:** `POST /api/medicalrecords`

**Description:** Create a new medical record for a patient

**Headers:**
```
Authorization: Bearer {token}
```

**Request Body:**
```json
{
  "patientId": 1,
  "doctorId": 2,
  "appointmentId": 5,
  "diagnosis": "Type 2 Diabetes Mellitus",
  "visitNotes": "Patient presents with elevated blood sugar levels",
  "symptoms": "Fatigue, Increased thirst, Frequent urination",
  "physicalExamination": "Weight: 85kg, Blood Pressure: 145/90, Heart Rate: 78",
  "labTestResults": "HbA1c: 8.2%, Fasting Glucose: 145 mg/dL",
  "treatmentPlan": "Start Metformin 500mg twice daily, Lifestyle modifications"
}
```

**Response (201 Created):**
```json
{
  "success": true,
  "message": "Medical record created successfully",
  "data": {
	"id": 3,
	"patientId": 1,
	"patientName": "John Doe",
	"doctorId": 2,
	"doctorName": "Dr. Michael Smith",
	"appointmentId": 5,
	"diagnosis": "Type 2 Diabetes Mellitus",
	"visitNotes": "Patient presents with elevated blood sugar levels",
	"symptoms": "Fatigue, Increased thirst, Frequent urination",
	"physicalExamination": "Weight: 85kg, Blood Pressure: 145/90, Heart Rate: 78",
	"labTestResults": "HbA1c: 8.2%, Fasting Glucose: 145 mg/dL",
	"treatmentPlan": "Start Metformin 500mg twice daily, Lifestyle modifications",
	"prescriptions": [],
	"createdAt": "2024-01-15T10:00:00Z",
	"updatedAt": "2024-01-15T10:00:00Z"
  },
  "statusCode": 201
}
```

---

### 4. Update Medical Record (Doctor Only)

**Endpoint:** `PUT /api/medicalrecords/{id}`

**Description:** Update medical record details

**Headers:**
```
Authorization: Bearer {token}
```

**URL Parameters:**
- `id` (integer): Medical Record ID

**Request Body:**
```json
{
  "diagnosis": "Type 2 Diabetes Mellitus - Well Controlled",
  "visitNotes": "Follow-up visit, blood sugar levels improved",
  "symptoms": "Mild fatigue",
  "physicalExamination": "Weight: 82kg, Blood Pressure: 135/85, Heart Rate: 76",
  "labTestResults": "HbA1c: 7.1%, Fasting Glucose: 120 mg/dL",
  "treatmentPlan": "Continue Metformin, add Lisinopril for blood pressure"
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Medical record updated successfully",
  "data": {
	"id": 3,
	"patientId": 1,
	"patientName": "John Doe",
	"doctorId": 2,
	"doctorName": "Dr. Michael Smith",
	"appointmentId": 5,
	"diagnosis": "Type 2 Diabetes Mellitus - Well Controlled",
	"visitNotes": "Follow-up visit, blood sugar levels improved",
	"symptoms": "Mild fatigue",
	"physicalExamination": "Weight: 82kg, Blood Pressure: 135/85, Heart Rate: 76",
	"labTestResults": "HbA1c: 7.1%, Fasting Glucose: 120 mg/dL",
	"treatmentPlan": "Continue Metformin, add Lisinopril for blood pressure",
	"prescriptions": [
	  {
		"id": 1,
		"medicalRecordId": 3,
		"medicationName": "Metformin",
		"dosage": "500mg",
		"frequency": "Twice daily",
		"duration": "30 days",
		"instructions": "Take with meals",
		"quantity": 60,
		"refills": 3,
		"createdAt": "2024-01-15T10:00:00Z"
	  }
	],
	"createdAt": "2024-01-15T10:00:00Z",
	"updatedAt": "2024-01-15T14:00:00Z"
  },
  "statusCode": 200
}
```

---

### 5. Add Prescription to Medical Record

**Endpoint:** `POST /api/medicalrecords/{id}/prescriptions`

**Description:** Add a prescription to a medical record

**Headers:**
```
Authorization: Bearer {token}
```

**URL Parameters:**
- `id` (integer): Medical Record ID

**Request Body:**
```json
{
  "medicationName": "Lisinopril",
  "dosage": "10mg",
  "frequency": "Once daily",
  "duration": "30 days",
  "instructions": "Take in the morning",
  "quantity": 30,
  "refills": 3
}
```

**Response (201 Created):**
```json
{
  "success": true,
  "message": "Prescription added successfully",
  "data": {
	"id": 2,
	"medicalRecordId": 3,
	"medicationName": "Lisinopril",
	"dosage": "10mg",
	"frequency": "Once daily",
	"duration": "30 days",
	"instructions": "Take in the morning",
	"quantity": 30,
	"refills": 3,
	"createdAt": "2024-01-15T14:30:00Z"
  },
  "statusCode": 201
}
```

---

### 6. Delete Medical Record

**Endpoint:** `DELETE /api/medicalrecords/{id}`

**Description:** Soft delete a medical record

**Headers:**
```
Authorization: Bearer {token}
```

**URL Parameters:**
- `id` (integer): Medical Record ID

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Medical record deleted successfully",
  "statusCode": 200
}
```

---

## Dashboard Endpoints

### 1. Get Dashboard Statistics (Admin Only)

**Endpoint:** `GET /api/dashboard/statistics`

**Description:** Get overall hospital statistics

**Headers:**
```
Authorization: Bearer {token}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Statistics retrieved successfully",
  "data": {
	"totalPatients": 125,
	"totalDoctors": 25,
	"totalAppointments": 450,
	"totalDepartments": 8,
	"appointmentsToday": 18,
	"appointmentsPending": 25,
	"appointmentsCompleted": 400,
	"appointmentsCancelled": 25,
	"newPatientsThisMonth": 15,
	"newAppointmentsThisMonth": 78
  },
  "statusCode": 200
}
```

---

### 2. Get Today's Appointments (Admin/Doctor/Receptionist)

**Endpoint:** `GET /api/dashboard/today-appointments`

**Description:** Get all appointments scheduled for today

**Headers:**
```
Authorization: Bearer {token}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Today's appointments retrieved successfully",
  "data": [
	{
	  "id": 5,
	  "patientId": 1,
	  "patientName": "John Doe",
	  "doctorId": 2,
	  "doctorName": "Dr. Michael Smith",
	  "appointmentDateTime": "2024-01-15T09:30:00Z",
	  "reasonForVisit": "Regular checkup",
	  "status": "Scheduled",
	  "appointmentType": "Clinical",
	  "durationInMinutes": 30
	}
  ],
  "statusCode": 200
}
```

---

### 3. Get Doctor's Dashboard (Doctor Only)

**Endpoint:** `GET /api/dashboard/doctor/{doctorId}`

**Description:** Get doctor-specific dashboard data

**Headers:**
```
Authorization: Bearer {token}
```

**URL Parameters:**
- `doctorId` (integer): Doctor ID

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Doctor dashboard retrieved successfully",
  "data": {
	"doctorId": 2,
	"doctorName": "Dr. Michael Smith",
	"specialization": "Cardiology",
	"totalPatients": 45,
	"appointmentsToday": 5,
	"appointmentsPending": 8,
	"appointmentsCompleted": 250,
	"medicalRecordsCreated": 120,
	"upcomingAppointments": [
	  {
		"id": 5,
		"patientName": "John Doe",
		"appointmentDateTime": "2024-01-15T09:30:00Z",
		"reasonForVisit": "Regular checkup",
		"status": "Scheduled"
	  }
	]
  },
  "statusCode": 200
}
```

---

### 4. Get Patient's Dashboard (Patient Only)

**Endpoint:** `GET /api/dashboard/patient/{patientId}`

**Description:** Get patient-specific dashboard data

**Headers:**
```
Authorization: Bearer {token}
```

**URL Parameters:**
- `patientId` (integer): Patient ID

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Patient dashboard retrieved successfully",
  "data": {
	"patientId": 1,
	"patientName": "John Doe",
	"upcomingAppointments": [
	  {
		"id": 5,
		"doctorName": "Dr. Michael Smith",
		"departmentName": "Cardiology",
		"appointmentDateTime": "2024-02-20T14:30:00Z",
		"status": "Scheduled"
	  }
	],
	"recentMedicalRecords": [
	  {
		"id": 3,
		"doctorName": "Dr. Michael Smith",
		"diagnosis": "Type 2 Diabetes Mellitus",
		"createdAt": "2024-01-15T10:00:00Z"
	  }
	],
	"totalAppointments": 15,
	"completedAppointments": 12,
	"cancelledAppointments": 1
  },
  "statusCode": 200
}
```

---

## Response Format

### Success Response
```json
{
  "success": true,
  "message": "Operation completed successfully",
  "data": {},
  "statusCode": 200
}
```

### Error Response
```json
{
  "success": false,
  "message": "Operation failed",
  "error": "Detailed error message",
  "statusCode": 400
}
```

### Paginated Response
```json
{
  "success": true,
  "message": "Data retrieved successfully",
  "data": {
	"pageNumber": 1,
	"pageSize": 10,
	"totalCount": 50,
	"totalPages": 5,
	"items": []
  },
  "statusCode": 200
}
```

---

## Error Handling

### HTTP Status Codes

| Status Code | Description |
|-------------|-------------|
| 200 | OK - Request successful |
| 201 | Created - Resource created successfully |
| 400 | Bad Request - Invalid input data |
| 401 | Unauthorized - Missing or invalid authentication |
| 403 | Forbidden - Insufficient permissions |
| 404 | Not Found - Resource not found |
| 409 | Conflict - Resource already exists |
| 500 | Internal Server Error - Server error |

### Common Error Messages

```json
{
  "success": false,
  "message": "Validation failed",
  "error": "Email is required, Password must be at least 8 characters",
  "statusCode": 400
}
```

```json
{
  "success": false,
  "message": "Unauthorized",
  "error": "Invalid or expired token",
  "statusCode": 401
}
```

```json
{
  "success": false,
  "message": "Forbidden",
  "error": "You do not have permission to access this resource",
  "statusCode": 403
}
```

```json
{
  "success": false,
  "message": "Not Found",
  "error": "Patient not found",
  "statusCode": 404
}
```

---

## JavaScript Client Implementation

### 1. API Service Class

```javascript
class HospitalAPI {
  constructor() {
	this.baseURL = 'http://localhost:5000/api';
	this.token = localStorage.getItem('token');
  }

  // Set Authorization Header
  getHeaders(includeAuth = true) {
	const headers = {
	  'Content-Type': 'application/json',
	};

	if (includeAuth && this.token) {
	  headers['Authorization'] = `Bearer ${this.token}`;
	}

	return headers;
  }

  // Generic Fetch Method
  async fetchAPI(endpoint, options = {}) {
	const url = `${this.baseURL}${endpoint}`;
	const config = {
	  headers: options.headers || this.getHeaders(options.auth !== false),
	  ...options,
	};

	try {
	  const response = await fetch(url, config);
	  const data = await response.json();

	  if (!response.ok) {
		this.handleError(response.status, data);
		return { success: false, ...data };
	  }

	  return data;
	} catch (error) {
	  console.error('API Error:', error);
	  return {
		success: false,
		message: 'Network error',
		error: error.message,
		statusCode: 0,
	  };
	}
  }

  // Error Handler
  handleError(status, data) {
	if (status === 401) {
	  // Token expired, redirect to login
	  localStorage.removeItem('token');
	  localStorage.removeItem('refreshToken');
	  window.location.href = '/login';
	}
	console.error(`API Error ${status}:`, data.error);
  }

  // === AUTH ENDPOINTS ===

  async register(userData) {
	return this.fetchAPI('/auth/register', {
	  method: 'POST',
	  body: JSON.stringify(userData),
	  auth: false,
	});
  }

  async login(credentials) {
	const response = await this.fetchAPI('/auth/login', {
	  method: 'POST',
	  body: JSON.stringify(credentials),
	  auth: false,
	});

	if (response.success) {
	  this.token = response.data.token;
	  localStorage.setItem('token', response.data.token);
	  localStorage.setItem('refreshToken', response.data.refreshToken);
	  localStorage.setItem('user', JSON.stringify(response.data));
	}

	return response;
  }

  async logout() {
	const response = await this.fetchAPI('/auth/logout', { method: 'POST' });
	if (response.success) {
	  localStorage.removeItem('token');
	  localStorage.removeItem('refreshToken');
	  localStorage.removeItem('user');
	  this.token = null;
	}
	return response;
  }

  async refreshToken(refreshToken) {
	return this.fetchAPI('/auth/refresh', {
	  method: 'POST',
	  body: JSON.stringify({ refreshToken }),
	  auth: false,
	});
  }

  async changePassword(currentPassword, newPassword, confirmPassword) {
	return this.fetchAPI('/auth/change-password', {
	  method: 'POST',
	  body: JSON.stringify({
		currentPassword,
		newPassword,
		confirmPassword,
	  }),
	});
  }

  // === PATIENT ENDPOINTS ===

  async getAllPatients(pageNumber = 1, pageSize = 10) {
	return this.fetchAPI(`/patients?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  async getPatientById(id) {
	return this.fetchAPI(`/patients/${id}`);
  }

  async updatePatient(id, patientData) {
	return this.fetchAPI(`/patients/${id}`, {
	  method: 'PUT',
	  body: JSON.stringify(patientData),
	});
  }

  async getPatientAppointments(id) {
	return this.fetchAPI(`/patients/${id}/appointments`);
  }

  async getPatientMedicalRecords(id) {
	return this.fetchAPI(`/patients/${id}/medical-records`);
  }

  async deletePatient(id) {
	return this.fetchAPI(`/patients/${id}`, { method: 'DELETE' });
  }

  // === DOCTOR ENDPOINTS ===

  async getAllDoctors(pageNumber = 1, pageSize = 10) {
	return this.fetchAPI(`/doctors?pageNumber=${pageNumber}&pageSize=${pageSize}`, {
	  auth: false,
	});
  }

  async getDoctorById(id) {
	return this.fetchAPI(`/doctors/${id}`, { auth: false });
  }

  async updateDoctor(id, doctorData) {
	return this.fetchAPI(`/doctors/${id}`, {
	  method: 'PUT',
	  body: JSON.stringify(doctorData),
	});
  }

  async getDoctorsByDepartment(departmentId) {
	return this.fetchAPI(`/doctors/department/${departmentId}`, { auth: false });
  }

  async getDoctorSchedule(doctorId) {
	return this.fetchAPI(`/doctors/${doctorId}/schedule`);
  }

  async deleteDoctor(id) {
	return this.fetchAPI(`/doctors/${id}`, { method: 'DELETE' });
  }

  // === DEPARTMENT ENDPOINTS ===

  async getAllDepartments(pageNumber = 1, pageSize = 10) {
	return this.fetchAPI(`/departments?pageNumber=${pageNumber}&pageSize=${pageSize}`, {
	  auth: false,
	});
  }

  async getDepartmentById(id) {
	return this.fetchAPI(`/departments/${id}`, { auth: false });
  }

  async createDepartment(departmentData) {
	return this.fetchAPI('/departments', {
	  method: 'POST',
	  body: JSON.stringify(departmentData),
	});
  }

  async updateDepartment(id, departmentData) {
	return this.fetchAPI(`/departments/${id}`, {
	  method: 'PUT',
	  body: JSON.stringify(departmentData),
	});
  }

  async deleteDepartment(id) {
	return this.fetchAPI(`/departments/${id}`, { method: 'DELETE' });
  }

  // === APPOINTMENT ENDPOINTS ===

  async bookAppointment(appointmentData) {
	return this.fetchAPI('/appointments', {
	  method: 'POST',
	  body: JSON.stringify(appointmentData),
	});
  }

  async getAllAppointments(pageNumber = 1, pageSize = 10) {
	return this.fetchAPI(`/appointments?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  async getAppointmentById(id) {
	return this.fetchAPI(`/appointments/${id}`);
  }

  async updateAppointment(id, appointmentData) {
	return this.fetchAPI(`/appointments/${id}`, {
	  method: 'PUT',
	  body: JSON.stringify(appointmentData),
	});
  }

  async cancelAppointment(id, reason) {
	return this.fetchAPI(`/appointments/${id}/cancel`, {
	  method: 'PUT',
	  body: JSON.stringify({ cancellationReason: reason }),
	});
  }

  async getDoctorAvailableSlots(doctorId, date) {
	return this.fetchAPI(`/appointments/doctor/${doctorId}/available-slots?date=${date}`);
  }

  async deleteAppointment(id) {
	return this.fetchAPI(`/appointments/${id}`, { method: 'DELETE' });
  }

  // === MEDICAL RECORDS ENDPOINTS ===

  async getAllMedicalRecords(pageNumber = 1, pageSize = 10) {
	return this.fetchAPI(`/medicalrecords?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  async getMedicalRecordById(id) {
	return this.fetchAPI(`/medicalrecords/${id}`);
  }

  async createMedicalRecord(recordData) {
	return this.fetchAPI('/medicalrecords', {
	  method: 'POST',
	  body: JSON.stringify(recordData),
	});
  }

  async updateMedicalRecord(id, recordData) {
	return this.fetchAPI(`/medicalrecords/${id}`, {
	  method: 'PUT',
	  body: JSON.stringify(recordData),
	});
  }

  async addPrescription(recordId, prescriptionData) {
	return this.fetchAPI(`/medicalrecords/${recordId}/prescriptions`, {
	  method: 'POST',
	  body: JSON.stringify(prescriptionData),
	});
  }

  async deleteMedicalRecord(id) {
	return this.fetchAPI(`/medicalrecords/${id}`, { method: 'DELETE' });
  }

  // === DASHBOARD ENDPOINTS ===

  async getDashboardStatistics() {
	return this.fetchAPI('/dashboard/statistics');
  }

  async getTodayAppointments() {
	return this.fetchAPI('/dashboard/today-appointments');
  }

  async getDoctorDashboard(doctorId) {
	return this.fetchAPI(`/dashboard/doctor/${doctorId}`);
  }

  async getPatientDashboard(patientId) {
	return this.fetchAPI(`/dashboard/patient/${patientId}`);
  }
}

// Create global instance
const api = new HospitalAPI();
```

---

### 2. Example: Login Form

```html
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Hospital Management System - Login</title>
  <style>
	* {
	  margin: 0;
	  padding: 0;
	  box-sizing: border-box;
	}

	body {
	  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
	  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
	  min-height: 100vh;
	  display: flex;
	  justify-content: center;
	  align-items: center;
	}

	.login-container {
	  background: white;
	  padding: 40px;
	  border-radius: 10px;
	  box-shadow: 0 10px 25px rgba(0, 0, 0, 0.2);
	  width: 100%;
	  max-width: 400px;
	}

	h1 {
	  text-align: center;
	  color: #333;
	  margin-bottom: 30px;
	  font-size: 28px;
	}

	.form-group {
	  margin-bottom: 20px;
	}

	label {
	  display: block;
	  margin-bottom: 8px;
	  color: #555;
	  font-weight: 600;
	}

	input {
	  width: 100%;
	  padding: 12px;
	  border: 2px solid #e0e0e0;
	  border-radius: 5px;
	  font-size: 16px;
	  transition: border-color 0.3s;
	}

	input:focus {
	  outline: none;
	  border-color: #667eea;
	  box-shadow: 0 0 0 3px rgba(102, 126, 234, 0.1);
	}

	button {
	  width: 100%;
	  padding: 12px;
	  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
	  color: white;
	  border: none;
	  border-radius: 5px;
	  font-size: 16px;
	  font-weight: 600;
	  cursor: pointer;
	  transition: transform 0.2s, box-shadow 0.2s;
	}

	button:hover {
	  transform: translateY(-2px);
	  box-shadow: 0 5px 15px rgba(102, 126, 234, 0.4);
	}

	button:active {
	  transform: translateY(0);
	}

	.error {
	  color: #e74c3c;
	  font-size: 14px;
	  margin-top: 5px;
	  display: none;
	}

	.error.show {
	  display: block;
	}

	.success {
	  background: #d4edda;
	  color: #155724;
	  padding: 12px;
	  border-radius: 5px;
	  margin-bottom: 20px;
	  display: none;
	}

	.success.show {
	  display: block;
	}

	.spinner {
	  display: none;
	  width: 20px;
	  height: 20px;
	  border: 3px solid rgba(255, 255, 255, 0.3);
	  border-top-color: white;
	  border-radius: 50%;
	  animation: spin 0.8s linear infinite;
	  margin: 0 auto;
	}

	@keyframes spin {
	  to { transform: rotate(360deg); }
	}

	button.loading {
	  pointer-events: none;
	  opacity: 0.9;
	}

	button.loading .spinner {
	  display: block;
	}

	button.loading span {
	  display: none;
	}
  </style>
</head>
<body>
  <div class="login-container">
	<h1>Hospital Login</h1>

	<div class="success" id="successMessage"></div>

	<form id="loginForm">
	  <div class="form-group">
		<label for="email">Email Address</label>
		<input 
		  type="email" 
		  id="email" 
		  name="email" 
		  placeholder="Enter your email"
		  required
		>
		<div class="error" id="emailError"></div>
	  </div>

	  <div class="form-group">
		<label for="password">Password</label>
		<input 
		  type="password" 
		  id="password" 
		  name="password" 
		  placeholder="Enter your password"
		  required
		>
		<div class="error" id="passwordError"></div>
	  </div>

	  <button type="submit" id="loginBtn">
		<div class="spinner"></div>
		<span>Login</span>
	  </button>
	</form>
  </div>

  <script src="api-client.js"></script>
  <script>
	const loginForm = document.getElementById('loginForm');
	const loginBtn = document.getElementById('loginBtn');
	const successMessage = document.getElementById('successMessage');

	loginForm.addEventListener('submit', async (e) => {
	  e.preventDefault();

	  const email = document.getElementById('email').value.trim();
	  const password = document.getElementById('password').value;

	  // Validation
	  if (!email || !password) {
		showError('emailError', 'Email and password are required');
		return;
	  }

	  // Show loading state
	  loginBtn.classList.add('loading');

	  try {
		const response = await api.login({ email, password });

		if (response.success) {
		  showSuccess('Login successful! Redirecting...');
		  setTimeout(() => {
			window.location.href = '/dashboard';
		  }, 1500);
		} else {
		  showError('emailError', response.error || 'Login failed');
		}
	  } catch (error) {
		showError('emailError', 'An error occurred. Please try again.');
	  } finally {
		loginBtn.classList.remove('loading');
	  }
	});

	function showError(elementId, message) {
	  const errorElement = document.getElementById(elementId);
	  errorElement.textContent = message;
	  errorElement.classList.add('show');
	}

	function clearError(elementId) {
	  const errorElement = document.getElementById(elementId);
	  errorElement.classList.remove('show');
	}

	function showSuccess(message) {
	  successMessage.textContent = message;
	  successMessage.classList.add('show');
	}

	// Clear errors on input
	['email', 'password'].forEach(id => {
	  document.getElementById(id).addEventListener('focus', () => {
		clearError(id + 'Error');
	  });
	});
  </script>
</body>
</html>
```

---

### 3. Example: Patient List

```html
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Patients - Hospital Management System</title>
  <style>
	* {
	  margin: 0;
	  padding: 0;
	  box-sizing: border-box;
	}

	body {
	  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
	  background: #f5f5f5;
	  color: #333;
	}

	.container {
	  max-width: 1200px;
	  margin: 0 auto;
	  padding: 20px;
	}

	.header {
	  display: flex;
	  justify-content: space-between;
	  align-items: center;
	  margin-bottom: 30px;
	}

	h1 {
	  font-size: 32px;
	  color: #333;
	}

	.btn {
	  padding: 10px 20px;
	  background: #667eea;
	  color: white;
	  border: none;
	  border-radius: 5px;
	  cursor: pointer;
	  font-size: 14px;
	  font-weight: 600;
	  transition: background 0.3s;
	}

	.btn:hover {
	  background: #764ba2;
	}

	.search-box {
	  margin-bottom: 20px;
	  display: flex;
	  gap: 10px;
	}

	.search-box input {
	  flex: 1;
	  padding: 10px 15px;
	  border: 2px solid #e0e0e0;
	  border-radius: 5px;
	  font-size: 14px;
	}

	.search-box button {
	  padding: 10px 20px;
	  background: #667eea;
	  color: white;
	  border: none;
	  border-radius: 5px;
	  cursor: pointer;
	  font-weight: 600;
	}

	table {
	  width: 100%;
	  border-collapse: collapse;
	  background: white;
	  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
	  border-radius: 5px;
	  overflow: hidden;
	}

	thead {
	  background: #667eea;
	  color: white;
	}

	th {
	  padding: 15px;
	  text-align: left;
	  font-weight: 600;
	}

	td {
	  padding: 15px;
	  border-bottom: 1px solid #e0e0e0;
	}

	tbody tr:hover {
	  background: #f9f9f9;
	}

	.btn-small {
	  padding: 5px 10px;
	  background: #667eea;
	  color: white;
	  border: none;
	  border-radius: 3px;
	  cursor: pointer;
	  font-size: 12px;
	  margin-right: 5px;
	}

	.btn-small.danger {
	  background: #e74c3c;
	}

	.pagination {
	  display: flex;
	  justify-content: center;
	  gap: 10px;
	  margin-top: 20px;
	}

	.pagination button {
	  padding: 8px 12px;
	  border: 1px solid #ddd;
	  background: white;
	  cursor: pointer;
	  border-radius: 3px;
	  transition: background 0.3s;
	}

	.pagination button.active {
	  background: #667eea;
	  color: white;
	  border-color: #667eea;
	}

	.loading {
	  text-align: center;
	  padding: 40px;
	  font-size: 18px;
	  color: #666;
	}

	.error-message {
	  background: #f8d7da;
	  color: #721c24;
	  padding: 15px;
	  border-radius: 5px;
	  margin-bottom: 20px;
	  display: none;
	}

	.error-message.show {
	  display: block;
	}
  </style>
</head>
<body>
  <div class="container">
	<div class="header">
	  <h1>Patients</h1>
	  <button class="btn" onclick="openAddPatientModal()">Add Patient</button>
	</div>

	<div class="error-message" id="errorMessage"></div>

	<div class="search-box">
	  <input 
		type="text" 
		id="searchInput" 
		placeholder="Search patients by name or email..."
	  >
	  <button onclick="searchPatients()">Search</button>
	</div>

	<div id="tableContainer">
	  <div class="loading">Loading patients...</div>
	</div>

	<div class="pagination" id="pagination"></div>
  </div>

  <script src="api-client.js"></script>
  <script>
	let currentPage = 1;
	const pageSize = 10;

	// Load patients on page load
	document.addEventListener('DOMContentLoaded', () => {
	  loadPatients(1);
	  checkAuthentication();
	});

	function checkAuthentication() {
	  const token = localStorage.getItem('token');
	  if (!token) {
		window.location.href = '/login';
	  }
	}

	async function loadPatients(pageNumber) {
	  const tableContainer = document.getElementById('tableContainer');
	  tableContainer.innerHTML = '<div class="loading">Loading patients...</div>';

	  try {
		const response = await api.getAllPatients(pageNumber, pageSize);

		if (response.success) {
		  displayPatients(response.data.items);
		  displayPagination(response.data.totalPages, response.data.pageNumber);
		  currentPage = pageNumber;
		} else {
		  showError(response.error || 'Failed to load patients');
		}
	  } catch (error) {
		showError('An error occurred while loading patients');
	  }
	}

	function displayPatients(patients) {
	  if (!patients || patients.length === 0) {
		document.getElementById('tableContainer').innerHTML = 
		  '<div class="loading">No patients found</div>';
		return;
	  }

	  let html = `
		<table>
		  <thead>
			<tr>
			  <th>ID</th>
			  <th>Name</th>
			  <th>Email</th>
			  <th>Phone</th>
			  <th>Blood Group</th>
			  <th>Actions</th>
			</tr>
		  </thead>
		  <tbody>
	  `;

	  patients.forEach(patient => {
		html += `
		  <tr>
			<td>${patient.id}</td>
			<td>${patient.fullName}</td>
			<td>${patient.email}</td>
			<td>${patient.phoneNumber}</td>
			<td>${patient.bloodGroup || 'N/A'}</td>
			<td>
			  <button class="btn-small" onclick="viewPatient(${patient.id})">View</button>
			  <button class="btn-small" onclick="editPatient(${patient.id})">Edit</button>
			  <button class="btn-small danger" onclick="deletePatient(${patient.id})">Delete</button>
			</td>
		  </tr>
		`;
	  });

	  html += `
		  </tbody>
		</table>
	  `;

	  document.getElementById('tableContainer').innerHTML = html;
	}

	function displayPagination(totalPages, currentPageNum) {
	  let html = '';

	  if (currentPageNum > 1) {
		html += `<button onclick="loadPatients(${currentPageNum - 1})">← Previous</button>`;
	  }

	  for (let i = 1; i <= totalPages; i++) {
		const active = i === currentPageNum ? 'active' : '';
		html += `<button class="${active}" onclick="loadPatients(${i})">${i}</button>`;
	  }

	  if (currentPageNum < totalPages) {
		html += `<button onclick="loadPatients(${currentPageNum + 1})">Next →</button>`;
	  }

	  document.getElementById('pagination').innerHTML = html;
	}

	async function viewPatient(id) {
	  try {
		const response = await api.getPatientById(id);
		if (response.success) {
		  const patient = response.data;
		  alert(`
Patient: ${patient.fullName}
Email: ${patient.email}
Phone: ${patient.phoneNumber}
Blood Group: ${patient.bloodGroup}
Allergies: ${patient.allergies}
Medical History: ${patient.medicalHistory}
		  `);
		}
	  } catch (error) {
		showError('Failed to load patient details');
	  }
	}

	function editPatient(id) {
	  alert('Edit patient ' + id + ' - Implement edit modal');
	}

	async function deletePatient(id) {
	  if (confirm('Are you sure you want to delete this patient?')) {
		try {
		  const response = await api.deletePatient(id);
		  if (response.success) {
			loadPatients(currentPage);
		  } else {
			showError(response.error || 'Failed to delete patient');
		  }
		} catch (error) {
		  showError('An error occurred while deleting patient');
		}
	  }
	}

	function searchPatients() {
	  const searchValue = document.getElementById('searchInput').value;
	  alert('Implement search functionality for: ' + searchValue);
	}

	function openAddPatientModal() {
	  alert('Implement add patient modal');
	}

	function showError(message) {
	  const errorMessage = document.getElementById('errorMessage');
	  errorMessage.textContent = message;
	  errorMessage.classList.add('show');
	  setTimeout(() => {
		errorMessage.classList.remove('show');
	  }, 5000);
	}
  </script>
</body>
</html>
```

---

### 4. Example: Book Appointment

```html
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Book Appointment - Hospital Management System</title>
  <style>
	* {
	  margin: 0;
	  padding: 0;
	  box-sizing: border-box;
	}

	body {
	  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
	  background: #f5f5f5;
	  padding: 20px;
	}

	.container {
	  max-width: 600px;
	  margin: 0 auto;
	  background: white;
	  padding: 30px;
	  border-radius: 10px;
	  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
	}

	h1 {
	  text-align: center;
	  margin-bottom: 30px;
	  color: #333;
	}

	.form-group {
	  margin-bottom: 20px;
	}

	label {
	  display: block;
	  margin-bottom: 8px;
	  font-weight: 600;
	  color: #555;
	}

	input, select, textarea {
	  width: 100%;
	  padding: 12px;
	  border: 2px solid #e0e0e0;
	  border-radius: 5px;
	  font-size: 14px;
	  font-family: inherit;
	  transition: border-color 0.3s;
	}

	input:focus, select:focus, textarea:focus {
	  outline: none;
	  border-color: #667eea;
	  box-shadow: 0 0 0 3px rgba(102, 126, 234, 0.1);
	}

	.form-row {
	  display: grid;
	  grid-template-columns: 1fr 1fr;
	  gap: 15px;
	}

	.button-group {
	  display: flex;
	  gap: 10px;
	  margin-top: 30px;
	}

	button {
	  flex: 1;
	  padding: 12px;
	  border: none;
	  border-radius: 5px;
	  font-size: 16px;
	  font-weight: 600;
	  cursor: pointer;
	  transition: all 0.3s;
	}

	.btn-submit {
	  background: #667eea;
	  color: white;
	}

	.btn-submit:hover {
	  background: #764ba2;
	  transform: translateY(-2px);
	  box-shadow: 0 5px 15px rgba(102, 126, 234, 0.4);
	}

	.btn-cancel {
	  background: #e0e0e0;
	  color: #555;
	}

	.btn-cancel:hover {
	  background: #d0d0d0;
	}

	.error {
	  color: #e74c3c;
	  font-size: 13px;
	  margin-top: 5px;
	  display: none;
	}

	.error.show {
	  display: block;
	}

	.success {
	  background: #d4edda;
	  color: #155724;
	  padding: 12px;
	  border-radius: 5px;
	  margin-bottom: 20px;
	  display: none;
	}

	.success.show {
	  display: block;
	}

	.available-slots {
	  display: grid;
	  grid-template-columns: repeat(4, 1fr);
	  gap: 10px;
	  margin-top: 10px;
	}

	.time-slot {
	  padding: 10px;
	  border: 2px solid #e0e0e0;
	  border-radius: 5px;
	  text-align: center;
	  cursor: pointer;
	  transition: all 0.3s;
	}

	.time-slot:hover {
	  border-color: #667eea;
	}

	.time-slot.selected {
	  background: #667eea;
	  color: white;
	  border-color: #667eea;
	}

	.time-slot.unavailable {
	  color: #999;
	  cursor: not-allowed;
	  background: #f5f5f5;
	}

	.loading {
	  text-align: center;
	  color: #666;
	  font-size: 14px;
	}
  </style>
</head>
<body>
  <div class="container">
	<h1>Book an Appointment</h1>

	<div class="success" id="successMessage"></div>

	<form id="appointmentForm">
	  <div class="form-group">
		<label for="doctorId">Select Doctor *</label>
		<select id="doctorId" name="doctorId" required onchange="loadAvailableSlots()">
		  <option value="">Choose a doctor...</option>
		</select>
		<div class="error" id="doctorError"></div>
	  </div>

	  <div class="form-group">
		<label for="appointmentDate">Appointment Date *</label>
		<input 
		  type="date" 
		  id="appointmentDate" 
		  name="appointmentDate" 
		  required
		  onchange="loadAvailableSlots()"
		>
		<div class="error" id="dateError"></div>
	  </div>

	  <div class="form-group">
		<label>Available Time Slots *</label>
		<div class="available-slots" id="timeSlots">
		  <div class="loading">Select a doctor and date to see available slots</div>
		</div>
		<div class="error" id="timeError"></div>
		<input type="hidden" id="appointmentTime" name="appointmentTime">
	  </div>

	  <div class="form-group">
		<label for="reasonForVisit">Reason for Visit *</label>
		<textarea 
		  id="reasonForVisit" 
		  name="reasonForVisit" 
		  placeholder="Describe your symptoms or reason for visiting"
		  required
		></textarea>
		<div class="error" id="reasonError"></div>
	  </div>

	  <div class="form-row">
		<div class="form-group">
		  <label for="appointmentType">Appointment Type *</label>
		  <select id="appointmentType" name="appointmentType" required>
			<option value="Clinical">Clinical</option>
			<option value="Follow-up">Follow-up</option>
			<option value="Consultation">Consultation</option>
		  </select>
		</div>

		<div class="form-group">
		  <label for="durationInMinutes">Duration (Minutes) *</label>
		  <input 
			type="number" 
			id="durationInMinutes" 
			name="durationInMinutes" 
			value="30"
			min="15"
			max="120"
			required
		  >
		</div>
	  </div>

	  <div class="button-group">
		<button type="submit" class="btn-submit">Book Appointment</button>
		<button type="button" class="btn-cancel" onclick="window.history.back()">Cancel</button>
	  </div>
	</form>
  </div>

  <script src="api-client.js"></script>
  <script>
	let selectedTime = null;

	document.addEventListener('DOMContentLoaded', () => {
	  checkAuthentication();
	  setMinimumDate();
	  loadDoctors();
	});

	function checkAuthentication() {
	  const token = localStorage.getItem('token');
	  if (!token) {
		window.location.href = '/login';
	  }
	}

	function setMinimumDate() {
	  const today = new Date();
	  const tomorrow = new Date(today);
	  tomorrow.setDate(today.getDate() + 1);
	  document.getElementById('appointmentDate').min = 
		tomorrow.toISOString().split('T')[0];
	}

	async function loadDoctors() {
	  try {
		const response = await api.getAllDoctors(1, 100);
		if (response.success) {
		  populateDoctorSelect(response.data.items);
		}
	  } catch (error) {
		showError('doctorError', 'Failed to load doctors');
	  }
	}

	function populateDoctorSelect(doctors) {
	  const select = document.getElementById('doctorId');
	  doctors.forEach(doctor => {
		const option = document.createElement('option');
		option.value = doctor.id;
		option.textContent = `Dr. ${doctor.fullName} - ${doctor.specialization}`;
		select.appendChild(option);
	  });
	}

	async function loadAvailableSlots() {
	  const doctorId = document.getElementById('doctorId').value;
	  const date = document.getElementById('appointmentDate').value;

	  if (!doctorId || !date) {
		document.getElementById('timeSlots').innerHTML = 
		  '<div class="loading">Select a doctor and date to see available slots</div>';
		return;
	  }

	  const slotsContainer = document.getElementById('timeSlots');
	  slotsContainer.innerHTML = '<div class="loading">Loading available slots...</div>';

	  try {
		const response = await api.getDoctorAvailableSlots(doctorId, date);
		if (response.success) {
		  displayTimeSlots(response.data);
		} else {
		  slotsContainer.innerHTML = 
			'<div class="loading">No slots available for this date</div>';
		}
	  } catch (error) {
		showError('timeError', 'Failed to load available slots');
	  }
	}

	function displayTimeSlots(slots) {
	  const slotsContainer = document.getElementById('timeSlots');
	  let html = '';

	  slots.forEach(slot => {
		const disabled = !slot.available ? 'unavailable' : '';
		html += `
		  <div 
			class="time-slot ${disabled}"
			onclick="${slot.available ? `selectTime('${slot.time}')` : ''}"
		  >
			${slot.time}
		  </div>
		`;
	  });

	  slotsContainer.innerHTML = html;
	}

	function selectTime(time) {
	  // Remove previous selection
	  document.querySelectorAll('.time-slot.selected').forEach(slot => {
		slot.classList.remove('selected');
	  });

	  // Add selection to clicked slot
	  event.target.classList.add('selected');
	  selectedTime = time;
	  document.getElementById('appointmentTime').value = time;
	}

	document.getElementById('appointmentForm').addEventListener('submit', async (e) => {
	  e.preventDefault();

	  if (!selectedTime) {
		showError('timeError', 'Please select a time slot');
		return;
	  }

	  const doctorId = parseInt(document.getElementById('doctorId').value);
	  const date = document.getElementById('appointmentDate').value;
	  const time = selectedTime;
	  const reasonForVisit = document.getElementById('reasonForVisit').value;
	  const appointmentType = document.getElementById('appointmentType').value;
	  const durationInMinutes = parseInt(document.getElementById('durationInMinutes').value);

	  const appointmentDateTime = new Date(`${date}T${time}:00`).toISOString();

	  const appointmentData = {
		patientId: 1, // Get from user context
		doctorId,
		appointmentDateTime,
		reasonForVisit,
		appointmentType,
		durationInMinutes
	  };

	  try {
		const response = await api.bookAppointment(appointmentData);
		if (response.success) {
		  const successMsg = document.getElementById('successMessage');
		  successMsg.textContent = 'Appointment booked successfully! Redirecting...';
		  successMsg.classList.add('show');
		  setTimeout(() => {
			window.location.href = '/appointments';
		  }, 2000);
		} else {
		  showError('timeError', response.error || 'Failed to book appointment');
		}
	  } catch (error) {
		showError('timeError', 'An error occurred while booking the appointment');
	  }
	});

	function showError(elementId, message) {
	  const error = document.getElementById(elementId);
	  error.textContent = message;
	  error.classList.add('show');
	}

	function clearError(elementId) {
	  const error = document.getElementById(elementId);
	  error.classList.remove('show');
	}
  </script>
</body>
</html>
```

---

## Notes for Frontend Development

1. **Always Check Authentication**: Before making any request, verify that the user has a valid token
2. **Handle Token Expiration**: Implement token refresh logic using the refresh token endpoint
3. **Store User Data**: After login, store user info in localStorage for access throughout the app
4. **Error Handling**: Always check the `success` field in responses and handle errors gracefully
5. **Loading States**: Show loading indicators while API calls are in progress
6. **Pagination**: Use query parameters for pagination (pageNumber, pageSize)
7. **Date Format**: Always send dates in ISO 8601 format (YYYY-MM-DDTHH:mm:ssZ)
8. **Authorization**: Check user roles before displaying admin-only features
9. **CORS**: Ensure CORS is enabled on the backend for your frontend domain
10. **API Rate Limiting**: Implement request debouncing to avoid rate limiting issues

---

## Summary

This comprehensive guide includes:
- ✅ All API endpoints with detailed documentation
- ✅ Complete request/response JSON examples
- ✅ DTOs and data structures
- ✅ Authorization and authentication flows
- ✅ Error handling and status codes
- ✅ Reusable JavaScript API client class
- ✅ Three complete HTML/CSS/JavaScript examples
- ✅ Best practices and development notes

You now have everything needed to build a fully functional frontend without any mistakes!
