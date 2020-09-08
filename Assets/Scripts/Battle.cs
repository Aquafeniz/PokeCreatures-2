using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    /*public class Battle
    {
        Player player1;
        Player player2;
        Critter critter1;
        Critter critter2;
        int activeTurn;
        int turn = 1;
        int n;
        string nextTurn = "Y";

        public Battle(Player _player1, Player _player2)
        {
            player1 = _player1;
            player2 = _player2;
        }

        public void TestFight()
        {
            Console.WriteLine("\n\n==== BATALLA DE TESTEO 1 ====");
            Console.WriteLine("\nEl daño recibido por un Critter tipo Earth al ser atacado con una skill tipo Fire debe ser 0.");

            critter1 = player1.battleCritter[0];
            critter2 = player2.battleCritter[0];

            if (critter1.RealSpeed > critter2.RealSpeed)
            {
                Console.WriteLine("{0} es más veloz que {1} y por lo tanto comienza primero.\n", critter1.Name, critter2.Name);
                Console.WriteLine("== Turno {0}: {1} == ", turn, critter1.Name);
                Console.WriteLine("\n== STATS == ");
                Console.WriteLine("HP: {0}  |  SPD: {1}", critter1.HP, critter1.RealSpeed);
                Console.WriteLine("ATK: {0}  |  DEF: {1}", critter1.BaseAttack + critter1.SkillPower, critter1.RealDefense);
                Console.WriteLine("\n{0} utiliza {1} contra {2}", critter1.Name, critter1.moveSet[0].Name, critter2.Name);
                critter1.moveSet[0].UseSkill(critter2); // Hard-coded para que podamos verificar el ataque
                Console.WriteLine("\n¿Continuar batalla? Y/N");
                nextTurn = Console.ReadLine().ToUpper();
                activeTurn = 1;
                turn++;
            }
            else
            {
                Console.WriteLine("{0} es más veloz que {1} y por lo tanto comienza primero.\n", critter2.Name, critter1.Name);
                Console.WriteLine("== Turno {0}: {1} == ", turn, critter2.Name);
                Console.WriteLine("\n== STATS == ");
                Console.WriteLine("HP: {0}  |  SPD: {1}", critter2.HP, critter2.RealSpeed);
                Console.WriteLine("ATK: {0}  |  DEF: {1}", critter2.BaseAttack + critter2.SkillPower, critter2.RealDefense);
                Console.WriteLine("\n{0} utiliza {1} contra {2}", critter2.Name, critter2.moveSet[0].Name, critter1.Name);
                critter2.moveSet[0].UseSkill(critter1); // Hard-coded para que podamos verificar el ataque
                Console.WriteLine("\n¿Continuar batalla? Y/N");
                nextTurn = Console.ReadLine().ToUpper();
                activeTurn = 0;
                turn++;

            }

            while ((critter1.isDefeated == false || critter2.isDefeated == false) && nextTurn == "Y")
            {                
                switch (activeTurn)
                {
                    case 0:
                        Console.WriteLine("== Turno {0}: {1} == ", turn, critter1.Name);
                        Console.WriteLine("\n== STATS == ");
                        Console.WriteLine("HP: {0}  |  SPD: {1}", critter1.HP, critter1.RealSpeed);
                        Console.WriteLine("ATK: {0}  |  DEF: {1}\n", critter1.BaseAttack + critter1.SkillPower, critter1.RealDefense);

                        Random random1 = new Random();
                        n = random1.Next(0, critter1.moveSet.Count - 1);
                        if (n == 0) Console.WriteLine("{0} utiliza {1} contra {2}", critter1.Name, critter1.moveSet[n].Name, critter2.Name);
                        else Console.WriteLine("{0} utiliza {1}", critter1.Name, critter1.moveSet[n].Name);
                        critter1.moveSet[n].UseSkill(critter2);
                        if (critter1.isDefeated || critter2.isDefeated) break;
                        Console.WriteLine("\n¿Continuar batalla? Y/N");
                        nextTurn = Console.ReadLine().ToUpper();
                        activeTurn = 1;
                        break;
                    case 1:
                        Console.WriteLine("== Turno {0}: {1} == ", turn, critter2.Name);
                        Console.WriteLine("\n== STATS == ");
                        Console.WriteLine("HP: {0}  |  SPD: {1}", critter2.HP, critter2.RealSpeed);
                        Console.WriteLine("ATK: {0}  |  DEF: {1}\n", critter2.BaseAttack + critter2.SkillPower, critter2.RealDefense);

                        Random random = new Random();
                        n = random.Next(0, critter2.moveSet.Count - 1);
                        if (n == 0) Console.WriteLine("{0} utiliza {1} contra {2}", critter2.Name, critter2.moveSet[n].Name, critter1.Name);
                        else Console.WriteLine("{0} utiliza {1}", critter2.Name, critter2.moveSet[n].Name);
                        critter2.moveSet[n].UseSkill(critter1);
                        if (critter1.isDefeated || critter2.isDefeated) break;
                        Console.WriteLine("\n¿Continuar batalla? Y/N");
                        nextTurn = Console.ReadLine().ToUpper();
                        activeTurn = 0;
                        break;
                    default:
                        break;
                }
                turn++;
                if (critter1.isDefeated || critter2.isDefeated) break;
            }

            if (critter1.isDefeated)
            {
                Console.WriteLine("{0} ha muerto y ha sido enviado a la colección de {1}", critter1.Name, player2.Name);
                player1.battleCritter.Remove(critter1);
                player1.collection.Remove(critter1);
                player2.collection.Add(critter1);

            }
            else if (critter2.isDefeated)
            {
                Console.WriteLine("{0} ha muerto y ha sido enviado a la colección de {1}", critter2.Name, player1.Name);
                player2.battleCritter.Remove(critter2);
                player2.collection.Remove(critter2);
                player1.collection.Add(critter2);
            }

            Console.WriteLine("La nueva colección de {0}, tiene: ", player2.Name);
            foreach (Critter c in player2.collection)
            {
                Console.WriteLine(c.Name);
            }
            Console.WriteLine("La nueva colección de {0}, tiene: ", player1.Name);
            foreach (Critter c in player1.collection)
            {
                Console.WriteLine(c.Name);
            }
        }

        public void TestFight2()
        {
            Console.WriteLine("\n\n==== BATALLA DE TESTEO 2 ====");
            Console.WriteLine("\nEl daño recibido por un Critter tipo Wind al ser atacado con una skill tipo Water debe ser x2 según la fórmula.");

            critter1 = player1.battleCritter[1];
            critter2 = player2.battleCritter[1];

            if (critter1.RealSpeed > critter2.RealSpeed)
            {
                Console.WriteLine("{0} es más veloz que {1} y por lo tanto comienza primero.\n", critter1.Name, critter2.Name);
                Console.WriteLine("== Turno {0}: {1} == ", turn, critter1.Name);
                Console.WriteLine("\n== STATS == ");
                Console.WriteLine("HP: {0}  |  SPD: {1}", critter1.HP, critter1.RealSpeed);
                Console.WriteLine("ATK: {0}  |  DEF: {1}", critter1.BaseAttack + critter1.SkillPower, critter1.RealDefense);
                Console.WriteLine("\n{0} utiliza {1} contra {2}", critter1.Name, critter1.moveSet[0].Name, critter2.Name);
                critter1.moveSet[0].UseSkill(critter2); // Hard-coded para que podamos verificar el ataque
                if (!critter2.isDefeated)
                {
                    Console.WriteLine("\n¿Continuar batalla? Y/N");
                    nextTurn = Console.ReadLine().ToUpper();
                }
                else
                {
                    nextTurn = "N";
                }
                activeTurn = 1;
                turn++;
            }
            else
            {
                Console.WriteLine("{0} es más veloz que {1} y por lo tanto comienza primero.\n", critter2.Name, critter1.Name);
                Console.WriteLine("== Turno {0}: {1} == ", turn, critter2.Name);
                Console.WriteLine("\n== STATS == ");
                Console.WriteLine("HP: {0}  |  SPD: {1}", critter2.HP, critter2.RealSpeed);
                Console.WriteLine("ATK: {0}  |  DEF: {1}", critter2.BaseAttack + critter2.SkillPower, critter2.RealDefense);
                Console.WriteLine("\n{0} utiliza {1} contra {2}", critter2.Name, critter2.moveSet[0].Name, critter1.Name);
                critter2.moveSet[0].UseSkill(critter1); // Hard-coded para que podamos verificar el ataque
                if (!critter1.isDefeated)
                {
                    Console.WriteLine("\n¿Continuar batalla? Y/N");
                    nextTurn = Console.ReadLine().ToUpper();
                }
                else
                {
                    nextTurn = "N";
                }
                activeTurn = 0;
                turn++;

            }

            while ((critter1.isDefeated == false || critter2.isDefeated == false) && nextTurn == "Y")
            {
                switch (activeTurn)
                {
                    case 0:
                        Console.WriteLine("== Turno {0}: {1} == ", turn, critter1.Name);
                        Console.WriteLine("\n== STATS == ");
                        Console.WriteLine("HP: {0}  |  SPD: {1}", critter1.HP, critter1.RealSpeed);
                        Console.WriteLine("ATK: {0}  |  DEF: {1}\n", critter1.BaseAttack + critter1.SkillPower, critter1.RealDefense);

                        Random random1 = new Random();
                        n = random1.Next(0, critter1.moveSet.Count - 1);
                        if (n == 0) Console.WriteLine("{0} utiliza {1} contra {2}", critter1.Name, critter1.moveSet[n].Name, critter2.Name);
                        else Console.WriteLine("{0} utiliza {1}", critter1.Name, critter1.moveSet[n].Name);
                        critter1.moveSet[n].UseSkill(critter2);
                        if (critter1.isDefeated || critter2.isDefeated) break;
                        Console.WriteLine("\n¿Continuar batalla? Y/N");
                        nextTurn = Console.ReadLine().ToUpper();
                        activeTurn = 1;
                        break;
                    case 1:
                        Console.WriteLine("== Turno {0}: {1} == ", turn, critter2.Name);
                        Console.WriteLine("\n== STATS == ");
                        Console.WriteLine("HP: {0}  |  SPD: {1}", critter2.HP, critter2.RealSpeed);
                        Console.WriteLine("ATK: {0}  |  DEF: {1}\n", critter2.BaseAttack + critter2.SkillPower, critter2.RealDefense);

                        Random random = new Random();
                        n = random.Next(0, critter2.moveSet.Count - 1);
                        if (n == 0) Console.WriteLine("{0} utiliza {1} contra {2}", critter2.Name, critter2.moveSet[n].Name, critter1.Name);
                        else Console.WriteLine("{0} utiliza {1}", critter2.Name, critter2.moveSet[n].Name);
                        critter2.moveSet[n].UseSkill(critter1);
                        if (critter1.isDefeated || critter2.isDefeated) break;
                        Console.WriteLine("\n¿Continuar batalla? Y/N");
                        nextTurn = Console.ReadLine().ToUpper();
                        activeTurn = 0;
                        break;
                    default:
                        break;
                }
                turn++;
                if (critter1.isDefeated || critter2.isDefeated) break;
            }

            if (critter1.isDefeated)
            {
                Console.WriteLine("{0} ha muerto y ha sido enviado a la colección de {1}", critter1.Name, player2.Name);
                player1.battleCritter.Remove(critter1);
                player1.collection.Remove(critter1);
                player2.collection.Add(critter1);

            }
            else if (critter2.isDefeated)
            {
                Console.WriteLine("{0} ha muerto y ha sido enviado a la colección de {1}", critter2.Name, player1.Name);
                player2.battleCritter.Remove(critter2);
                player2.collection.Remove(critter2);
                player1.collection.Add(critter2);
            }

            Console.WriteLine("La nueva colección de {0}, tiene: ", player2.Name);
            foreach (Critter c in player2.collection)
            {
                Console.WriteLine(c.Name);
            }
            Console.WriteLine("La nueva colección de {0}, tiene: ", player1.Name);
            foreach (Critter c in player1.collection)
            {
                Console.WriteLine(c.Name);
            }
        }

        public void TestFight3()
        {
            Console.WriteLine("\n\n==== BATALLA DE TESTEO 3 ====");
            Console.WriteLine("\nTras aplicar por una única ocasión el skill SpdDown a un critter, el valor de su velocidad base se mantiene.");

            critter1 = player1.battleCritter[1];
            critter2 = player2.battleCritter[1];

            if (critter1.RealSpeed > critter2.RealSpeed)
            {
                Console.WriteLine("{0} es más veloz que {1} y por lo tanto comienza primero.\n", critter1.Name, critter2.Name);
                Console.WriteLine("== Turno {0}: {1} == ", turn, critter1.Name);
                Console.WriteLine("\n== STATS == ");
                Console.WriteLine("HP: {0}  |  SPD: {1}", critter1.HP, critter1.RealSpeed);
                Console.WriteLine("Base SPD: {0}", critter1.BaseSpeed);
                Console.WriteLine("ATK: {0}  |  DEF: {1}", critter1.BaseAttack + critter1.SkillPower, critter1.RealDefense);
                Console.WriteLine("\n{0} utiliza {1} contra {2}", critter1.Name, critter1.moveSet[0].Name, critter2.Name);
                critter1.moveSet[1].UseSkill(critter2); // Hard-coded para que podamos verificar el ataque
                Console.WriteLine("\n¿Continuar batalla? Y/N");
                nextTurn = Console.ReadLine().ToUpper();
                turn++;
            }

            if (nextTurn == "Y")
            {
                Console.WriteLine("== Turno {0}: {1} == ", turn, critter2.Name);
                Console.WriteLine("\n== STATS == ");
                Console.WriteLine("HP: {0}  |  SPD: {1}", critter2.HP, critter2.RealSpeed);
                Console.WriteLine("Base SPD: {0}", critter2.BaseSpeed);
                Console.WriteLine("ATK: {0}  |  DEF: {1}", critter2.BaseAttack + critter2.SkillPower, critter2.RealDefense);
                Console.WriteLine("\n{0} utiliza {1} contra {2}", critter2.Name, critter2.moveSet[0].Name, critter1.Name);
                critter2.moveSet[1].UseSkill(critter1); // Hard-coded para que podamos verificar el ataque
                turn++; 
            }
            Console.WriteLine("\nFin de la batalla de testeo 3.");
        }

        public void TestFight4()
        {
            Console.WriteLine("\n\n==== BATALLA DE TESTEO 4 ====");
            Console.WriteLine("\nTras aplicar 4 veces el skill DefUp a un Critter, la defensa no puede reflejar un valor diferente al que tuviera tras la tercera aplicación");

            critter1 = player1.battleCritter[1];
            critter2 = player2.battleCritter[1];

            if (critter1.RealSpeed > critter2.RealSpeed)
            {
                Console.WriteLine("{0} es más veloz que {1} y por lo tanto comienza primero.\n", critter1.Name, critter2.Name);
                Console.WriteLine("== Turno {0}: {1} == ", turn, critter1.Name);
                Console.WriteLine("\n== STATS == ");
                Console.WriteLine("HP: {0}  |  SPD: {1}", critter1.HP, critter1.RealSpeed);
                Console.WriteLine("Base SPD: {0}", critter1.BaseSpeed);
                Console.WriteLine("ATK: {0}  |  DEF: {1}", critter1.BaseAttack + critter1.SkillPower, critter1.RealDefense);
                Console.WriteLine("\n{0} pasa su turno.", critter1.Name);
                Console.WriteLine("\n¿Continuar batalla? Y/N");
                nextTurn = Console.ReadLine().ToUpper();
                turn++;
            }

            if (nextTurn == "Y")
            {
                Console.WriteLine("== Turno {0}: {1} == ", turn, critter2.Name);
                Console.WriteLine("\n== STATS == ");
                Console.WriteLine("HP: {0}  |  SPD: {1}", critter2.HP, critter2.RealSpeed);
                Console.WriteLine("Base SPD: {0}", critter2.BaseSpeed);
                Console.WriteLine("ATK: {0}  |  DEF: {1}", critter2.BaseAttack + critter2.SkillPower, critter2.RealDefense);
                Console.WriteLine("\n{0} sobrecarga su poder y utiliza 4 veces {1} contra {2}", critter2.Name, critter2.moveSet[0].Name, critter1.Name);
                critter2.moveSet[2].UseSkill(critter1); // Utiliza el skill DefUp 5 veces
                critter2.moveSet[2].UseSkill(critter1); 
                critter2.moveSet[2].UseSkill(critter1); 
                critter2.moveSet[2].UseSkill(critter1);
                critter2.moveSet[2].UseSkill(critter1);

                turn++;
            }
            Console.WriteLine("\nFin de la batalla de testeo 4.");


        }
    }*/

}
