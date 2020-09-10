using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDSystem : MonoBehaviour, IObserver
{
    public static HUDSystem Instance {get; set;}

    [Header("Player HUD")]
    [SerializeField] Text playerCritterName;
    [SerializeField] Text playerCritterHealth;
    [SerializeField] Text playerCritterATK;
    [SerializeField] Text playerCritterDEF;
    [SerializeField] Text playerCritterSPD;
    [SerializeField] Image playerTurn;
    [Header("Enemy HUD")]
    [SerializeField] Text enemyCritterName;
    [SerializeField] Text enemyCritterHealth;
    [SerializeField] Text enemyCritterATK;
    [SerializeField] Text enemyCritterDEF;
    [SerializeField] Text enemyCritterSPD;
    [SerializeField] Image enemyTurn;


    [Header("Status")]
    [SerializeField] Text statusText;

    [Header("PlayerButtons")]
    [SerializeField] GameObject buttons;
    [SerializeField] Button button1;
    [SerializeField] Button button2;
    [SerializeField] Button button3;


    void Awake()
    {
        if (Instance == null) Instance = this;
    }


    void Start()
    {        
        statusText.text = "";
        
    }

    public void OnNotify(object value, NotificationType notificationType)
    {    
        if (value is Critter && notificationType == NotificationType.UpdatePlayerStats)
        {
            Critter critter = value as Critter;
            playerCritterName.text = critter.Name;
            playerCritterHealth.text = "HP: " + critter.currentHP.ToString();
            playerCritterATK.text = "ATK: " + critter.RealAttack.ToString();
            playerCritterDEF.text = "DEF: " + critter.RealDefense.ToString();
            playerCritterSPD.text = "SPD: " + critter.RealSpeed.ToString();
        }
        else if (value is Critter && notificationType == NotificationType.UpdateEnemyStats)
        {
            Critter critter = value as Critter;
            enemyCritterName.text = critter.Name;
            enemyCritterHealth.text = "HP: "+critter.currentHP.ToString();
            enemyCritterATK.text = "ATK: " + critter.RealAttack.ToString();
            enemyCritterDEF.text = "DEF: " + critter.RealDefense.ToString();
            enemyCritterSPD.text = "SPD: " + critter.RealSpeed.ToString();
        }

        if (value is Player && notificationType == NotificationType.EnemyTurn)
        {
            DisableButtons();
        }
        else if (value is Player && notificationType == NotificationType.PlayerTurn)
        {
            EnableButtons();
        }

        if (value is Player && notificationType == NotificationType.UpdateHUD)
        {
            EnableButtons();
            button1.interactable = true;
            button2.interactable = true;
            button3.interactable = true;
        }

        if (value is string && notificationType == NotificationType.UpdateStatus)
        {
            statusText.text = value.ToString();
        }
    }

    public void OnNotify(object value, object value2, NotificationType notificationType)
    {
        switch(notificationType)
        {
            case NotificationType.UpdateStatus:
            if (value is Critter && value2 is SupportSkill)
            {
                Critter critter = value as Critter;
                Skill skill = value2 as Skill;
                statusText.text = $"{critter.Name} utiliza {skill.Name}!";
            }
            else if (value is Critter && value2 is AttackSkill)
            {
                Critter critter = value as Critter;
                Skill skill = value2 as Skill;
                statusText.text = $"{critter.Name} utiliza {skill.Name} y hace {skill.DamageValue} de daño!";
            }
            break;
            case NotificationType.UpdateHUD:
            if (value is Skill && value2 is int)
            {
                int i = (int)value2;
                Skill skill = value as Skill;
                if (i == 1)
                {
                    button2.interactable = false;
                }
                else if (i == 2)
                {
                    button3.interactable = false;
                }
            }
            break;
            case NotificationType.CritterDeath:
            if (value is Critter && value2 is Critter)
            {
                Critter critter = value as Critter;
                Critter critter2 = value2 as Critter;
                statusText.text = $"{critter.Name} fue derrotado y {critter2.Name} toma su lugar en la batalla.";
            }

            break;
        }
        
        
        // if ((value is Critter && value2 is SupportSkill) && notificationType == NotificationType.UpdateStatus)
        // {
        //     Critter critter = value as Critter;
        //     Skill skill = value2 as Skill;

        //     statusText.text = $"{critter.Name} utiliza {skill.Name}!";
        // }
        // else if ((value is Critter && value2 is AttackSkill) && notificationType == NotificationType.UpdateStatus)
        // {
        //     Critter critter = value as Critter;
        //     Skill skill = value2 as Skill;
        //     statusText.text = $"{critter.Name} utiliza {skill.Name} y hace {skill.DamageValue} de daño!";
        // }

        // if ((value is Skill && value2 is int) && notificationType == NotificationType.UpdateHUD)
        // {
        //     int i = (int)value2;
        //     Skill skill = value as Skill;
        //     if (i == 1)
        //     {
        //         button2.interactable = false;
        //     }
        //     else if (i == 2)
        //     {
        //         button3.interactable = false;
        //     }
        // }
    }

    public void DisableButtons()
    {
        buttons.SetActive(false);
        playerTurn.enabled = false;
        enemyTurn.enabled = true;
    }

    public void EnableButtons()
    {
        buttons.SetActive(true);
        playerTurn.enabled = true;
        enemyTurn.enabled = false;
    }


}
