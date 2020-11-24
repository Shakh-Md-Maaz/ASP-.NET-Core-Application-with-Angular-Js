using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreOrderManagementProject.Models
{
     public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployeeDeatils(int id);
        Employee AddEmployee(Employee objEmployee);
        Employee UpdateEmployee(Employee changeEmployee);
        Employee DeleteEmpoyee(int id);
    }
}
