# PokeCreatures-2
 
>>Observer:
Se ha creado un observer para actualizar la interfaz con diferentes metodos, haciendo que solo se actualice la interfaz unicamente cuando sea necesaria.
esta interfaz actualiza segun una batalla, unicamente hay eventos en determinados puntos de la batalla, como cuando se ejecuta una habilidad, pierde un enemigo o pierde tu critter.

>>Singleton:
Se uso el Singleton para llamar varias instancias de objetos que eran necesarias. 
  Ejemplo: HUDSystem, debido a que solo hay un HUDSystem se eligio un singleton para poder refereciarlo mas facil.

>>Decorator:
Se penso en usar para las skills. al final no se termino usando debido a que era implementarle una interfaz a las skills y cambiar en su parte de diseño que no vimos necesaria para este ejercicio, debido a que se tenia la funcionalidad base de las skills con clases heredadas sin necesidad de usar el decorator
          
      
se decidio crear un arbrito dentro de la batalla, que es el que autoriza los turnos de los jugadores esto con una flecha roja que se posiciona encima del critter que tenga el turno actual. indica que critatura enemiga y la aliada y la encargada de notificar a la interfaz que se cambie, ademas de desactivar la interfaz cuando es el turno del enemigo para que el jugador no pueda interactuar con ella.
Los Supports skills que son usados se muestran arriba en la barra de estado con el nombre y lo que hacen. ademas de que se ve un cambio inmediato en los stats que reflajan no solo la vida que tiene el critter que se vio afectado, ademas si no cuantos critters quedan por salir, los stats base y una referencia del nombre
la interfaz no tiene referencias de los Critters, debido a que se mandan con eventos de un decorator, haciendo todo mucho mas facil.


Ademas el arbitro se encarga de determinar quien empieza primero segun la velocidad que tenga el critter que va a salir, haciendo una comparacion de los 2 primeros critters para determinar quien empieza a jugar.

deben agregarse antes de iniciar el juego los critters (Prefabs) con el inspector a estos (al Prefab) se le pueden cambiar los atributos, esta normalizado para evitar que se pueda salir del rango requerido en el ejercicio. 

