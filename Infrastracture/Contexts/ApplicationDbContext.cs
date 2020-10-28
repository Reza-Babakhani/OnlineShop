using Domain.Models;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Infrastructure.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<ApplicationUser>()
            //    .HasIndex(u => u.PhoneNumber)
            //    .IsUnique();
        }


        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<SubCategoryItem> SubCategoryItems { get; set; }


        public DbSet<Product> Products { get; set; }

        public DbSet<ProductOptions> ProductOptions { get; set; }


        public DbSet<Warranty> Warranties { get; set; }

        public DbSet<ProductPrices> ProductPrices { get; set; }


        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<ProductDetails> ProductDetails { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<ProductImages> ProductImages { get; set; }

        public DbSet<ProductStrengths> ProductStrengths { get; set; }

        public DbSet<ProductWeaknesses> ProductWeaknesses { get; set; }

        public DbSet<ProductSpecification> ProductSpecifications { get; set; }

        public DbSet<SpecialReview> SpecialReviews { get; set; }


    }
}

