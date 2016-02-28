using RMS.Client.Models.View.MenuModels;

namespace RMS.Client.Models.View.OrderModels
{
    /// <summary>
    /// Order view model.
    /// </summary>
    public class OrderItemModel
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets dish.
        /// </summary>
        public DishItemModel Dish { get; set; }

        /// <summary>
        /// Gets or sets RestaurantId.
        /// </summary>
        public int RestaurantId { get; set; }
    }
}