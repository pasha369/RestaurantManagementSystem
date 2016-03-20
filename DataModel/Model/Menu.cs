using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.Model
{
    [Table("Menu")]
    public class Menu
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public virtual List<Category> Categories { set; get; } 
    }

    [Table("Category")]
    public class Category
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Required]
        [MaxLength(180)]
        public string Name { set; get; }
        public virtual Cuisine Cuisine { set; get; }
        public virtual List<Dish> Dishes { set; get; }
        public virtual Menu Menu { set; get; }
    }

    [Table("Cuisine")]
    public class Cuisine
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public string Name { set; get; }
        public virtual List<Restaurant> Restoraunts { set; get; }
    }

    [Table("Dish")]
    public class Dish
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Required]
        [MaxLength(180)]
        public string Name { set; get; }
        public int Cost { set; get; }
        public string Description { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Ingredient> Ingredients { get; set; }

    }

    [Table("Ingredient")]
    public class Ingredient
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        public virtual List<Dish> Dishes { get; set; }
    }
}