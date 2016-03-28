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

        public List<string> Cuisines { get; set; }

        public List<string> Cities { get; set; }

        public List<string> CityList { get; set; }

        public List<string> CuisineList { get; set; }

        [UIHint("RestaurantModels")]
        public List<RestaurantModel> RestaurantModels { get; set; }

        public int? RestaurantCount { get; set; }

        public int? ItemId { get; set; }

        public RestaurantLst()
        {
            this.CityList = new List<string>();
            this.CuisineList = new List<string>();
            this.RestaurantModels = new List<RestaurantModel>();
        }

        public RestaurantLst(IDataManager<Cuisine> cuisinManager) : this()
        {
            _cuisinManager = cuisinManager;
        }
    }
}