namespace DataObject;

public class ConsumableDTO
{
    public int Id { get; set; }
    
    public string Name { get; set; } = String.Empty;
    
    public bool Bonus { get; set; } //Bonus : true -> bonus (+) / false -> malus (-)
    
    public string Effect { get; set; } = String.Empty;
    
    public int Value { get; set; }
    
    public double Price { get; set; }
}