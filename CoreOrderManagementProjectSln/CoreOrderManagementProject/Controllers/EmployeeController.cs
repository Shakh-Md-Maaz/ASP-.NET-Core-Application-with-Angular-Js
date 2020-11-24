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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreOrderManagementProject.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        //SqlEmployeeRepository repo = new SqlEmployeeRepository();
        public EmployeeController(IEmployeeRepository employeeRepository, IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            this._hostingEnvironment = hostingEnvironment;
        }
        [AllowAnonymous]
        public ViewResult Index()
        {
            List<Employee> employees = _employeeRepository.GetEmployees().ToList();
            return View(employees);
        }
        [AllowAnonymous]
        public ViewResult Details(int id)
        {

            //throw new Exception(); 
            Employee emp = _employeeRepository.GetEmployeeDeatils(id);
            if (emp == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id);
            }
            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel()
            {
                Employee = _employeeRepository.GetEmployeeDeatils(id),
                PageTitle = "Employee Details"
            };
            return View(homeIndexViewModel);
        }
        [HttpGet]

        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessFileUpload(model);
                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                _employeeRepository.AddEmployee(newEmployee);
                return RedirectToAction("Details", new { id = newEmployee.Id });
            }
            return View();
        }
        [HttpGet]

        public ViewResult Update(int id)
        {
            Employee employee = _employeeRepository.GetEmployeeDeatils(id);
            EditEmployeeViewModel model = new EditEmployeeViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Email = employee.Email,
                ExistingPhotoPath = employee.PhotoPath,
                PageTitle = "Employee Details"
            };
            return View(model);
        }
        [HttpPost]

        public IActionResult Update(EditEmployeeViewModel obj)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployeeDeatils(obj.Id);
                employee.Name = obj.Name;
                employee.Email = obj.Email;
                employee.Department = obj.Department;
                if (obj.Photo != null)
                {
                    if (obj.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", obj.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = ProcessFileUpload(obj);
                }

                Employee emp = _employeeRepository.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }
            return View();
        }

        private string ProcessFileUpload(CreateEmployeeViewModel obj)
        {
            string uniqueFileName = null;
            if (obj.Photo != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + obj.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    obj.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        [HttpPost]

        public IActionResult Delete(int id)
        {
            Employee employee = _employeeRepository.GetEmployeeDeatils(id);
            if (employee != null)
            {
                Employee deleteEmployee = _employeeRepository.DeleteEmpoyee(employee.Id);
                return RedirectToAction("index");
            }
            return View();
        }
    }
}
