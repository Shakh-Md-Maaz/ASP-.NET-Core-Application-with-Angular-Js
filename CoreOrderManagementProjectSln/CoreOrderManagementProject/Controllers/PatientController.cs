using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreOrderManagementProject.Models;
using CoreOrderManagementProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace CoreOrderManagementProject.Controllers
{
   //[Authorize(Roles = "Admin")]
    public class PatientController : Controller
        {
            private IPatientsRepository _productRepository;
            private readonly IHostingEnvironment _hostingEnviroment;

            public PatientController(IPatientsRepository productRepository, IHostingEnvironment hostingEnvironment)
            {
                _productRepository = productRepository;
                _hostingEnviroment = hostingEnvironment;
            }

            public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
            {
                ViewData["CurrentSort"] = sortOrder;
                ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                if (searchString != null)
                {
                    pageNumber = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                ViewData["CurrentFilter"] = searchString;
                var data = _productRepository.GetPatients();
                var ps = from s in data select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    ps = ps.Where(p => p.PatientName.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        ps = (ps.OrderBy(p => p.PatientName));
                        break;
                    default:
                        ps = (ps.OrderByDescending(p => p.PatientName));
                        break;
                }
                int pageSize = 3;
                return View(
                    PaginetedList<Patient>.Create(ps.AsQueryable<Patient>(), pageNumber ?? 1, pageSize));


            }
            [HttpGet]
            public ActionResult Create()
            {
                CategoryDDL();
                return View();
            }
            [HttpPost]
            public ActionResult Create(CreatePatientViewModel model)
            {
                if (ModelState.IsValid)
                {
                    string urlImage = "";
                    var files = HttpContext.Request.Form.Files;

                    foreach (var image in files)
                    {
                        if (image != null && image.Length > 0)
                        {
                            var file = image;
                            var uploadFile = Path.Combine(_hostingEnviroment.WebRootPath, "images");
                            if (file.Length > 0)
                            {
                                var fileName = Guid.NewGuid().ToString().Replace("_", "") + file.FileName;
                                using (var fileStream = new FileStream(Path.Combine(uploadFile, fileName), FileMode.Create))
                                {
                                    file.CopyTo(fileStream);
                                    urlImage = fileName;
                                }
                            }
                        }
                    }
                    var data = new Patient()
                    {
                        PatientName = model.PatientName,
                        Age = model.Age,
                        DoctorId = model.DoctorId,
                        UrlImage = urlImage
                    };
                    _productRepository.AddPatient(data);
                    return RedirectToAction("Index");
                }
                CategoryDDL();
                return View();
            }
            [HttpGet]
            public ActionResult Edit(int id)
            {
                var patient = _productRepository.GetPatientById(id);
                CategoryDDL();
                return View(patient);
            }
            [HttpPost]
            public IActionResult Edit(int id, Patient changePatient)
            {
                if (ModelState.IsValid)
                {
                    string UrlImage = "";
                    var files = HttpContext.Request.Form.Files;
                    foreach (var image in files)
                    {
                        if (image != null && image.Length > 0)
                        {
                            var file = image;
                            var uploadFile = Path.Combine(_hostingEnviroment.WebRootPath, "images");
                            if (file.Length > 0)
                            {
                                var fileName = Guid.NewGuid().ToString().Replace("_", "") + file.FileName;
                                using (var fileStream = new FileStream(Path.Combine(uploadFile, fileName), FileMode.Create))
                                {
                                    file.CopyTo(fileStream);
                                    UrlImage = fileName;
                                }
                            }

                        }
                    }
                    var data = _productRepository.GetPatientById(id);
                    data.PatientName = changePatient.PatientName;
                    data.Age = changePatient.Age;
                    data.DoctorId = changePatient.DoctorId;
                    if (data.UrlImage != null && data.UrlImage != "")
                    {
                        string fp = Path.Combine(_hostingEnviroment.WebRootPath, "images", data.UrlImage);
                        System.IO.File.Delete(fp);
                    }
                    data.UrlImage = UrlImage;
                    _productRepository.UpdatePatient(data);
                    return RedirectToAction("Index");
                }
                CategoryDDL();
                return View();
            }
            private void CategoryDDL(object doctorSelect = null)
            {
                var doctors = _productRepository.GetDoctors();
                ViewBag.ListofCategry = new SelectList(doctors, "DoctorId", "DoctorName"
                    , doctorSelect);
            }
            public ActionResult Delete(int id)
            {
                _productRepository.DeletePatient(id);
                return RedirectToAction("Index");
            }
        }
    
}
