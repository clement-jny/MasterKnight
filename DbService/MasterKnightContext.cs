using DbService.Entities;
using Microsoft.EntityFrameworkCore;

namespace DbService;

public class MasterKnightContext : DbContext
{
    //public MasterKnightContext() { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql("server=localhost;database=MasterKnight;uid=root;pwd=root", 
                Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.39-mysql"));
        }
        optionsBuilder.EnableSensitiveDataLogging();
        
        //base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);
    }

    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<Armor> Armors { get; set; } = null!;
    public DbSet<Weapon> Weapons { get; set; } = null!;
    public DbSet<Consumable> Consumables { get; set; } = null!;
    public DbSet<Monster> Monsters { get; set; } = null!;
}