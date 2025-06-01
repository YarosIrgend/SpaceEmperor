namespace SpaceEmperor;

public class ColonyBuilder
{
    private readonly Colony _colony;
    private readonly Player _player;

    public ColonyBuilder(Colony colony, Player player)
    {
        _colony = colony;
        _player = player;
    }

    private void TryBuildModule(ColonyModule module)
    {
        if (_player.Money >= module.BuildCostMoney && _player.Raw >= module.BuildCostRaw)
        {
            _player.Money -= module.BuildCostMoney;
            _player.Raw -= module.BuildCostRaw;
            _colony.Modules.Add(module);
            Console.WriteLine($"{module.Name} побудовано. Залишок: {_player.Money}₴, {_player.Raw}🔧");
        }
        else
        {
            Console.WriteLine($"Недостатньо ресурсів для побудови {module.Name}!");
        }
    }

    public void AddHousing()
    {
        TryBuildModule(new HousingModule());
    }

    public void AddIndustry()
    {
        TryBuildModule(new IndustrialModule());
    }

    public void AddMilitary()
    {
        TryBuildModule(new MilitaryModule());
    }
}

