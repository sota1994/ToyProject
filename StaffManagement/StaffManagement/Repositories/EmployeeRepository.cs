using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StaffManagement.Models;

namespace StaffManagement.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();
       // Employee GetById(int id);
        int Add(Employee employee);

        IQueryable<Employee> GetQueryableByExpression(Expression<Func<Employee, bool>> expression);
        bool DeleteByExpression(Expression<Func<Employee, bool>> expression);
    }
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly StaffManagementDbContext _dbContext;
        public EmployeeRepository(StaffManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Employee employee)
        {
            var employeeId = _dbContext.Employees.Add(employee).Entity.Id;
            Save();
            return employeeId;
        }

        public bool DeleteByExpression(Expression<Func<Employee, bool>> expression)
        {
            var employeeToDeleteList = _dbContext.Employees.Where(expression).ToList();
            if (employeeToDeleteList.Any())
            {
                _dbContext.Employees.RemoveRange(employeeToDeleteList);
                Save();
                return true;
            }

            return false;
        }

        /*public bool DeleteById(int id)
        {
            var employeeToDelete = _dbContext.Employees.Find(id);
            if (employeeToDelete == null) return false;
            _dbContext.Employees.Remove(employeeToDelete);
            Save();
            return true;

        }*/

        public List<Employee> GetAll()
        {
            return _dbContext.Employees
                .Include( e => e.Department)
                .Include(e => e.Employees)
                .Include(e => e.Manager)
                .ToList();
        }

       /* public Employee GetById(int id)
        {
            return _dbContext.Employees.Find(id);
        }*/

        public IQueryable<Employee> GetQueryableByExpression(Expression<Func<Employee, bool>> expression)
        {
            return _dbContext.Employees.Where(expression);
        }

        private void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
