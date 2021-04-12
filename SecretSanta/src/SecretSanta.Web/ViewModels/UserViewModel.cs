using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Web.ViewModels {
  public class UserViewModel {
    public int ID { get; set; }
    [Required]
    [Display(Name="First name")]
    public string FirstName { get; set; } = "";
    [Required]
    [Display(Name="Last name")]
    public string LastName { get; set; } = "";
    public string FullName { get => $"{FirstName} {LastName}"; }
  }
}
