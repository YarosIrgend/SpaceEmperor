namespace SpaceEmperor;

public class Planet(string name, bool isCaptured, Colony colony, List<Ship> ships, IDefenseStrategy defenseStrategy)
{
    public string Name { get; set; } = name;
    public bool IsCaptured { get; set; } = isCaptured;
    public Colony Colony { get; set; } = colony;
    public List<Ship> Ships { get; set; } = ships;
    public IDefenseStrategy DefenseStrategy { get; set; } = defenseStrategy;
    
    public void SetColony(Colony colony)
    {
        Colony = colony;
    }
}