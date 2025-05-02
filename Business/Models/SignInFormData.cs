using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class SignInFormData
{
    [Display(Name = "Email", Prompt = "Enter email address")]
    [DataType(DataType.EmailAddress)]
    [Required]
    public string Email { get; set; } = null!;

    [Display(Name = "Password", Prompt = "Enter password")]
    [DataType(DataType.Password)]
    [Required]
    public string Password { get; set; } = null!;


    public bool IsPersistent { get; set; }
}
