using System.Collections.Generic;
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
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets Dishes.
        /// </summary>
        public List<DishItemModel> Dishes { get; set; }

        /// <summary>
        /// Gets or sets RestaurantId.
        /// </summary>
        public int RestaurantId { get; set; }

        /// <summary>
        /// Gets or sets TableId.
        /// </summary>
        public int TableId { get; set; }

        /// <summary>
        /// Gets or sets client name.
        /// </summary>
        public string ClientName { get; set; }  
    }
}