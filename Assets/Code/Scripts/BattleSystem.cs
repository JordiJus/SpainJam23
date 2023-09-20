using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CardsTypes { FOOL, MAGICIAN, LOVERS, STRENGTH, DEATH, DEVIL, MOON, SUN }
public enum BattleStates { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour {
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public GameObject buttonPrefab;

    public GameObject combatMenu;
    public GameObject skillsMenu;

    public Transform playerPlace;
    public Transform enemyPlace;

    public Transform buttonPlace1;
    public Transform buttonPlace2;
    public Transform buttonPlace3;

    public BattleStates state;

    Unit playerUnit;
    Unit enemyUnit;

    Cards button1Cards;
    Cards button2Cards;
    Cards button3Cards;

    TMP_Text button1Text;
    TMP_Text button2Text;
    TMP_Text button3Text;

    public CardsSelected cardsForPlayer;

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

        playerUnit.PlayerCards = cardsForPlayer;

        // This has to be eliminated!!
        // playerUnit.PlayerCards.card1 = CardsTypes.MAGICIAN;
        // playerUnit.PlayerCards.card2 = CardsTypes.SUN;

        GameObject enemyGameObj = Instantiate(enemyPrefab, enemyPlace);
        enemyUnit =  enemyGameObj.GetComponent<Unit>();

        dialogue.SetText("The Battle Begins!");

        
        playerHUD.UpdateHUD(playerUnit);
        enemyHUD.UpdateHUD(enemyUnit);


        GameObject button1Obj = Instantiate(buttonPrefab, buttonPlace1);
        UnityEngine.UI.Button button1 = button1Obj.GetComponent<UnityEngine.UI.Button>();
        button1Cards =  button1Obj.GetComponent<Cards>();
        button1Cards.initiateButton(playerUnit, enemyUnit);

        button1Text = button1Obj.GetComponentInChildren<TMP_Text>();
        button1Text.SetText(Enum.GetName(typeof(CardsTypes), playerUnit.PlayerCards.card1));

        

        GameObject button2Obj = Instantiate(buttonPrefab, buttonPlace2);
        UnityEngine.UI.Button button2 = button2Obj.GetComponent<UnityEngine.UI.Button>();
        button2Cards =  button2Obj.GetComponent<Cards>();
        button2Cards.initiateButton(playerUnit, enemyUnit);

        button2Text = button2Obj.GetComponentInChildren<TMP_Text>();
        button2Text.SetText(Enum.GetName(typeof(CardsTypes), playerUnit.PlayerCards.card2));


        GameObject button3Obj = Instantiate(buttonPrefab, buttonPlace3);
        UnityEngine.UI.Button button3 = button3Obj.GetComponent<UnityEngine.UI.Button>();
        button3Cards =  button3Obj.GetComponent<Cards>();
        button3Cards.initiateButton(playerUnit, enemyUnit);

        button3Text = button3Obj.GetComponentInChildren<TMP_Text>();
        button3Text.SetText(Enum.GetName(typeof(CardsTypes), playerUnit.PlayerCards.card3));

        button1.onClick.AddListener(OnButton1);
        button2.onClick.AddListener(OnButton2);
        button3.onClick.AddListener(OnButton3);

        
        
        yield return new WaitForSeconds(2f);

        state = BattleStates.PLAYERTURN;

        PlayerTurn();

    }

    // The logic behind the player's turn
    void PlayerTurn() {
        combatMenu.SetActive(true);
        skillsMenu.SetActive(false);
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

    public void OnButton1() {
        if(state != BattleStates.PLAYERTURN)
            return;

        if(skillsMenu.activeSelf == true) {
            dialogue.SetText(playerUnit.unitName + " uses a Card!");
            
            combatMenu.SetActive(false);
            skillsMenu.SetActive(false);

           StartCoroutine(PlayerUseCard(playerUnit.PlayerCards.card1));
        }
    }

    public void OnButton2() {
        if(state != BattleStates.PLAYERTURN)
            return;

        if(skillsMenu.activeSelf == true) {
            dialogue.SetText(playerUnit.unitName + " uses a Card!");
            
            combatMenu.SetActive(false);
            skillsMenu.SetActive(false);

           StartCoroutine(PlayerUseCard(playerUnit.PlayerCards.card2));
        }
    }

    public void OnButton3() {
        if(state != BattleStates.PLAYERTURN)
            return;

        if(skillsMenu.activeSelf == true) {
            dialogue.SetText(playerUnit.unitName + " uses a Card!");
            
            combatMenu.SetActive(false);
            skillsMenu.SetActive(false);

           StartCoroutine(PlayerUseCard(playerUnit.PlayerCards.card3));
        }
    }

    IEnumerator PlayerUseCard(CardsTypes card) {
        bool canUse = button1Cards.UseCard(card);
        if(canUse) {

            playerHUD.setMana(playerUnit.currentMana);
            playerHUD.setHP(playerUnit.currentHP);
            enemyHUD.setHP(enemyUnit.currentHP);

            yield return new WaitForSeconds(1f);

            StartCoroutine(EnemyTurn());
        } else {
            dialogue.SetText("Not enough mana!");

            yield return new WaitForSeconds(1f);

            PlayerTurn();
        }
        

    }

    IEnumerator PlayerAttack() {   
        bool isDead = enemyUnit.takeDamage(playerUnit.damage);
        enemyHUD.setHP(enemyUnit.currentHP);
        playerUnit.recoverMana(1);
        playerHUD.setMana(playerUnit.currentMana);

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
        playerHUD.setMana(playerUnit.currentMana);



        yield return new WaitForSeconds(1f);

        state = BattleStates.ENEMYTURN;
        StartCoroutine(EnemyTurn());
        
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
            SceneManager.LoadScene(3);
        } else if (state == BattleStates.LOST) {
            dialogue.SetText("You were defeated!");
            SceneManager.LoadScene(3);
        }
    }

}
