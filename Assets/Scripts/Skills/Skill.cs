using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;



public enum ESkillType { AttackSkill, SupportSkill}

    public abstract class Skill : MonoBehaviour
    {
        [HideInInspector] public Critter myCritter;
        public EAffinity affinity;
        public int suportSkillUsed;

        //public ESkillType skillType;

        public string Name;
        public abstract float Power {get; set;}
        public float DamageValue { get; protected set; }

        void Start()
        {
            myCritter = GetComponentInParent<Critter>();        
        }

        public abstract void UseSkill(Critter target);
    }

