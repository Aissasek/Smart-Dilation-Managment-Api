using System.ComponentModel.DataAnnotations;

namespace Smart_Dilation_Management.DTO.PatientDTO
{
    public class AddNewPatient
    {

        [MaxLength(100)]
        public string FullName { get; set; } = null!;
        public int DoctorId { get; set; } 
        public DateTime DateOfBirth { get; set; }
    }
}
