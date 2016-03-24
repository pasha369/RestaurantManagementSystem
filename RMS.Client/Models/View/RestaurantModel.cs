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
        public bool IsExistFreeTable { get; set; }
    }
}