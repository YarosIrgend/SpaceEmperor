namespace SpaceEmperor;

public class MilitaryModuleFacade
{
    private readonly MilitaryModule _module;

    public MilitaryModuleFacade(MilitaryModule module)
    {
        _module = module;
    }

    public void BuildShip(Player player)
    {
        _module.BuildShip(player);
    }

    public void RepairShips(Player player)
    {
        _module.RepairShips(player);
    }
}
