using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public enum ESkillType { AttackSkill, SupportSkill}

    public abstract class Skill
    {
        public Critter myCritter;
        public EAffinity affinity;
        //public ESkillType skillType;
        public string Name { get; protected set; }
        public float Power { get; protected set; }
        public float DamageValue { get; protected set; }


        public Skill(string _name, EAffinity _affinity, float _power, Critter critter)
        {
            Name = _name;
            affinity = _affinity;
            Power = _power;
            myCritter = critter;
        }

        public Skill()
        {
        }

        public virtual void UseSkill(Critter target)
        {
        }
    }
}
