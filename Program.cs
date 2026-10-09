
using System;

class Cosmos2D
{
    static string[] planets = {
        "Mercurio", "Venus", "Tierra", "Marte",
        "Jupiter", "Saturno", "Urano", "Neptuno"
    };

    static int[] orbit = { 3, 6, 9, 13, 19, 25, 30, 35 };
    static int[] danger = { 2, 1, 0, 1, 3, 2, 1, 2 };

    static int fuel = 100;
    static int credits = 100;
    static int current = 2;
    static bool landed = false;
    static bool[] visited = new bool[8];

    static void Main()
    {
        Console.Title = "COSMOS 2D";

        while (true)
        {
            Console.Clear();
            DrawMap();

            Console.WriteLine("\n=== COSMOS 2D ===");
            Console.WriteLine("Nave: EXPLORER-01");
            Console.WriteLine("Combustible: " + fuel + "%");
            Console.WriteLine("Creditos: " + credits);
            Console.WriteLine("Ubicacion: " +
                (landed ? planets[current] + " (superficie)"
                        : "Orbita de " + planets[current]));

            Console.WriteLine("\n1. Viajar a otro planeta");
            Console.WriteLine("2. Aterrizar");
            Console.WriteLine("3. Despegar");
            Console.WriteLine("4. Explorar planeta");
            Console.WriteLine("5. Recargar combustible");
            Console.WriteLine("6. Ver informacion");
            Console.WriteLine("0. Salir");
            Console.Write("\nSelecciona: ");

            string op = Console.ReadLine();

            switch (op)
            {
                case "1": Travel(); break;
                case "2": Land(); break;
                case "3": TakeOff(); break;
                case "4": Explore(); break;
                case "5": Refuel(); break;
                case "6": Information(); break;
                case "0": return;
                default: Pause("Opcion no valida."); break;
            }
        }
    }

    static void DrawMap()
    {
        Console.WriteLine("         MAPA ESTELAR 2D\n");
        Console.WriteLine("Sol *----o----o----o----o----o----o----o");
        Console.WriteLine("     Me   Ve   Ti   Ma   Ju   Sa   Ur   Ne");
        Console.WriteLine();

        for (int i = 0; i < planets.Length; i++)
        {
            string marker = i == current ? " <NAVE>" : "";
            string status = visited[i] ? " [Explorado]" : "";

            Console.WriteLine(
                planets[i] + marker + status);
        }
    }

    static void Travel()
    {
        if (landed)
        {
            Pause("Primero debes despegar.");
            return;
        }

        Console.WriteLine("\nDestino:");
        for (int i = 0; i < planets.Length; i++)
            Console.WriteLine((i + 1) + ". " + planets[i]);

        Console.Write("Numero: ");
        int dest;

        if (!int.TryParse(Console.ReadLine(), out dest) ||
            dest < 1 || dest > planets.Length)
        {
            Pause("Destino no valido.");
            return;
        }

        dest--;

        if (dest == current)
        {
            Pause("Ya estas en este planeta.");
            return;
        }

        int cost = Math.Abs(orbit[dest] - orbit[current]) * 3 + 5;

        Console.WriteLine("Costo del viaje: " + cost + "%");

        if (fuel < cost)
        {
            Pause("Combustible insuficiente.");
            return;
        }

        fuel -= cost;
        current = dest;

        Pause("Llegaste a " + planets[current] + ".");
    }

    static void Land()
    {
        if (landed)
        {
            Pause("Ya estas en la superficie.");
            return;
        }

        if (fuel < 5)
        {
            Pause("Necesitas al menos 5% de combustible.");
            return;
        }

        fuel -= 5;
        landed = true;

        Pause("Aterrizaje completado en " +
              planets[current] + ".");
    }

    static void TakeOff()
    {
        if (!landed)
        {
            Pause("Ya estas en orbita.");
            return;
        }

        if (fuel < 10)
        {
            Pause("Necesitas 10% de combustible.");
            return;
        }

        fuel -= 10;
        landed = false;

        Pause("Despegue completado.");
    }

    static void Explore()
    {
        if (!landed)
        {
            Pause("Debes aterrizar primero.");
            return;
        }

        if (visited[current])
        {
            Pause("Ya exploraste este planeta.");
            return;
        }

        visited[current] = true;

        int reward = 20 + danger[current] * 10;
        credits += reward;

        Console.WriteLine("\nExploracion completada.");
        Console.WriteLine("Planeta: " + planets[current]);
        Console.WriteLine("Peligro: " + danger[current] + "/3");
        Console.WriteLine("Recompensa: " + reward + " creditos");

        Pause("Datos cientificos guardados.");
    }

    static void Refuel()
    {
        if (landed == false)
        {
            Pause("Debes estar en una superficie.");
            return;
        }

        int missing = 100 - fuel;

        if (missing == 0)
        {
            Pause("El tanque ya esta lleno.");
            return;
        }

        int affordable = credits / 2;
        int amount = Math.Min(missing, affordable);

        if (amount <= 0)
        {
            Pause("No tienes suficientes creditos.");
            return;
        }

        fuel += amount;
        credits -= amount * 2;

        Pause("Recargaste " + amount +
              "% de combustible.");
    }

    static void Information()
    {
        Console.WriteLine("\n=== DATOS DE LA MISION ===");
        Console.WriteLine("Planetas descubiertos: " +
            Array.FindAll(visited, x => x).Length + "/8");
        Console.WriteLine("Combustible: " + fuel + "%");
        Console.WriteLine("Creditos: " + credits);
        Console.WriteLine("Estado: " +
            (landed ? "En superficie" : "En orbita"));

        Pause("Fin del informe.");
    }

    static void Pause(string message)
    {
        Console.WriteLine("\n" + message);
        Console.WriteLine("Presiona ENTER para continuar...");
        Console.ReadLine();
    }
}
