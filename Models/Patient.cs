using Smart_Dilation_Management.Enums;
using Smart_Dilation_Management.Models;
using System.ComponentModel.DataAnnotations;

namespace Smart_Dilation_Management.Class
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string FullName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }

        public int DilantionId { get; set; }
        public DilationOrder DilationOrder { get; set; } = null!;

    }
}
