using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Web.ViewModels {
  public class GroupViewModel {
    public int ID { get; set; }
    [Required]
    [Display(Name="Name")]
    public string Name { get; set; } = "";
  }
}
