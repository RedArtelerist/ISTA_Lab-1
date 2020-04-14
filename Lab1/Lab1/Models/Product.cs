using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
    public partial class Product
    {
       /* public Product()
        {
            Comment = new HashSet<Comment>();
            Review = new HashSet<Review>();
            SellerProduct = new HashSet<SellerProduct>();
        }*/

        public int Id { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9""'\s-]*$", ErrorMessage = "Only latina")]
        [Required(ErrorMessage = "The field should not be empty!")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Info")]
        public string Info { get; set; }
        [Required(ErrorMessage = "The field should not be empty!")]
        [Display(Name = "Specifications")]
        public string Specification { get; set; }
        [Required(ErrorMessage = "The field should not be empty!")]
        [Display(Name = "Rating")]
        public double? Rating { get; set; }
        public int SubcategoryId { get; set; }
        [Display(Name = "Subcategory")]
        public Subcategory Subcategory { get; set; }
        public ICollection<Comment> Comment { get; set; }
        public ICollection<Review> Review { get; set; }
        //public virtual ICollection<SellerProduct> SellerProduct { get; set; }
        [Display(Name = "Shops")]
        public ICollection<SellerProduct> SellerProducts { get; set; }

    }
}
