using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffManagement.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("Manager")]
        public int? ManagerId { get; set; }
        public Employee Manager { get; set;}
       

        
        
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

    }
}
