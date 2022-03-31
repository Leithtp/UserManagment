using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagment.Domain.Entities;

namespace UserManagment.Domain
{
    public class AppDbContext : IdentityDbContext<UserData>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //public DbSet<UserData> UserData { get; set; }
        public DbSet<TextField> TextFields { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "558d2ec3-6b10-47f6-875f-fc4d82845007",
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            modelBuilder.Entity<UserData>().HasData(new UserData
            {
                Id = "1c603b19-cb58-474a-b793-d7fe1dd371bd",
                UserName = "admin",   
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<UserData>().HashPassword(null, "Password123!"),
                SecurityStamp = string.Empty,
                Surname = "Ivanov",
                Name = "Ivan"
                
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "558d2ec3-6b10-47f6-875f-fc4d82845007",
                UserId = "1c603b19-cb58-474a-b793-d7fe1dd371bd"
            }) ;

            modelBuilder.Entity<TextField>().HasData(new TextField
            {
                id = new Guid("e9351b34-5ef5-40b4-85c1-3ecbd137df6f"),
                CodeWord = "PageIndex",
                Title = "Главная"
            }) ;

            modelBuilder.Entity<TextField>().HasData(new TextField
            {
                id = new Guid("f15417ab-32ac-4f8d-9c50-a0d8fe26bc43"),
                CodeWord = "PageUsers",
                Title = "Пользователи"
            });




        }
    }
}
