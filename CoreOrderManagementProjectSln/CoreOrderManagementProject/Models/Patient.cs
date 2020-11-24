using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreOrderManagementProject.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public decimal Age { get; set; }
        public string UrlImage { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
