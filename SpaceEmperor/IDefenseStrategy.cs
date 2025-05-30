namespace SpaceEmperor;

public interface IDefenseStrategy
{
    void Defense(List<Ship> planetShips, List<Ship> playerShips);
    void DefenseDisplay();
}

public class DistributedDefense : IDefenseStrategy
{
    public void Defense(List<Ship> planetShips, List<Ship> playerShips)
    {
        for (int i = 0; i < planetShips.Count; i++)
        {
            var attacker = planetShips[i];
            var defender = playerShips[i % playerShips.Count];
            defender.HP -= attacker.Attack;
            defender.HP = Math.Max(0, defender.HP);
        }
    }

    public void DefenseDisplay()
    {
        Console.WriteLine("\tТактика захисту: розподілений");
    } 
}

public class AllOnOneDefense : IDefenseStrategy
{
    public void Defense(List<Ship> planetShips, List<Ship> playerShips)
    {
        int defenderIndex = 0;

        for (int i = 0; i < planetShips.Count && defenderIndex < playerShips.Count; i++)
        {
            var attacker = planetShips[i];
            var defender = playerShips[defenderIndex];

            defender.HP -= attacker.Attack;
            defender.HP = Math.Max(0, defender.HP);

            if (defender.HP == 0)
            {
                defenderIndex++;
            }
        }

        // Видаляємо знищені кораблі гравця після атаки (опційно, залежно від логіки гри)
        playerShips.RemoveAll(s => s.HP <= 0);
    }
    
    public void DefenseDisplay()
    {
        Console.WriteLine("\tТактика захисту: кілька проти одного");
    } 
}
