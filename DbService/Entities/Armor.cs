using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbService.Entities;

[Table("armor")]
public class Armor
{
    /* Properties */
    [Key]
    [Column("id", Order = 0)]
    public int Id { get; set; }


    [Column("name", TypeName = "varchar")]
    [StringLength(255)]
    public string Name { get; set; } = String.Empty;
    
    
    [Column("life_point")]
    public double LifePoint { get; set; }
    
    
    [Column("protection")]
    public double Protection { get; set; }
    
    
    [Column("price")]
    public double Price { get; set; }
}