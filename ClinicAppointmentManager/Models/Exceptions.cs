namespace ClinicManagment.Models.Exceptions;
using ClinicManagment.Models.Appointment;


public class InvalidPhoneNumberException: Exception
{
    public InvalidPhoneNumberException(string message): base(message)
    {
        
    }
}

public class InvalidIdException: Exception
{
    public InvalidIdException(string message): base(message)
    {
        
    }
}

public class TimesHaveOverlapException: Exception
{
    public Appointment? appointment;

    public TimesHaveOverlapException(string message, Appointment? errorAppointment): base(message)
    {
        appointment = errorAppointment;
    }
}

public class InvalidDateTimesException: Exception
{
    public InvalidDateTimesException(string message): base(message)
    {
    }
}

public class InvalidStatusException: Exception
{
    public InvalidStatusException(string message): base(message)
    {
    }
}

public class InvalidDateException: Exception
{
    public InvalidDateException(string message): base(message)
    {
    }
}

public class InvalidDoctorSpecialization: Exception
{
    public InvalidDoctorSpecialization(string message): base(message)
    {
        
    }
}