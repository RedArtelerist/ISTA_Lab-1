using Lab1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.Data
{
    public class SellerIndexData
    {
        public IEnumerable<Seller> Sellers { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<SellerProduct> SellerProducts { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}
