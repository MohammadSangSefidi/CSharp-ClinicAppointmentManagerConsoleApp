namespace ClinicManagment.Repository.Appointment;
using ClinicManagment.Models.Appointment;

public class AppointmentRepository
{
    private Dictionary<int, Appointment> AppointmentDict = [];
    
    public int LastId {get; private set;} = 1000;

    public void Add(Appointment appointment)
    {
        appointment.SetId(LastId);
        AppointmentDict[appointment.Id] = appointment;
        LastId += 1;
    }

    public List<Appointment> GetAll()
    {
        return AppointmentDict.Values.ToList();
    }

    public Appointment? GetById(int id)
    {
        if (AppointmentDict.TryGetValue(id, out Appointment? appointment)){
            return appointment;
        }
        else
        {
            return null;
        }
    }
}
