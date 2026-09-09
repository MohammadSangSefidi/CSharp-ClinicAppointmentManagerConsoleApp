using ClinicManagment.Repository.User;
using ClinicManagment.Repository.Appointment;
using ClinicManagment.Services.User;
using ClinicManagment.Services.Appointment;
using ClinicManagment.Models.Users;
using ClinicManagment.Models.Exceptions;
using ClinicManagment.Models.Appointment;

string menu = """
==================================================
             CLINIC APPOINTMENT MANAGER
==================================================
1. Manage Doctors
2. Manage Patients
3. Manage Appointments
4. Exit

""";

bool isRunning = true;

UserRepository userRepository = new UserRepository();
AppointmentRepository appointmentRepository = new AppointmentRepository();

UserService userService = new UserService(userRepository);
AppointmentService appointmentService = new AppointmentService(appointmentRepository, userRepository);

while (isRunning)
{   
    Console.WriteLine(menu);
    Console.WriteLine("=============================================");
    Console.WriteLine("Select:");
    
    string? userChoice = Console.ReadLine();

    switch (userChoice)
    {
        case "1":
            bool inDoctorMenu = true;
            while (inDoctorMenu){
                Console.WriteLine("============================");
                Console.WriteLine("""
                1. Get Doctors
                2. Get Doctor By Id
                3. Create Doctor
                4. Back
                """);
                Console.WriteLine("Select:");
                string? doctorListChoice = Console.ReadLine();


                switch (doctorListChoice)
                {
                    case "1":
                        List<Doctor> doctorsList = userService.GetDoctorsList();

                        foreach (Doctor d in doctorsList)
                        {
                            Console.WriteLine("==========");
                            ShowDoctorsInConsole(d);
                            Console.WriteLine("==========");           
                        }
                        break;

                    case "2":
                        Console.WriteLine("==========");
                        string? id;
                        do
                        {
                            Console.WriteLine("Enter Id:");
                            id = Console.ReadLine();
                        }
                        while (!int.TryParse(id, out _));

                        try{
                            Doctor doctor = userService.GetDoctorById(int.Parse(id));
                            Console.WriteLine("==========");
                            ShowDoctorsInConsole(doctor);
                            Console.WriteLine("==========");  
                        }
                        catch (InvalidIdException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "3":
                        Console.WriteLine("============================");
                        Console.WriteLine("Enter First Name:");
                        string? firstName = Console.ReadLine();

                        Console.WriteLine("Enter Last Name:");
                        string? lastName = Console.ReadLine();

                        Console.WriteLine("Enter Phone Number:");
                        string? phoneNumber = Console.ReadLine();

                        Console.WriteLine("Enter Specialization:");
                        Array values = Enum.GetValues(typeof(DoctorSpecialization));

                        foreach (DoctorSpecialization s in values)
                        {
                            Console.WriteLine($"{(int)s}- {s}");
                        }
                        string? specialization = Console.ReadLine();

                        try{
                            userService.CreateDoctor(firstName, lastName, phoneNumber, specialization);
                            Console.WriteLine("Doctor Created Successfully!");
                        }
                        catch (InvalidDoctorSpecialization ex){
                            Console.WriteLine(ex.Message);
                        }
                        catch (InvalidPhoneNumberException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "4":
                        inDoctorMenu = false;
                        break;

                    default:
                        Console.WriteLine("Invalid Choice!");
                        break;
                }
            }
            break;

        case "2":
            bool inPatientMenu = true;
            while (inPatientMenu){
                Console.WriteLine("============================");
                Console.WriteLine("""
                1. Get Patients
                2. Get Patient By Id
                3. Create Patient
                4. Back
                """);
                Console.WriteLine("Select:");
                string? patientListChoice = Console.ReadLine();


                switch (patientListChoice)
                {
                    case "1":
                        List<Patient> patientsList = userService.GetPatientsList();

                        foreach (Patient p in patientsList)
                        {
                            Console.WriteLine("==========");
                            ShowPatientInConsole(p);
                            Console.WriteLine("==========");           
                        }
                        break;

                    case "2":
                        Console.WriteLine("==========");
                        string? id;
                        do
                        {
                            Console.WriteLine("Enter Id:");
                            id = Console.ReadLine();
                        }
                        while (!int.TryParse(id, out _));

                        try{
                            Patient patient = userService.GetPatientById(int.Parse(id));
                            Console.WriteLine("==========");
                            ShowPatientInConsole(patient);
                            Console.WriteLine("==========");  
                        }
                        catch (InvalidIdException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "3":
                        Console.WriteLine("============================");
                        Console.WriteLine("Enter First Name:");
                        string? firstName = Console.ReadLine();

                        Console.WriteLine("Enter Last Name:");
                        string? lastName = Console.ReadLine();

                        Console.WriteLine("Enter Phone Number:");
                        string? phoneNumber = Console.ReadLine();

                        string? dateOfBirth;

                        do{
                            Console.WriteLine("Enter DateOfBirth:");
                            dateOfBirth = Console.ReadLine();
                        }
                        while(!DateOnly.TryParse(dateOfBirth, out _));

                        try{
                            userService.CreatePatient(firstName, lastName, phoneNumber, DateOnly.Parse(dateOfBirth));
                            Console.WriteLine("Patient Created Successfully!");
                        }
                        catch (InvalidPhoneNumberException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "4":
                        inPatientMenu = false;
                        break;

                    default:
                        Console.WriteLine("Invalid Choice!");
                        break;
                }
            }
            break;

        case "3":
            bool inAppointmentMenu = true;
            while (inAppointmentMenu){
                Console.WriteLine("============================");
                Console.WriteLine("""
                1. Get Appointments
                2. Get Appointment By Id
                3. Create Appointment
                4. Complete Appointment
                5. Cancel Appointment
                6. Back
                """);
                Console.WriteLine("Select:");
                string? appointmentListChoice = Console.ReadLine();


                switch (appointmentListChoice)
                {
                    case "1":
                        List<Appointment> appointmentsList = appointmentService.GetAllAppointment();

                        foreach (Appointment a in appointmentsList)
                        {
                            Console.WriteLine("==========");
                            ShowAppointmentInConsole(a);
                            Console.WriteLine("==========");           
                        }
                        break;

                    case "2":
                        Console.WriteLine("==========");
                        string? id;
                        do
                        {
                            Console.WriteLine("Enter Id:");
                            id = Console.ReadLine();
                        }
                        while (!int.TryParse(id, out _));

                        try{
                            Appointment appointment = appointmentService.GetAppointmentById(int.Parse(id));
                            Console.WriteLine("==========");
                            ShowAppointmentInConsole(appointment);
                            Console.WriteLine("==========");  
                        }
                        catch (InvalidIdException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "3":
                        Console.WriteLine("============================");
                        string? doctorId;
                        do
                        {
                            Console.WriteLine("Enter Doctor Id:");
                            doctorId = Console.ReadLine();
                        }
                        while (!int.TryParse(doctorId, out _));

                        string? patientId;
                        do
                        {
                            Console.WriteLine("Enter Patient Id:");
                            patientId = Console.ReadLine();
                        }
                        while (!int.TryParse(patientId, out _));

                        string? date;
                        do{
                            Console.WriteLine("Enter Date:");
                            date = Console.ReadLine();
                        }
                        while(!DateOnly.TryParse(date, out _));

                        string? startTime;
                        do{
                            Console.WriteLine("Enter Start Time:");
                            startTime = Console.ReadLine();
                        }
                        while(!TimeOnly.TryParse(startTime, out _));

                        string? endTime;
                        do{
                            Console.WriteLine("Enter End Time:");
                            endTime = Console.ReadLine();
                        }
                        while(!TimeOnly.TryParse(endTime, out _));

                        Console.WriteLine("Enter Description:");
                        string? description = Console.ReadLine();

                        try{
                            appointmentService.CreateAppointment(DateOnly.Parse(date), TimeOnly.Parse(startTime),
                                TimeOnly.Parse(endTime), int.Parse(doctorId), int.Parse(patientId), description);
                            Console.WriteLine("Appointment Created Successfully!");
                        }
                        catch (InvalidDateTimesException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (InvalidIdException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (TimesHaveOverlapException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "4":
                        string? completeId;
                        do
                        {
                            Console.WriteLine("Enter Id:");
                            completeId = Console.ReadLine();
                        }
                        while (!int.TryParse(completeId, out _));

                        try{
                            appointmentService.CompleteAppointment(int.Parse(completeId));
                            Console.WriteLine("Appointment Completed.");
                        }
                        catch (InvalidIdException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (InvalidStatusException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "5":
                        string? cancelId;
                        do
                        {
                            Console.WriteLine("Enter Id:");
                            cancelId = Console.ReadLine();
                        }
                        while (!int.TryParse(cancelId, out _));

                        try{
                            appointmentService.CancelAppointment(int.Parse(cancelId));
                            Console.WriteLine("Appointment Canceled.");
                        }
                        catch (InvalidIdException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (InvalidStatusException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "6":
                        inAppointmentMenu = false;
                        break;

                    default:
                        Console.WriteLine("Invalid Choice!");
                        break;
                }
            }
            break;

        case "4":
            isRunning = false;
            break;
        
        default:
            Console.WriteLine("Invalid Choice!");
            break;
    }
}

void ShowDoctorsInConsole(Doctor doctor)
{
    Console.WriteLine($"Id: {doctor.Id}");
    Console.WriteLine($"First Name: {doctor.FirstName}");
    Console.WriteLine($"Last Name: {doctor.LastName}");
    Console.WriteLine($"Phone Number: {doctor.PhoneNumber}");
    Console.WriteLine($"Specialization: {doctor.Specialization}");
}

void ShowPatientInConsole(Patient patient)
{
    Console.WriteLine($"Id: {patient.Id}");
    Console.WriteLine($"First Name: {patient.FirstName}");
    Console.WriteLine($"Last Name: {patient.LastName}");
    Console.WriteLine($"Phone Number: {patient.PhoneNumber}");
    Console.WriteLine($"Date Of Birth: {patient.DateOfBirth}");
}

void ShowAppointmentInConsole(Appointment appointment)
{
    Console.WriteLine($"Id : {appointment.Id}");
    Console.WriteLine($"Date : {appointment.Date}");
    Console.WriteLine($"StartTime : {appointment.StartTime}");
    Console.WriteLine($"EndTime : {appointment.EndTime}");
    Console.WriteLine($"Status : {appointment.Status}");
    Console.WriteLine($"Patient===>");
    ShowPatientInConsole(appointment.Patient);
    Console.WriteLine($"=====>");
    Console.WriteLine($"AssignedDoctor===>");
    ShowDoctorsInConsole(appointment.AssignedDoctor);
    Console.WriteLine($"=====>");
    Console.WriteLine($"Description : {appointment.Description}");
}