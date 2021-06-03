using Microsoft.AspNetCore.Mvc;
using StaffManagement.Models;
using StaffManagement.Services;

namespace StaffManagement.Controllers
{
    public class StaffController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;
        public StaffController(IDepartmentService departmentService, IEmployeeService employeeService)
        {
            _departmentService = departmentService;
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            var model = _employeeService.GetAll();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            //if (ModelState.IsValid) 
            _employeeService.Add(employee);
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _employeeService.DeleteById(id);
            return RedirectToAction("Index");
        }

    }
}
