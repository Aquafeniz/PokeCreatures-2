using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{
        public Transform collectionPos;
        public string Name { get; protected set; }
        public string _name;
        public List<GameObject> collection = new List<GameObject>();

        public List<GameObject> battleCritter;

        public Stack<GameObject> critterStack = new Stack<GameObject>();

        void Awake()
        {
            Name = _name;
            foreach (GameObject critter in battleCritter)
            {
                GameObject instance = Instantiate(critter, collectionPos);
                critterStack.Push(instance);
                collection.Add(instance);
            }
        }
    }

