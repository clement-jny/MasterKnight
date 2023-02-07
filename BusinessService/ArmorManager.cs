using DataObject;
using DbService;
using DbService.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessService;

public static class ArmorManager
{
    private static ArmorDTO ObjectToDTO(Armor armor)
    {
        ArmorDTO armorDto = new()
        {
            Name = armor.Name,
            LifePoint = armor.LifePoint,
            Protection = armor.Protection,
            Price = armor.Price
        };

        return armorDto;
    }

    private static Armor ObjectToEntitie(int armorId, ArmorDTO armorDto)
    {
        Armor armor = new()
        {
            Id = armorId,
            Name = armorDto.Name,
            LifePoint = armorDto.LifePoint,
            Protection = armorDto.Protection,
            Price = armorDto.Price
        };

        return armor;
    }

    public static async Task<ArmorDTO> GetArmorByIdAsync(int armorId)
    {
        Armor armor = new();

        using (var _context = new MasterKnightContext())
        {
            armor = await _context.Armors.FirstOrDefaultAsync(a => a.Id == armorId);
        }

        return ObjectToDTO(armor);
    }
}