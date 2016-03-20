using System.Linq;
using System.Web.Http;
using System.Web.Services;
using AutoMapper;
using DataAccess.Abstract.Menu;
using DataModel.Model;
using RMS.Client.Models.View.MenuModels;

namespace RMS.Client.Controllers.WebApi.Menu
{
    public class DishController : ApiController
    {
        private IDishManager _dishManager;
        private IManager<Ingredient> _ingredientManager; 
        private ICategoryManager _categoryManager;

        public DishController(IDishManager dishManager, ICategoryManager categoryManager, IManager<Ingredient> ingredientManager)
        {
            _dishManager = dishManager;
            _categoryManager = categoryManager;
            _ingredientManager = ingredientManager;
        }

        [HttpPost]
        public DishModel Add(DishModel dishModel)
        {
            var dish = Mapper.Map<Dish>(dishModel);
            dish.Category = _categoryManager.GetById(dishModel.CategoryId);
            dish.Ingredients = _ingredientManager.Get()
                .Where(x => dishModel.IngredientIds.Contains(x.Id))
                .ToList();
            _dishManager.Add(dish);
            return Mapper.Map<DishModel>(dish);
        }

        [WebMethod]
        public void Remove(int id)
        {
            _dishManager.Delete(id);
        }
    }
}
