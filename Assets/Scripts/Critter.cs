using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
    public enum EAffinity { Fire, Wind, Water, Earth, Dark, Light}

public class Critter : MonoBehaviour
{
    public string Name; // { get; protected set; } //
   
    [Header("Base")]
    public float BaseAttack;// { get; }
    public float BaseDefense;// { get; }
    public float BaseSpeed;// { get; protected set; }
    public EAffinity affinity;

    #region Hide Atribbutes
    public float RealAttack { get; set; }
    public float AffinityMultiplier { get; }
    public float DefenseValue { get; set; }
    public float RealDefense { get; set; }
    public float SpeedValue { get; set; }
    public float RealSpeed { get; set; }

    #endregion

    public List<Skill> moveSet = new List<Skill>(); //

    [Header("UpSkill")]
    public int atkUpSkill;
    public int defUpSkill;
    public int spdDownSkill;

    [Header("Battle")]
    public float HP;
    public bool isDefeated = false;

    public Critter(string _name, float _baseAtk, float _baseDef, float _baseSpd, EAffinity _affinity, float _hp, List<Skill> _skills)
    {
            Name = _name;
            BaseAttack = _baseAtk;
            if (BaseAttack > 100)
            {
                BaseAttack = 100;
                Console.WriteLine("'{0}' no puede tener ataque mayor a 100, por lo tanto el ataque será 100.", _name);
            }
            if (BaseAttack < 10) 
            {
                BaseAttack = 10;
                Console.WriteLine("'{0}' no puede tener ataque menor a 10, por lo tanto el ataque será 10.", _name);
            }
            RealAttack = BaseAttack;
            BaseDefense = _baseDef;
            if (BaseDefense > 100) 
            {
                BaseDefense = 100;
                Console.WriteLine("'{0}' no puede tener defensa mayor a 100, por lo tanto el defensa será 100.", _name);
            }
            if (BaseDefense < 10) 
            {
                BaseDefense = 10;
                Console.WriteLine("'{0}' no puede tener defensa menor a 10, por lo tanto el defensa será 10.", _name);
            }
            RealDefense = BaseDefense;
            BaseSpeed = _baseSpd;
            if (BaseSpeed > 50) 
            {
                BaseSpeed = 50;
                Console.WriteLine("'{0}' no puede tener velocidad mayor a 50, por lo tanto el velocidad será 50.", _name);
            }
            if (BaseSpeed < 1)
            {
                BaseSpeed = 1;
                Console.WriteLine("'{0}' no puede tener velocidad menor a 1, por lo tanto el velocidad será 1.", _name);
            }
            RealSpeed = BaseSpeed;
            affinity = _affinity;
            HP = _hp;
            moveSet = _skills;
            foreach (Skill s in moveSet)
            {
                s.myCritter = this;
            }
    }

    public Critter()
    {
    }

    public void TakeDamage(float damage)
    {
            HP -= damage;
            Console.WriteLine("{0} recibe {1} puntos de daño!", Name, damage);
            if (HP <= 0)
            {
                Console.WriteLine("{0} ha muerto. :(", Name);
                isDefeated = true;
            }
    }


}

