using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class AttackSkill : Skill
    {
        public override float Power {get; set; }
        [Range(1,10)] public float power;
        private float multiplier;

        void Start()
        {
            Power = power;
        }

        public AttackSkill(string _name, EAffinity _affinity, float _power)
        {
            Name = _name;
            affinity = _affinity;
            Power = _power;
            if (_power < 1)
            {
                Power = 1;
                Console.WriteLine("El skill de ataque '{0}' no puede tener poder menor a 1, por lo tanto el poder será 1.", _name);
            }
            if (_power > 10)
            {
                Power = 10;
                Console.WriteLine("El skill de ataque '{0}' no puede tener poder mayor a 10, por lo tanto el poder será 10.", _name);
            }
        }


        public override void UseSkill(Critter target)
        {
            AffinityCalculator(target);
            DamageValue = (myCritter.RealAttack + Power) * multiplier;
            target.TakeDamage(DamageValue);
        }

        private void AffinityCalculator(Critter target) 
        {
            switch (affinity)  // Afinidad de mi ataque
            {
                case EAffinity.Dark:
                    switch (target.affinity) //Afinidad de mi enemigo
                    {
                        case EAffinity.Dark:
                            multiplier = 0.5f;
                            break;

                        case EAffinity.Light:
                            multiplier = 2f;
                            break;
                        default:
                            multiplier = 1f;
                            break;
                    }
                    break;
                case EAffinity.Light:
                    switch (target.affinity)
                    {
                        case EAffinity.Dark:
                            multiplier = 2f;
                            break;

                        case EAffinity.Light:
                            multiplier = 0.5f;
                            break;
                        default:
                            multiplier = 1f;
                            break;
                    }
                    break;
                case EAffinity.Fire:
                    switch (target.affinity)
                    {
                        case EAffinity.Fire:
                            multiplier = 0.5f;
                            break;

                        case EAffinity.Water:
                            multiplier = 2f;
                            break;
                        case EAffinity.Earth:
                            multiplier = 0f;
                            break;
                        default:
                            multiplier = 1f;
                            break;
                    }
                    break;
                case EAffinity.Water:
                    switch (target.affinity)
                    {
                        case EAffinity.Fire:
                            multiplier = 0.5f;
                            break;
                        case EAffinity.Water:
                            multiplier = 0.5f;
                            break;
                        case EAffinity.Wind:
                            multiplier = 2f;
                            break;
                        default:
                            multiplier = 1f;
                            break;
                    }
                    break;
                case EAffinity.Wind:
                    switch (target.affinity)
                    {
                        case EAffinity.Water:
                            multiplier = 0.5f;
                            break;

                        case EAffinity.Wind:
                            multiplier = 0.5f;
                            break;
                        case EAffinity.Earth:
                            multiplier = 0.5f;
                            break;
                        default:
                            multiplier = 1f;
                            break;
                    }
                    break;
                case EAffinity.Earth:
                    switch (target.affinity)
                    {
                        case EAffinity.Wind:
                            multiplier = 2f;
                            break;

                        case EAffinity.Earth:
                            multiplier = 0.5f;
                            break;
                        default:
                            multiplier = 1f;
                            break;
                    }
                    break;
            }
        }
    }

