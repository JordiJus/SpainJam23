using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;


public class IntroSceneManager : MonoBehaviour
{
    private PlayerStats playerStats;
    
    public GameObject vin1;
    public GameObject vin2;
    public GameObject vin3;
    public GameObject vin4;

    private int counter;

    public TMP_Text dialogue;

    public void Start(){
        playerStats = FindObjectOfType<PlayerStats>();

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

        vin1.SetActive(true);
        vin2.SetActive(false);
        vin3.SetActive(false);
        vin4.SetActive(false);

        dialogue.SetText("Axa learned from his master all the theory about star weaving and divination. This magic is powerful, but can only be used at night. Axa's master dedicated all his life to find an artifact that can increase the power of an Star Weaver.");
    }
    public void Update()
    {
        if (UnityEngine.Input.GetKeyDown("z")){
            if (counter == 0) {
                vin1.SetActive(false);
                vin2.SetActive(true);
                dialogue.SetText("This artifact could only be found in the Shadow Sea, a place where it's almost impossible to sail due to it's lack of wind. But before he could set on an adventure to find it, he died.");
            } else if (counter == 1) {
                vin2.SetActive(false);
                vin3.SetActive(true);
                dialogue.SetText("Axa inherited their master's belongings and will, and decided to set on an adventure their owns, to find the lost artifact. Axa will use the power of star weaving to move the ship in the night, to reach their destination,");
            } else if (counter == 2) {
                vin3.SetActive(false);
                vin4.SetActive(true);
                dialogue.SetText("Axa is searching for the three pieces of the artifact, guarded by three powerful guardians. Reuniting the three pieces will complete the last will of Axa's master.");
            } else if (counter == 3) {
                SceneManager.LoadScene(2);
            }
            counter++;
        }
    }
}
