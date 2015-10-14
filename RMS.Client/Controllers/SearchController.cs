using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Services;
using DataAccess.Concrete;
using DataModel.Model;
using RMS.Client.Models.View;

namespace RMS.Client.Controllers
{
    public class SearchController : ApiController
    {
        [HttpGet]
        public List<RestaurantModel> FindByName(string name)
        {
            var rstManager = new RestaurantManager();
            var lstRestaurant = rstManager.GetAll()
                .Where(r => r.Name.ToLower().Contains(name.ToLower()))
                .Select(r => new RestaurantModel()
                                 {
                                     Name = r.Name,
                                     Description = r.Description,
                                 })
                .ToList();
            return lstRestaurant;
        } 
    }
}
