# Especificación SDD — Sistema de Cofres y Recompensas

## 1. Problema

En el videojuego que en este caso es una simple aplicación por consolael jugador puede obtener **cofres** al
completar misiones. Existen 4 categorías de cofre —Común, Raro, Épico y
Legendario— y cada una otorga un tipo de recompensa distinto: monedas,
armas, armaduras u objetos especiales respectivamente.

Si el código que gestiona la apertura de cofres decidiera con un
`if / switch` qué clase de recompensa instanciar directamente
(`new RecompensaMonedas()`, `new RecompensaArma()`, etc.), cada vez que el
juego agregue una nueva categoría de cofre (por ejemplo "Cofre Mítico")
habría que modificar esa misma lógica central, mezclando la mecánica de
juego con la creación de objetos concretos.

**¿Quién usa el sistema?** El jugador, a través de un menú donde elige qué
tipo de cofre abrir; el sistema debe entregarle la recompensa correspondiente
sin que el motor del juego conozca los detalles internos de cada recompensa.

**Necesidad al crear objetos:** Generar distintos tipos de recompensa de
forma uniforme según la categoría de cofre, permitiendo agregar nuevas
categorías en el futuro sin modificar el código que ya abre los cofres.

## 2. Requisitos

| ID | Requisito |
|----|-----------|
| R1 | El sistema debe soportar al menos 4 tipos de cofre: Común, Raro, Épico y Legendario. |
| R2 | El código que abre el cofre (`AbrirCofre`) no debe depender directamente de las clases concretas de recompensa (`RecompensaMonedas`, `RecompensaArma`, `RecompensaArmadura`, `RecompensaObjetoEspecial`). |
| R3 | Cada recompensa debe poder mostrarse con su propio nombre, un símbolo distintivo y su puntaje. |
| R4 | Debe ser posible agregar un nuevo tipo de cofre creando nuevas clases, sin modificar `CofreCreator.AbrirCofre`. |
| R5 | El programa debe permitir al usuario elegir interactivamente qué cofre abrir, mostrar el resultado por consola y permitir salir del programa. |
| R6 | El sistema debe llevar un registro de las recompensas obtenidas durante la sesión (inventario) y acumular puntos según la rareza de cada recompensa obtenida. |

## 3. Patrón seleccionado

**Factory Method** (patrón creacional del catálogo GoF).

### Justificación

- El problema central es: *"tengo distintas categorías de cofre que deben
  generar cada una un tipo de recompensa distinto, y quiero que el código
  que abre el cofre no dependa de las clases concretas de recompensa."*
- **Factory Method** resuelve esto definiendo un método de fábrica
  (`CrearRecompensa()`) en la clase abstracta `CofreCreator`, delegando en
  cada subclase (`CofreComunCreator`, `CofreRaroCreator`, `CofreEpicoCreator`,
  `CofreLegendarioCreator`) la decisión de qué recompensa concreta generar.
- Ventajas frente a instanciar la recompensa directamente con `new` dentro
  de un único método:
  1. **Principio Abierto/Cerrado**: agregar un "Cofre Mítico" solo requiere
     una nueva recompensa + un nuevo creador; `AbrirCofre()` no se toca (R4).
  2. **Desacopla creación de uso**: `AbrirCofre()` solo conoce `IRecompensa`,
     nunca las clases concretas (R2).
  3. **Encapsula la variación**: si una recompensa cambia sus posibles
     variantes (por ejemplo, se agregan más armas), el cambio queda
     contenido en su propia clase.
- Se descartó **Abstract Factory** porque solo hay un producto por crear
  (la recompensa), no familias de productos relacionados. Se descartó
  **Builder** porque las recompensas no se construyen paso a paso con
  configuración variable. **Singleton** y **Prototype** no aplican: no se
  necesita instancia única ni clonar objetos existentes.

## 4. Diseño propuesto
IRecompensa (interfaz - Producto)
├── RecompensaMonedas
├── RecompensaArma
├── RecompensaArmadura
└── RecompensaObjetoEspecial

CofreCreator (clase abstracta - Creador)
│   + AbrirCofre(): IRecompensa       <- logica comun, devuelve la recompensa
│   # CrearRecompensa(): IRecompensa  <- Factory Method (abstracto)
├── CofreComunCreator       -> crea RecompensaMonedas
├── CofreRaroCreator        -> crea RecompensaArma
├── CofreEpicoCreator       -> crea RecompensaArmadura
└── CofreLegendarioCreator  -> crea RecompensaObjetoEspecial

Program.cs (cliente)

	•	El jugador elige la categoria de cofre por menu.
	•	El resto del flujo (mostrar cofre, obtener recompensa) usa solo
CofreCreator / IRecompensa, nunca las clases concretas.
	•	El inventario y el puntaje (R6) son estado del “juego”, manejado en
Program.cs a partir del valor que devuelve AbrirCofre(); no forman
parte de la estructura del patron Factory Method.

## 5. Criterios de aceptación

- [ ] **CA1**: Al seleccionar cada una de las 4 opciones del menú, el
  programa muestra el mensaje "Has abierto un [Cofre X]!" seguido de una
  recompensa acorde a esa categoría. *(cumple R1, R5)*
- [ ] **CA2**: `CofreCreator.cs` no contiene ninguna referencia directa a
  `RecompensaMonedas`, `RecompensaArma`, `RecompensaArmadura` ni
  `RecompensaObjetoEspecial`. *(cumple R2)*
- [ ] **CA3**: Cada clase de recompensa implementa `IRecompensa` y responde
  correctamente a `Nombre`, `Valor` y `Mostrar()`. *(cumple R3)*
- [ ] **CA4**: Es posible agregar una quinta categoría (ej. "Cofre Mítico")
  creando solo una nueva recompensa y un nuevo creador, sin modificar
  `CofreCreator.cs`. *(cumple R4)*
- [ ] **CA5**: El proyecto compila y ejecuta en Visual Studio con F5 sin
  errores, y la opción "5. Salir" termina el programa correctamente.
  *(cumple R5)*
- [ ] **CA6**: Al elegir la opción "6. Ver mi inventario", se listan las
  recompensas obtenidas hasta el momento agrupadas por nombre, y se muestra
  el puntaje total acumulado. Al salir, se muestra un resumen final con el
  total de cofres abiertos y el mejor objeto obtenido. *(cumple R6)*
