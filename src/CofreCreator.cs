using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCofres
{
    /// <summary>
    /// Creador abstracto. Define el algoritmo comun para abrir cualquier cofre
    /// (AbrirCofre) y delega en las subclases QUE recompensa concreta se genera
    /// (CrearRecompensa). Este metodo abstracto es el "Factory Method".
    /// Devuelve la recompensa creada para que el cliente (Program.cs) pueda
    /// registrarla en el inventario del jugador, sin que el patron en si cambie.
    /// </summary>
    public abstract class CofreCreator
    {
        protected abstract string NombreCofre { get; }
        protected abstract IRecompensa CrearRecompensa();

        public IRecompensa AbrirCofre()
        {
            Console.WriteLine();
            Console.WriteLine($"  >> Has abierto un {NombreCofre}! <<");
            Console.WriteLine("  ------------------------------------------");
            IRecompensa recompensa = CrearRecompensa();
            recompensa.Mostrar();
            return recompensa;
        }
    }
}
