using System;
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

            Mapper.CreateMap<Dish, DishModel>();
            Mapper.CreateMap<Category, CategoryModel>()
                .ForMember(m => m.DishModels,
                o => o.MapFrom(c => c.Dishes));
            var categoryLst = new List<CategoryModel>();

            Mapper.Map<List<Category>, List<CategoryModel>>(menu.Categories.ToList(), categoryLst);

            return categoryLst;
        }

        [HttpPost]
        public void AddCategory(CategoryModel model)
        {
            Mapper.CreateMap<CategoryModel, Category>();
            var category = Mapper.Map<Category>(model);

            _categoryManager.Add(model.MenuId, category);
        }

    }
}
