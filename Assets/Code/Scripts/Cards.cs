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
        if(card == CardsTypes.MAGICIAN) {
            playerUnit.recoverMana(5);
            //playerUnit.recoverMana(5);
        } else if (card == CardsTypes.SUN) {
            if (playerUnit.currentMana < 5){
                return false;
            } else {
                playerUnit.useMana(5);
                enemyUnit.takeDamage(5);

                
            }
        } else {
            return false;
        }
        return true;       
    }
}
