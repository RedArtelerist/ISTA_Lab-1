using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The field should not be empty!")]
        [Display(Name = "Info")]
        public string Info { get; set; }
        [Required(ErrorMessage = "The field should not be empty!")]
        [Display(Name = "Date")]
        public string Date { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public Product Product { get; set; }
        public User Us { get; set; }
    }
}
