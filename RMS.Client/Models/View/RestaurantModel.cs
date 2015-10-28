using System.Collections.Generic;
using DataModel.Model;

namespace RMS.Client.Models.View
{
    public class RestaurantModel
    {
        public int Id { set; get; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public int Rating { set; get; }
        public Address Address { get; set; }
        public int CommentCount { get; set; }
        public List<Cuisine> Cuisines { get; set; }
    }

    public class Address
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
    }
}