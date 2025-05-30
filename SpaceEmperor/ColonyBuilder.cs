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

    private bool TryBuildModule(ColonyModule module)
    {
        if (_player.Money >= module.BuildCostMoney && _player.Raw >= module.BuildCostRaw)
        {
            _player.Money -= module.BuildCostMoney;
            _player.Raw -= module.BuildCostRaw;
            _colony.Modules.Add(module);
            Console.WriteLine($"{module.Name} побудовано. Залишок: {_player.Money}₴, {_player.Raw}🔧");
            return true;
        }
        else
        {
            Console.WriteLine($"Недостатньо ресурсів для побудови {module.Name}!");
            return false;
        }
    }

    public ColonyBuilder AddHousing()
    {
        TryBuildModule(new HousingModule());
        return this;
    }

    public ColonyBuilder AddIndustry()
    {
        TryBuildModule(new IndustrialModule());
        return this;
    }

    public ColonyBuilder AddMilitary()
    {
        TryBuildModule(new MilitaryModule());
        return this;
    }
}

