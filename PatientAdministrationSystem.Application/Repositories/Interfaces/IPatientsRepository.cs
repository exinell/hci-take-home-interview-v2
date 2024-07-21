using System.Linq.Expressions;
using PatientAdministrationSystem.Application.Entities;

namespace PatientAdministrationSystem.Application.Repositories.Interfaces;

public interface IPatientsRepository : IEntitiesRepository<PatientEntity, Guid>
{
    Task<(IEnumerable<PatientEntity>, int)> SearchPatientsAsync(Guid hospitalId, string searchTerm, int page, int pageSize);
    Task<(IEnumerable<VisitEntity>, int)> GetPatientVisitsAsync(Guid hospitalId, Guid patientId, int page, int pageSize);
}