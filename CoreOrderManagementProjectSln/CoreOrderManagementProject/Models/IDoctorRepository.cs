using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreOrderManagementProject.Models
{
    public interface IDoctorRepository
    {
        IEnumerable<Doctor> GetDoctors();
    }
}
