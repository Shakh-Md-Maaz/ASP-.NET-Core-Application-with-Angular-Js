using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreOrderManagementProject.Models
{
    public interface IPatientsRepository
    {
        IEnumerable<Patient> GetPatients();
        Patient AddPatient(Patient patient);
        Patient GetPatientById(int id);
        Patient UpdatePatient(Patient changePatient);
        void DeletePatient(int id);

        IEnumerable<Doctor> GetDoctors();
    }
}
