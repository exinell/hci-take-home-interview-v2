using Microsoft.EntityFrameworkCore;
using PatientAdministrationSystem.Application.Entities;
using PatientAdministrationSystem.Application.Repositories.Interfaces;
using PatientAdministrationSystem.Infrastructure.Repositories;
using PatientAdministrationSystem.Infrastructure;

public class PatientsRepository : EntityRepository<PatientEntity, Guid>, IPatientsRepository
{
    private readonly HciDataContext _context;

    public PatientsRepository(HciDataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<(IEnumerable<PatientEntity>, int)> SearchPatientsAsync(Guid hospitalId, string searchTerm, int page, int pageSize)
    {
        var query = _dbSet
            .Where(p => (p.FirstName.Contains(searchTerm) || p.LastName.Contains(searchTerm) || p.Email.Contains(searchTerm)) &&
                        p.PatientHospitals.Any(ph => ph.HospitalId == hospitalId))
            .OrderBy(p => p.LastName)
            .ThenBy(p => p.FirstName);

        var totalRecords = await query.CountAsync();
        var patients = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (patients, totalRecords);
    }

    public async Task<(IEnumerable<VisitEntity>, int)> GetPatientVisitsAsync(Guid hospitalId, Guid patientId, int page, int pageSize)
    {
        var visitsQuery = _context.Visits
            .Where(v => v.PatientHospitals.Any(ph => ph.PatientId == patientId && ph.HospitalId == hospitalId))
            .OrderBy(v => v.Date);

        var totalRecords = await visitsQuery.CountAsync();
        var visits = await visitsQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (visits, totalRecords);
    }
}
