using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Critter currentCritter;
    Critter playerCritter;

    public int ChooseAction(Critter critter, Critter critter2)
    {
        currentCritter = critter;
        playerCritter = critter2;
        int i = 0;
        SupportSkill skill1 = currentCritter.moveSet[1] as SupportSkill;
        SupportSkill skill2 = currentCritter.moveSet[2] as SupportSkill;

        if (currentCritter.maxHP == currentCritter.currentHP) //Si mi critter está full vida
        {
            
            
        }

        else if ((currentCritter.maxHP - currentCritter.currentHP) > (currentCritter.maxHP / 2)) //Si mi critter tiene más de la mitad de la vida
        {
           
        }

        return i;



    }
}
