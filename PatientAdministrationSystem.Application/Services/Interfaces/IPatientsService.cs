using PatientAdministrationSystem.Application.Entities;
using PatientAdministrationSystem.Application.Repositories.Interfaces;

namespace PatientAdministrationSystem.Application.Services.Interfaces;

public interface IPatientsService
{
    Task<PatientEntity> GetPatientByIdAsync(Guid id);
    Task<IEnumerable<PatientEntity>> GetAllPatientsAsync();
    Task AddPatientAsync(PatientEntity patient);
    Task UpdatePatientAsync(PatientEntity patient);
    Task DeletePatientAsync(Guid id);
    Task<(IEnumerable<PatientEntity>, int)> SearchPatientsAsync(Guid hospitalId, string searchTerm, int page, int pageSize);
    Task<(IEnumerable<VisitEntity>, int)> GetPatientVisitsAsync(Guid hospitalId, Guid patientId, int page, int pageSize);
}