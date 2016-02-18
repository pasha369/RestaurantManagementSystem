using System.Collections.Generic;
using System.Linq;
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

        public MenuController(IMenuManager menuManager,
            ICategoryManager categoryManager)
        {
            _menuManager = menuManager;
            _categoryManager = categoryManager;
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

    }
}
