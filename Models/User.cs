using Smart_Dilation_Management.Enums;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int Id { get; set; }

    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!; 
    public UserRole Role { get; set; }
    public bool IsFree { get; set; }=true;

}
