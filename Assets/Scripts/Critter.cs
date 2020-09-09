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
    [Range(10,100)] public float BaseAttack;// { get; }
    [Range(10, 100)] public float BaseDefense;// { get; }
    [Range(1, 50)] public float BaseSpeed;// { get; protected set; }
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

    [HideInInspector] public int atkUpSkill;
    [HideInInspector] public int defUpSkill;
    [HideInInspector] public int spdDownSkill;

    [Header("Battle")]
    [Range(10, 500)]public float HP;
    public bool isDefeated = false;

    void Start()
    {
        RealAttack = BaseAttack;
        RealDefense = BaseDefense;
        RealSpeed = BaseSpeed;
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

