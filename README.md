# 🏥 Clinic Appointment Manager

A console-based clinic appointment management system built with **C# and .NET**.

This project was created as a practical exercise to apply fundamental C# and .NET concepts, including **Object-Oriented Programming, Collections, Exception Handling, Services, Repositories, and basic separation of concerns**.

---

## 📌 Features

### 👨‍⚕️ Doctor Management

- View all doctors
- Find a doctor by ID
- Create a new doctor
- Assign a specialization to each doctor
- Validate doctor phone numbers

### 🧑‍🤝‍🧑 Patient Management

- View all patients
- Find a patient by ID
- Create a new patient
- Store patient's date of birth
- Validate patient phone numbers

### 📅 Appointment Management

- View all appointments
- Find an appointment by ID
- Create an appointment
- Complete an appointment
- Cancel an appointment
- Assign a doctor and patient to an appointment

### 🛡️ Business Rules

The application validates several business rules when creating appointments:

- Appointment date cannot be in the past
- Appointment time must be in the future
- End time must be after start time
- Doctor must exist
- Patient must exist
- A doctor cannot have overlapping appointments
- A patient cannot have overlapping appointments
- Only scheduled appointments can be completed
- Only scheduled appointments can be cancelled

---

## 🏗️ Project Architecture

The project follows a simple layered structure:

```text
Program
   │
   ▼
Services
   │
   ▼
Repositories
   │
   ▼
Models
```

### Program

Responsible for:

- Console menus
- Reading user input
- Displaying information
- Handling exceptions and showing error messages

### Services

Responsible for:

- Business logic
- Validation
- Appointment conflict checking
- Coordinating repositories
- Managing application workflows

Examples:

```text
UserService
AppointmentService
```

### Repositories

Responsible for:

- Storing data
- Adding entities
- Retrieving entities
- Searching by ID

Examples:

```text
UserRepository
AppointmentRepository
```

### Models

Represent the application's domain entities.

Examples:

```text
User
Doctor
Patient
Appointment
```

---

## 🧩 Main Domain Models

### Doctor

A doctor contains information such as:

```text
Id
FirstName
LastName
PhoneNumber
Specialization
```

Available specializations include:

```text
Cardiologist
Dermatologist
Neurologist
Dentist
GeneralPractitioner
Orthopedist
```

### Patient

A patient contains:

```text
Id
FirstName
LastName
PhoneNumber
DateOfBirth
```

### Appointment

An appointment contains:

```text
Id
Date
StartTime
EndTime
Status
Description
Patient
AssignedDoctor
```

Appointment statuses:

```text
Scheduled
Completed
Cancelled
```

---

## ⏰ Appointment Overlap Detection

The application prevents doctors and patients from having two appointments at the same time.

The overlap condition is:

```csharp
startTime < appointment.EndTime &&
endTime > appointment.StartTime
```

For example:

```text
Appointment 1: 10:00 ───── 10:30
Appointment 2:          10:30 ───── 11:00
```

These appointments are considered valid because they only touch at `10:30`.

However:

```text
Appointment 1: 10:00 ───── 10:30
Appointment 2:       10:15 ───── 10:45
```

These appointments overlap and are rejected.

---

## ⚠️ Exception Handling

Custom exceptions are used to represent invalid application states.

Examples:

```text
InvalidIdException
InvalidPhoneNumberException
InvalidDateTimesException
InvalidStatusException
TimesHaveOverlapException
```

The general flow is:

```text
User Input
    ↓
Service
    ↓
Business Validation
    ↓
Exception
    ↓
Program catches exception
    ↓
Display error message
```

This keeps business validation out of the console UI.

---

## 🧠 C# Concepts Practiced

This project was built to practice the following