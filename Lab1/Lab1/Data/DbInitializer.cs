using Lab1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.Data
{
    public static class DbInitializer
    {
        //HitlineContext context
        public static void Initialize(IdentityContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            var categories = new Category[]
            {
                new Category{Name = "Category 1"},
                new Category{Name = "Category 2"}
            };

            foreach(Category category in categories)
            {
                context.Categories.Add(category);
            }

            context.SaveChanges();

            var subcategories = new Subcategory[]
            {
                new Subcategory{Name = "Subcategory 1", CategoryId = categories.Single(s => s.Name == "Category 1").Id},
                new Subcategory{Name = "Subcategory 2", CategoryId = categories.Single(s => s.Name == "Category 1").Id}
            };

            foreach (Subcategory subcategory in subcategories)
            {
                context.Subcategories.Add(subcategory);
            }

            context.SaveChanges();

            var products = new Product[]
            {
            new Product{Name = "Iphone", Info = "info 1", Specification = "specif 1",Rating = 0, SubcategoryId = subcategories.Single(s => s.Name == "Subcategory 1").Id},
            new Product{Name = "GalaxyS20", Info = "info 2", Specification = "specif 2",Rating = 0, SubcategoryId = subcategories.Single(s => s.Name == "Subcategory 1").Id},
            new Product{Name = "Xiaomi", Info = "info 3", Specification = "specif 3",Rating = 0, SubcategoryId = subcategories.Single(s => s.Name == "Subcategory 1").Id},
            new Product{Name = "Ipad", Info = "info 4", Specification = "specif 4",Rating = 0,SubcategoryId = subcategories.Single(s => s.Name == "Subcategory 2").Id},
            new Product{Name = "Airpods", Info = "info 5", Specification = "specif 5",Rating = 0, SubcategoryId = subcategories.Single(s => s.Name == "Subcategory 2").Id}
            };
            foreach (Product s in products)
            {
                context.Products.Add(s);
            }
            context.SaveChanges();

            var sellers = new Seller[]
            {
            new Seller{Name = "Citrus", Adress = "afafaf", Rating = 7},
            new Seller{Name = "Allo", Adress = "agssgsg", Rating = 5},
            new Seller{Name = "Rozetka", Adress = "hddhhd", Rating = 10}
            };
            foreach (Seller c in sellers)
            {
                context.Sellers.Add(c);
            }
            context.SaveChanges();

            var sellerProducts = new SellerProduct[]
            {
                new SellerProduct
                {
                    ProductId = products.Single(s => s.Name == "Iphone").Id,
                    SellerId = sellers.Single(s => s.Name == "Citrus").Id,
                },
                new SellerProduct
                {
                    ProductId = products.Single(s => s.Name == "Iphone").Id,
                    SellerId = sellers.Single(s => s.Name == "Allo").Id,
                },
                new SellerProduct
                {
                    ProductId = products.Single(s => s.Name == "Xiaomi").Id,
                    SellerId = sellers.Single(s => s.Name == "Citrus").Id,
                }, 
                new SellerProduct
                {
                    ProductId = products.Single(s => s.Name == "GalaxyS20").Id,
                    SellerId = sellers.Single(s => s.Name == "Rozetka").Id,
                }, 
                new SellerProduct
                {
                    ProductId = products.Single(s => s.Name == "Iphone").Id,
                    SellerId = sellers.Single(s => s.Name == "Rozetka").Id,
                }
            };

            foreach (SellerProduct sp in sellerProducts)
            {
                var sellerProductInDataBase = context.SellerProducts.Where(
                    s =>
                            s.Product.Id == sp.ProductId &&
                            s.Seller.Id == sp.SellerId).SingleOrDefault();
                if (sellerProductInDataBase == null)
                {
                    context.SellerProducts.Add(sp);
                }
            }
            context.SaveChanges();
        }
    }
}

