using CookBook.Models;
using System.Linq;

namespace CookBook.IRepository
{
    public interface IRecipeRepository
    {
        Recipe GetById(int id);
        IQueryable<Recipe> GetByUser(int id);
        int AddRecipe(int UserId, Recipe recipe);
        int UpdateRecipe(Recipe recipe);
    }
}
