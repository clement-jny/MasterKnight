using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbService.Entities;

[Table("weapon")]
public class Weapon
{
    /* Properties */
    [Key]
    [Column("id", Order = 0)]
    public int Id { get; set; }
    
    [Column("name", TypeName = "varchar")]
    [StringLength(255)]
    public string Name { get; set; } = String.Empty;
    
    
    [Column("damage")]
    public double Damage { get; set; }
    
    
    [Column("price")]
    public double Price { get; set; }
}