using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataObject;

namespace DbService.Entities;

[Table("consumable")]
public class Consumable
{
    /* Properties */
    [Key]
    [Column("id", Order = 0)]
    public int Id { get; set; }
    
    [Required]
    [Column("name", TypeName = "varchar")]
    [StringLength(255)]
    public string Name { get; set; } = String.Empty;
    
    [Required]
    [Column("bonus")]
    public bool Bonus { get; set; } //Bonus : true -> bonus (+) / false -> malus (-)
    
    [Required]
    [Column("effect")]
    public string Effect { get; set; } = String.Empty;
    /*[Required]
    [Column("effect")]
    public int Effect { get; set; }*/
    
    [Required]
    [Column("value")]
    public int Value { get; set; }
    
    [Required]
    [Column("price")]
    public double Price { get; set; }
    
    
    [Required]
    [ForeignKey("Player")]
    [Column("fk_player_id")]
    public int PlayerId { get; set; }
}