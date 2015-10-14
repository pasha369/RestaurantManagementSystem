using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.Model
{
    [Table("Order")]
    public class Order
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public virtual Dish OrderDish { set; get; } 
    }

    [Table("Receipt")]
    public class Receipt
    {
        [Key]
        [Required]
        public int Id { set; get; }
        [DataType(DataType.DateTime)]
        public DateTime CurrentDateTime { set; get; }
        public virtual ClientInfo Client { set; get; }
        public virtual List<Order> ClientOrders { set; get; }
    }
}
