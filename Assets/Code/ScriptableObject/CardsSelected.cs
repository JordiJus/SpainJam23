using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardsSelected : ScriptableObject
{
    [SerializeField] public int MaxNumOfCards;
    [SerializeField] public CardsTypes card1;
    [SerializeField] public CardsTypes card2;
    [SerializeField] public CardsTypes card3;
}
