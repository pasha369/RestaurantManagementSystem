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
        public virtual Dish Dish { set; get; }
        public virtual Restaurant Restaurant { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Created { get; set; }
    }

    [Table("Receipt")]
    public class Receipt
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [DataType(DataType.DateTime)]
        public DateTime CurrentDateTime { set; get; }
        public virtual UserInfo Client { set; get; }
        public virtual List<Order> ClientOrders { set; get; }
        public ReceiptStatus ReceiptStatus { get; set; }
    }

    public enum ReceiptStatus { Open, Closed }
    public enum OrderStatus { Active, Wait, Ready }
}
