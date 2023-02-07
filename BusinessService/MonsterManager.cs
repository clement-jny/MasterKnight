using DataObject;
using DbService;
using DbService.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessService;

public static class MonsterManager
{
    private static MonsterDTO ObjectToDTO(Monster monster)
    {
        MonsterDTO monsterDto = new()
        {
            Name = monster.Name,
            LifePoint = monster.LifePoint,
            Strength = monster.Strength
        };

        return monsterDto;
    }

    private static Monster ObjectToEntie(int monsterId, MonsterDTO monsterDto)
    {
        Monster monster = new()
        {
            Id = monsterId,
            Name = monsterDto.Name,
            LifePoint = monsterDto.LifePoint,
            Strength = monsterDto.Strength
        };

        return monster;
    }

    public static async Task<List<MonsterDTO>> GetAllMonstersAsync()
    {
        List<Monster> monsters = new();
        List<MonsterDTO> monstersDto = new();

        using (var _context = new MasterKnightContext())
        {
            monsters = await _context.Monsters.ToListAsync();
        }

        foreach (var monster in monsters)
        {
            monstersDto.Add(ObjectToDTO(monster));
        }

        return monstersDto;
    }

    public static async Task<MonsterDTO> GetOneMonsterAsync()
    {
        List<MonsterDTO> monstersDto = await GetAllMonstersAsync();

        int monsterId = new Random().Next(monstersDto.Count());

        return monstersDto[monsterId];
    }
}