using AutoMapper;
using FCI_API.Dto.Department;
using FCI_API.Dto.Post;
using FCI_API.Dto.Subject;
using FCI_API.Models;
using FCI_DataAccess.Models;

namespace FCI_API.Helper
{
    /// <summary>
    /// Represents the AutoMapper configuration for mapping between entities and DTOs.
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperProfile"/> class.
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<Post, CreatePostDto>().ReverseMap();
            CreateMap<Subject, CreateSubjectDto>().ReverseMap();
            CreateMap<Subject, SubjectDto>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Department, CreateDepartmentDto>().ReverseMap();
        }
    }
}