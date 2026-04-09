using Microsoft.EntityFrameworkCore;
using prakt15.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace prakt15.data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<ProductsTags> ProductsTags { get; set; }

        protected override void OnConfiguring
        (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer
            ("Server=(localdb)\\MSSQLLocalDB;Database=prakt15;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>()
                .HasMany(c => c.Products)
                .WithOne(c => c.Categories)
                .HasForeignKey(c => c.Category_id);

            modelBuilder.Entity<Brand>()
                .HasMany(c => c.Products)
                .WithOne(c => c.Brand)
                .HasForeignKey(c => c.Brand_id);

            modelBuilder.Entity<ProductsTags>()
                .HasKey(pt => new {pt.ProductId, pt.TagsId});

            modelBuilder.Entity<ProductsTags>()
                .HasOne(pt => pt.Product)
                .WithMany(pt => pt.ProductsTags)
                .HasForeignKey(pt => pt.ProductId);

            modelBuilder.Entity<ProductsTags>()
                .HasOne(pt => pt.Tags)
                .WithMany(pt => pt.ProductsTags)
                .HasForeignKey(pt => pt.TagsId);

        }
    }
}
