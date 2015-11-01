using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.Client.Models.View.MenuModels
{
    public class DishModel
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public int Cost { set; get; }
        public int CategoryId { get; set; }
    }
}