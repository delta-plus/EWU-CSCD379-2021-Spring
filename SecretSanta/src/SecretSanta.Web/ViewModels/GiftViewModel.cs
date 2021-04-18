using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Web.ViewModels
{
    public class GiftViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Url { get; set; } = "";
        [Required]
        public int Priority { get; set; }
        [Required]
        [Display(Name="User")]
        public int UserId { get; set; }
    }
}
