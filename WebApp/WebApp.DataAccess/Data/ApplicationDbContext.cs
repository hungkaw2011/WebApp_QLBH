﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.DataAccess
{
    //add-migration AddCategory -Context ApplicationDbContext -Project WebApp.DataAccess

    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);//Cấu hình mặc định của lớp IdentityDbContext được thực hiện trước khi thực hiện các cấu hình của riêng
            //Tạo dữ liệu mẫu
			modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Scifi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Window",
                    Author = "Microsoft",
                    Description = "Windows 11 is the latest major release of Microsoft's Windows NT operating system, released in October 2021.",
                    ISBN = "WD2021",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryId = 1,
                    ImageUrl=""
                },
                new Product
                {
                    Id = 2,
                    Title = "Linux",
                    Author = "Linus Torvalds",
                    Description = "Linux is a family of open-source Unix-like operating systems based on the Linux kernel.",
                    ISBN = "LN2021",
                    ListPrice = 99,
                    Price = 70,
                    Price50 = 85,
                    Price100 = 80,
					CategoryId = 2,
					ImageUrl = ""
				}
				); ;
        }
    }
}
