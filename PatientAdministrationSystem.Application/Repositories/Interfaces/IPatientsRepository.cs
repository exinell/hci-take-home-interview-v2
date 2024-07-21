using System.Linq.Expressions;
using PatientAdministrationSystem.Application.Entities;

namespace PatientAdministrationSystem.Application.Repositories.Interfaces;

public interface IPatientsRepository : IEntitiesRepository<PatientEntity, Guid>
{
    Task<(IEnumerable<PatientEntity>, int)> SearchPatientsAsync(string searchTerm, int page, int pageSize);
    Task<(IEnumerable<PatientEntity>, int)> GetPatientsByHospitalAsync(Guid hospitalId, int page, int pageSize);
    Task<(IEnumerable<VisitEntity>, int)> GetPatientVisitsAsync(Guid patientId, int page, int pageSize);
}