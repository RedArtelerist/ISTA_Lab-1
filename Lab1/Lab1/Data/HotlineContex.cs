using Lab1.Models;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.Data
{
    public class HotlineContex : DbContext
    {
        public HotlineContex(DbContextOptions<HotlineContex> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<SellerProduct> SellerProducts { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Subcategory>().ToTable("Subcategory");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<SellerProduct>().ToTable("SellerProduct");
            modelBuilder.Entity<Seller>().ToTable("Seller");
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<Review>().ToTable("Review");
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
