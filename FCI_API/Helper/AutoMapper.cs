using AutoMapper;
using FCI_API.Dto.Post;
using FCI_API.Models;

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
        }
    }
}