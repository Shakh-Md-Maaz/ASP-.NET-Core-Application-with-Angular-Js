using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreOrderManagementProject.Models
{
    public class PatientsRepository : IPatientsRepository
    {
        private readonly ApplicationDbContext _db;

        public PatientsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Patient AddPatient(Patient patient)
        {
            _db.Patients.Add(patient);
            _db.SaveChanges();
            return patient;
        }

     





        public void DeletePatient(int id)
        {
            var data = GetPatientById(id);
            if (data != null)
            {
                _db.Remove(data);
            }
            _db.SaveChanges();

        }

  

        public IEnumerable<Doctor> GetDoctors()
        {
            var data = _db.Doctors.ToList();
            return data;
        }

        public Patient GetPatientById(int id)
        {
            Patient patient = _db.Patients.FirstOrDefault(p => p.PatientId == id);
            return patient;
        }

        public IEnumerable<Patient> GetPatients()
        {
            var data = _db.Patients.Select(p => new Patient
            {
                PatientId = p.PatientId,
                PatientName = p.PatientName,
                Age = p.Age,
                UrlImage = p.UrlImage,
                DoctorId = p.DoctorId,
                Doctor = _db.Doctors.Where(c => c.DoctorId == p.DoctorId).FirstOrDefault()
            }).ToList();
            return data;
        }

      

       

        public Patient UpdatePatient(Patient changePatient)
        {
            var patient = _db.Patients.Attach(changePatient);
            patient.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
            return changePatient;
        }

       
    }
}

