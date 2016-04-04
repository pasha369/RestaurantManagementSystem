namespace RMS.Client.Models.View
{
    /// <summary>
    /// Represents restaurant model.
    /// </summary>
    public class RestaurantModel
    {
        /// <summary>
        /// Gets or sets Id
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets PhoneNumber.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets PhotoUrl.
        /// </summary>
        public string PhotoUrl { get; set; }

        /// <summary>
        /// Gets or sets Rating.
        /// </summary>
        public int Rating { set; get; }

        /// <summary>
        /// Gets or sets LongAddress.
        /// </summary>
        public string LongAddress { get; set; }

        /// <summary>
        /// Gets or sets CityName.
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets CommentCount.
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// Gets or sets IsExistFreeTable.
        /// </summary>
        public bool IsExistFreeTable { get; set; }
    }
}