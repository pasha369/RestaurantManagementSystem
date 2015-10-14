using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.Model
{
    [Table("UserInfo")]
    public class UserInfo
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public string Name { set; get; }
        [MaxLength(60)]
        public string Login { set; get; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        [MaxLength(60)]
        public string Password { set; get; }
        [DataType(DataType.PhoneNumber)]
        public int Phone { set; get; }
        public bool IsBanned { set; get; }
        public Role Position { set; get; }
    }
    [Table("CustomerInfo")]
    public class CustomerInfo:UserInfo
    {
        public string Detail { set; get; }

    }
    
    [Table("ClientInfo")]
    public class ClientInfo
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public virtual UserInfo UserInfo { set; get; }
        public virtual Restaurant Restaurant { set; get; }
    }

    public enum Role
    {
        Guest,
        User,
        Restaurateur
    }
}