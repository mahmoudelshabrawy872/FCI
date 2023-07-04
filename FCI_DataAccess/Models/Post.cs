using System.ComponentModel.DataAnnotations;

namespace FCI_API.Models
{
    public class Post
    {
        public int Id { get; set; }
        [MaxLength(600)] public string Titles { get; set; } = null!;
        [MaxLength(2500)] public string Body { get; set; } = null!;
        public string? ImageURL { get; set; }
        public DateTime AddedOn { get; set; } = DateTime.Now;
    }
}
