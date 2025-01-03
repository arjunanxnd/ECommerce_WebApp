﻿using ECommerce_WebApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_WebApp.Services
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> option) : base(option)
        {
            
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<UserCart> UserCarts { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureProdandCategory();
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserCart>()
                .HasOne(uc => uc.Product)
                .WithMany()
                .HasForeignKey(uc => uc.ProductId);

            modelBuilder.UsersSeed();
        }

        // While creating the database it was giving 'Microsoft.EntityFrameworkCore.Migrations.PendingModelChangesWarning'
        // Had to create this method to bypass this warning 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}
