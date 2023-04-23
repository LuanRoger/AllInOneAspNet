using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllInOneAspNet.Models.UserModels;

[Table("Users")]
public class UserModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    [Column("ID")]
    public int id { get; set; }
    
    [Required]
    [Column("Username")]
    public string username { get; set; }
    
    [Column("Email")]
    public string email { get; set; }
    
    [Required]
    [Column("Password")]
    public string password { get; set; }
}