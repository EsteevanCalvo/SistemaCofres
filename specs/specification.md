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