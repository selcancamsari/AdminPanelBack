using AdminPanel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {

        }
        public MyDbContext(DbContextOptions<DbContext> options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql(@"Server=localhost;port=3306;Database=panteoncase;Uid=root;Pwd=password",
                ServerVersion.AutoDetect(@"Server=localhost;port=3306;Database=panteoncase;Uid=root;Pwd=password"),
                options => options.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: System.TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null)
                );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().ToTable("Users");

            modelBuilder.Entity<UserModel>().Property(x => x.Id).IsRequired();
            modelBuilder.Entity<UserModel>().Property(x => x.UserName).IsRequired();
            modelBuilder.Entity<UserModel>().Property(x => x.Password).IsRequired();
            modelBuilder.Entity<UserModel>().Property(x => x.Email).IsRequired();

//            modelBuilder.Entity<UserModel>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<UserModel>().HasIndex(x => x.UserName).IsUnique();
            modelBuilder.Entity<UserModel>().HasIndex(x => x.Email).IsUnique();

            //modelBuilder.Entity<UserModel>().Property(x => x.UserName).HasMaxLength(50);

            base.OnModelCreating(modelBuilder);
        }
    }
}
