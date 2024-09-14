using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Newtonsoft.Json;
using Core.Models;
using System.Text;

namespace organizationEmployeesAPI.Tests.Tests
{
    public class EmployeeControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public EmployeeControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllEmployees_ShouldReturnSuccessStatusCode()
        {
            // Act
            var response = await _client.GetAsync("/api/employees");

            // Assert
            response.EnsureSuccessStatusCode(); 
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetEmployeeById_WithValidId_ShouldReturnEmployee()
        {
            // Arrange
            int validId = 4; // Assume this ID exists

            // Act
            var response = await _client.GetAsync($"/api/employees/{validId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var employee = JsonConvert.DeserializeObject<Employee>(content);
            employee.Should().NotBeNull();
            employee.EmployeeID.Should().Be(validId);
        }

        [Fact]
        public async Task GetEmployeeById_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            int invalidId = 9999; // Assume this ID doesn't exist

            // Act
            var response = await _client.GetAsync($"/api/employees/{invalidId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateEmployee_WithValidData_ShouldReturnCreatedEmployee()
        {
            // Arrange
            var newEmployee = new Employee(0, "John Doe", "IT", 50000);
            var content = new StringContent(JsonConvert.SerializeObject(newEmployee), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/employees", content);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var responseContent = await response.Content.ReadAsStringAsync();
            var createdEmployee = JsonConvert.DeserializeObject<Employee>(responseContent);
            createdEmployee.Should().NotBeNull();
            createdEmployee.Name.Should().Be("John Doe");
        }

    }
}