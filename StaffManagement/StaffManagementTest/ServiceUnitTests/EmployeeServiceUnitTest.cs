using Moq;
using StaffManagement.Models;
using StaffManagement.Repositories;
using StaffManagement.Services;
using System;
using System.Linq.Expressions;
using Xunit;

namespace StaffManagementTest
{
    public class EmployeeServiceUnitTest
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        public EmployeeServiceUnitTest()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();

        }
        [Fact]
        public void EmployeeServiceGetAllTest()
        {
            var employeeService = new EmployeeService(_mockEmployeeRepository.Object);

            employeeService.GetAll();

            _mockEmployeeRepository.Verify(m => m.GetAll(), Times.Once);

        }

        [Fact]
        public void EmployeeServiceGetByIdTest()
        {
            var employeeService = new EmployeeService(_mockEmployeeRepository.Object);

            employeeService.GetById(It.IsAny<Int32>());

            _mockEmployeeRepository.Verify(m => m.GetQueryableByExpression(It.IsAny<Expression<Func<Employee,bool>>>()), Times.Once);

        }

        [Fact]
        public void EmployeeServiceDeleteByIdTest()
        {
            var employeeService = new EmployeeService(_mockEmployeeRepository.Object);

            employeeService.DeleteById(It.IsAny<Int32>());

            _mockEmployeeRepository.Verify(m => m.DeleteByExpression(It.IsAny<Expression<Func<Employee, bool>>>()), Times.Once);

        }

        [Fact]
        public void EmployeeServiceAddTest()
        {
            var employeeService = new EmployeeService(_mockEmployeeRepository.Object);

            employeeService.Add(It.IsAny<Employee>());

            _mockEmployeeRepository.Verify(m => m.Add(It.IsAny<Employee>()), Times.Once);
        }

       

    }
}
