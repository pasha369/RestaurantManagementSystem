namespace RMS.Client.Models.View
{
    public class ProfileModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhotoUrl { get; set; }

        public string Position { get; set; }
        // For restaurateur profile.
        public int RestaurantId { set; get; }

        public string Password { get; set; }

        public string About { set; get; }

        public string Facebook { get; set; }

        public string Email { get; set; }

        public string Skype { get; set; }

        public string Phone { get; set; }
    }
}