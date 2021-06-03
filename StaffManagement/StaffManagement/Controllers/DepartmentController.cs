using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StaffManagement.ModelDTOs;
using StaffManagement.Models;
using StaffManagement.Services;
using StaffManagement.ViewModels;

namespace StaffManagement.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, IEmployeeService employeeService, IMapper mapper)
        {
            _departmentService = departmentService;
            _employeeService = employeeService;
            _mapper = mapper;
        }

        public JsonResult GetChildrenDepartments(int id)
        {
            var childDepartments = _departmentService.GetChildDepartments(id);
            var childDepartmentsDTO = _mapper.Map<IList<Department>, IList<DepartmentDTO>>(childDepartments);

            return new JsonResult(childDepartmentsDTO);
        }
        public IActionResult Index()
        {
            var departments = _departmentService.GetRootDepartments();
            var departmentsDTO = _mapper.Map<IList<Department>, IList<DepartmentDTO>>(departments);
            var model = new IndexViewModel()
            {
                Departments = departmentsDTO
            };
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            _departmentService.Add(department);

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _departmentService.DeleteById(id);

            return RedirectToAction("Index");
        }
    }
}
