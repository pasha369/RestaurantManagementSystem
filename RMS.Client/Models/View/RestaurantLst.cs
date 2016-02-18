using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using DataAccess.Abstract;
using DataModel.Model;

namespace RMS.Client.Models.View
{
    public class RestaurantLst
    {
        private IDataManager<Cuisine> _cuisinManager;

        public bool Rating { get; set; }

        public bool Recent { get; set; }

        public string Cuisine { get; set; }

        public List<string> CountryList { get; set; }

        public List<string> CuisineList { get; set; }

        public IEnumerable<SelectListItem> Cuisines { get; set; }

        [UIHint("RestaurantModels")]
        public List<RestaurantModel> RestaurantModels { get; set; }

        public RestaurantLst()
        {
            this.CountryList = new List<string>();
            this.CuisineList = new List<string>();
            this.RestaurantModels = new List<RestaurantModel>();
        }

        public RestaurantLst(IDataManager<Cuisine> cuisinManager) : this()
        {
            _cuisinManager = cuisinManager;
            Cuisines = _cuisinManager.GetAll()
                .Select(c => new SelectListItem() { Text = c.Name, Value = c.Name }).AsEnumerable()
            ;
        }
    }
}