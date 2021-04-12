using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Web.ViewModels {
  public class GiftViewModel {
    public int ID { get; set; }
    [Required]
    [Display(Name="Title")]
    public string Title { get; set; } = "";
    [Required]
    [Display(Name="Description")]
    public string Description { get; set; } = "";
    [Required]
    [Display(Name="Link")]
    public string URL { get; set; } = "";
    [Required]
    [Display(Name="Priority")]
    public int Priority { get; set; }
    [Required]
    [Display(Name="Recipient")]
    public int UserID { get; set; }
  }
}
