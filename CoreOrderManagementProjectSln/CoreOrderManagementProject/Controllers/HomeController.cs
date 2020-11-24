using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CoreOrderManagementProject.Models;
using CoreOrderManagementProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;



namespace CoreOrderManagementProject.Controllers
{

    //[Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private IPatientsRepository _productRepository;
        private readonly IHostingEnvironment _hostingEnviroment;
        public HomeController(IPatientsRepository productRepository, IHostingEnvironment hostingEnvironment)
        {
            _productRepository = productRepository;
            _hostingEnviroment = hostingEnvironment;
        }

        public ActionResult Index()
        {
            List<Patient> patients = _productRepository.GetPatients().ToList();
            return View(patients);
        }

        //public ViewResult Details(int id)
        //{

        //    //throw new Exception(); 
        //    Patient pro = _productRepository.GetPatientById(id);
        //    if (pro == null)
        //    {
        //        Response.StatusCode = 404;
        //        return View("EmployeeNotFound", id);
        //    }
        //    PatientIndexViewModel patientIndexViewModel = new Patient()
        //    {
        //        Patient = _productRepository.GetPatientById(id),
        //        PageTitle = "Order Details"
        //    };
        //    return View(patientIndexViewModel);
        //}
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
