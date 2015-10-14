using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel.Model;

namespace RMS.Reservation.Models.ViewModel
{
    public class DetailModel
    {
        public int Guests { get; set; }
        public Restaurant Restaurant { set; get; }
        public DateTime Time { set; get; }

        public int RestorauntId { get; set; }
    }
}