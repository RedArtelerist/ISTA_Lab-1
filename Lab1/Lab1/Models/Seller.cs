using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab1.Models
{
    public partial class Seller
    {
        /*public Seller()
        {
            SellerProduct = new HashSet<SellerProduct>();
        }*/
        public int Id { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9""'\s-]*$", ErrorMessage = "Only latina")]
        [Required(ErrorMessage = "The field should not be empty!")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The field should not be empty!")]
        [Display(Name = "Adress")]
        public string Adress { get; set; }
        [Range(0,10,ErrorMessage = "The value must be between 0 and 10")]
        [Required(ErrorMessage = "The field should not be empty!")]
        [Display(Name = "Rating")]
        public double Rating { get; set; }
        //public virtual ICollection<SellerProduct> SellerProduct { get; set; }
        public virtual ICollection<SellerProduct> SellerProducts { get; set; }

    }
}
