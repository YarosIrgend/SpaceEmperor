namespace SpaceEmperor;

public class Colony
{
    public List<ColonyModule> Modules { get; private set; } = new();
    public ColonyBuilder Builder { get; private set; }

    public Colony(List<ColonyModule> colonyModules, Player player)
    {
        Modules = colonyModules;
        Builder = new ColonyBuilder(this, player);
    }

    public int TotalMoneyProduction => Modules.Sum(m => m.GetMoneyProduction());
    public int TotalRawProduction => Modules.Sum(m => m.GetRawProduction());
    public bool HasShipyard => Modules.Any(m => m.AllowsShipyard);
    
    public bool HasModule<T>() where T : ColonyModule
    {
        return Modules.Any(m => m is T);
    }
    public void UpgradeModule(int moduleIndex, Player player)
    {
        if (moduleIndex < 0 || moduleIndex >= Modules.Count)
        {
            Console.WriteLine("Неправильний індекс модуля.");
            return;
        }

        Modules[moduleIndex].Upgrade(ref player);
    }
    public void DisplayInfo()
    {
        Console.WriteLine("Колонія:");
        foreach (var module in Modules)
        {
            Console.WriteLine($"- {module}");
        }
    }
}


