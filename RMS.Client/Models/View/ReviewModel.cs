namespace RMS.Client.Models.View
{
    public class ReviewModel
    {
        public int Stars { get; set; }

        public int Food { get; set; }

        public int Service { get; set; }

        public int Ambience { get; set; }

        public string Comment { get; set; }

        public string Author { get; set; }

        public int RestaurantId { get; set; }

        public string PhotoUrl { set; get; }
    }
}