using Microsoft.EntityFrameworkCore;
using ProductsShop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsShop.Data
{
    public partial class ProductShopContext : DbContext
    {

        public ProductShopContext()
        {
        }
        public ProductShopContext(DbContextOptions options)
            : base(options)
        {
        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<CategoryProduct> CategoryProducts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-M8B0TGR\\SQLEXPRESS;Database=ProductShop;Integrated Security=True");
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryProduct>
                (entity => entity
                .HasKey(x => new { x.CategoryId, x.ProductId }));
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>
                (entity => entity
                .HasMany(b => b.ProductsBought)
                .WithOne(b => b.Buyer)
                .HasForeignKey(fk => fk.BuyerId)
                .OnDelete(DeleteBehavior.Restrict)
                );
            modelBuilder.Entity<User>
             (entity => entity
             .HasMany(s => s.ProductsSold)
             .WithOne(s => s.Seller)
             .HasForeignKey(fk => fk.BuyerId)
             .OnDelete(DeleteBehavior.Restrict)

                );
        }
    }
}
