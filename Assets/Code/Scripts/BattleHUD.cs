using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    
    public TMP_Text nameText;
    public Slider hpBar;
    public TMP_Text hpNum;

    public Slider manaBar;
    public TMP_Text manaNum;

    public void UpdateHUD(Unit unit)
    {
        nameText.SetText(unit.unitName);
        hpBar.maxValue = unit.maxHP;
        hpBar.value = unit.currentHP;
        hpNum.SetText(unit.currentHP + " / " + unit.maxHP);

        manaBar.maxValue = unit.maxMana;
        manaBar.value = unit.currentMana;
        manaNum.SetText(unit.currentMana + " / " + unit.maxMana);

    }

    public void setHP(int hp)
    {
        hpBar.value = hp;
        hpNum.SetText(hpBar.value + " / " + hpBar.maxValue);
    }

    public void setMana(int mana)
    {
        manaBar.value = mana;
        manaNum.SetText(manaBar.value + " / " + manaBar.maxValue);
    }
}
