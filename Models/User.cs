using System.ComponentModel.DataAnnotations;

namespace MoneyFlow.Models
{
    public class User : Entity
    {
        [Required(AllowEmptyStrings = true)]
        [StringLength(64)]
        public string Username { get; set;}

        [Required(AllowEmptyStrings = true)]
        [StringLength(255)]
        public string FullName { get; set;}

        [Required(AllowEmptyStrings = true)]
        [StringLength(64)]
        public string Password { get; set;}

        [Required(AllowEmptyStrings = true)]
        [StringLength(255)]
        public string Email { get; set;}

        public User() 
        {
            Username = "";
            FullName = "";
            Password = "";
            Email = "";
        }
    }
}