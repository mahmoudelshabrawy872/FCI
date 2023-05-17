using System.ComponentModel.DataAnnotations;

namespace FCI_API.Models
{
    public class Post
    {
        public int Id { get; set; }
        [MaxLength(600)]
        public string? Titles { get; set; }
        [MaxLength(2500)]
        public string? ImageURL { get; set; }

    }
}
