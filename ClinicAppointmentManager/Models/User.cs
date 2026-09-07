namespace ClinicManagment.Models.Users;
using System.Text.RegularExpressions;
using ClinicManagment.Models.Exceptions;

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
    private static int lastId = 1000;

    public static Dictionary<int, User> UserDict = [];

    public int Id {get; private set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public string PhoneNumber {get; private set
        {
            string pattern = @"^09\d{9}$";
            bool isValid = Regex.IsMatch(value, pattern);

            if (!isValid)
            {
                throw new InvalidPhoneNumberException("Invalid phone number.");
            }

            field = value;
        }
    }

    public User (string firstName, string lastName, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;

        Id = lastId;
        lastId += 1;

        UserDict[Id] = this;
    }

    public abstract void ShowUser();
}


public class Doctor: User
{
    public DoctorSpecialization Specialization {get; private set;}

    public Doctor(string firstName, string lastName, string phoneNumber, DoctorSpecialization specialization): base(firstName, lastName, phoneNumber)
    {
        Specialization = specialization;
    }

    public override void ShowUser()
    {
        Console.WriteLine($"First Name: {FirstName}");
        Console.WriteLine($"Last Name: {LastName}");
        Console.WriteLine($"Phone Number: {PhoneNumber}");
        Console.WriteLine($"Specialization: {Specialization}");
    }
}


public class Patient: User
{
    public DateOnly DateOfBirth {get; private set;}

    public Patient(string firstName, string lastName, string phoneNumber, DateOnly dateOfBirth): base(firstName, lastName, phoneNumber)
    {
        DateOfBirth = dateOfBirth;
    }
    public override void ShowUser()
    {
        Console.WriteLine($"First Name: {FirstName}");
        Console.WriteLine($"Last Name: {LastName}");
        Console.WriteLine($"Phone Number: {PhoneNumber}");
        Console.WriteLine($"Date Of Birth: {DateOfBirth}");
    }
}
