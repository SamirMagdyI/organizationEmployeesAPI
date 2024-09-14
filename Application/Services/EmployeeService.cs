using Core.Interfaces;
using Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Employee> _validator;

        public EmployeeService(IUnitOfWork unitOfWork, IValidator<Employee> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        // Retrieve all employees
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _unitOfWork.Employees.GetAllAsync();
        }

        // Retrieve an employee by ID
        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _unitOfWork.Employees.GetByIdAsync(id);
        }

        // Add a new employee
        public async Task<(bool Success, string[] Errors)> AddEmployeeAsync(Employee employee)
        {
            var validationResult = await _validator.ValidateAsync(employee);

            if (!validationResult.IsValid)
            {
                return (false, validationResult.Errors.Select(e => e.ErrorMessage).ToArray());
            }

            await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.CompleteAsync();

            return (true, Array.Empty<string>());
        }

        // Update an existing employee
        public async Task<(bool Success, string[] Errors)> UpdateEmployeeAsync(Employee employee)
        {
            var validationResult = await _validator.ValidateAsync(employee);

            if (!validationResult.IsValid)
            {
                return (false, validationResult.Errors.Select(e => e.ErrorMessage).ToArray());
            }

            await _unitOfWork.Employees.UpdateAsync(employee);
            await _unitOfWork.CompleteAsync();

            return (true, Array.Empty<string>());
        }

        // Delete an employee by ID
        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            await _unitOfWork.Employees.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
