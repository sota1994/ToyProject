using System;
using Moq;
using StaffManagement.Models;
using StaffManagement.Services;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StaffManagement.Controllers;
using Xunit;

namespace StaffManagementTest.ControllerUnitTests
{
    public class StaffControllerUnitTest
    {
        private readonly Mock<IDepartmentService> _mockDepartmentService;
        private readonly Mock<IEmployeeService> _mockEmployeeService;
        public StaffControllerUnitTest()
        {
            _mockDepartmentService = new Mock<IDepartmentService>();
            _mockEmployeeService = new Mock<IEmployeeService>();
        }

        private List<Employee> GetEmployees()
        {
            var employees = new List<Employee>
            {
                new Employee()
                {
                    Id = 1,
                    Name = "Doe Doe",
                    DepartmentId = 1
                },

                new Employee()
                {
                    Id = 2,
                    Name = "John John",
                    DepartmentId = 2
                }
            };

            return employees;
        }
        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfEmployees()
        {
            //Arrange
            _mockEmployeeService.Setup(s => s.GetAll()).Returns(GetEmployees());

            var controller = new StaffController(_mockDepartmentService.Object, _mockEmployeeService.Object);

            //Act
            var result = controller.Index();

            //Assert

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Employee>>(viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void StaffControllerCreateTest()
        {
            //Arrange
            //_mockEmployeeService.Setup(mes => mes.Add(It.IsAny<Employee>())).Returns(1);

            var controller = new StaffController(_mockDepartmentService.Object, _mockEmployeeService.Object);


            //Act
            var result = controller.Create(It.IsAny<Employee>());

            //Assert
            _mockEmployeeService.Verify(mes => mes.Add(It.IsAny<Employee>()), Times.Once);
            Assert.IsType<RedirectToActionResult>(result);
            //Assert.Equal(1, result.);
            
        }

        [Fact]
        public void StaffControllerDeleteTest()
        {
            //Arrange
            var controller = new StaffController(_mockDepartmentService.Object, _mockEmployeeService.Object);

            //Act
            var result = controller.Delete((It.IsAny<Int32>()));
            
            //Assert
            _mockEmployeeService.Verify(mes => mes.DeleteById(It.IsAny<Int32>()), Times.Once);
            Assert.IsType<RedirectToActionResult>(result);
        }


    }
}
