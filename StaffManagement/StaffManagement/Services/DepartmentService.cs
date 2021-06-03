using System;
using StaffManagement.Models;
using StaffManagement.Repositories;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace StaffManagement.Services
{
    public interface IDepartmentService
    {
        List<Department> GetAll();
        Department GetById(int id);

        bool DeleteById(int id);

        int Add(Department department);

        IList<Department> GetRootDepartments();

        IList<Department> GetChildDepartments(int id);

    }

    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public int Add(Department department)
        {
            return _departmentRepository.Add(department);
        }

        public bool DeleteById(int id)
        {
            return _departmentRepository.DeleteByExpression(d => d.Id == id);
        }

        public List<Department> GetAll()
        {
            return _departmentRepository.GetQueryableByExpression(d => true).ToList();
            //throw new NotImplementedException();
        }

        public Department GetById(int id)
        {
            return _departmentRepository.GetQueryableByExpression(d => d.Id == id).FirstOrDefault();
        }

        public IList<Department> GetRootDepartments()
        {

            return _departmentRepository.GetQueryableByExpression(d => d.ParentId == null).ToList();
        }

        /*private void VisitNode(Department d, int height, IList<HierarchyDepartment> hierarchyDepartments)
        {
            //Add this department to hierarchy list
            var hierarchyDepartment = new HierarchyDepartment()
            {
                Name = d.Name,
                Level = height
            };
            hierarchyDepartments.Add(hierarchyDepartment);
            //Visit children nodes
            foreach (var department in d.ChildDepartments)
            {
                VisitNode(department, height + 1, hierarchyDepartments);
            }
        }
        public IList<HierarchyDepartment> GetHierarchyDepartments()
        {
            var rootDepartments = GetRootDepartments();

            var result = new List<HierarchyDepartment>();

            foreach (var rootDepartment in rootDepartments)
            {
                VisitNode(rootDepartment, 0, result);
            }

            return result;
        }*/

        public IList<Department> GetChildDepartments(int id)
        {
            var parentDepartment = GetById(id);
            return parentDepartment?.ChildDepartments?.ToList();
            //throw new NotImplementedException();
        }

    }
}
