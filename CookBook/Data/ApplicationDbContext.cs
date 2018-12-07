using System;
using System.Collections.Generic;
using System.Text;
using CookBook.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ingredient> Ingredient { get; set; }
        public virtual DbSet<Recipe> Recipe { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Recipe>()
                .HasOne(u => u.User)
                .WithMany(r => r.Recipes)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<Ingredient>()
                .HasOne(r => r.Recipe)
                .WithMany(i => i.Ingredients)
                .HasForeignKey(r => r.RecipeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
