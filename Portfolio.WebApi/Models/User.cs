using Portfolio.WebApi.Commons;
using Portfolio.WebApi.Enum;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Portfolio.WebApi.Models
{
    public class User:Auditable
    {
        [MinLength(2), MaxLength(50)]
        public string FirstName { get; set; } = String.Empty;

        [MinLength(2), MaxLength(50)]
        public string LastName { get; set; } = String.Empty;

        public DateTime BrithDate { get; set; }

        public Gender Gender { get; set; }

        public UserRole UserRole { get; set; } = UserRole.User;

        public string ImagePath { get; set; } = String.Empty;

        [EmailAddress, MaxLength(50)]
        public string Email { get; set; } = String.Empty;

        public string PasswordHash { get; set; } = String.Empty;

        public string Salt { get; set; } = String.Empty;

        public bool EmailConfirmed { get; set; } = false;

        [MinLength(7), MaxLength(15)]
        public string PhoneNumber { get; set; } = String.Empty;
    }
}
