using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card : MonoBehaviour
{
    public int manaCost;
    public string typeOfCard;
    public int manaRecovery;

    public Card(Card c){
        manaCost =c.manaCost;
        typeOfCard = c.typeOfCard;
        manaRecovery = c.manaRecovery;
    }
}
