namespace SpaceEmperor;

public abstract class ColonyModule
{
    public string Name { get; protected set; }
    public int Level { get; protected set; } = 1;

    public abstract int GetMoneyProduction();
    public abstract int GetRawProduction();

    public abstract int BuildCostMoney { get; }
    public abstract int BuildCostRaw { get; }

    public virtual bool AllowsShipyard => false;

    public void Upgrade(ref Player player)
    {
        if (Level >= 3)
        {
            Console.WriteLine($"{Name} вже має максимальний рівень.");
            return;
        }

        int upgradeCostMoney = BuildCostMoney;
        int upgradeCostRaw = BuildCostRaw;

        if (player.Money >= upgradeCostMoney && player.Raw >= upgradeCostRaw)
        {
            player.Money -= upgradeCostMoney;
            player.Raw -= upgradeCostRaw;
            Level++;
            Console.WriteLine($"{Name} покращено до рівня {Level}.");
        }
        else
        {
            Console.WriteLine($"Недостатньо ресурсів для покращення {Name}. Потрібно: {upgradeCostMoney}₴, {upgradeCostRaw}🔧.");
        }
    }
    
    public override string ToString()
    {
        return $"{Name} (Level {Level}) — Дохід: {GetMoneyProduction()}₴, {GetRawProduction()}🔧";
    }
}

public class HousingModule : ColonyModule
{
    public HousingModule() => Name = "Житловий модуль";

    public override int GetMoneyProduction() => 60 * Level;
    public override int GetRawProduction() => 10 * Level;
    
    public override int BuildCostMoney => 100 * Level;
    public override int BuildCostRaw => 15 * Level;
}

public class IndustrialModule : ColonyModule
{
    public IndustrialModule() => Name = "Промисловий модуль";

    public override int GetMoneyProduction() => 20 * Level;
    public override int GetRawProduction() => 80 * Level;
    public override int BuildCostMoney => 60 * Level;
    public override int BuildCostRaw => 40 * Level;
}

public class MilitaryModule : ColonyModule
{
    public MilitaryModule() => Name = "Військовий модуль";

    public override int GetMoneyProduction() => 15 * Level;
    public override int GetRawProduction() => 15 * Level;
    
    public override int BuildCostMoney => 80 * Level;
    public override int BuildCostRaw => 70 * Level;
    public override bool AllowsShipyard => true;
    
    public int HPRepair => 10 * Level;

    public void BuildShip(Player player)
    {
        Console.WriteLine("Можна побудувати: ");
        Console.WriteLine("Малий корабель: HP - 30, Attack - 10. Ціна: 50₴, 25\ud83d\udd27");
        if(Level >= 2)
            Console.WriteLine("Середній корабель: HP - 60, Attack - 20. Ціна: 80₴, 40\ud83d\udd27");
        if(Level == 3)
            Console.WriteLine("Великий корабель: HP - 90, Attack - 30. Ціна: 120₴, 70\ud83d\udd27");
        int userShipChoice = int.Parse(Console.ReadLine());
        switch (userShipChoice)
        {
            case 1:
                if (player.Money >= 50 && player.Raw >= 25)
                {
                    player.Money -= 50;
                    player.Raw -= 25;
                    player.Ships.Add(new Ship(30,10,30));
                }
                else
                {
                    Console.WriteLine("Недостатньо ресурсів чи грошей");
                }
                
                break;
            case 2:
                if (Level >= 2)
                {
                    
                    if (player.Money >= 80 && player.Raw >= 40)
                    {
                        player.Ships.Add(new Ship(60, 20,60));
                        player.Money -= 80;
                        player.Raw -= 40;
                    }
                    else
                    {
                        Console.WriteLine("Недостатньо ресурсів чи грошей");
                    }
                }
                else
                {
                    Console.WriteLine("Треба як мінімум 2 рівень заводу");
                }
                break;
            case 3:
                if (Level == 3)
                {
                    if (player.Money >= 120 && player.Raw >= 70)
                    {
                        player.Ships.Add(new Ship(90, 30,90));
                        player.Money -= 120;
                        player.Raw -= 70;
                    }
                    else
                    {
                        Console.WriteLine("Недостатньо ресурсів чи грошей");
                    }
                }
                else
                {
                    Console.WriteLine("Треба 3 рівень заводу");
                }
                break;
            default:
                Console.WriteLine("Не той ввід");
                break;
        }
    }

    public void RepairShips(Player player)
    {
        foreach (var ship in player.Ships)
        {
            if (ship.MaxHp - ship.HP < HPRepair)
            {
                ship.HP = ship.MaxHp;
            }
            else
            {
                ship.HP += HPRepair;
            }
        }
    }
}