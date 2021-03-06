﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.Client.Models.View.MenuModels
{
    public class CategoryModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int RestaurantId { get; set; }
        public  List<DishItemModel> DishModels { set; get; } 
    }

    public class DishItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
    }
}