using System.Linq;
using DataAccess.Abstract.Menu;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Concrete.Menu
{
    /// <summary>
    /// Represents ingredients repository.
    /// </summary>
    public class IngredientManager : IManager<Ingredient>
    {
        private RestorauntDbContext _ctx = new ContextManager().Context;

        /// <summary>
        /// Get query for all ingredients.
        /// </summary>
        /// <returns>Ingredient query</returns>
        public IQueryable<Ingredient> Get()
        {
            return _ctx.Ingredients;
        }

        /// <summary>
        /// Get ingredient by id.
        /// </summary>
        /// <param name="id">Ingredient id</param>
        /// <returns>Ingredient entity</returns>
        public Ingredient Get(int id)
        {
            return _ctx.Ingredients.Find(id);
        }
    }
}
