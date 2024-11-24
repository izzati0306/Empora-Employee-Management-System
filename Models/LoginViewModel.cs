// Models/LoginViewModel.cs
namespace WorksAway.Models
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class Admin
    {
        public int AdminId { get; set; }     // Unique identifier for the admin
        public string Email { get; set; }    // Admin email (to match the login email)
        public string Password { get; set; } // The admin's password (which should be hashed)
    }
}