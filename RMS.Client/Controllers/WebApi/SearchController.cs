using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DataAccess.Concrete;
using RMS.Client.Models.View;

namespace RMS.Client.Controllers.WebApi
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
                                     Id = r.Id,
                                     Name = r.Name,
                                     Description = r.Description,
                                 })
                .ToList();
            return lstRestaurant;
        } 
    }
}
