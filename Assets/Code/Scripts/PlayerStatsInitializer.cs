using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsInitializer : MonoBehaviour
{
    public PlayerStats playerStats;

    private void Awake()
    {
        if (playerStats == null)
        {
            playerStats = ScriptableObject.CreateInstance<PlayerStats>();
            playerStats.maxHP = 10;
            playerStats.currentHP = 10;
            playerStats.damage = 2;
            playerStats.mana = 5;
            playerStats.posShipX = 0.5f;
            playerStats.posShipY = 0.5f;
            playerStats.movements = 8;
            playerStats.supplies = 50;

            playerStats.isDuckDead = false;
            playerStats.isPenguDead = false;
            playerStats.isRaccDead = false;
        }

        DontDestroyOnLoad(gameObject);
    }
}

