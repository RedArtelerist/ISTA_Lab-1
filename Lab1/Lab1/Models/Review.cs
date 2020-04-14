using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
    public partial class Review
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The field should not be empty!")]
        [Display(Name = "Info")]
        public string Info { get; set; }
        [Required(ErrorMessage = "The field should not be empty!")]
        [Display(Name = "Date")]
        public string Date { get; set; }
        [Range(0,10,ErrorMessage = "The value must be between 0 and 10")]
        [Required(ErrorMessage = "The field should not be empty!")]
        [Display(Name = "Rating")]
        public double? Rating { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
