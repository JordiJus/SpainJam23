using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CardsTypes { FOOL, MAGICIAN, LOVERS, STRENGTH, DEATH, DEVIL, MOON, SUN }
public enum BattleStates { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public enum ShipStations { NONE, CANNONS, CABIN, CELLAR, ARMORY, KITCHEN, INFIRMARY, STWHEEL, TOP }
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
    public PlayerStats playerStats;
    public EnemyManager enemyManager;

    public TMP_Text dialogue;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    bool startMoon;

    void Start() {
        state = BattleStates.START;
        StartCoroutine(SetupBattle());
    }

    // This initiates the battle

    IEnumerator SetupBattle() {

        combatMenu.SetActive(false);
        skillsMenu.SetActive(false);

        GameObject playerGameObj = Instantiate(playerPrefab, playerPlace);
        playerUnit = playerGameObj.GetComponent<Unit>();

        playerUnit.maxHP = playerStats.maxHP;
        playerUnit.currentHP = playerStats.currentHP;
        playerUnit.PlayerCards = cardsForPlayer;

        playerUnit.damage = playerStats.damage;
        playerUnit.maxMana = playerStats.mana;
        playerUnit.currentMana = playerStats.mana;

        // This has to be eliminated!!
        // playerUnit.PlayerCards.card1 = CardsTypes.MAGICIAN;
        // playerUnit.PlayerCards.card2 = CardsTypes.SUN;

        GameObject enemyGameObj = enemyManager.CreateEnemy(enemyPlace, playerStats.currentTile);;
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
        startMoon = true;

        StartCoroutine(PlayerTurn());

    }

    // The logic behind the player's turn
    IEnumerator PlayerTurn() {
        
        if(startMoon) {
            if(playerUnit.moonProtection){
                dialogue.SetText("Due to The Moon, "+ playerUnit.unitName + " spends 1 mana.");
                playerUnit.UseMana(1);
                playerHUD.setMana(playerUnit.currentMana);
                if(playerUnit.currentMana <= 0) {
                    yield return new WaitForSeconds(1f);

                    dialogue.SetText(playerUnit.unitName + " loses the protection of The Moon.");
                    playerUnit.moonProtection = false;
                }
            }
        }
        
        startMoon = true;

        yield return new WaitForSeconds(1f);

        combatMenu.SetActive(true);
        skillsMenu.SetActive(false);

        dialogue.SetText("Choose an action");
    }

    public void OnAttackButton() {
        if(state != BattleStates.PLAYERTURN)
            return;
        
        dialogue.SetText(playerUnit.unitName + " attacks!");
        combatMenu.SetActive(false);
        skillsMenu.SetActive(false);
        
        StartCoroutine(PlayerAttack());
    }

    public void OnDefendButton() {
        if(state != BattleStates.PLAYERTURN)
            return;
        
        dialogue.SetText(playerUnit.unitName + " defends!");
        combatMenu.SetActive(false);
        skillsMenu.SetActive(false);

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
        yield return new WaitForSeconds(1f);

        dialogue.SetText(playerUnit.unitName + " uses The " + Enum.GetName(typeof(CardsTypes), card));
        bool canUse = button1Cards.UseCard(card);

        if(canUse) {
            
            playerHUD.setMana(playerUnit.currentMana);
            playerHUD.setHP(playerUnit.currentHP);
            enemyHUD.setHP(enemyUnit.currentHP);

            yield return new WaitForSeconds(1f);
            
            if(enemyUnit.currentHP <= 0){
                state = BattleStates.WON;
                EndBattle();
            } else {
                state = BattleStates.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
            
        } else {
            if (playerUnit.moonProtection == true && playerUnit.currentMana > 0) {
                dialogue.SetText("The Moon already active!");
                startMoon = false;
            } else if (playerUnit.canDefend == false && playerUnit.currentMana >= 3) {
                dialogue.SetText("The Devil already active!");
            } else {
                dialogue.SetText("Not enough mana!");
            }
            

            yield return new WaitForSeconds(1f);

            StartCoroutine(PlayerTurn());
        }
        

    }

    IEnumerator PlayerAttack() {   
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        enemyHUD.setHP(enemyUnit.currentHP);
        playerUnit.RecoverMana(1);
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
        bool canDefend = playerUnit.UnitDefend();
        
        playerHUD.setMana(playerUnit.currentMana);

        yield return new WaitForSeconds(1f);

        if (canDefend) {
            state = BattleStates.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        } else {
            dialogue.SetText("Due to the Devil " + playerUnit.unitName + " can't defend!");

            yield return new WaitForSeconds(1f);
            combatMenu.SetActive(true);
        }
        
        
    }


    // Enemy's turn
    IEnumerator EnemyTurn() {
        if(playerStats.station1 == ShipStations.CANNONS || playerStats.station2 == ShipStations.CANNONS || playerStats.station3 == ShipStations.CANNONS) {
            dialogue.SetText("Cannons make 1 damage to enemy");

            bool isCannonDead = enemyUnit.TakeDamage(1);
            enemyHUD.setHP(enemyUnit.currentHP);

            yield return new WaitForSeconds(1);
            
            if (isCannonDead) {
                state = BattleStates.WON;
                EndBattle();
            }
        } 
        dialogue.SetText("Enemy attacks!");

        yield return new WaitForSeconds(1);

        bool isDead= enemyManager.IAEnemy(playerUnit, enemyUnit, playerStats.currentTile);

        playerHUD.setHP(playerUnit.currentHP);
        enemyHUD.setHP(enemyUnit.currentHP);

        yield return new WaitForSeconds(1);

        if (isDead) {
            state = BattleStates.LOST;
            EndBattle();
        } else {
            state = BattleStates.PLAYERTURN;
            StartCoroutine(PlayerTurn());
        
        }
        
    }

    void EndBattle() {
        playerStats.currentHP = playerUnit.currentHP;
        if (state == BattleStates.WON) {
            dialogue.SetText("You won the battle!");
            if(playerStats.currentTile == 4) {
                playerStats.isPenguDead = true;
            } else if(playerStats.currentTile == 5) {
                playerStats.isDuckDead = true;
            } else if(playerStats.currentTile == 6) {
                playerStats.isRaccDead = true;
            }

            if(playerStats.isPenguDead && playerStats.isDuckDead && playerStats.isRaccDead) {
                SceneManager.LoadScene(7);
            } else {
                SceneManager.LoadScene(2);
            }
        } else if (state == BattleStates.LOST) {
            dialogue.SetText("You were defeated!");
            SceneManager.LoadScene(6);
        }
    }

}
