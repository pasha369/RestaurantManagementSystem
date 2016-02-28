using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DataAccess.Abstract;
using DataModel.Model;
using RMS.Client.Models.View;

namespace RMS.Client.Controllers.WebApi
{
    public class SearchController : ApiController
    {
        private IDataManager<Restaurant> _rstManager;

        public SearchController(IDataManager<Restaurant> rstManager)
        {
            _rstManager = rstManager;
        }

        [HttpGet]
        public List<RestaurantModel> FindByName(string name)
        {
            var lstRestaurant = new List<RestaurantModel>();

            if (!string.IsNullOrEmpty(name))
            {
                lstRestaurant = _rstManager.Get()
                    .Where(r => r.Name.ToLower().Contains(name.ToLower()))
                    .Select(r => new RestaurantModel()
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Description = r.Description,
                    })
                    .ToList();
            }

            return lstRestaurant;
        }
    }
}
