using PatientAdministrationSystem.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientAdministrationSystem.Application.Services
{
    public class HospitalService : IHospitalService
    {
        public Guid CurrentHospitalId { get; set; }

        public HospitalService()
        {
            // Initialize with the given hospital ID for demonstration purposes
            // Assuming here that the current user belongs to a certain hospital and this property was set on login
            CurrentHospitalId = new Guid("ff0c022e-1aff-4ad8-2231-08db0378ac98");
        }
    }

}
