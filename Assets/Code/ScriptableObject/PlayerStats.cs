using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    public int maxHP;
    public int currentHP;

    public int damage;

    public int mana;

    public float posShipX;
    public float posShipY;
    public int movements;
    public int supplies;

    public bool isPenguDead;
    public bool isDuckDead;
    public bool isRaccDead;

    public int currentTile;
    public ShipStations station1;
    public ShipStations station2;
    public ShipStations station3;
}
