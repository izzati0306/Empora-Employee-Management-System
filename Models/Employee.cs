using System.ComponentModel.DataAnnotations;

namespace WorksAway.Models
{
public class Employee
{
    [Key]
    public int EmployeeID { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Email { get; set; }
}
}