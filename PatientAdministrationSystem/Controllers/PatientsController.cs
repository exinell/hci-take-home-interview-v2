using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using PatientAdministrationSystem.Application.Dtos;
using PatientAdministrationSystem.Application.Entities;
using PatientAdministrationSystem.Application.Services.Interfaces;
using System;

namespace Hci.Ah.Home.Api.Gateway.Controllers.Patients
{
    [Route("api/v{version:apiVersion}/patients")]
    [ApiExplorerSettings(GroupName = "Patients")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsService _patientsService;

        public PatientsController(IPatientsService patientsService)
        {
            _patientsService = patientsService;
        }

        [HttpGet("getpatients")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetPatients([FromQuery] string name, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new ApiResponse<object>(false, "Search term cannot be empty.", null));
            }

            try
            {
                var (patients, totalRecords) = await _patientsService.SearchPatientsAsync(name, page, pageSize);
                return Ok(new ApiResponse<IEnumerable<PatientEntity>>(true, "Patients retrieved successfully.", patients, totalRecords));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(false, $"An error occurred: {ex.Message}", null));
            }
        }

        [HttpGet("getpatientvisits")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetPatientVisits([FromQuery] string patientId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (!Guid.TryParse(patientId, out var patientGuid))
            {
                return BadRequest(new ApiResponse<object>(false, "Invalid patient ID format.", null));
            }

            try
            {
                var (visits, totalRecords) = await _patientsService.GetPatientVisitsAsync(patientGuid, page, pageSize);
                return Ok(new ApiResponse<IEnumerable<VisitEntity>>(true, "Patient visits retrieved successfully.", visits, totalRecords));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(false, $"An error occurred: {ex.Message}", null));
            }
        }
    }
}
