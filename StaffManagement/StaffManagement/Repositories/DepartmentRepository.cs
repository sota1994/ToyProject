using StaffManagement.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;


namespace StaffManagement.Repositories
{
    public interface IDepartmentRepository
    {
        IQueryable<Department> GetQueryableByExpression(Expression<Func<Department, bool>> expression);
        Department GetById(int id);

        bool DeleteByExpression(Expression<Func<Department, bool>> expression );
        int Add(Department department);
    }
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly StaffManagementDbContext _dbContext;

        public DepartmentRepository(StaffManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Department department)
        {
            var addId = _dbContext.Departments.Add(department);
            Save();
            return addId.Entity.Id;
        }

        /*public bool DeleteById(int id)
        {
            var departmentToDelete = _dbContext.Departments.Find(id);
            if (departmentToDelete == null) return false;
            _dbContext.Departments.Remove(departmentToDelete);
            //var express = Predicate();

            _dbContext.Departments
                 .Where(express)
                 .Where(express)
                 .Where(express)
                 .Where(express);



            Save();
            return true;
        }*/
        
        public IQueryable<Department> GetQueryableByExpression(Expression<Func<Department, bool>> expression)
        {
            return _dbContext.Departments.Where(expression).Include( d=>d.ChildDepartments);
        }


        public Department GetById(int id)
        {
            return _dbContext.Departments.Find(id);
        }


        private void Save()
        {
            _dbContext.SaveChanges();
        }

        public bool DeleteByExpression(Expression<Func<Department, bool>> expression)
        {
            var departmentList = _dbContext.Departments.Where(expression).ToList();
            if (departmentList.Any())
            {
                _dbContext.Remove(departmentList);
                Save();
                return true;
            }

            return false;
        }

    }
}
