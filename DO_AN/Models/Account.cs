using System.ComponentModel.DataAnnotations;

namespace DO_AN.Models
{
    public partial class Account 
    {
        public Account()
        {
            Customers = new HashSet<Customer>();
        }

        public int IdAccount { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public bool? Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int IdRole { get; set; }
        public virtual Role IdRoleNavigation { get; set; } = null!;
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
