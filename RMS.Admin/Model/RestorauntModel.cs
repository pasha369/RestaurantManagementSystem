using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel.Model;

namespace RMS.Admin.Model
{
    public sealed class RestorauntModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public Address Adress { set; get; }

        public int PhoneNumber { set; get; }

        public List<Hall> Halls { set; get; }
        public List<Cuisine> Cuisines { set; get; }

        public RestorauntModel()
        {
            Cuisines = new List<Cuisine>();
        }
    }
}