using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestAPI.DAL.Entities;

namespace TestAPI.DAL.DataContext
{
    public class DatabaseContext : DbContext
    {
        public class OptionsBuild
        {
            public OptionsBuild()
            {
                Settings = new AppConfiguration();
                OptionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
                OptionsBuilder.UseSqlServer(Settings.SqlConnectionString);
                DatabaseOptions = OptionsBuilder.Options;
            }
            public DbContextOptionsBuilder<DatabaseContext> OptionsBuilder { get; set; }
            public DbContextOptions<DatabaseContext> DatabaseOptions { get; set; }
            private AppConfiguration Settings { get; set; }
        }

        public static OptionsBuild Options = new OptionsBuild();
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User
            modelBuilder.Entity<User>().ToTable("user");
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.Id).UseIdentityColumn(1, 1).IsRequired(true).HasColumnName("user_id");
            modelBuilder.Entity<User>().Property(u => u.UserName).IsRequired(true).HasMaxLength(30).HasColumnName("username");
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired(true).HasColumnName("email");
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired(true).HasColumnName("password");
            modelBuilder.Entity<User>().Property(u => u.Role).IsRequired(true).HasMaxLength(5).HasColumnName("role");
            #endregion

            #region Product
            modelBuilder.Entity<Product>().ToTable("product");
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().Property(p => p.Id).UseIdentityColumn(1, 1).IsRequired(true).HasColumnName("product_id");
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired(true).HasColumnName("name");
            modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired(true).HasColumnName("price");
            modelBuilder.Entity<Product>().Property(p => p.Quantity).IsRequired(true).HasColumnName("quantity");
            modelBuilder.Entity<Product>().Property(p => p.Description).IsRequired(true).HasColumnName("description");
            #endregion
        }
    }
}
