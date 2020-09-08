using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class Program
    {
        static void Main(string[] args)
        {

            List<Critter> battleGroup1 = new List<Critter>();
            List<Critter> battleGroup2 = new List<Critter>();

            //Creación de MoveSets
            List<Skill> moveSet1 = new List<Skill>(); //Ataque debil, buff de ataque, buff defensa | Fuego
            List<Skill> moveSet2 = new List<Skill>(); //Ataque fuerte, debuff speed, buff ataque | Wind
            List<Skill> moveSet3 = new List<Skill>(); //Ataque normal, buff ataque, buff defensa | Wind
            List<Skill> moveSet4 = new List<Skill>(); //Ataque debil, dos buff de ataque normal | Earth

            MoveSets();
            CreateCritters();

            Player player1 = new Player(battleGroup1, "Restrepito");
            Player player2 = new Player(battleGroup2, "Danielito");

            //Battle battle = new Battle(player1, player2);
            //battle.TestFight4();

            void MoveSets()
            {
                //Moveset1
                moveSet1.Add(new AttackSkill("Fuego débil", EAffinity.Fire, 3f));
                moveSet1.Add(new SupportSkill("Fuego infernal", EAffinity.Fire, ESupportSkill.AtkUp));
                moveSet1.Add(new SupportSkill("Defensa ignea", EAffinity.Fire, ESupportSkill.DefUp));

                //Moveset2
                moveSet2.Add(new AttackSkill("Oscuridad fuerte", EAffinity.Wind, 100f)); //1. Un attack skill no puede crearse con un poder de 0 o fuera de rango.
                moveSet2.Add(new SupportSkill("Viscosidad", EAffinity.Wind, ESupportSkill.SpdDown)); //2. Un suport skill no puede crearse con más de 0 de poder (en este caso no recibe parámetro de poder)
                moveSet2.Add(new SupportSkill("Fuerza Umbral", EAffinity.Wind, ESupportSkill.AtkUp));

                //Moveset3
                moveSet3.Add(new AttackSkill("Corriente asesina", EAffinity.Water, 5f));
                moveSet3.Add(new SupportSkill("Tornado", EAffinity.Water, ESupportSkill.AtkUp));
                moveSet3.Add(new SupportSkill("Corriente protectora", EAffinity.Water, ESupportSkill.DefUp));

                //Moveset4
                moveSet4.Add(new AttackSkill("Temblor", EAffinity.Earth, 3.5f));
                moveSet4.Add(new SupportSkill("Placa tectónica", EAffinity.Earth, ESupportSkill.AtkUp));
                moveSet4.Add(new SupportSkill("Muro de piedra", EAffinity.Earth, ESupportSkill.DefUp));
            }            

            void CreateCritters()
            {
                battleGroup1.Add(new Critter("Fueguin", 45f, 20f, 30f, EAffinity.Fire, 125, moveSet1));
                battleGroup1.Add(new Critter("Ventosin", 35f, 50f, 57f, EAffinity.Wind, 100, moveSet2));
                battleGroup2.Add(new Critter("Tierrin", 50f, 70f, 10f, EAffinity.Earth, 150, moveSet4));
                battleGroup2.Add(new Critter("Waterin", 130f, 150f, 37f, EAffinity.Dark, 200, moveSet3));
                new Critter("Restrepin", 0f, 0f, 0f, EAffinity.Earth, 25, moveSet1);
            }
        }
    }
}
