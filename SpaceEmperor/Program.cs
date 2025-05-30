using SpaceEmperor;

Planet earth = new Planet("Земля", true, null, null, null);
Planet venus = new Planet("Венера", false, null, [new(30, 10, 30), new(30, 10, 30)], new AllOnOneDefense());
Planet mars = new Planet("Марс", false, null, null, new AllOnOneDefense());

PlanetSystem sun = new PlanetSystem("Сонце", [earth, venus, mars]);

Player player = new Player(300, 100, [new(30, 10, 30), new(30, 10, 30), new(30, 10, 30)], earth, sun);

// існуюча колонія Землі
Colony earthColony = new Colony(new List<ColonyModule>
    { new HousingModule(), new IndustrialModule(), new MilitaryModule() }, player);
earth.SetColony(earthColony);

Planet newFives = new Planet("Нові Фіви", false, null, [new(60, 20, 30), new(30, 10, 30)], new AllOnOneDefense());
Planet orlenon = new Planet("Орленон", false, null, [new(60, 20, 30), new(30, 10, 30)], new DistributedDefense());
PlanetSystem betelgeuse = new PlanetSystem("Бетельгейзе", [newFives, orlenon]);
sun.AssignNeighbours(new List<PlanetSystem>{betelgeuse});
betelgeuse.AssignNeighbours(new List<PlanetSystem>{sun});

List<PlanetSystem> planetSystems = [sun, betelgeuse];

// ігровий процес
Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("Ви міжпланетний загарбник");
Console.ResetColor();
int day = 1;
int userChoice;
do
{
    Console.WriteLine($"День: {day}");
    if (player.CurrentPlanet != null) // гравець на планеті
    {
        Console.WriteLine($"Ви на планеті {player.CurrentPlanet.Name}, системи {player.CurrentPlanetSystem.StarName}");
        if (player.CurrentPlanet.Colony != null)
        {
            player.CurrentPlanet.Colony.DisplayInfo();
        }
        else
        {
            Console.WriteLine("Поки немає колонії");
        }

        Console.WriteLine("\n0 - Завершити гру");
        Console.WriteLine("1 - Будувати колонію");
        Console.WriteLine("2 - Здійснити виліт");
        Console.WriteLine("3 - Переглянути гравця");
        Console.WriteLine("4 - Закінчити день");
        if (player.CurrentPlanet.Colony != null)
        {
            if (player.CurrentPlanet.Colony.HasShipyard)
            {
                Console.WriteLine("5 - Будувати кораблі");
            }
        }

        userChoice = int.Parse(Console.ReadLine());

        switch (userChoice)
        {
            case 0:
                break;
            case 1:
                BuildColony();
                break;
            case 2:
                EndDay();
                player.CurrentPlanet = null; // вилітає з планети
                break;
            case 3:
                player.DisplayInfo(planetSystems);
                Console.ReadLine();
                break;
            case 4:
                EndDay();
                ShipsRepair();
                break;
            case 5:
                BuildShip();
                break;
        }

        Console.Clear();
    }
    else // тобто гравець у космосі
    {
        Console.WriteLine($"Ви в системі {player.CurrentPlanetSystem.StarName}");
        player.CurrentPlanetSystem.DisplayInfo();
        Console.WriteLine("\n0 - Завершити гру");
        Console.WriteLine("1 - Обрати планету для висадки");
        Console.WriteLine("2 - Здійснити гіперстрибок в іншу систему");
        Console.WriteLine("3 - Переглянути гравця");
        Console.WriteLine("4 - Закінчити день");
        userChoice = int.Parse(Console.ReadLine());

        switch (userChoice)
        {
            case 0:
                break;
            case 1:
                CaptureOrSitPlanet();
                break;
            case 2:
                DoHyperjump();
                break;
            case 3:
                player.DisplayInfo(planetSystems);
                Console.ReadLine();
                break;
            case 4:
                EndDay();
                break;
        }

        Console.Clear();
    }
} while (userChoice != 0);

Console.WriteLine($"Ви закінчили гру на {day} дні");
Thread.Sleep(2000);

// методи program
void EndDay()
{
    day++;
    player.Money += player.GetMoneyIncome(planetSystems);
    player.Raw += player.GetRawIncome(planetSystems);
}

void BuildColony()
{
    var colony = player.CurrentPlanet.Colony;

    // Якщо колонії ще нема — створюємо
    if (colony == null)
    {
        colony = new Colony(new List<ColonyModule>(), player);
        player.CurrentPlanet.Colony = colony;
        Console.WriteLine("Колонію створено.");
    }

    bool done = false;
    while (!done)
    {
        Console.Clear();
        colony.DisplayInfo();
        Console.WriteLine("\nЩо бажаєте зробити?");
        Console.WriteLine($"1 - Побудувати житловий модуль (Вартість: 100₴, 15🔧)");
        Console.WriteLine("2 - Побудувати промисловий модуль (Вартість: 60₴, 40🔧)");
        Console.WriteLine("3 - Побудувати військовий модуль (Вартість: 80₴, 70🔧)");
        Console.WriteLine("4 - Покращити модуль");
        Console.WriteLine("0 - Назад");

        Console.Write("Ваш вибір: ");
        if (!int.TryParse(Console.ReadLine(), out int choice)) continue;

        switch (choice)
        {
            case 0:
                done = true;
                break;

            case 1:
                if (colony.HasModule<HousingModule>())
                    Console.WriteLine("Житловий модуль вже існує.");
                else
                    colony.Builder.AddHousing();
                break;

            case 2:
                if (colony.HasModule<IndustrialModule>())
                    Console.WriteLine("Промисловий модуль вже існує.");
                else
                    colony.Builder.AddIndustry();
                break;

            case 3:
                if (colony.HasModule<MilitaryModule>())
                    Console.WriteLine("Військовий модуль вже існує.");
                else
                    colony.Builder.AddMilitary();
                break;

            case 4:
                Console.WriteLine("Який модуль хочете покращити?");
                for (int i = 0; i < colony.Modules.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {colony.Modules[i]}");
                }

                if (int.TryParse(Console.ReadLine(), out int upgradeIndex))
                {
                    colony.UpgradeModule(upgradeIndex - 1, player);
                }
                else
                {
                    Console.WriteLine("Невірний ввід.");
                }
                break;

            default:
                Console.WriteLine("Невірна опція.");
                break;
        }

        Console.WriteLine("\nНатисніть Enter, щоб продовжити...");
        Console.ReadLine();
    }
}

void ShipsRepair()
{
    if (player.CurrentPlanet.Colony != null)
    {
        if (player.CurrentPlanet.Colony.HasShipyard) // ремонт кораблів, якщо є військова база
        {
            var militaryModule = player.CurrentPlanet.Colony.Modules.OfType<MilitaryModule>().FirstOrDefault();
            if (militaryModule != null)
            {
                var facade = new MilitaryModuleFacade(militaryModule);
                facade.RepairShips(player.Ships);
            }
        }
    }
}

void BuildShip()
{
    if (player.CurrentPlanet.Colony != null && player.CurrentPlanet.Colony.HasShipyard)
    {
        var militaryModule = player.CurrentPlanet.Colony.Modules.OfType<MilitaryModule>().FirstOrDefault();
        if (militaryModule != null)
        {
            var facade = new MilitaryModuleFacade(militaryModule);
            facade.BuildShip(player);
        }
    }
}


void CaptureOrSitPlanet()
{
    Console.WriteLine("Виберіть планету:");
    int choice = 1;
    foreach (var planet in player.CurrentPlanetSystem.Planets)
    {
        Console.WriteLine($"{choice} - {planet.Name}");
        choice++;
    }

    int userPlanetChoice = int.Parse(Console.ReadLine());
    Planet planetToLand = null;

    try
    {
        planetToLand = player.CurrentPlanetSystem.Planets[userPlanetChoice - 1];
    }
    catch
    {
        Console.WriteLine("Немає планети з таким номером.");
        return;
    }

    if (planetToLand != null)
    {
        if (planetToLand.IsCaptured || planetToLand.Ships == null || planetToLand.Ships.Count == 0)
        {
            player.CurrentPlanet = planetToLand;
            planetToLand.IsCaptured = true;
            Console.WriteLine($"Висадка на планету {planetToLand.Name} без бою.");
            EndDay();
            return;
        }

        Console.WriteLine($"Починається битва за планету {planetToLand.Name}...");
        Thread.Sleep(1500);

        var playerShips = player.Ships;
        var planetShips = planetToLand.Ships;

        if (playerShips.Count == 0)
        {
            Console.WriteLine("У вас немає кораблів для атаки.");
            EndDay();
            return;
        }
        
        // Атака гравця
        for (int i = 0; i < playerShips.Count; i++)
        {
            var attacker = playerShips[i];
            var defender = planetShips[i % planetShips.Count];
            defender.HP -= attacker.Attack;
            defender.HP = Math.Max(0, defender.HP);
        }
        // оборона планети
        planetToLand.DefenseStrategy.Defense(planetShips, playerShips); 
        
        
        // Якщо ворог повністю знищений
        if (planetShips.Count == 0)
        {
            planetToLand.IsCaptured = true;
            planetToLand.Ships = null;
            player.CurrentPlanet = planetToLand;
            player.CurrentPlanet.DefenseStrategy = null;
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Флот ворога розбитий");
            Console.ResetColor();
        }
        
        if (playerShips.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("УВАГА! Усі ваші кораблі знищено!");
            Console.ResetColor();
        }

        Thread.Sleep(1500);
        EndDay();
    }
}

void DoHyperjump()
{
    Console.WriteLine("В яку систему хочете стрибнути:");
    int choice = 1;
    foreach (var planetSystem in player.CurrentPlanetSystem.Neighbours)
    {
        Console.WriteLine($"{choice} - {planetSystem.StarName}");
    }
    int userSystemChoice = int.Parse(Console.ReadLine());

    PlanetSystem planetSystemToJump = null;
    try
    {
        planetSystemToJump = player.CurrentPlanetSystem.Neighbours[userSystemChoice - 1];
    }
    catch
    {
        Console.WriteLine("Немає планети з таким номером.");
    }

    if (planetSystemToJump != null)
    {
        player.CurrentPlanetSystem = planetSystemToJump;
        Console.WriteLine("Здійсненюється гіперстрибок...");
        Thread.Sleep(1500);
    }
    EndDay();
}