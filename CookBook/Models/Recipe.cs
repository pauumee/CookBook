using System.Collections.Generic;

namespace CookBook.Models
{
    public class Recipe
    {
        public Recipe()
        {
            Ingredients = new HashSet<Ingredient>();
        }
        public Recipe(Recipe recipe)
        {
            Name = recipe.Name;
            Ingredients = recipe.Ingredients;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual User User { get; set; }
    }
}
