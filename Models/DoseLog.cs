using Smart_Dilation_Management.Models;
using System.ComponentModel.DataAnnotations;

public class DoseLog
{
    [Key]
    public int Id { get; set; }

    public DateTime GivenAt { get; set; } = DateTime.Now;

    public int DilationOrderId { get; set; }
    public DilationOrder DilationOrder { get; set; } = null!;
    public int StaffId { get; set; }
    public User Staff { get; set; } = null!;
}
