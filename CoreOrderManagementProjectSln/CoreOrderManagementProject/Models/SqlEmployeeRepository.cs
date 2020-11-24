using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreOrderManagementProject.Models
{
    public class SqlEmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public SqlEmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Employee AddEmployee(Employee objEmployee)
        {
            _context.Employees.Add(objEmployee);
            _context.SaveChanges();
            return objEmployee;
        }

        public Employee DeleteEmpoyee(int id)
        {
            Employee deleteEmployee = GetEmployeeDeatils(id);
            if (deleteEmployee != null)
            {
                _context.Employees.Remove(deleteEmployee);
                _context.SaveChanges();
            }
            return deleteEmployee;
        }

        public Employee GetEmployeeDeatils(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees;
        }

        public Employee UpdateEmployee(Employee changeEmployee)
        {
            var upEmployee = _context.Employees.Attach(changeEmployee);
            upEmployee.State = EntityState.Modified;
            _context.SaveChanges();
            return changeEmployee;
        }
    }
}