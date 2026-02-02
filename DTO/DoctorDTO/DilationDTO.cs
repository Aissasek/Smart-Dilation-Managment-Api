using Smart_Dilation_Management.Class;
using Smart_Dilation_Management.Enums;
using Smart_Dilation_Management.Models;
using System.ComponentModel.DataAnnotations;

namespace Smart_Dilation_Management.DTO.DoctorDTO
{
    public class DilationDTO
    {
        public int EyeDropId { get; set; }
        public int StaffId { get; set; }
        public int? DropsRequired { get; set; }
        public string Message { get; set; } = null!;
    }
}
