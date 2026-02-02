using Smart_Dilation_Management.Class;
using Smart_Dilation_Management.Enums;
using System.ComponentModel.DataAnnotations;

namespace Smart_Dilation_Management.Models
{
    public class DilationOrder
    {
        [Key]
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;
        public int? EyeDropId { get; set; }
        public EyeDrop EyeDrop { get; set; } = null!; 
        public int DoctorId { get; set; }
        public User Doctor { get; set; } = null!;
        public int? DropsGiven { get; set; }
        public int? DropsRequired { get; set; }
        public PatientStatus Status { get; set; } = PatientStatus.Waiting;
       
       
        public ICollection<DoseLog>? DoseLogs { get; set; }

    }
}
