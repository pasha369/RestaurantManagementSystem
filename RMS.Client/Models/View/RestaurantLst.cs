using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RMS.Client.Models.View
{
    public class RestaurantLst
    {
        [UIHint("RestaurantModels")]
        public List<RestaurantModel> RestaurantModels { get; set; }
        public RestaurantLst()
        {
            this.RestaurantModels = new List<RestaurantModel>();
        }
    }
}