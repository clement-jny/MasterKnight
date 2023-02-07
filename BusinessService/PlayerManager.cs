using DbService;
using DbService.Entities;
using DataObject;
using Microsoft.EntityFrameworkCore;

namespace BusinessService;

public static class PlayerManager
{
    private static PlayerDTO ObjectToDTO(Player player)
    {
        PlayerDTO playerDto = new()
        {
            Name = player.Name,
            BaseLifePoint = player.BaseLifePoint,
            LifePoint = player.LifePoint,
            BaseStrength = player.BaseStrength,
            Strength = player.Strength,
            Money = player.Money,
            ArmorId = player.ArmorId,
            WeaponId = player.WeaponId
        };

        return playerDto;
    }

    private static Player ObjectToEntitie(int playerId, PlayerDTO playerDto)
    {
        Player player = new()
        {
            Id = playerId,
            Name = playerDto.Name,
            BaseLifePoint = playerDto.BaseLifePoint,
            LifePoint = playerDto.LifePoint,
            BaseStrength = playerDto.BaseStrength,
            Strength = playerDto.Strength,
            Money = playerDto.Money,
            ArmorId = playerDto.ArmorId,
            WeaponId = playerDto.WeaponId
        };

        return player;
    }
    
    
    public static bool PlayerExist()
    {
        bool playerExist = false;
        
        using (var _context = new MasterKnightContext())
        {
            playerExist = _context.Players.Any();
        }

        return playerExist;
    }

    public static async Task<PlayerDTO> GetPlayerByIdAsync(int playerId)
    {
        Player player = new();

        using (var _context = new MasterKnightContext())
        {
            player = await _context.Players.FirstOrDefaultAsync(p => p.Id == playerId);
        }
        
        return ObjectToDTO(player);
    }

    public static async Task AddPlayerAsync(int playerId, PlayerDTO playerDto)
    {
        Player player = ObjectToEntitie(playerId, playerDto);
        using (var _context = new MasterKnightContext())
        {
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
        }
    }

    public static async Task<PlayerDTO> UpdatePlayerAsync(int playerId, PlayerDTO playerDto)
    {
        Player player = ObjectToEntitie(playerId, playerDto);

        using (var _context = new MasterKnightContext())
        {
            _context.Players.Update(player);
            await _context.SaveChangesAsync();
        }
        
        return ObjectToDTO(player);
    }

    public static async Task DeletePlayerAsync(int playerId)
    {
        Player player = ObjectToEntitie(playerId, await GetPlayerByIdAsync(playerId));
        
        using (var _context = new MasterKnightContext())
        {
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }
    }

    public static async Task SaveGame(int playerId, PlayerDTO playerDto)
    {
        Player player = ObjectToEntitie(playerId, playerDto);
        using (var _context = new MasterKnightContext())
        {
            _context.Players.Update(player);
            await _context.SaveChangesAsync();
        }
    }
}