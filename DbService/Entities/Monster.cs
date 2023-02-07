using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbService.Entities;

[Table("monster")]
public class Monster
{
    [Key]
    [Column("id", Order = 0)]
    public int Id { get; set; }
    
    [Required]
    [Column("name", TypeName = "varchar")]
    [StringLength(255)]
    public string Name { get; set; } = String.Empty;
    
    [Required]
    [Column("life_point")]
    public double LifePoint { get; set; }
    
    
    [Required]
    [Column("strength")]
    public double Strength { get; set; }
}