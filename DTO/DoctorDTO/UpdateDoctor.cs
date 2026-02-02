using System.ComponentModel.DataAnnotations;

namespace Smart_Dilation_Management.DTO.DoctorDTO
{
    public class UpdateDoctor
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
    }
}
