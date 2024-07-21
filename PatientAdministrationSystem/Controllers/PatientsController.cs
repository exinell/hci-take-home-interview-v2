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
        private readonly IHospitalService _hospitalService;

        public PatientsController(IPatientsService patientsService, IHospitalService hospitalService)
        {
            _patientsService = patientsService;
            _hospitalService = hospitalService;
        }

        /// <summary>
        /// Retrieves a list of patients based on the provided search criteria.
        /// </summary>
        /// <param name="name">The name to search for patients.</param>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <returns>A list of patients that match the search criteria.</returns>
        /// <response code="200">Patients retrieved successfully.</response>
        /// <response code="400">If the search term is empty or invalid.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("getpatients")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<PatientEntity>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        [Produces("application/json")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetPatients([FromQuery] string name, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new ApiResponse<object>(false, "Search term cannot be empty.", null));
            }

            try
            {
                var hospitalId = _hospitalService.CurrentHospitalId;
                var (patients, totalRecords) = await _patientsService.SearchPatientsAsync(hospitalId, name, page, pageSize);
                return Ok(new ApiResponse<IEnumerable<PatientEntity>>(true, "Patients retrieved successfully.", patients, totalRecords));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(false, $"An error occurred: {ex.Message}", null));
            }
        }

        /// <summary>
        /// Retrieves a list of visits for a specific patient.
        /// </summary>
        /// <param name="patientId">The ID of the patient to retrieve visits for.</param>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <returns>A list of visits for the specified patient.</returns>
        /// <response code="200">Patient visits retrieved successfully.</response>
        /// <response code="400">If the patient ID format is invalid.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("getpatientvisits")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<VisitEntity>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        [Produces("application/json")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetPatientVisits([FromQuery] string patientId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (!Guid.TryParse(patientId, out var patientGuid))
            {
                return BadRequest(new ApiResponse<object>(false, "Invalid patient ID format.", null));
            }

            try
            {
                var hospitalId = _hospitalService.CurrentHospitalId;
                var (visits, totalRecords) = await _patientsService.GetPatientVisitsAsync(hospitalId, patientGuid, page, pageSize);
                return Ok(new ApiResponse<IEnumerable<VisitEntity>>(true, "Patient visits retrieved successfully.", visits, totalRecords));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(false, $"An error occurred: {ex.Message}", null));
            }
        }
    }
}
