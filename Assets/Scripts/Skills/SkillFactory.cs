using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillFactory 
{
    // SupportSkill supportSkill;
    // AttackSkill attackSkill;
    public static MonoBehaviour CreateSuportSkill()
    {      
        return new SupportSkill("Test", EAffinity.Dark, ESupportSkill.AtkUp);
    }

}
