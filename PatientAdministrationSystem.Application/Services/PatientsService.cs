using PatientAdministrationSystem.Application.Entities;
using PatientAdministrationSystem.Application.Services.Interfaces;
using PatientAdministrationSystem.Application.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PatientAdministrationSystem.Application.Services;

public class PatientsService : IPatientsService
{
    private readonly IPatientsRepository _patientsRepository;

    public PatientsService(IPatientsRepository patientRepository)
    {
        _patientsRepository = patientRepository;
    }

    public async Task<PatientEntity> GetPatientByIdAsync(Guid id)
    {
        return await _patientsRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<PatientEntity>> GetAllPatientsAsync()
    {
        return await _patientsRepository.GetAllAsync();
    }

    public async Task AddPatientAsync(PatientEntity patient)
    {
        await _patientsRepository.AddAsync(patient);
    }

    public async Task UpdatePatientAsync(PatientEntity patient)
    {
        await _patientsRepository.UpdateAsync(patient);
    }

    public async Task DeletePatientAsync(Guid id)
    {
        await _patientsRepository.DeleteAsync(id);
    }

    public async Task<(IEnumerable<PatientEntity>, int)> SearchPatientsAsync(string searchTerm, int page, int pageSize)
    {
        return await _patientsRepository.SearchPatientsAsync(searchTerm,page,pageSize);
    }

    public async Task<(IEnumerable<PatientEntity>, int)> GetPatientsByHospitalAsync(Guid hospitalId, int page, int pageSize)
    {
        return await _patientsRepository.GetPatientsByHospitalAsync(hospitalId, page, pageSize);
    }

    public async Task<(IEnumerable<VisitEntity>, int)> GetPatientVisitsAsync(Guid patientId, int page, int pageSize)
    {
        return await _patientsRepository.GetPatientVisitsAsync(patientId, page, pageSize);
    }
}
