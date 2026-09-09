namespace ClinicManagment.Repository.User;
using ClinicManagment.Models.Users;

public enum UserTypes
{
    Doctor,
    Patient
}

public class UserRepository
{
    public int LastId {get; private set;} = 1000;
    private Dictionary<int, User> UserDict = [];

    public List<Doctor> GetAllDoctors()
    {   
        List<Doctor> list = [];
        foreach (User user in UserDict.Values.ToList())
            {
                if (user is Doctor doctor)
                {
                    list.Add(doctor);
                }
            }
            return list;
    }

    public List<Patient> GetAllPatiens()
    {   
        List<Patient> list = [];
        foreach (User user in UserDict.Values.ToList())
            {
                if (user is Patient patient)
                {
                    list.Add(patient);
                }
            }
            return list;
    }

    public List<User> GetAll()
    {
        return UserDict.Values.ToList();
        
    }

    public Doctor? GetDoctorById(int id)
    {
        if (UserDict.TryGetValue(id, out User? user) && user is Doctor doctor){
            return doctor;
        }
        else
        {
            return null;
        }
    }

    public Patient? GetPatientById(int id)
    {
        if (UserDict.TryGetValue(id, out User? user) && user is Patient patient){
            return patient;
        }
        else
        {
            return null;
        }
    }

    public void Add(User user)
    {
        user.SetId(LastId);
        UserDict[user.Id] = user;
        LastId += 1;
    }
}