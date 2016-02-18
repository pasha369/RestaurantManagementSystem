using System.Web.Http;
using AutoMapper;
using DataAccess.Abstract.Menu;
using DataModel.Model;
using RMS.Client.Models.View.MenuModels;

namespace RMS.Client.Controllers.WebApi.Menu
{
    public class DishController : ApiController
    {
        private IDishManager _dishManager;
        private ICategoryManager _categoryManager;

        public DishController(IDishManager dishManager, ICategoryManager categoryManager)
        {
            _dishManager = dishManager;
            _categoryManager = categoryManager;
        }

        [HttpPost]
        public void Add(DishModel model)
        {
            var dish = Mapper.Map<Dish>(model);
            var category = _categoryManager.GetById(model.CategoryId);

            _dishManager.Add(category, dish);
        }
    }
}
