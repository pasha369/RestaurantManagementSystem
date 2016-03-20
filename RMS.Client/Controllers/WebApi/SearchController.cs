using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using DataAccess.Abstract;
using DataModel.Model;
using RMS.Client.Models.View;

namespace RMS.Client.Controllers.WebApi
{
    /// <summary>
    /// Represents SearchController.
    /// </summary>
    public class SearchController : ApiController
    {
        private IDataManager<Restaurant> _rstManager;

        /// <summary>
        /// Initialize SearchController instance.
        /// </summary>
        /// <param name="rstManager"></param>
        public SearchController(IDataManager<Restaurant> rstManager)
        {
            _rstManager = rstManager;
        }

        /// <summary>
        /// Search by restaurant name.
        /// </summary>
        /// <param name="name">Search name.</param>
        /// <returns>Search result.</returns>
        [HttpGet]
        public List<RestaurantModel> FindByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var restaurants = _rstManager.Get()
                    .Where(r => r.Name.ToLower().Contains(name.ToLower()));

                return Mapper.Map<List<RestaurantModel>>(restaurants);
            }
            return null;
        }
    }
}
