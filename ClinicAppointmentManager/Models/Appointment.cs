namespace ClinicManagment.Models.Appointment;
using ClinicManagment.Models.Users;
using ClinicManagment.Models.Exceptions;

public enum AppointmentStatus
{
    Scheduled,
    Completed,
    Cancelled
}

public class Appointment
{
    private static List<Appointment> AppointmentList = [];
    private static Dictionary<int, Appointment> AppointmentDict = [];
    
    private static int lastId = 1000;

    public int Id {get; private set;}
    public Doctor AssignedDoctor {get; private set;}
    public Patient Patient {get; private set;}

    public DateOnly Date {get; private set;}

    public TimeOnly StartTime {get; private set;}
    public TimeOnly EndTime {get; private set;}

    public AppointmentStatus Status {get; set;}

    public string Description {get; private set;}

    public void Show()
    {
        Console.WriteLine("==========");
        Console.WriteLine($"Id : {Id}");
        Console.WriteLine($"Date : {Date}");
        Console.WriteLine($"StartTime : {StartTime}");
        Console.WriteLine($"EndTime : {EndTime}");
         Console.WriteLine($"Status : {Status}");
        Console.WriteLine($"Patient===>");
        Patient.ShowUser();
        Console.WriteLine($"=====>");
        Console.WriteLine($"AssignedDoctor===>");
        AssignedDoctor.ShowUser();
        Console.WriteLine($"=====>");
        Console.WriteLine($"Description : {Description}");
        
        Console.WriteLine("==========");
    }

    public Appointment(int doctorId, int patientId, DateOnly date, TimeOnly startTime, TimeOnly endTime, string description)
    {   
        if (User.UserDict.TryGetValue(doctorId, out User selectedDoctor) && selectedDoctor is Doctor docter)
        {
            AssignedDoctor = docter;
        }
        else
        {
            throw new InvalidIdException("Invalid Doctor Id.");
        }

        if (User.UserDict.TryGetValue(patientId, out User selectedPatient) && selectedPatient is Patient patient)
        {
            Patient = patient;
        }
        else
        {
            throw new InvalidIdException("Invalid Patient Id.");
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

        foreach (Appointment appointment in AppointmentList)
        {
            if (appointment.Status == AppointmentStatus.Scheduled && appointment.Date == date && appointment.AssignedDoctor == AssignedDoctor)
            {
                bool isOverlapping = startTime < appointment.EndTime && endTime > appointment.StartTime;
                if (isOverlapping)
                {
                    throw new TimesHaveOverlapException("Times Have Overlap With Doctor Appointments!", appointment);
                }
            }

            if (appointment.Status == AppointmentStatus.Scheduled && appointment.Date == date && appointment.Patient == Patient)
            {
                bool isOverlapping = startTime < appointment.EndTime && endTime > appointment.StartTime;
                if (isOverlapping)
                {
                    throw new TimesHaveOverlapException("Times Have Overlap With Patient Appointments!", appointment);
                }
            }
        }

        if (endTime <= startTime)
        {
            throw new InvalidDateTimesException("End time must be after start time.");
        }

        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        Id = lastId;
        Description = description;
        lastId += 1;
        Status = AppointmentStatus.Scheduled;

        AppointmentList.Add(this);
        AppointmentDict[Id] = this;
        
    }
}