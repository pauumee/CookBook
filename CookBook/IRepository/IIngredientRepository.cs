using CookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.IRepository
{
    public interface IIngredientRepository
    {
        IQueryable<Ingredient> GetByRecipeId(int id);
        int UpdateIngredient(int id, Ingredient ingredient);
    }
}
