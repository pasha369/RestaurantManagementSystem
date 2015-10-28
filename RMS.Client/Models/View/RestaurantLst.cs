using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataModel.Model;

namespace RMS.Client.Models.View
{
    public class RestaurantLst
    {
        private IDataManager<Cuisine> _cuisinManager;
        public bool Rating { get; set; }
        public bool Recent { get; set; }
        public string Cuisine { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }
        [UIHint("RestaurantModels")]
        public List<RestaurantModel> RestaurantModels { get; set; }
        public RestaurantLst()
        {
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