using AutoMapper;
using StaffManagement.Models;
using StaffManagement.ViewModels;
using System.Linq;

namespace StaffManagement.MappingProfiles
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Employee, EmployeeViewModel>()
                .ForMember(
                    dest => dest.DepartmentName,
                    opt => opt.MapFrom(src => src.Department.Name)
                )
                .ForMember(
                    dest => dest.ManagerName,
                    opt => opt.MapFrom(src => src.Manager.Name)
                    )
                .ForMember(
                        dest => dest.EmployeeNames,
                        opt => opt.MapFrom(src => src.Employees.Select(e => e.Name))
                    );
        }
    }
}
