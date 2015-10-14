using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel.Model;

namespace RMS.Reservation.Models.ViewModel
{
    public class ReserveModel
    {
        public DateTime From { set; get; }
        public Restaurant Restaurant { set; get; }
        public String Name { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string SpecialRequest { get; set; }
        public int Geusts { set; get; }
    }
}