#  Sistema de Cofres y Recompensas

Proyecto académico desarrollado en **C# y .NET** para aplicar el patrón de diseño creacional **Factory Method**.

##  Problema

En un videojuego existen diferentes tipos de cofres: Común, Raro, Épico y Legendario. Cada cofre debe generar una recompensa diferente.

El problema consiste en evitar que la lógica principal del juego tenga que crear directamente cada tipo de recompensa. Para resolverlo, se utiliza un patrón creacional que permite delegar la creación de las recompensas.

##  Patrón utilizado: Factory Method

Se utilizó el patrón **Factory Method**.

La clase abstracta `CofreCreator` define el comportamiento general para abrir un cofre y declara el método `CrearRecompensa()`.

Cada clase concreta de cofre implementa este método y decide qué recompensa crear:

* `CofreComunCreator` crea una recompensa de monedas.
* `CofreRaroCreator` crea una recompensa de arma.
* `CofreEpicoCreator` crea una recompensa de armadura.
* `CofreLegendarioCreator` crea una recompensa de objeto especial.

De esta forma, el programa puede trabajar con la abstracción `CofreCreator` sin depender directamente de las clases concretas de recompensa.

## ▶ Cómo ejecutar el proyecto

### Desde Visual Studio

1. Abrir la solución `SistemaCofres.slnx`.
2. Seleccionar el proyecto como proyecto de inicio.
3. Presionar **F5** para ejecutar.

### Desde la terminal

Ubicarse en la carpeta principal del proyecto y ejecutar:

```bash
dotnet run --project src/SistemaCofres.csproj
```

##  Funcionamiento

El usuario puede seleccionar diferentes tipos de cofres desde la consola.

Cada cofre utiliza su implementación del método `CrearRecompensa()` para generar una recompensa diferente. La recompensa obtenida se muestra en pantalla y se agrega al inventario del jugador.

Esto permite demostrar el funcionamiento del patrón **Factory Method**, ya que cada creador concreto es responsable de decidir qué objeto concreto crear.

##  Estructura del proyecto

```text
proyecto/
├── specs/
│   └── specification.md
├── src/
│   ├── CofreCreator.cs
│   ├── CofresConcretos.cs
│   ├── IRecompensa.cs
│   ├── RecompensasConcretas.cs
│   ├── Program.cs
│   └── SistemaCofres.csproj
└── README.md
```
