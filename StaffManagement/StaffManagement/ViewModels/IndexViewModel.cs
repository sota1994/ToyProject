using StaffManagement.ModelDTOs;
using System.Collections.Generic;

namespace StaffManagement.ViewModels
{
    public class IndexViewModel
    {
        public IList<DepartmentDTO> Departments { get; set; }
    }
}
