using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.Model
{
    [Table("Restaurant")]
    public class Restaurant
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { get; set; }
        public Address Adress { set; get; }
        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { set; get; }
        public string PhotoUrl { get; set; }
        public virtual List<Hall> Halls { set; get; }
        public virtual List<Cuisine> Cuisines { set; get; }
        public virtual List<Review> Reviews { set; get; }

    }

    public enum Status
    {
        Unknown,
        Applied,
        Spam
    }

    [Table("Review")]
    public class Review
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Required]
        public int Stars { set; get; }
        [Required]
        public int Food { set; get; }
        [Required]
        public int Service { set; get; }
        [Required]
        public int Ambience { set; get; }
        [MaxLength(50)]
        public string ShortDesc { set; get; }
        [Required]
        [MaxLength(250)]
        public string Comment { set; get; }
        [DataType(DataType.DateTime)]
        public DateTime ReviewTime { set; get; }

        public Restaurant Restaurant { set; get; }
        [MaxLength(60)]
        public string Author { set; get; }

        public Status Status { set; get; }
    }


    [Table("Hall")]
    public class Hall
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public string Number { set; get; }
        public virtual List<DinnerTable> Tables { set; get; }

        public virtual Restaurant Restaurant { set; get; }
    }

    [Table("DinnerTable")]
    public class DinnerTable
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public int Number { set; get; }
        public bool IsReserved { set; get; }
        public Restaurant Restaurant { set; get; }
        public Hall Hall { set; get; }
    }
    [Table(("Reservation"))]
    public class Reservation
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public ReservationStatus Status { get; set; }
        [Required]
        public virtual UserInfo User { set; get; }
        [Required]
        public virtual DinnerTable Table { set; get; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime From { set; get; }
        [DataType(DataType.DateTime)]
        public DateTime To { set; get; }

        public int PeopleCount { set; get; }
        public string SpecialRequest { set; get; }
    }

    public enum ReservationStatus
    {
        New,
        Confirmed,
        Canceled,
        NoShow
    }

    [Table("Favorite")]
    public class Favorite
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual UserInfo User { set; get; }
        public virtual Restaurant Restaurant { set; get; }
    }
}