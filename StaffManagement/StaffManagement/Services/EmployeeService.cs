using System;
using StaffManagement.Models;
using StaffManagement.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StaffManagement.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetAll();
        Employee GetById(int id);

        bool DeleteById(int id);

        int Add(Employee employee);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public int Add(Employee employee)
        {
            return _employeeRepository.Add((employee));
        }

        public bool DeleteById(int id)
        {
            Expression<Func<Employee, bool>> getByIdExpression = e => e.Id == id;
            return _employeeRepository.DeleteByExpression(getByIdExpression);
        }

        public List<Employee> GetAll()
        {
            //throw new NotImplementedException();
            return _employeeRepository.GetAll();
        }

        public Employee GetById(int id)
        {
            Expression<Func<Employee, bool>> getByIdExpression = e => e.Id == id;
            return _employeeRepository.GetQueryableByExpression(getByIdExpression).FirstOrDefault();
        }

    }
}
