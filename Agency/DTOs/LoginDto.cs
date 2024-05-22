using System.ComponentModel.DataAnnotations;

namespace Agency.DTOs
{
    public class LoginDto
    {
        [Required]
        public string UserNameOrPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemember {  get; set; }
    }
}
