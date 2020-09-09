using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

public enum ESupportSkill {AtkUp, DefUp, SpdDown}

    public class SupportSkill : Skill
    {
        public ESupportSkill supportSkill;

        public override float Power {get; set; }

        void Start()
        {
            Power = 0;
        }

        public SupportSkill(string _name, EAffinity _affinity, ESupportSkill _eSupportSkill)
        {
            Name = _name;
            affinity = _affinity;
            supportSkill = _eSupportSkill;
            Power = 0;
        }

        public override void UseSkill(Critter target)
        {
            if (supportSkill == ESupportSkill.AtkUp && myCritter.atkUpSkill < 3)
            {
                float modifier = (myCritter.BaseAttack * 20) / 100;
                myCritter.RealAttack += modifier;
                suportSkillUsed++;
                myCritter.atkUpSkill++;
                Console.WriteLine("El ataque de {0} aumentó en {1}. (Base: {2})", myCritter.Name, modifier, myCritter.BaseAttack);

            }
            else if (supportSkill == ESupportSkill.DefUp && myCritter.defUpSkill < 3)
            {
                myCritter.DefenseValue += (myCritter.BaseDefense * 20) / 100;
                suportSkillUsed++;
                myCritter.defUpSkill++;
                myCritter.RealDefense = myCritter.BaseDefense + myCritter.DefenseValue;
                Console.WriteLine("La defensa de {0} aumentó en {1}, y ahora su defensa total es {2}. (Base: {3})", myCritter.Name, myCritter.DefenseValue, myCritter.RealDefense, myCritter.BaseDefense);
            }
            else if (supportSkill == ESupportSkill.SpdDown && target.spdDownSkill < 3)
            {
                target.SpeedValue += (target.BaseSpeed * 30) / 100;
                suportSkillUsed++;
                target.spdDownSkill++;
                target.RealSpeed = target.BaseSpeed - target.SpeedValue;
                Console.WriteLine("La velocidad de {0} se redujo en {1}, y ahora su velocidad total es {2}. (Base: {3})", target.Name, target.SpeedValue, target.RealSpeed, target.BaseSpeed);
            }
            else
            {
                Console.WriteLine("Este Critter no puede acumular más buffs/debuff");
            }            
        }
    }

