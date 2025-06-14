﻿namespace SpaceEmperor;

public class PlanetSystem(string starName, List<Planet> planets)
{
    public string StarName { get; set; } = starName;
    public List<Planet> Planets { get; set; } = planets;
    public List<PlanetSystem> Neighbours { get; set; }

    public void AssignNeighbours(List<PlanetSystem> systems)
    {
        if (Neighbours == null)
        {
            Neighbours = new List<PlanetSystem>();
        }
        foreach (var system in systems)
        {
            Neighbours.Add(system);
            if (system.Neighbours == null)
            {
                system.Neighbours = new List<PlanetSystem>();
            }
            system.Neighbours.Add(this);
        }
    }

    public void DisplayInfo()
    {
        foreach (Planet planet in Planets)
        {
            Console.Write($"{planet.Name} - ");
            if (planet.IsCaptured)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("ЗАХОПЛЕНА");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("НЕ ЗАХОПЛЕНА");
                Console.ResetColor();
                if (planet.Ships != null)
                {
                    foreach (var ship in planet.Ships)
                    {
                        Console.WriteLine($"\t- HP: {ship.HP}, Attack: {ship.Attack}");
                    }
                }

                if (planet.DefenseStrategy != null)
                {
                    planet.DefenseStrategy.DefenseDisplay();
                }
            }
        }
    }
}