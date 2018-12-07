using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base() { }
        public int ProfileId { get; set; }

        [ForeignKey("ProfileId")]
        public User UserProfile { get; set; }
    }
}
