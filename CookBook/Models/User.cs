using System.Collections.Generic;

namespace CookBook.Models
{
    public class User
    {
        public User()
        {
            Recipes = new HashSet<Recipe>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}
