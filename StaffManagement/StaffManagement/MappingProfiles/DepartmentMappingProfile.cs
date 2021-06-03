using AutoMapper;
using StaffManagement.ModelDTOs;
using StaffManagement.Models;

namespace StaffManagement.MappingProfiles
{
    public class DepartmentMappingProfile : Profile
    {
        public DepartmentMappingProfile()
        {
            CreateMap<Department, DepartmentDTO>();
        }
    }
}
