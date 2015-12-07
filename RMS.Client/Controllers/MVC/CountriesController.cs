using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DataAccess.Abstract;
using DataModel.Model;
using PagedList;
using RMS.Client.Models.View;

namespace RMS.Client.Controllers.MVC
{
    public class CountriesController : Controller
    {
        private IDataManager<Country> _countryManager;
        private IDataManager<Restaurant> _rstManager;
 
        public CountriesController(IDataManager<Country> countryManager, IDataManager<Restaurant> rstManager)
        {
            _countryManager = countryManager;
            _rstManager = rstManager;
        }


        // GET: /Countries/
        public ActionResult ContriesPage()
        {
            var model = new CountriesModel();
            model.Countries = _countryManager.GetAll()
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 3)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();

            return View(model);
        }

        public ActionResult GetRstByCountry(string country, int? page)
        {
            var model = new RestaurantLst();
            var restaurants = _rstManager.GetAll();
            var lst = restaurants.Where(r => r.Adress.Country != null && r.Adress.Country.Name == country).ToList();

            Mapper.CreateMap<Restaurant, RestaurantModel>();
            model.RestaurantModels = Mapper.Map<List<Restaurant>, List<RestaurantModel>>(lst).ToPagedList(page??1, 3);

            return View(model);
        }

    }
}
