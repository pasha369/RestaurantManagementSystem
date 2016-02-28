using System.Collections.Generic;

namespace RMS.Client.Models.View.MenuModels
{
    /// <summary>
    /// Represents dish model.
    /// </summary>
    public class DishModel
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// Gets or sets Cost. 
        /// </summary>
        public int Cost { set; get; }

        /// <summary>
        /// Gets or sets ingredientsIds.
        /// </summary>
        public List<int> IngredientIds { get; set; }

        /// <summary>
        /// Gets or sets categoryId.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        public string Description { get; set; }
    }
}