using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, PlayerTurn, EnemyTurn, Won, Lost }

public class BattleSystem : MonoBehaviour
{
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
        SetupBattle();
    }

    void SetupBattle()
    {
        Debug.Log("Iniciando batalla");
        state = BattleState.Start;
        playerCritter = playerCritterStack.Pop().GetComponent<Critter>();
        enemyCritter = enemyCritterStack.Pop().GetComponent<Critter>();

        playerCritter = Instantiate(playerCritter, playerBattlePos);
        enemyCritter = Instantiate(enemyCritter, enemyBattlePos);

        //Actualizar HUD

        state = BattleState.PlayerTurn;
        PlayerTurn();

    }

    void PlayerTurn()
    {
        BattleHUD.Instance.EnableButtons();
        Debug.Log("Turno del jugador");
        state = BattleState.PlayerTurn;


        //Actualizar HUD
        //Notificar Observer de que es el turno del player
    }

    IEnumerator EnemyTurn()
    {
        BattleHUD.Instance.DisableButtons();
        state = BattleState.EnemyTurn;
        yield return new WaitForSeconds(2f);
        EnemyAction();
        Debug.Log("Turno del enemigo");

    }

    void PlayerAction(int i)
    {
        Skill currentSkill = playerCritter.moveSet[i];
        //Debug.Log(playerCritter.RealAttack);

        if (currentSkill as SupportSkill)
        {
            if (currentSkill.suportSkillUsed < 3)
            {
                Debug.LogWarning(playerCritter.Name + " utiliza " + playerCritter.moveSet[i].Name);
                playerCritter.moveSet[i].UseSkill(enemyCritter);
                //Notificar observer para que bloquee el boton
            }
            else Debug.LogError(playerCritter.Name + " no puede utilizar más esa habilidad.");
        }        
        else 
        {
            Debug.Log(playerCritter.Name + " utiliza " + playerCritter.moveSet[i].Name);
            currentSkill.UseSkill(enemyCritter);
            Debug.Log($"{playerCritter.Name} hace {currentSkill.DamageValue} de daño");
        }

        if (enemyCritter.isDefeated)
        {
            if (enemyCritterStack.Count > 0)
            {
                Destroy(enemyCritter.gameObject);
                NextEnemyCritter();                
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
    }

    void EnemyAction()
    {
        int i = Random.Range(0, 3);
        Skill currentSkill = enemyCritter.moveSet[i];
        

        if (currentSkill as SupportSkill)
        {
            if (currentSkill.suportSkillUsed < 3)
            {
                Debug.LogWarning(enemyCritter.Name + " utiliza " + enemyCritter.moveSet[i].Name);
                enemyCritter.moveSet[i].UseSkill(playerCritter);
            }
            else Debug.LogError(enemyCritter.Name + " no puede utilizar más esa habilidad");
        }        
        else 
        {
            Debug.Log(enemyCritter.Name + " utiliza " + currentSkill.Name);
            currentSkill.UseSkill(playerCritter);
            Debug.Log($"{enemyCritter.Name} hace {currentSkill.DamageValue} de daño");
        }

        if (playerCritter.isDefeated)
        {
            if (playerCritterStack.Count > 0)
            {
                Destroy(playerCritter.gameObject);
                NextPlayerCritter();
                PlayerTurn();
            }
            else
            {
                BattleLost();
            }        
        }
        else 
        {
            PlayerTurn();
        }

    }

    public void OnButtonClick(int i)
    {
        PlayerAction(i);
    }

    void NextEnemyCritter()
    {
        enemyCritter = enemyCritterStack.Pop().GetComponent<Critter>();
        enemyCritter = Instantiate(enemyCritter, enemyBattlePos);
    }

    void NextPlayerCritter()
    {
        playerCritter = playerCritterStack.Pop().GetComponent<Critter>();
        playerCritter = Instantiate(playerCritter, playerBattlePos);

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

    // void Update()
    // {
    //     Debug.Log("Critter actual: " + playerCritter.Name);
    //     Debug.Log("Critters restantes: " + player.GetComponent<Player>().critterStack.Count);
    // }
}

