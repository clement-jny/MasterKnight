namespace DataObject;

public class PlayerDTO
{
    public string Name { get; set; } = string.Empty;
    
    public double BaseLifePoint { get; set; }
    
    public double LifePoint { get; set; }
    
    public double BaseStrength { get; set; }
    
    public double Strength { get; set; }
    
    public double Money { get; set; }

    public int ArmorId { get; set; }
    public virtual ArmorDTO Armor { get; set; } = new();
    
    public int WeaponId { get; set; }
    public virtual WeaponDTO Weapon { get; set; } = new();

    public virtual List<ConsumableDTO> Inventory { get; set; } = new();
}