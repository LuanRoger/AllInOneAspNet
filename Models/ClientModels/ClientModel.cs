using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AllInOneAspNet.Models.UserModels;

namespace AllInOneAspNet.Models.ClientModels;

[Table("Clients")]
public class ClientModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    [Column("ID")]
    public int id { get; set; }
    
    [Required]
    [MaxLength(100)]
    [Column("Username")]
    public string username { get; set; }
    
    [Required]
    [Column("CreatedBy")]
    public UserModel createdBy { get; set; }
}