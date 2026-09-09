namespace ClinicManagment.Models.Users;

public enum DoctorSpecialization
{
    Cardiologist,
    Dermatologist,
    Neurologist,
    Dentist,
    GeneralPractitioner,
    Orthopedist,
};


public abstract class User
{
    public int Id {get; private set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public string PhoneNumber {get; private set;}
    
    public User (string firstName, string lastName, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
    }

    public void SetId(int id)
    {
        Id = id;
    }
}


public class Doctor: User
{
    public DoctorSpecialization Specialization {get; private set;}

    public Doctor(string firstName, string lastName, string phoneNumber, DoctorSpecialization specialization): base(firstName, lastName, phoneNumber)
    {
        Specialization = specialization;
    }
}


public class Patient: User
{
    public DateOnly DateOfBirth {get; private set;}

    public Patient(string firstName, string lastName, string phoneNumber, DateOnly dateOfBirth): base(firstName, lastName, phoneNumber)
    {
        DateOfBirth = dateOfBirth;
    }
}
