﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
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

        [DataMember(IsRequired = true)]
        public DateTime From { get; set; }

        public int FromHour { get; set; }

        public int FromMinutes { get; set; }

        [DataMember(IsRequired = true)]
        public DateTime To { set; get; }

        public int ToHour { get; set; }

        public int ToMinutes { get; set; }

        [DataMember(IsRequired = true)]
        public int PeopleNum { get; set; }

        public int RestaurantId { get; set; }

        public ReservationStatus Status { get; set; }
    }
}