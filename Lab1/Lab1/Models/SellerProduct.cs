using System;
using System.Collections.Generic;

namespace Lab1.Models
{
    public partial class SellerProduct
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public Seller Seller { get; set; }
    }
}
