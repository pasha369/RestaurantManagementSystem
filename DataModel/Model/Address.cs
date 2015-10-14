using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.Model
{
    [Table("Address")]
    public class Address
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public virtual Country Country { set; get; }
        public virtual City City { set; get; }
        public string  Street { get; set; }
    }

    [Table("Country")]
    public class Country
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Required]
        [MaxLength(180)]
        public string Name { set; get; }
    }
    [Table("Region")]
    public class Region
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Required]
        [MaxLength(180)]
        public string Name { set; get; }
    }

    [Table("City")]
    public class City
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Required]
        [MaxLength(180)]
        public string Name { set; get; }
        public virtual Country Country { set; get; }
    }
}