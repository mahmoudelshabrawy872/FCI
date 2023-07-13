using System.ComponentModel.DataAnnotations;

namespace FCI_DataAccess.Models
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]

        public string Password { get; set; }
    }
}
