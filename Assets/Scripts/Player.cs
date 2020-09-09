using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{
        public string Name { get; protected set; }
        public string _name;
        public List<Critter> collection = new List<Critter>();

        public List<GameObject> battleCritter;

        public Stack<GameObject> critterStack = new Stack<GameObject>();

        void Awake()
        {
            Name = _name;
            foreach (GameObject critter in battleCritter)
            {
                critterStack.Push(critter);                
            }
        }

        // public Player(List<Critter> critters, string _name)
        // {
        //     Name = _name;
        //     battleCritter = critters;
        //     collection = critters;
        // }
    }

