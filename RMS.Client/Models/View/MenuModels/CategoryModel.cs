using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.Client.Models.View.MenuModels
{
    public class CategoryModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int MenuId { get; set; }
        public  List<DishModel> DishModels { set; get; } 
    }
}