using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCofres
{
    public class CofreComunCreator : CofreCreator
    {
        protected override string NombreCofre => "Cofre Comun";
        protected override IRecompensa CrearRecompensa() => new RecompensaMonedas();
    }

    public class CofreRaroCreator : CofreCreator
    {
        protected override string NombreCofre => "Cofre Raro";
        protected override IRecompensa CrearRecompensa() => new RecompensaArma();
    }

    public class CofreEpicoCreator : CofreCreator
    {
        protected override string NombreCofre => "Cofre Epico";
        protected override IRecompensa CrearRecompensa() => new RecompensaArmadura();
    }

    public class CofreLegendarioCreator : CofreCreator
    {
        protected override string NombreCofre => "Cofre Legendario";
        protected override IRecompensa CrearRecompensa() => new RecompensaObjetoEspecial();
    }
}