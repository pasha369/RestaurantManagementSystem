using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataModel.Model;

namespace RMS.Client.Models.View
{
    public class CountriesModel
    {
        [UIHint("Countries")]
        public List<List<Country>> Countries { set; get; }
    }
}