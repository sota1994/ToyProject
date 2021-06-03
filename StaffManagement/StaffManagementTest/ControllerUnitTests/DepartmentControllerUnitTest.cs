using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using StaffManagement.Controllers;
using StaffManagement.ModelDTOs;
using StaffManagement.Models;
using StaffManagement.Services;
using StaffManagement.ViewModels;
using Xunit;
using Assert = Xunit.Assert;

namespace StaffManagementTest.ControllerUnitTests
{
    public class DepartmentControllerUnitTest
    {
        private readonly Mock<IDepartmentService> _mockDepartmentService;
        private readonly Mock<IEmployeeService> _mockEmployeeService;
        private readonly Mock<IMapper> _mockMapperService;
        public DepartmentControllerUnitTest()
        {
            _mockDepartmentService = new Mock<IDepartmentService>();
            _mockEmployeeService = new Mock<IEmployeeService>();
            _mockMapperService = new Mock<IMapper>();
        }

        private List<Department> GetDepartments()
        {
            var employees = new List<Department>
            {
                new Department()
                {
                    Id = 1,
                    Name = "IT"
                },

                new Department()
                {
                    Id = 2,
                    Name = "Finance"
                }
            };

            return employees;
        }

        private List<DepartmentDTO> getDepartmentsDTO()
        {
            var employees = new List<DepartmentDTO>
            {
                new DepartmentDTO()
                {
                    Id = 1,
                    Name = "IT"
                },

                new DepartmentDTO()
                {
                    Id = 2,
                    Name = "Finance"
                }
            };

            return employees;
        }

        [Fact]
        public void Index_ReturnsAViewResult_WithADepartmentList()
        {
            //Arrange
            _mockDepartmentService.Setup(d => d.GetAll()).Returns(GetDepartments());
            _mockMapperService.Setup(mms => mms.Map<IList<Department>, IList<DepartmentDTO>>(It.IsAny<IList<Department>>())).Returns(getDepartmentsDTO());
            var controller = new DepartmentController(_mockDepartmentService.Object, _mockEmployeeService.Object, _mockMapperService.Object);

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IndexViewModel>(viewResult.ViewData.Model);
            //var actualCount = model.Departments.Count;

            Assert.Equal(2, model.Departments.Count);
        }

        [Fact]
        public void Create_ReturnsRedirectAction_WithAddCalledOnce()
        {
            //Arrange
            //_mockEmployeeService.Setup(mes => mes.Add(It.IsAny<Employee>())).Returns(1);

            var controller = new DepartmentController(_mockDepartmentService.Object, _mockEmployeeService.Object, _mockMapperService.Object);


            //Act
            var result = controller.Create(It.IsAny<Department>());

            //Assert
            _mockDepartmentService.Verify(med => med.Add(It.IsAny<Department>()), Times.Once);
            Assert.IsType<RedirectToActionResult>(result);
            
        }

        [Fact]
        public void Delete_ReturnsRedirectAction_WithDeleteCalledOnce()
        {
            //Arrange
            var controller = new DepartmentController(_mockDepartmentService.Object, _mockEmployeeService.Object, _mockMapperService.Object);

            //Act
            var result = controller.Delete((It.IsAny<Int32>()));

            //Assert
            _mockDepartmentService.Verify(med => med.DeleteById(It.IsAny<Int32>()), Times.Once);
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void GetChildDepartments_ShouldReturnJsonResult()
        {
            //Arrange
            var controller = new DepartmentController(_mockDepartmentService.Object, _mockEmployeeService.Object, _mockMapperService.Object);
            //Act
            var result = controller.GetChildrenDepartments(It.IsAny<int>());
            //Assert
            Assert.IsType<JsonResult>(result);

        }
    }
}
