using System.ComponentModel.DataAnnotations;

namespace Smart_Dilation_Management.DTO.LoginDTO
{
    public class LoginDTO
    {
            [EmailAddress]
            public string Email { get; set; } = null!;
            [Required]
            [Length(4, 20)]
            public string Password { get; set; } = null!;
        }
}

