using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBook.IRepository;
using CookBook.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : Controller
    {
        private IIngredientRepository _ingredientRepository;
        public IngredientsController(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        // GET: api/Ingredients
        [HttpGet("{id}")]
        public IQueryable<Ingredient> GetIngredient([FromRoute] int id)
        {
            var ingredients = _ingredientRepository.GetByRecipeId(id);
            return ingredients;
        }

        // PUT: api/Ingredients/5
        [HttpPut("{id}")]
        public IActionResult PutIngredient([FromRoute] int id, [FromBody] Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ing = _ingredientRepository.UpdateIngredient(id, ingredient);

            return Ok(ing);
        }
    }
}