using BusinessService;
using DataObject;

namespace MasterKnight;

public static class Program
{
    private const int PLAYERID = 1;
    
    private static void Main(string[] args)
    {
        Console.Clear();
        
        Console.WriteLine("---- Welcome to Master Knight ----");
        Console.WriteLine("Will you succeed in becoming the best knight in this kingdom...");
        Console.WriteLine("While mixing combat and magic in front of formidable enemies.");

        Menu();
    }

    private static void Menu()
    {
        bool playerExist = false;
        bool validInput = false;
        int menuGameOption = 0;
        
        do
        {
            //Console.Clear();
            
            Console.WriteLine("\nHere is the game menu:");
        
            if (PlayerManager.PlayerExist())
            {
                playerExist = true;
                Console.WriteLine("\t1- Create a new game.");
                Console.WriteLine("\t2- Load a game.");
            }
            else
            {
                Console.WriteLine("\t1- Create a new game.");
            }
            
            Console.Write("\nWhat do you want to do? ");
            validInput = int.TryParse(Console.ReadLine(), out menuGameOption);
        } while (!validInput);

        switch (menuGameOption)
        {
            case 1:
                NewGame(playerExist);
                break;
            
            case 2:
                LoadGame();
                break;
            
            default:
                Menu();
                break;
        }
    }

    private static async void NewGame(bool playerExist)
    {
        if (playerExist)
        {
            bool validInput = false;
            char newGameChoice = '.';

            do
            {
                Console.Clear();
                
                Console.Write("Are you sure you want to restart a game (Y/N)? ");
                validInput = char.TryParse(Console.ReadLine(), out newGameChoice);
            } while (!validInput);

            switch (newGameChoice)
            {
                case 'Y':
                    await PlayerManager.DeletePlayerAsync(PLAYERID);
                    playerExist = false;
                    NewGame(playerExist);
                    break;
                
                case 'N':
                    LoadGame();
                    break;
                
                default:
                    NewGame(playerExist);
                    break;
            }
        }
        else
        {
            Console.Clear();
            
            Console.Write("\nFirst, please choose your username: ");
            string nickname = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(nickname))
            {
                nickname = Console.ReadLine();
            }
            else
            {
                PlayerDTO player = new()
                {
                    Name = nickname,
                    BaseLifePoint = 10,
                    LifePoint = 10,
                    BaseStrength = 5,
                    Strength = 5,
                    Money = 0,
                    ArmorId = 1,
                    WeaponId = 1
                };

                await PlayerManager.AddPlayerAsync(PLAYERID, player);
            
                StartGame(player);
            }
        }
    }

    private static async void LoadGame()
    {
        PlayerDTO player = await PlayerManager.GetPlayerByIdAsync(PLAYERID);
        player.Armor = await ArmorManager.GetArmorByIdAsync(player.ArmorId);
        player.Weapon = await WeaponManager.GetWeaponByIdAsync(player.WeaponId);
        player.Inventory = await ConsumableManager.GetAllConsumablesForPlayer(PLAYERID);
        
        StartGame(player);
    }

    private static async void StartGame(PlayerDTO player)
    {
        bool validInput = false;
        int menuOption = 0;

        do
        {
            Console.Clear();
        
            Console.WriteLine($"Hello Knight {player.Name}, what would you like to do today?");
        
            Console.WriteLine("\n\t1- Go to rest.");
            Console.WriteLine("\t2- Go to the store.");
            Console.WriteLine("\t3- Save your progress.");
            Console.WriteLine("\t4- Go hunting.");

            Console.Write("\nWhat is your choice? ");
            validInput = int.TryParse(Console.ReadLine(), out menuOption);
        } while (!validInput);

        switch (menuOption)
        {
            case 1:
                Rest(player);
                break;
            
            case 2:
                Store(player);
                break;
            
            case 3:
                SaveGame(player);
                break;
            
            case 4:
                MonsterDTO monsterDto = await MonsterManager.GetOneMonsterAsync();
                Fight(player, monsterDto);
                break;
            
            default:
                StartGame(player);
                break;
        }
    }

    private static async void Rest(PlayerDTO player)
    {
        bool validInput = false;
        int restChoice = 0;

        if (player.Money == 0)
        {
            Console.WriteLine("You don't have enought money. Come back later.");
        }
        else
        {
            Console.WriteLine("You spend 5$ and restore all your life points.");

            player.Money -= 5;
            player.LifePoint = player.BaseLifePoint;
            player = await PlayerManager.UpdatePlayerAsync(PLAYERID, player);
        }
        
        StartGame(player);
    }

    private static void Store(PlayerDTO player)
    {
        bool validInput = false;
        int storeChoice = 0;

        do
        {
            Console.Clear();
        
            Console.WriteLine("Welcome to the store.");

            Console.WriteLine("\t1- Buy");
            Console.WriteLine("\t2- Sale");
        
            Console.Write("\nWhat do you want to do? ");
            validInput = int.TryParse(Console.ReadLine(), out storeChoice);
        } while (!validInput);

        switch (storeChoice)
        {
            case 1:
                Store_Buy(player);
                break;
            
            case 2:
                Store_Sale(player);
                break;
            
            default:
                Store(player);
                break;
        }
    }

    private static async void Store_Buy(PlayerDTO player)
    {
        if (player.Money == 0)
        {
            Console.WriteLine("You have no money, try to sell something.");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Store(player);
        }
        else
        {
            List<ConsumableDTO> consumables = await ConsumableManager.GetAllFreeConsumables();

            foreach (var consumable in consumables)
            {
                char bonus = consumable.Bonus  ? '+' : '-';
                
                Console.WriteLine($"{consumable.Id} - Effect: {bonus}{consumable.Value} on {consumable.Effect}, price: {consumable.Price}$");
            }
            
            bool validInput = false;
            int cChoice = 0;

            do
            {
                Console.Write("What do you want to buy?");
                validInput = int.TryParse(Console.ReadLine(), out cChoice);
            } while (!validInput);


            ConsumableDTO cDto = await ConsumableManager.GetConsumableById(cChoice);
            
            player.Money -= cDto.Price;
            await ConsumableManager.AddConsumableForPlayer(cDto.Id, PLAYERID);
            player.Inventory.Add(cDto);
            
            
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        
            StartGame(player);
        }
    }

    private static async void Store_Sale(PlayerDTO player)
    {
        if (player.Inventory.Count == 0)
        {
            Console.WriteLine("You don't own any items, try to buy some");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Store(player);
        }
        else
        {
            foreach (var consumable in player.Inventory)
            {
                char bonus = consumable.Bonus  ? '+' : '-';
                
                //Lost 25% of the real price
                double price = consumable.Price - (consumable.Price * 25 / 100);
                
                Console.WriteLine($"{consumable.Id} - Effect: {bonus}{consumable.Value} on {consumable.Effect}, price: {consumable.Price}$");
            }
            
            bool validInput = false;
            int cChoice = 0;

            do
            {
                Console.Write("What do you want to sell? ");
                validInput = int.TryParse(Console.ReadLine(), out cChoice);
            } while (!validInput);
            
            
            ConsumableDTO cDto = await ConsumableManager.GetConsumableById(cChoice);
            double p = cDto.Price - (cDto.Price * 25 / 100);

            player.Money += p;
            await ConsumableManager.RemoveConsumableForPlayer(cDto.Id);
            player.Inventory.Remove(cDto);
            
            
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        
            StartGame(player);
        }
    }

    private static async void SaveGame(PlayerDTO player)
    {
        await PlayerManager.SaveGame(PLAYERID, player);
        
        Console.WriteLine("You saved the game.");
        Console.Write("Press any key to continue...");
        Console.ReadKey();
        
        StartGame(player);
    }

    private static void Fight(PlayerDTO player, MonsterDTO monsterDto)
    {
        Console.Clear();
        Console.WriteLine($"You will fight {monsterDto.Name}. Life point: {monsterDto.LifePoint}, strength: {monsterDto.Strength}.");
        
        Console.WriteLine($"You attack the monster, it loses {player.Strength} life points.");
        monsterDto.LifePoint -= player.Strength;

        if (monsterDto.LifePoint > 0)
        {
            Console.WriteLine($"The monster attacks you back, you lose {monsterDto.Strength} life points.");
            player.LifePoint -= monsterDto.Strength;

            if (player.LifePoint > 0)
            {
                Fight(player, monsterDto);
            }
            else
            {
                Console.WriteLine("The monster killed you.");
                player.LifePoint = player.BaseLifePoint;
                Console.WriteLine("You come back to the village.");
            
                Console.Write("Press any key to continue...");
                Console.ReadKey();
        
                StartGame(player);
            }
        }
        else
        {
            Console.WriteLine("You killed the monster.");
            Console.WriteLine("You come back to the village.");
            
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        
            StartGame(player);
        }
    }
}

