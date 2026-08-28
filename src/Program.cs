using SistemaCofres;


Console.WriteLine("============================================");
Console.WriteLine("     SISTEMA DE COFRES Y RECOMPENSAS");
Console.WriteLine("============================================");
Console.WriteLine("Abre cofres, junta puntos y arma tu inventario de aventurero.");

// Estado de la partida (fuera del patron: esto es solo "el juego" que usa
// el resultado que entrega el Factory Method, no forma parte de el).
var inventario = new List<IRecompensa>();
int puntosTotales = 0;
int cofresAbiertos = 0;

bool salir = false;
while (!salir)
{
    Console.WriteLine();
    Console.WriteLine("--------------------------------------------");
    Console.WriteLine($" Cofres abiertos: {cofresAbiertos}   |   Puntos: {puntosTotales}");
    Console.WriteLine("--------------------------------------------");
    Console.WriteLine(" 1. Abrir Cofre Comun      (*)");
    Console.WriteLine(" 2. Abrir Cofre Raro       (**)");
    Console.WriteLine(" 3. Abrir Cofre Epico      (***)");
    Console.WriteLine(" 4. Abrir Cofre Legendario (****)");
    Console.WriteLine(" 5. Salir");
    Console.WriteLine(" 6. Ver mi inventario");
    Console.Write(" Opcion: ");
    string? opcion = Console.ReadLine();

    // El cliente solo elige QUE creador concreto usar (no crea la recompensa
    // directamente). La creacion real de la recompensa queda delegada
    // dentro de cada CofreCreator mediante el Factory Method.
    CofreCreator? cofre = opcion switch
    {
        "1" => new CofreComunCreator(),
        "2" => new CofreRaroCreator(),
        "3" => new CofreEpicoCreator(),
        "4" => new CofreLegendarioCreator(),
        _ => null
    };

    switch (opcion)
    {
        case "5":
            salir = true;
            continue;

        case "6":
            MostrarInventario(inventario, puntosTotales);
            continue;
    }

    if (cofre is null)
    {
        Console.WriteLine(" [x] Opcion invalida, intenta de nuevo.");
        continue;
    }

    IRecompensa recompensa = cofre.AbrirCofre();
    inventario.Add(recompensa);
    puntosTotales += recompensa.Valor;
    cofresAbiertos++;
}

MostrarResumenFinal(inventario, puntosTotales, cofresAbiertos);

// --- Funciones auxiliares del "juego" (no forman parte del patron) ---

void MostrarInventario(List<IRecompensa> items, int puntos)
{
    Console.WriteLine();
    Console.WriteLine("============================================");
    Console.WriteLine("             TU INVENTARIO");
    Console.WriteLine("============================================");
    if (items.Count == 0)
    {
        Console.WriteLine(" (vacio por ahora, abre un cofre!)");
        return;
    }

    foreach (var grupo in items.GroupBy(r => r.Nombre))
    {
        Console.WriteLine($" - {grupo.Key,-22} x{grupo.Count()}");
    }
    Console.WriteLine("--------------------------------------------");
    Console.WriteLine($" Puntos totales: {puntos}");
}

void MostrarResumenFinal(List<IRecompensa> items, int puntos, int cofres)
{
    Console.WriteLine();
    Console.WriteLine("============================================");
    Console.WriteLine("           RESUMEN DE LA PARTIDA");
    Console.WriteLine("============================================");
    Console.WriteLine($" Cofres abiertos: {cofres}");
    Console.WriteLine($" Puntos totales:  {puntos}");

    if (items.Count > 0)
    {
        var mejor = items.OrderByDescending(r => r.Valor).First();
        Console.WriteLine($" Mejor objeto obtenido: {mejor.Nombre} (+{mejor.Valor} pts)");
    }

    Console.WriteLine("============================================");
    Console.WriteLine(" Gracias por jugar!");
}