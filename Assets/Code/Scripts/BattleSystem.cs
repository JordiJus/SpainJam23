using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum CardsTypes { FOOL, MAGICIAN, LOVERS, STRENGTH, DEATH, DEVIL, MOON, SUN }
public enum BattleStates { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour {
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public GameObject combatMenu;
    public GameObject skillsMenu;

    public Transform playerPlace;
    public Transform enemyPlace;
    public BattleStates state;

    Unit playerUnit;
    Unit enemyUnit;

    public TMP_Text dialogue;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    void Start() {
        state = BattleStates.START;
        StartCoroutine(SetupBattle());
    }

    // This initiates the battle

    IEnumerator SetupBattle() {
        GameObject playerGameObj = Instantiate(playerPrefab, playerPlace);
        playerUnit = playerGameObj.GetComponent<Unit>();

        GameObject enemyGameObj = Instantiate(enemyPrefab, enemyPlace);
        enemyUnit =  enemyGameObj.GetComponent<Unit>();

        dialogue.SetText("The Battle Begins!");

        
        playerHUD.UpdateHUD(playerUnit);
        enemyHUD.UpdateHUD(enemyUnit);
        
        yield return new WaitForSeconds(2f);

        state = BattleStates.PLAYERTURN;

        PlayerTurn();


        

    }

    // The logic behind the player's turn
    void PlayerTurn() {
        combatMenu.SetActive(true);
        dialogue.SetText("Choose an action");
    }

    public void OnAttackButton() {
        if(state != BattleStates.PLAYERTURN)
            return;
        
        dialogue.SetText(playerUnit.unitName + " attacks!");
        combatMenu.SetActive(false);
        
        StartCoroutine(PlayerAttack());
    }

    public void OnDefendButton() {
        if(state != BattleStates.PLAYERTURN)
            return;
        
        dialogue.SetText(playerUnit.unitName + " defends!");
        combatMenu.SetActive(false);

        StartCoroutine(PlayerDefend());
    }

    public void OnSkillsButton() {
        if(state != BattleStates.PLAYERTURN)
            return;
        
        if(skillsMenu.activeSelf == true) {
            skillsMenu.SetActive(false);
        } else {
            skillsMenu.SetActive(true);
        }
        
    }

    public void OnMagicianButton() {
        if(state != BattleStates.PLAYERTURN)
            return;
        
        if(skillsMenu.activeSelf == true) {
            dialogue.SetText(playerUnit.unitName + " defends!");
            combatMenu.SetActive(false);
            skillsMenu.SetActive(false);
            StartCoroutine(PlayerUseMagician());
        }
    }

    IEnumerator PlayerAttack() {   
        bool isDead = enemyUnit.takeDamage(playerUnit.damage);
        enemyHUD.setHP(enemyUnit.currentHP);
        playerUnit.recoverMana(1);

        yield return new WaitForSeconds(1f);

        if(isDead) {
            state = BattleStates.WON;
            EndBattle();
        } else {
            state = BattleStates.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerDefend() {   
        playerUnit.unitDefend();
        playerUnit.recoverMana(2);



        yield return new WaitForSeconds(1f);

        state = BattleStates.ENEMYTURN;
        StartCoroutine(EnemyTurn());
        
    }

    IEnumerator PlayerUseMagician() {

        yield return new WaitForSeconds(1f);
    }

    // Enemy's turn
    IEnumerator EnemyTurn() {
        dialogue.SetText("Enemy attacks!");

        yield return new WaitForSeconds(1);

        bool isDead = playerUnit.takeDamage(enemyUnit.damage);

        playerHUD.setHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1);

        if (isDead) {
            state = BattleStates.LOST;
            EndBattle();
        } else {
            state = BattleStates.PLAYERTURN;
            PlayerTurn();
        }

    }

    void EndBattle() {
        if (state == BattleStates.WON) {
            dialogue.SetText("You won the battle!");
        } else if (state == BattleStates.LOST) {
            dialogue.SetText("You were defeated!");
        }
    }
}
