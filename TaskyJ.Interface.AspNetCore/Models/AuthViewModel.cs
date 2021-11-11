using System.ComponentModel.DataAnnotations;

namespace TaskyJ.Interface.AspNet.Models
{
    public class AuthViewModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
