using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbService.Entities;

[Table("player")]
public class Player
{
    /* Properties */
    [Key]
    [Column("id", Order = 0)]
    public int Id { get; set; }
    
    [Column("name", TypeName = "varchar")]
    [StringLength(255)]
    public string Name { get; set; } = String.Empty;
    
    [Column("base_life_point")]
    public double BaseLifePoint { get; set; }
    
    
    [Column("life_point")]
    public double LifePoint { get; set; }
    
    [Column("base_strength")]
    public double BaseStrength { get; set; }
    
    
    [Column("strength")]
    public double Strength { get; set; }
    
    [Column("money")]
    public double Money { get; set; }
    
    /* Relationship */
    [ForeignKey("Armor")]
    [Column("fk_armor_id")]
    public int ArmorId { get; set; }


    [Required]
    [ForeignKey("Weapon")]
    [Column("fk_weapon_id")]
    public int WeaponId { get; set; }
}