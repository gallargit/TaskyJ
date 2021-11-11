using System.ComponentModel.DataAnnotations;

namespace TaskyJ.Business.API.AspNetCore.Models
{
    public class CategoryJAPIViewModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Icon")]
        public string IconBase64 { get; set; }
    }
}
