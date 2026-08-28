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

IRecompensa (interfaz)
├── RecompensaMonedas
├── RecompensaArma
├── RecompensaArmadura
└── RecompensaObjetoEspecial

CofreCreator (abstracta)
├── CofreComunCreator
├── CofreRaroCreator
├── CofreEpicoCreator
└── CofreLegendarioCreator


## Como ejecutar en Visual Studio

1. Abre `CofresApp.sln` (o la solucion del proyecto).
2. Presiona **F5**.
3. En la consola, elige un numero del 1 al 4 para abrir un cofre, 6 para
   ver tu inventario, o 5 para salir.

### Ejemplo de salida
============================================
SISTEMA DE COFRES Y RECOMPENSAS

Abre cofres, junta puntos y arma tu inventario de aventurero.

Cofres abiertos: 0 | Puntos: 0
Abrir Cofre Comun (*)
Abrir Cofre Raro (**)
Abrir Cofre Epico (***)
Abrir Cofre Legendario (****)
Salir
Ver mi inventario
Opcion: 3

Has abierto un Cofre Epico!

[0] Escudo sagrado *** (+50 pts)


## Sistema de puntos e inventario

Cada recompensa otorga puntos segun su rareza (Monedas=10, Arma=25,
Armadura=50, Objeto especial=100). El inventario y el puntaje son manejados
por `Program.cs` a partir de lo que devuelve `AbrirCofre()`; no forman parte
del patron Factory Method en si, solo enriquecen la experiencia de juego.

## Como extender el ejemplo

Para agregar un "Cofre Mitico" no se modifica ni una linea de
`CofreCreator.cs`:

1. Crear `RecompensaMitica : IRecompensa`.
2. Crear `CofreMiticoCreator : CofreCreator` que devuelva `new RecompensaMitica()`.
3. Agregar la opcion "7. Mitico" al `switch` de `Program.cs`.

## Guion rapido para la exposicion (5 min)

1. **(0-1 min)** Problema: distintos cofres -> distintas recompensas, sin
   acoplar el codigo del juego a clases concretas de recompensa.
2. **(1-2 min)** Por que Factory Method (y por que no Abstract Factory,
   Builder, Singleton o Prototype).
3. **(2-3 min)** Diagrama de clases: `IRecompensa`, `CofreCreator` y sus hijas.
4. **(3-4 min)** Demo en vivo con F5, abriendo varios cofres, revisando el
   inventario, y mostrando `CofreCreator.cs` para evidenciar que no conoce
   las clases concretas.
5. **(4-5 min)** Resumen + pregunta abierta.