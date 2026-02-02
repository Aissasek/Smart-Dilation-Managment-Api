using System.ComponentModel.DataAnnotations;

namespace Smart_Dilation_Management.DTO.PatientDTO
{
    public class UpdatePatient
    {
        [MaxLength(100)]
        public string FullName { get; set; } = null!;
        public int DoctorId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Status { get; set; }
        public int? DropsGiven { get; set; }
        public int? DropsRequired { get; set; }
        public int? DropTypeId { get; set; }
        public DateTime LastDropTime { get; set; }
    }
}
