namespace ClinicManagment.Services.User;
using System.Text.RegularExpressions;
using ClinicManagment.Models.Exceptions;
using ClinicManagment.Models.Users;
using ClinicManagment.Repository.User;

public class UserService(UserRepository userRepository)
{
    private readonly UserRepository _userRepository = userRepository;

    public List<Doctor> GetDoctorsList()
    {
        return _userRepository.GetAllDoctors();
    }

    public List<Patient> GetPatientsList()
    {
        return _userRepository.GetAllPatiens();
    }

    public Doctor GetDoctorById(int id)
    {
        Doctor user = _userRepository.GetDoctorById(id) ?? throw new InvalidIdException("Invalid Doctor Id.");
        return user; 
    }

    public Patient GetPatientById(int id)
    {
        Patient user = _userRepository.GetPatientById(id) ?? throw new InvalidIdException("Invalid Patient Id.");
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

    public void CreateDoctor(string firstName, string lastName, string phoneNumber, string specialization)
    {
        CheckPhoneNumber(phoneNumber);

        if (Enum.TryParse<DoctorSpecialization>(specialization, true, out DoctorSpecialization doctorSpecialization))
        {
            Doctor newDoctor = new Doctor(firstName, lastName, phoneNumber, doctorSpecialization);
            _userRepository.Add(newDoctor);
        }
        else
        {
            throw new InvalidDoctorSpecialization("Invalid Doctor Specialization.");
        }
        
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
    // }
    // public static void ShowPatientInConsol(Patient patient)
    // {

    // }
}
