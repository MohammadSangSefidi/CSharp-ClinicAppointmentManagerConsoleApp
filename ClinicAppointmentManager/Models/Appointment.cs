namespace ClinicManagment.Models.Appointment;
using ClinicManagment.Models.Users;

public enum AppointmentStatus
{
    Scheduled,
    Completed,
    Cancelled
}

public class Appointment
{
    public int Id {get; private set;}
    public Doctor AssignedDoctor {get; private set;}
    public Patient Patient {get; private set;}

    public DateOnly Date {get; private set;}

    public TimeOnly StartTime {get; private set;}
    public TimeOnly EndTime {get; private set;}

    public AppointmentStatus Status {get; private set;}

    public string Description {get; private set;}

    public Appointment(DateOnly date, TimeOnly startTime, TimeOnly endTime, string description, Doctor doctor, Patient patient)
    {
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        Description = description;
        
        AssignedDoctor = doctor;
        Patient = patient;

        Status = AppointmentStatus.Scheduled;
    }

    public void SetId(int id)
    {
        Id = id;
    }

    public void CompleteAppointment()
    {
        Status = AppointmentStatus.Completed;
    }

    public void CancelAppointment()
    {
        Status = AppointmentStatus.Cancelled;
    }
}