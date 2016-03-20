using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Services;
using AutoMapper;
using DataAccess.Abstract;
using DataAccess.Abstract.Menu;
using DataModel.Model;
using RMS.Client.Models.View.MenuModels;

namespace RMS.Client.Controllers.WebApi.Menu
{
    public class MenuController : ApiController
    {
        private IMenuManager _menuManager;
        private ICategoryManager _categoryManager;
        private IManager<Ingredient> _ingredientManager;
        private IDataManager<Restaurant> _restaurantManager; 

        public MenuController(IMenuManager menuManager,
            ICategoryManager categoryManager, IManager<Ingredient> ingredientManager, IDataManager<Restaurant> restaurantManager)
        {
            _menuManager = menuManager;
            _categoryManager = categoryManager;
            _ingredientManager = ingredientManager;
            _restaurantManager = restaurantManager;
        }

        [WebMethod]
        public List<CategoryModel> GetCategories(int rstId)
        {
            var menu = _menuManager.GetByRestaurant(rstId);
            var categoryLst = Mapper.Map<List<Category>, List<CategoryModel>>(menu?.Categories.ToList());
            return categoryLst;
        }

        [WebMethod]
        public CategoryModel AddCategory(CategoryModel model)
        {
            var restaurant = _restaurantManager.Get(model.RestaurantId);
            var categoryEntity = Mapper.Map<Category>(model);
            _categoryManager.Add(restaurant.Menu.Id, categoryEntity);
            var categoryModel = Mapper.Map<CategoryModel>(categoryEntity);
            return categoryModel;
        }

        [WebMethod]
        public void RemoveCategory(int id)
        {
            _categoryManager.Remove(id);
        }

        [HttpPost]
        public HttpResponseMessage GetIngredients()
        {
            var result = _ingredientManager.Get()
                .Select(x => new {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        } 
    }
}