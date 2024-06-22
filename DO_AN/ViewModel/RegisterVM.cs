using DO_AN.Models;
using System.ComponentModel.DataAnnotations;

namespace DO_AN.ViewModel
{
    public class RegisterVM
    {
        public string? Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password do not match")]
        public string Cf_Password { get; set; } = null!;
        public bool? Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
