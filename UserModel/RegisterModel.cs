using System.ComponentModel.DataAnnotations;

namespace AsisProject.UserModel
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Surname is required")]
        public string Surname { get; set; }
        [Required(ErrorMessage ="Age is required")]
        public int Age { get; set; }
    }
}
