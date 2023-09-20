using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    public string unitName;

    public int damage;

    public int maxHP;
    public int currentHP;

    public int maxMana;
    public int currentMana;

    public CardsSelected PlayerCards;
    
    public bool defense;

    public bool takeDamage(int dmg)
    {
        if (defense){
            dmg = Mathf.FloorToInt(dmg/2);
            defense = false;
        }
        currentHP -= dmg;
        

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public void recoverMana(int mana){
        currentMana += mana;
        if (currentMana > maxMana) {
            currentMana = maxMana;
        }
    }

    public void useMana(int mana){
        currentMana -= mana;
        if (currentMana < 0) {
            currentMana = 0;
        }
    }

    public void unitDefend() {
        defense = true;
    }
}
