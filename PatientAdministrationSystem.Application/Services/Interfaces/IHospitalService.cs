using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientAdministrationSystem.Application.Services.Interfaces
{
    public interface IHospitalService
    {
        Guid CurrentHospitalId { get; set; }
    }
}
