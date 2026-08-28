# Sistema de Cofres y Recompensas

Proyecto de ejemplo de **Patrones Creacionales** en C# / .NET, usando
**Factory Method** para generar recompensas según el tipo de cofre abierto.

## El problema

Ver el detalle completo en [`specs/specification.md`](specs/specification.md).
En resumen: un videojuego tiene 4 tipos de cofre (Comun, Raro, Epico,
Legendario), cada uno otorga un tipo de recompensa distinto, y se necesita
poder agregar nuevas categorias de cofre sin modificar el codigo que ya
gestiona la apertura de cofres.

## Patron utilizado: Factory Method

`CofreCreator` es el creador abstracto: define `AbrirCofre()` (logica comun)
y declara `CrearRecompensa()` como metodo abstracto. Cada subclase
(`CofreComunCreator`, `CofreRaroCreator`, `CofreEpicoCreator`,
`CofreLegendarioCreator`) decide que `IRecompensa` concreta crear.

## Estructura de clases