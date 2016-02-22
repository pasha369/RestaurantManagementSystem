using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Services;
using AutoMapper;
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
         
        public MenuController(IMenuManager menuManager,
            ICategoryManager categoryManager, IManager<Ingredient> ingredientManager)
        {
            _menuManager = menuManager;
            _categoryManager = categoryManager;
            _ingredientManager = ingredientManager;
        }

        [WebMethod]
        public List<CategoryModel> GetCategories(int rstId)
        {
            var menu = _menuManager.GetByRestaurant(rstId);
            var categoryLst = Mapper.Map<List<Category>, List<CategoryModel>>(menu?.Categories.ToList());
            return categoryLst;
        }

        [HttpPost]
        public void AddCategory(CategoryModel model)
        {
            var category = Mapper.Map<Category>(model);
            _categoryManager.Add(model.MenuId, category);
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