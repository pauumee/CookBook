using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CookBook.Data;
using CookBook.Models;
using CookBook.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace CookBook.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : Controller
    {
        private IRecipeRepository _recipeRepository;
        private readonly ApplicationDbContext _context;
        public RecipesController(IRecipeRepository recipeRepository, ApplicationDbContext context)
        {
            _recipeRepository = recipeRepository;
            _context = context;
        }

        // GET: api/Recipes
        [HttpGet]
        public IEnumerable<Recipe> GetRecipe()
        {
            var userId = _context.Users.FirstOrDefault(q => q.UserName == User.Identity.Name).ProfileId;
            return _recipeRepository.GetByUser(userId);
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public IActionResult GetRecipe([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipe = _recipeRepository.GetById(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }

        // PUT: api/Recipes/5
        [HttpPut("{id}")]
        public IActionResult PutRecipe([FromRoute] int id, [FromBody] Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != recipe.Id)
            {
                return BadRequest();
            }

            _recipeRepository.UpdateRecipe(recipe);

            if (_recipeRepository.GetById(id) == null)
                return NotFound();

            return Ok(recipe);
        }

        // POST: api/Recipes
        [HttpPost]
        public IActionResult PostRecipe([FromBody] Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _context.Users.FirstOrDefault(q => q.UserName == User.Identity.Name).ProfileId;
            _recipeRepository.AddRecipe(userId, recipe);

            return CreatedAtAction("GetRecipe", new { id = recipe.Id }, recipe);
        }
    }
}