using CookBook.Data;
using CookBook.IRepository;
using CookBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Repository
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly ApplicationDbContext context;
        public IngredientRepository(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }
        public IQueryable<Ingredient> GetByRecipeId(int id)
        {
            return context.Ingredient.Include(p => p.Recipe).Where(q => q.RecipeId == id);
        }

        public int UpdateIngredient(int id, Ingredient ingredient)
        {
            var data = context.Ingredient.Find(id);
            data.IsChecked = ingredient.IsChecked;
            context.SaveChanges();

            return ingredient.Id;
        }
    }
}
