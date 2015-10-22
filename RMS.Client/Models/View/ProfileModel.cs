using System.Collections.Generic;

namespace RMS.Client.Models.View
{
    public class ProfileModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Contacts { get; set; }
        public string Position { get; set; }
        public int RestaurantId { set; get; }
        public string PhotoUrl { get; set; }
        public List<string> Education { get; set; }
        public string Password { get; set; }
    }
}