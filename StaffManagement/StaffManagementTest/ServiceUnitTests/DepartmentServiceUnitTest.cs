using Moq;
using StaffManagement.Models;
using StaffManagement.Repositories;
using StaffManagement.Services;
using System;
using System.Linq.Expressions;
using Xunit;

namespace StaffManagementTest
{
    public class DepartmentServiceUnitTest
    {

        private readonly Mock<IDepartmentRepository> _mockDepartmentRepository;
        public DepartmentServiceUnitTest()
        {
            _mockDepartmentRepository = new Mock<IDepartmentRepository>();
        }
        [Fact]
        public void DepartmentServiceGetAllTest()
        {

            IDepartmentService departmentService = new DepartmentService(_mockDepartmentRepository.Object);

            departmentService.GetAll();

            _mockDepartmentRepository.Verify(m => m.GetQueryableByExpression(It.IsAny<Expression<Func<Department, bool>>>()), Times.Once);

            //Assert.Single(actualDepartment);
        }

        [Fact]
        public void DepartmentServiceGetByIdTest()
        {
            IDepartmentService departmentService = new DepartmentService(_mockDepartmentRepository.Object);
            departmentService.GetById(It.IsAny<Int32>());
            _mockDepartmentRepository.Verify(m => m.GetQueryableByExpression(It.IsAny<Expression<Func<Department, bool>>>()), Times.Once);

        }

        [Fact]
        public void DepartmentServiceDeleteByIdTest()
        {
            IDepartmentService departmentService = new DepartmentService(_mockDepartmentRepository.Object);
            departmentService.DeleteById(It.IsAny<Int32>());
            _mockDepartmentRepository.Verify(m => m.DeleteByExpression(It.IsAny<Expression<Func<Department, bool>>>()), Times.Once);
        }

        [Fact]
        public void DepartmentServiceAddTest()
        {
            IDepartmentService departmentService = new DepartmentService(_mockDepartmentRepository.Object);
            departmentService.Add(It.IsAny<Department>());
            _mockDepartmentRepository.Verify(m => m.Add(It.IsAny<Department>()), Times.Once);
        }

        [Fact]

        public void EmployeeService_GetRootDepartmentsTest()
        {
            IDepartmentService departmentService = new DepartmentService(_mockDepartmentRepository.Object);

            departmentService.GetRootDepartments();

            _mockDepartmentRepository.Verify(m => m.GetQueryableByExpression(It.IsAny<Expression<Func<Department, bool>>>()), Times.Once);
        }

        [Fact]
        public void GetChildDepartmentsTest()
        {

            //Arrange
            IDepartmentService departmentService = new DepartmentService(_mockDepartmentRepository.Object);

            //Act
            departmentService.GetChildDepartments(It.IsAny<Int32>());

            //Assert
            _mockDepartmentRepository.Verify(m => m.GetQueryableByExpression(It.IsAny<Expression<Func<Department, bool>>>()), Times.Once);
            //Assert.IsType<IList>(childDepartmentList);
        }


    }
}
