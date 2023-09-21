using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    public int maxHP = 10;
    public int currentHP;
    public ShipStations station1;
    public ShipStations station2;
    public ShipStations station3;
}
