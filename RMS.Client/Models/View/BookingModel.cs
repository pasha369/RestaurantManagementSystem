using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataModel.Model;

namespace RMS.Client.Models.View
{
    public class BookingModel
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Msg { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime From { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime To { set; get; }
        [Required]
        public int PeopleNum { get; set; }
        public int RestaurantId { get; set; }

        public ReservationStatus Status { get; set; }
    }
}