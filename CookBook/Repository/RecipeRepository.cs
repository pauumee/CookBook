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
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ApplicationDbContext context;
        public RecipeRepository(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        public int AddRecipe(int UserId, Recipe recipe)
        {
            var _recipe = new Recipe(recipe);
            _recipe.UserId = UserId;
            context.Recipe.Add(_recipe);
            context.SaveChanges();
            return recipe.Id;
        }

        public Recipe GetById(int id)
        {
            return context.Recipe.Include(p => p.Ingredients).FirstOrDefault(q => q.Id == id);
        }

        public IQueryable<Recipe> GetByUser(int id)
        {
            var recipeList = context.Recipe.Include(p => p.Ingredients).Where(q => q.UserId == id);
            return recipeList;
        }

        public int UpdateRecipe(Recipe recipe)
        {
            context.Entry(recipe).State = EntityState.Modified;
            context.SaveChanges();
            return recipe.Id;
        }
    }
}
