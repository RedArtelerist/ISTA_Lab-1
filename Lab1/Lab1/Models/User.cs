using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
    public partial class User: IdentityUser
    {
        public User()
        {
            Comment = new HashSet<Comment>();
            Review = new HashSet<Review>();
        }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_\.-]*$", ErrorMessage = "Only latina or you entered the wrong characters")]
        public string Nick { get; set; }

        public ICollection<Comment> Comment { get; set; }
        public ICollection<Review> Review { get; set; }
    }
}
