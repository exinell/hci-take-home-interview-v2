using System.Text.Json.Serialization;

namespace PatientAdministrationSystem.Application.Entities;

public class VisitEntity : Entity<Guid>
{
    public DateTime Date { get; set; }
    [JsonIgnore]
    public ICollection<PatientHospitalRelation>? PatientHospitals { get; set; }
}