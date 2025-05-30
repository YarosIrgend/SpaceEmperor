namespace SpaceEmperor;

public class Player(int money, int raw, List<Ship> ships, Planet currentPlanet, PlanetSystem currentPlanetSystem)
{
    public int Money { get; set; } = money;
    public int Raw { get; set; } = raw;
    public List<Ship> Ships { get; set; } = ships;
    public Planet CurrentPlanet { get; set; } = currentPlanet;
    public PlanetSystem CurrentPlanetSystem { get; set; } = currentPlanetSystem;
    
    
    public void DisplayInfo(List<PlanetSystem> planetSystems)
    {
        Console.Clear();
        Console.WriteLine($"Гроші: {Money}, Ресурси: {Raw}");
        Console.WriteLine($"Дохід грошей: {GetMoneyIncome(planetSystems)}, Дохід ресурсів: {GetRawIncome(planetSystems)}");
        Console.WriteLine($"Кораблі: {Ships.Count}");
        foreach (var ship in Ships)
        {
            Console.WriteLine($"- HP: {ship.HP}, Attack: {ship.Attack}");
        }
        
    }

    public int GetMoneyIncome(List<PlanetSystem> planetSystems)
    {
        int moneyIncome = 0; 
        foreach (PlanetSystem planetSystem in planetSystems)
        {
            foreach (var planet in planetSystem.Planets)
            {
                if (planet.IsCaptured && planet.Colony != null)
                {
                    moneyIncome += planet.Colony.TotalMoneyProduction;
                }
            }
        }
        return moneyIncome;
    }
    
    public int GetRawIncome(List<PlanetSystem> planetSystems)
    {
        int rawIncome = 0; 
        foreach (PlanetSystem planetSystem in planetSystems)
        {
            foreach (var planet in planetSystem.Planets)
            {
                if (planet.IsCaptured && planet.Colony != null)
                {
                    rawIncome += planet.Colony.TotalRawProduction;
                }
            }
        }
        return rawIncome;
    }
    
}
