using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffManagement.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Department")]
        public int? ParentId { get; set; }

        public Department Parent { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Department> ChildDepartments { get; set; }

    }
}
