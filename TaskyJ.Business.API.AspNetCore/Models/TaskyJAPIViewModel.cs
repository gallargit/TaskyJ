using System;
using System.ComponentModel.DataAnnotations;

namespace TaskyJ.Business.API.AspNetCore.Models
{
    public class TaskyJAPIViewModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Task Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Task Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Creation date")]
        public DateTime? CreationDate { get; set; } = null;
        [Required]
        [Range(0, 100, ErrorMessage = "Value must range from 0 to 100")]
        [Display(Name = "% Completed")]
        public byte Completed { get; set; }
        public string FullName { get; set; }
    }
}
