using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCofres
{

    public class RecompensaMonedas : IRecompensa
    {
        private static readonly Random random = new();
        private static readonly string[] variantes =
            { "50 monedas de oro", "120 monedas de oro", "30 monedas de plata" };

        public string Nombre { get; }
        public int Valor { get; } = 10;
        public RecompensaMonedas() => Nombre = variantes[random.Next(variantes.Length)];
        public void Mostrar() => Console.WriteLine($"  [$] {Nombre,-22} *          (+{Valor} pts)");
    }

    public class RecompensaArma : IRecompensa
    {
        private static readonly Random random = new();
        private static readonly string[] variantes =
            { "Espada de fuego", "Arco elfico", "Daga envenenada" };

        public string Nombre { get; }
        public int Valor { get; } = 25;
        public RecompensaArma() => Nombre = variantes[random.Next(variantes.Length)];
        public void Mostrar() => Console.WriteLine($"  [/] {Nombre,-22} **         (+{Valor} pts)");
    }

    public class RecompensaArmadura : IRecompensa
    {
        private static readonly Random random = new();
        private static readonly string[] variantes =
            { "Armadura de dragon", "Escudo sagrado", "Casco del titan" };

        public string Nombre { get; }
        public int Valor { get; } = 50;
        public RecompensaArmadura() => Nombre = variantes[random.Next(variantes.Length)];
        public void Mostrar() => Console.WriteLine($"  [0] {Nombre,-22} ***        (+{Valor} pts)");
    }

    public class RecompensaObjetoEspecial : IRecompensa
    {
        private static readonly Random random = new();
        private static readonly string[] variantes =
            { "Anillo del tiempo", "Amuleto fenix", "Corona ancestral" };

        public string Nombre { get; }
        public int Valor { get; } = 100;
        public RecompensaObjetoEspecial() => Nombre = variantes[random.Next(variantes.Length)];
        public void Mostrar() => Console.WriteLine($"  [*] {Nombre,-22} ****       (+{Valor} pts)");
    }
}