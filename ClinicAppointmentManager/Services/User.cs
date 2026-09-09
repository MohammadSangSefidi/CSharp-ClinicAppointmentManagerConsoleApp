namespace ClinicManagment.Services.User;
using System.Text.RegularExpressions;
using ClinicManagment.Models.Exceptions;
using ClinicManagment.Models.Users;
using ClinicManagment.Repository.User;

public class UserService(UserRepository userRepository)
{
    private readonly UserRepository _userRepository = userRepository;

    public List<User> GetDoctorsList()
    {
        return _userRepository.GetAll(UserTypes.Doctor);
    }

    public List<User> GetPatientsList()
    {
        return _userRepository.GetAll(UserTypes.Patient);
    }

    public User GetDoctorById(int id)
    {
        User user = _userRepository.GetDoctorById(id) ?? throw new InvalidIdException("Invalid Doctor Id.");
        return user; 
    }

    public User GetPatientById(int id)
    {
        User user = _userRepository.GetPatientById(id) ?? throw new InvalidIdException("Invalid Patient Id.");
        return user; 
    }

    public static void CheckPhoneNumber(string value)
    {
        string pattern = @"^09\d{9}$";
        bool isValid = Regex.IsMatch(value, pattern);

        if (!isValid)
        {
            throw new InvalidPhoneNumberException("Invalid phone number.");
        }
    }

    public void CreateDoctor(string firstName, string lastName, string phoneNumber, DoctorSpecialization specialization)
    {
        CheckPhoneNumber(phoneNumber);
        Doctor newDoctor = new Doctor(firstName, lastName, phoneNumber, specialization);
        _userRepository.Add(newDoctor);
    }

    public void CreatePatient(string firstName, string lastName, string phoneNumber, DateOnly dateOfBirth)
    {
        CheckPhoneNumber(phoneNumber);

        if (dateOfBirth > DateOnly.FromDateTime(DateTime.Now))
        {
            throw new InvalidDateException("Date Of Birth Must Be In Future.");
        }
        
        Patient newPatient = new Patient(firstName, lastName, phoneNumber, dateOfBirth);
        _userRepository.Add(newPatient);
    }

    // public static void ShowDoctorInConsol(Doctor doctor)
    // {
    //     Console.WriteLine($"First Name: {doctor.FirstName}");
    //     Console.WriteLine($"Last Name: {doctor.LastName}");
    //     Console.WriteLine($"Phone Number: {doctor.PhoneNumber}");
    //     Console.WriteLine($"Specialization: {doctor.Specialization}");
    // }
    // public static void ShowPatientInConsol(Patient patient)
    // {
    //     Console.WriteLine($"First Name: {patient.FirstName}");
    //     Console.WriteLine($"Last Name: {patient.LastName}");
    //     Console.WriteLine($"Phone Number: {patient.PhoneNumber}");
    //     Console.WriteLine($"Date Of Birth: {patient.DateOfBirth}");
    // }
}
