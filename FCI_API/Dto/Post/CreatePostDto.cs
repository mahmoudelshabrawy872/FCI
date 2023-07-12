using System.ComponentModel.DataAnnotations;

namespace FCI_API.Dto.Post
{
    public class CreatePostDto
    {
        [MaxLength(600)]
        public string Titles { get; set; } = null!;
        [MaxLength(2500)]
        public string Body { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;


    }
}
