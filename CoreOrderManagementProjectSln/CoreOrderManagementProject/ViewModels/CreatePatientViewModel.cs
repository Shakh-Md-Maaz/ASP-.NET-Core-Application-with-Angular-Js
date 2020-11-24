using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreOrderManagementProject.ViewModels
{
    public class CreatePatientViewModel
    {
        [Required(ErrorMessage = "Enter Patient Name")]
        public string PatientName { get; set; }

        [Required(ErrorMessage = "Enter Patient Age")]
        public decimal Age { get; set; }

        // [Required(ErrorMessage = "Chose Image")]
        public IFormFile UrlImage { get; set; }

        // [Required(ErrorMessage = "Chose Category")]
        public int DoctorId
        {
            get; set;
        }
    }
}
