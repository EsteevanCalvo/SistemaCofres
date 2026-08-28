using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCofres
{
    public interface IRecompensa
    {
        string Nombre { get; }
        int Valor { get; }   // puntos que otorga al jugador, según su rareza
        void Mostrar();
    }
}