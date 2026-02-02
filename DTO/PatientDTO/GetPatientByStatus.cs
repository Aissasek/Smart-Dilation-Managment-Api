using System.ComponentModel.DataAnnotations;

namespace Smart_Dilation_Management.DTO.PatientDTO
{
    public class GetPatientByStatus
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string FullName { get; set; } = null!;
        public string DoctorName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string? Status { get; set; }
        public int? DropsGiven { get; set; }
        public int? DropsRequired { get; set; }
        public string? DropType { get; set; }
    }
}
