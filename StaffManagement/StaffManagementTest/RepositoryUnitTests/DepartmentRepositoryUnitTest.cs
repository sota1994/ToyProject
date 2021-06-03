using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Moq;
using StaffManagement.Models;
using StaffManagement.Repositories;
using Xunit;

namespace StaffManagementTest.RepositoryUnitTests
{
    public class DepartmentRepositoryUnitTest
    {
        private readonly StaffManagementDbContext _fakeDbContext;

        public DepartmentRepositoryUnitTest()
        {
            var options = new DbContextOptionsBuilder<StaffManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;

            _fakeDbContext = new StaffManagementDbContext(options);
        }


        [Fact]
        public void DepartmentRepositoryGetAllTest()
        {
            var actualRepository = new DepartmentRepository(_fakeDbContext);

            var expectedDepartmentList = _fakeDbContext.Departments.ToList();
            //var expectedDepartmentList = _fakeDbContext.Departments.ToList();
            Expression<Func<Department, bool>> getAllExpression = d => true;
            var actualDepartmentList = actualRepository.GetQueryableByExpression((getAllExpression));
            Assert.Equal(expected: expectedDepartmentList, actual: actualDepartmentList);
        }

        [Fact]
        public void DepartmentRepositoryGetByIdTest()
        {
            var testIndex = It.IsAny<Int32>();

            var expectedDepartment = _fakeDbContext.Departments.Find(testIndex);

            var actualRepository = new DepartmentRepository(_fakeDbContext);
            var actualDepartment = actualRepository.GetById(testIndex);

            Assert.Equal(expectedDepartment, actualDepartment);

        }

        [Fact]
        public void DepartmentRepositoryDeleteByIdTest()
        {
            var actualDepartmentRepository = new DepartmentRepository(_fakeDbContext);

            var newAddedDepartmentId = _fakeDbContext.Add(new Department()
            {
                Name = "New Department",

                Id = 100

            }).Entity.Id;

            actualDepartmentRepository.DeleteByExpression(d => d.Id == newAddedDepartmentId);
            //Expression<Func<Department, bool>> getAllExpression = d => true;
            Assert.Empty(_fakeDbContext.Departments);
        }

        [Fact]
        public void DepartmentRepositoryAddOneDepartmentTest()
        {
            var actualDepartmentRepository = new DepartmentRepository(_fakeDbContext);

            var beforeAddNewDepartment = _fakeDbContext.Departments.Count();

            var mockDepartment = new Mock<Department>();

            actualDepartmentRepository.Add(mockDepartment.Object);
            var afterAddNewDepartment = _fakeDbContext.Departments.Count();

            Assert.Equal(expected: beforeAddNewDepartment + 1, afterAddNewDepartment);

        }

        [Fact]
        public void DepartmentRepository_GetRootDepartments_Test()
        {
            var testDepartmentList = new List<Department>()
            {
                new Department()
                {
                    Id = 1,
                    Name = "IT Department",
                },
                
                new Department()
                {
                    Id = 2,
                    Name = "Finance Department",
                },

                new Department()
                {
                    Id = 3,
                    Name = "Support Department",
                },

                new Department()
                {
                    Id = 4,
                    Name = ".NET Department",
                    ParentId = 1
                }
            };
            _fakeDbContext.Departments.AddRange(testDepartmentList);

            _fakeDbContext.SaveChanges();
            var actualDepartmentRepository = new DepartmentRepository(_fakeDbContext);
            Expression<Func<Department, bool>> findRootDepartmentsExpression = d => d.ParentId == null;
            var actualRootDepartments = actualDepartmentRepository
                .GetQueryableByExpression(findRootDepartmentsExpression).ToList();
            var actualRootDepartmentsCount = actualRootDepartments.Count;
            Assert.Equal(3, actualRootDepartmentsCount );
        }
    }
}
