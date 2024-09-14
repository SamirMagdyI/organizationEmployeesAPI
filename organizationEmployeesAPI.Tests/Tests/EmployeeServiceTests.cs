using Application.Services;
using Core.Interfaces;
using Core.Models;
using FluentValidation;
using Moq;
using FluentAssertions;
using FluentValidation.Results;


namespace organizationEmployeesAPI.Tests.Tests
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IValidator<Employee>> _mockValidator;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockValidator = new Mock<IValidator<Employee>>();
            _employeeService = new EmployeeService(_mockUnitOfWork.Object, _mockValidator.Object);
        }

        [Fact]
        public async Task GetAllEmployeesAsync_ShouldReturnAllEmployees()
        {
            // Arrange
            var expectedEmployees = new List<Employee>
            {
                Employee.Create(1, "samir","IT", 30000),
                Employee.Create(2, "Mostafa","IT", 50000)
            };
            _mockUnitOfWork.Setup(uow => uow.Employees.GetAllAsync()).ReturnsAsync(expectedEmployees);

            // Act
            var result = await _employeeService.GetAllEmployeesAsync();

            // Assert
            Assert.Equal(expectedEmployees, result);
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_WithValidId_ShouldReturnEmployee()
        {
            // Arrange
            var expectedEmployee = Employee.Create(1, "samir", "IT", 30000);
            _mockUnitOfWork.Setup(uow => uow.Employees.GetByIdAsync(1)).ReturnsAsync(expectedEmployee);

            // Act
            var result = await _employeeService.GetEmployeeByIdAsync(1);

            // Assert
            Assert.Equal(expectedEmployee, result);
        }

        [Fact]
        public async Task AddEmployeeAsync_WithValidEmployee_ShouldAddEmployee()
        {
            // Arrange
            var newEmployee = Employee.Create(1, "samir", "IT", 30000);
            _mockValidator.Setup(v => v.ValidateAsync(newEmployee, default)).ReturnsAsync(new ValidationResult());

            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockUnitOfWork.Setup(uow => uow.Employees).Returns(mockEmployeeRepository.Object);

            // Act
            await _employeeService.AddEmployeeAsync(newEmployee);

            // Assert
            mockEmployeeRepository.Verify(repo => repo.AddAsync(newEmployee), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }
    }

}

