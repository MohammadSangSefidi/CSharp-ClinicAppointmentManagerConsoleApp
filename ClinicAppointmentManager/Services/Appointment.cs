namespace ClinicManagment.Services.Appointment;
using ClinicManagment.Models.Exceptions;
using ClinicManagment.Models.Appointment;
using ClinicManagment.Models.Users;
using ClinicManagment.Repository.Appointment;
using ClinicManagment.Repository.User;

public class AppointmentService(AppointmentRepository appointmentRepository, UserRepository userRepository)
{
    private readonly AppointmentRepository _appointmentRepository = appointmentRepository;
    private readonly UserRepository _userRepository = userRepository;

    public List<Appointment> GetAllAppointment()
    {
        return _appointmentRepository.GetAll();
    }

    public Appointment GetAppointmentById(int id)
    {
        Appointment appointment = _appointmentRepository.GetById(id) ?? throw new InvalidIdException("Invalid Appointment Id");
        return appointment;
    }

    public void CreateAppointment(DateOnly date, TimeOnly startTime, TimeOnly endTime, int doctorId, int patientId, string description)
    {
        if (endTime <= startTime)
        {
            throw new InvalidDateTimesException("End time must be after start time.");
        }

        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        TimeOnly now = TimeOnly.FromDateTime(DateTime.Now);

        if (date < today)
        {
            throw new InvalidDateTimesException("Appointment date cannot be in the past.");
        }

        if (date == today && startTime <= now)
        {
            throw new InvalidDateTimesException("Appointment time must be in the future.");
        }

        List<Appointment> appointmentList = _appointmentRepository.GetAll();

        Doctor? doctor = _userRepository.GetDoctorById(doctorId) ?? throw new InvalidIdException("Invalid Doctor Id");
        
        Patient? patient = _userRepository.GetPatientById(patientId) ?? throw new InvalidIdException("Invalid Patient Id");


        foreach (Appointment appointment in appointmentList)
        {
            if (appointment.Status == AppointmentStatus.Scheduled && appointment.Date == date && appointment.AssignedDoctor == doctor)
            {
                bool isOverlapping = startTime < appointment.EndTime && endTime > appointment.StartTime;
                if (isOverlapping)
                {
                    throw new TimesHaveOverlapException("Times Have Overlap With Doctor Appointments!", appointment);
                }
            }

            if (appointment.Status == AppointmentStatus.Scheduled && appointment.Date == date && appointment.Patient == patient)
            {
                bool isOverlapping = startTime < appointment.EndTime && endTime > appointment.StartTime;
                if (isOverlapping)
                {
                    throw new TimesHaveOverlapException("Times Have Overlap With Patient Appointments!", appointment);
                }
            }
        }

        Appointment newAppointment = new Appointment(date, startTime, endTime, description, doctor, patient);
        _appointmentRepository.Add(newAppointment);
    }   

    public void CompleteAppointment(int id)
    {
        Appointment appointment = GetAppointmentById(id);

        if (! (appointment.Status == AppointmentStatus.Scheduled))
        {
            throw new InvalidStatusException("Appointment Status must be Scheduled");
        }

        appointment.CompleteAppointment();
    }

    public void CancelAppointment(int id)
    {
        Appointment appointment = GetAppointmentById(id);

        if (! (appointment.Status == AppointmentStatus.Scheduled))
        {
            throw new InvalidStatusException("Appointment Status must be Scheduled");
        }

        appointment.CancelAppointment();
    }

    // public static void ShowAppointmentInConsol(Appointment appointment)
    // {
    //     Console.WriteLine("==========");
    //     Console.WriteLine($"Id : {appointment.Id}");
    //     Console.WriteLine($"Date : {appointment.Date}");
    //     Console.WriteLine($"StartTime : {appointment.StartTime}");
    //     Console.WriteLine($"EndTime : {appointment.EndTime}");
    //     Console.WriteLine($"Status : {appointment.Status}");
    //     Console.WriteLine($"Patient===>");
    //     UserService.ShowPatientInConsol(appointment.Patient);
    //     Console.WriteLine($"=====>");
    //     Console.WriteLine($"AssignedDoctor===>");
    //     UserService.ShowDoctorInConsol(appointment.AssignedDoctor);
    //     Console.WriteLine($"=====>");
    //     Console.WriteLine($"Description : {appointment.Description}");
        
    //     Console.WriteLine("==========");
    // }
}
