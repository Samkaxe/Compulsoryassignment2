using System.ComponentModel.DataAnnotations;

namespace Core;

public class Patient
{
    [Key]
    public string SSN { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public List<Measurements> Measurements { get; set; } 
}