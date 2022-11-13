using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebSSLProject.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductBrand> ProductBrands { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.Stock)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ProductPhoto)
                .IsUnicode(false);

            modelBuilder.Entity<ProductBrand>()
                .Property(e => e.BrandName)
                .IsUnicode(false);

            modelBuilder.Entity<ProductBrand>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.ProductBrand)
                .WillCascadeOnDelete(false);
        }
    }
}
