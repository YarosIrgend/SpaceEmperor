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

    public void RepairShips(List<Ship> ships)
    {
        foreach (var ship in ships)
        {
            if (ship.MaxHp - ship.HP < _module.HPRepair)
            {
                ship.HP = ship.MaxHp;
            }
            else
            {
                ship.HP += _module.HPRepair;
            }
        }
    }
}
