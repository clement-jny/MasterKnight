using DataObject;
using DbService;
using DbService.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessService;

public static class WeaponManager
{
    private static WeaponDTO ObjectToDTO(Weapon weapon)
    {
        WeaponDTO weaponDto = new()
        {
            Name = weapon.Name,
            Damage = weapon.Damage,
            Price = weapon.Price
        };

        return weaponDto;
    }

    private static Weapon ObjectToEntitie(int weaponId, WeaponDTO weaponDto)
    {
        Weapon weapon = new()
        {
            Id = weaponId,
            Name = weaponDto.Name,
            Damage = weaponDto.Damage,
            Price = weaponDto.Price
        };

        return weapon;
    }

    public static async Task<WeaponDTO> GetWeaponByIdAsync(int weaponId)
    {
        Weapon weapon = new();

        using (var _context = new MasterKnightContext())
        {
            weapon = await _context.Weapons.FirstOrDefaultAsync(w => w.Id == weaponId);
        }

        return ObjectToDTO(weapon);
    }
}