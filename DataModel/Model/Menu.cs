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
    }
}