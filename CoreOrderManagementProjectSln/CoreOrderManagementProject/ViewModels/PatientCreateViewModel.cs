using CoreOrderManagementProject.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreOrderManagementProject.ViewModels
{
    public class PatientCreateViewModel
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string Age { get; set; }
        public string UrlImage { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public IFormFile Photo { get; set; }
    }
}
