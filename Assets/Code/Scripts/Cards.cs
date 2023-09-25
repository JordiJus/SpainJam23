using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    Unit playerUnit;
    Unit enemyUnit;

    public void initiateButton(Unit player, Unit enemy) {
        playerUnit = player;
        enemyUnit = enemy;

    }
    public bool UseCard(CardsTypes card) {
        
        if (card == CardsTypes.FOOL) {
            if(playerUnit.canUseFool == true) {
                int random = Random.Range(0,5);
                if (random == 0) {
                    int dmg = Mathf.CeilToInt(enemyUnit.currentHP/2);
                    enemyUnit.TakeDamage(dmg);
                } else if (random == 1) {
                    playerUnit.RecoverLife(Mathf.CeilToInt(playerUnit.maxHP/2));
                } else if (random == 2) {
                    playerUnit.ModifyStrength(3);
                } else if (random == 3) {
                    playerUnit.ModifyStrength(-1);
                } else if (random == 4) {
                    playerUnit.TakeDamage(Mathf.FloorToInt(playerUnit.currentHP/2));
                }
                playerUnit.canUseFool = false;
            } else {
                return false;
            }
        } else if(card == CardsTypes.MAGICIAN) {
            playerUnit.RecoverMana(5);
        } else if (card == CardsTypes.LOVERS){
            if (playerUnit.currentMana < 2){
                return false;
            } else {
                playerUnit.UseMana(2);
            playerUnit.RecoverLife(4);
            }
            
        } else if (card == CardsTypes.DEATH) {
            if (playerUnit.currentMana < 5){
                return false;
            } else {
                playerUnit.UseMana(5);
                enemyUnit.TakeDamage(5);
            }
        } else if (card == CardsTypes.STRENGTH) {
            if (playerUnit.currentMana < 3){
                return false;
            } else {
                playerUnit.UseMana(3);
                playerUnit.ModifyStrength(1);
            }
            
        } else if (card == CardsTypes.DEVIL) {
            if (playerUnit.canDefend) {
                if (playerUnit.currentMana < 3){
                    return false;
                } else {
                    playerUnit.UseMana(3);
                    playerUnit.ModifyStrength(2);
                    playerUnit.canDefend = false;
                }
            } else {
                return false;
            }
        } else if (card == CardsTypes.MOON) {
            if (playerUnit.moonProtection == true) {
                return false;
            }
            playerUnit.moonProtection = true;
        } else if (card == CardsTypes.SUN) {
            if (playerUnit.currentMana < 4) {
                return false;
            } else {
                playerUnit.UseMana(4);
                enemyUnit.TakeDamage(3);
                playerUnit.RecoverLife(2);
            }
            
        } else {
            return false;
        }
        return true;       
    }
}
