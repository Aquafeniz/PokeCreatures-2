using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, PlayerTurn, EnemyTurn, Won, Lost }

public class BattleSystem : MonoBehaviour, ISubject
{
    [SerializeField]public List<IObserver> _observers = new List<IObserver>();

    [SerializeField] Player player;
    [SerializeField] Player enemy;

    [SerializeField] Transform playerBattlePos;
    [SerializeField] Transform enemyBattlePos;

    Critter playerCritter;
    Stack <GameObject> playerCritterStack; // Referencia a la pila de Critters de los jugadores únicamente para fácil acceso.
    Critter enemyCritter;
    Stack <GameObject> enemyCritterStack;

    public BattleState state;

    void Start()
    {
        playerCritterStack = player.critterStack;
        enemyCritterStack = enemy.critterStack;
        RegisterObserver(HUDSystem.Instance.GetComponent<IObserver>());
        SetupBattle();
        
    }

    public void RegisterObserver(IObserver observer) //Se suscriben los observadores interesados
    {
        _observers.Add(observer);
    }

    void SetupBattle()
    {
        state = BattleState.Start;
        playerCritter = playerCritterStack.Pop().GetComponent<Critter>();
        enemyCritter = enemyCritterStack.Pop().GetComponent<Critter>();        

        playerCritter = Instantiate(playerCritter, playerBattlePos);
        enemyCritter = Instantiate(enemyCritter, enemyBattlePos);

        Notify("Ha comenzado la batalla", NotificationType.UpdateStatus);
        Invoke("CritterStatsChange", 0.5f);
        if (playerCritter.RealSpeed > enemyCritter.RealSpeed) PlayerTurn();
        else StartCoroutine("EnemyTurn");

        state = BattleState.PlayerTurn;
        //PlayerTurn();
    }

    void PlayerTurn()
    {
        Notify(player, NotificationType.PlayerTurn);
        state = BattleState.PlayerTurn;
    }

    IEnumerator EnemyTurn()
    {
        Notify(enemy, NotificationType.EnemyTurn);
        state = BattleState.EnemyTurn;
        yield return new WaitForSeconds(2.5f);
        EnemyAction();
    }

    void PlayerAction(int i)
    {        
        Skill currentSkill = playerCritter.moveSet[i];

        if (currentSkill as SupportSkill)
        {
            if (currentSkill.suportSkillUsed < 3)
            {
                playerCritter.moveSet[i].UseSkill(enemyCritter);
                StatusChange(playerCritter, currentSkill);
                
            }
            if (currentSkill.suportSkillUsed == 3 && i == 1)
            {
                Notify(currentSkill, i, NotificationType.UpdateHUD);
            } 
            else if (currentSkill.suportSkillUsed == 3 && i == 2)
            {
                Notify(currentSkill, i, NotificationType.UpdateHUD);
            }
        }        
        else 
        {
            currentSkill.UseSkill(enemyCritter);
            StatusChange(playerCritter, currentSkill);
        }

        if (enemyCritter.isDefeated)
        {
            Critter oldCritter = enemyCritter;
            if (enemyCritterStack.Count > 0)
            {
                Destroy(enemyCritter.gameObject);
                NextEnemyCritter();       
                Notify(oldCritter, enemyCritter, NotificationType.CritterDeath);    
                StartCoroutine(EnemyTurn());
            }
            else
            {
                BattleWon();
            }        
        }
        else 
        {
            StartCoroutine(EnemyTurn());
        }
        CritterStatsChange(); 
    }

    void EnemyAction()
    {
        int i = Random.Range(0, 3);
        Skill currentSkill = enemyCritter.moveSet[i];        

        if (currentSkill as SupportSkill)
        {
            if (currentSkill.suportSkillUsed < 3)
            {
                enemyCritter.moveSet[i].UseSkill(playerCritter);
                StatusChange(enemyCritter, currentSkill);
            }
            else EnemyAction();
        }        
        else 
        {
            currentSkill.UseSkill(playerCritter);
            StatusChange(enemyCritter, currentSkill);
        }

        if (playerCritter.isDefeated)
        {
            Critter oldCritter = playerCritter;
            enemy.collection.Add(playerCritter);
            player.collection.Remove(playerCritter);
            if (playerCritterStack.Count > 0)
            {

                Destroy(playerCritter.gameObject);
                NextPlayerCritter();
                Notify(oldCritter, playerCritter, NotificationType.CritterDeath);         
                
                Invoke("PlayerTurn", 2f);
            }
            else
            {
                BattleLost();
            }        
        }
        else 
        {
            Invoke("PlayerTurn", 2f);
        }
        CritterStatsChange();

    }

    public void OnButtonClick(int i)
    {
        PlayerAction(i);
    }

    void NextEnemyCritter()
    {
        enemyCritter = enemyCritterStack.Pop().GetComponent<Critter>();
        enemyCritter = Instantiate(enemyCritter, enemyBattlePos);
        Invoke("CritterStatsChange", 0.5f);
    }

    void NextPlayerCritter()
    {
        playerCritter = playerCritterStack.Pop().GetComponent<Critter>();
        playerCritter = Instantiate(playerCritter, playerBattlePos);
        Notify(player, NotificationType.UpdateHUD);
        Invoke("CritterStatsChange", 0.5f);
    }

    void BattleWon()
    {
        state = BattleState.Won;
        Debug.Log(player.Name + " Ha ganado la batalla!");
    }

    void BattleLost()
    {
        state = BattleState.Lost;
        Debug.Log(player.Name + " ha sido derrotado!");
    }    

    public void Notify(object value, NotificationType notificationType)
    {
        foreach(IObserver obs in _observers)
        {
            obs.OnNotify(value, notificationType);
        }
    }

    public void Notify(object value, object value2, NotificationType notificationType)
    {
        foreach(IObserver obs in _observers)
        {
            obs.OnNotify(value, value2, notificationType);
        }
    }

    void StatusChange(Critter critter, Skill skill)
    {
        Notify(critter, skill, NotificationType.UpdateStatus);
    }

    void CritterStatsChange()
    {
        Notify(playerCritter, NotificationType.UpdatePlayerStats);
        Notify(enemyCritter, NotificationType.UpdateEnemyStats);
    }
}

