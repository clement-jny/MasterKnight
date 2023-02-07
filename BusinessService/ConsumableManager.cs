using DataObject;
using DbService;
using DbService.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessService;

public static class ConsumableManager
{
    private static ConsumableDTO ObjectToDTO(Consumable consumable)
    {
        ConsumableDTO consumableDto = new()
        {
            Id = consumable.Id,
            Name = consumable.Name,
            Bonus = consumable.Bonus,
            Effect = consumable.Effect,
            Value = consumable.Value,
            Price = consumable.Price
        };

        return consumableDto;
    }

    private static Consumable ObjectToEntitie(ConsumableDTO consumableDto)
    {
        Consumable consumable = new()
        {
            Id = consumableDto.Id,
            Name = consumableDto.Name,
            Bonus = consumableDto.Bonus,
            Effect = consumableDto.Effect,
            Value = consumableDto.Value,
            Price = consumableDto.Price
        };

        return consumable;
    }

    public static async Task<List<ConsumableDTO>> GetAllFreeConsumables()
    {
        List<Consumable> consumables = new();
        List<ConsumableDTO> consumablesDto = new();

        using (var _context = new MasterKnightContext())
        {
            consumables = await _context.Consumables.Where(c => c.PlayerId == 0).ToListAsync();
        }

        foreach (var consumable in consumables)
        {
            consumablesDto.Add(ObjectToDTO(consumable));
        }

        return consumablesDto;
    }

    public static async Task<List<ConsumableDTO>> GetAllConsumablesForPlayer(int playerId)
    {
        List<Consumable> consumables = new();
        List<ConsumableDTO> consumablesDto = new();

        using (var _context = new MasterKnightContext())
        {
            consumables = await _context.Consumables.Where(c => c.PlayerId == playerId).ToListAsync();
        }

        foreach (var consumable in consumables)
        {
            consumablesDto.Add(ObjectToDTO(consumable));
        }
        
        return consumablesDto;
    }

    public static async Task<ConsumableDTO> GetConsumableById(int consumableId)
    {
        Consumable consumable = new();

        using (var _context = new MasterKnightContext())
        {
            consumable = await _context.Consumables.FirstOrDefaultAsync(c => c.Id == consumableId);
        }

        return ObjectToDTO(consumable);
    }

    public static async Task AddConsumableForPlayer(int consumableId, int playerId)
    {
        using (var _context = new MasterKnightContext())
        {
            Consumable consumable = await _context.Consumables.FirstOrDefaultAsync(c => c.Id == consumableId);
            consumable.PlayerId = playerId;

            _context.Consumables.Update(consumable);
            await _context.SaveChangesAsync();
        }
    }

    public static async Task RemoveConsumableForPlayer(int consumableId)
    {
        using (var _context = new MasterKnightContext())
        {
            Consumable consumable = await _context.Consumables.FirstOrDefaultAsync(c => c.Id == consumableId);
            consumable.PlayerId = 0;

            _context.Consumables.Update(consumable);
            await _context.SaveChangesAsync();
        }
    }
}