using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    public string unitName;

    public int initialDamage;
    public int damage;

    public int maxHP;
    public int currentHP;

    public int maxMana;
    public int currentMana;

    public CardsSelected PlayerCards;
    
    public bool defense;
    public bool canDefend = true; 
    public bool moonProtection = false;

    public bool TakeDamage(int dmg)
    {
        if (defense){
            dmg = Mathf.FloorToInt(dmg/2);
            defense = false;
        }
        if (moonProtection) {
            dmg -= 1;
        }
        if (dmg <= 0) {
            dmg = 0;
        }
        currentHP -= dmg;
        

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public void RecoverLife(int life){
        currentHP += life;
        if (currentHP > maxHP) {
            currentHP = maxHP;
        }
    }

    public void RecoverMana(int mana){
        currentMana += mana;
        if (currentMana > maxMana) {
            currentMana = maxMana;
        }
    }

    public void UseMana(int mana){
        currentMana -= mana;
        if (currentMana < 0) {
            currentMana = 0;
        }
    }

    public bool UnitDefend() {
        if (canDefend){
            defense = true;
            RecoverMana(2);
            return true;
        } else {
            return false;
        }
        
    }

    public void ModifyStrength(int dmg){
        if (dmg == 0) {
            damage = initialDamage;
        } else {
            damage += dmg;
        }
    }
}
