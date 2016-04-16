using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.Client.Models.View
{
    public class TableViewModel
    {
        public int TableId { get; set; }
        public int Number { get; set; }
        public RestaurantModel Restaurant{ get; set; }
        public string Status { get; set; }
    }
}