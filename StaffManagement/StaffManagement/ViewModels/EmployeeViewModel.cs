using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StaffManagement.ViewModels
{
    public class EmployeeViewModel
    {
        public string Name { get; set; }

        public string ManagerName { get; set; }

        public string DepartmentName { get; set; }

        public List<SelectListItem> EmployeeNames { get; set; }

    }
}
