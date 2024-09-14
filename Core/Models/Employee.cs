using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Employee
    {
        public int EmployeeID { get; private set; }
        public string Name { get; private set; }
        public string Department { get; private set; }
        public decimal Salary { get; private set; }

        public Employee(int employeeID, string name, string department, decimal salary)
        {
            EmployeeID = employeeID;
            Name = name;
            Department = department;    
            Salary = salary;
        }
        
        public static Employee Create(int id, string name, string department, decimal salary)
        {
            return new Employee(id, name, department, salary);
        }
        public Employee Update(string name, string department, decimal salary)
        {
            Name = name;
            Department = department;
            Salary = salary;
            return this;
        }
    }
}
