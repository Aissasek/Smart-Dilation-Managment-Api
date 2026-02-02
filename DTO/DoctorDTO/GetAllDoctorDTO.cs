using System.ComponentModel.DataAnnotations;

namespace Smart_Dilation_Management.DTO.DoctorDTO
{
    public class GetAllDoctorDTO
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsFree { get; set; }
    }
}
