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

    [SerializeField] Critter playerCritter;
    public Stack <GameObject> playerCritterStack; // Referencia a la pila de Critters de los jugadores únicamente para fácil acceso.
    [SerializeField] Critter enemyCritter;
    public Stack <GameObject> enemyCritterStack;

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

        if (playerCritter.BaseSpeed > enemyCritter.BaseSpeed) PlayerTurn();         
        else StartCoroutine("EnemyTurn");

        playerCritter.transform.position = playerBattlePos.position;
        enemyCritter.transform.position = enemyBattlePos.position;

        Notify("Ha comenzado la batalla", NotificationType.UpdateStatus);
        Notify(this, NotificationType.UpdateHUD);
        Invoke("CritterStatsChange", 0.5f);


        //state = BattleState.PlayerTurn;
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
            enemy.collection.Remove(enemyCritter.gameObject);
            player.collection.Add(enemyCritter.gameObject);
            enemyCritter.transform.position = player.collectionPos.position;
            enemyCritter.transform.parent = player.collectionPos;

            if (enemyCritterStack.Count > 0)
            {                
                NextEnemyCritter();       
                Notify(oldCritter, enemyCritter, NotificationType.CritterDeath);
                Notify(this, NotificationType.UpdateHUD);
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
        if (state != BattleState.Won) CritterStatsChange(); 
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
            player.collection.Remove(playerCritter.gameObject);
            enemy.collection.Add(playerCritter.gameObject);
            playerCritter.transform.position = enemy.collectionPos.position;
            playerCritter.transform.parent = enemy.collectionPos;

            if (playerCritterStack.Count > 0)
            {
                NextPlayerCritter();
                Notify(oldCritter, playerCritter, NotificationType.CritterDeath);     
                Notify(this, NotificationType.UpdateHUD);                
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
        enemyCritter.transform.position = enemyBattlePos.position;
        Invoke("CritterStatsChange", 0.5f);
    }

    void NextPlayerCritter()
    {
        playerCritter = playerCritterStack.Pop().GetComponent<Critter>();
        playerCritter.transform.position = playerBattlePos.position;
        Notify(player, NotificationType.UpdateHUD);
        Invoke("CritterStatsChange", 0.5f);
    }

    void BattleWon()
    {
        state = BattleState.Won;
        Notify(player, NotificationType.BattleWon);
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

