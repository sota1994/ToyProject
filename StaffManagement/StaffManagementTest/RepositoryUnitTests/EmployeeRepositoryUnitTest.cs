using System;
using Microsoft.EntityFrameworkCore;
using StaffManagement.Models;
using StaffManagement.Repositories;
using System.Linq;
using Moq;
using Xunit;
using System.Linq.Expressions;

namespace StaffManagementTest
{
    public class EmployeeRepositoryUnitTest
    {
        private readonly StaffManagementDbContext _dbContext;
        public EmployeeRepositoryUnitTest()
        {
            var options = new DbContextOptionsBuilder<StaffManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;

            _dbContext = new StaffManagementDbContext(options);
        }
        [Fact]
        public void EmployeeRepositoryGetAllTest()
        {
            var employeeRepository = new EmployeeRepository(_dbContext);

            var expectedEmployeeList = _dbContext.Employees
                                            .Include(e => e.Department)
                                            .Include(e => e.Employees)
                                            .Include(e => e.Manager)
                                            .ToList(); 

            var actualEmployeeList = employeeRepository.GetAll();

            Assert.Equal(expectedEmployeeList, actualEmployeeList);
        }

        [Fact]
        public void EmployeeRepositoryGetByIdTest()
        {
            var employeeRepository = new EmployeeRepository(_dbContext);

            var dummyId = It.IsAny<Int32>();

            var expectedEmployee = _dbContext.Employees.Find(dummyId);

            Expression<Func<Employee, bool>> findByIdExpression = e => e.Id == dummyId;

            var actualEmployee = employeeRepository.GetQueryableByExpression(findByIdExpression).FirstOrDefault();

            Assert.Equal(expectedEmployee, actualEmployee);
        }

        [Fact]
        public void EmployeeRepositoryAddTest()
        {
            var employeeRepository = new EmployeeRepository(_dbContext);
            var mockEmployee = new Mock<Employee>();
            var beforeAddOneEmployeeCount = _dbContext.Employees.Count();
            employeeRepository.Add(mockEmployee.Object);
            var afterAddOneEmployeeCount = _dbContext.Employees.Count();
            Assert.Equal(beforeAddOneEmployeeCount + 1, afterAddOneEmployeeCount);

        }

        [Fact]
        public void EmployeeRepositoryDeleteByIdTest()
        {
            var actualEmployeeRepository = new EmployeeRepository(_dbContext);

            var mockEmployee = new Mock<Employee>();

            var newAddedEmployeeId = _dbContext.Add(mockEmployee.Object).Entity.Id;
            _dbContext.SaveChanges();

            var beforeDeleteOneEmployee = _dbContext.Employees.Count();

            Expression<Func<Employee, bool>> expression = e => e.Id == newAddedEmployeeId;

            actualEmployeeRepository.DeleteByExpression(expression);

            _dbContext.SaveChanges();

            var afterDeleteOneEmployee = _dbContext.Departments.Count();

            Assert.Equal(expected: beforeDeleteOneEmployee - 1, afterDeleteOneEmployee);
        }


    }
}
