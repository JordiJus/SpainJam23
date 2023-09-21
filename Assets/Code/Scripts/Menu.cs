using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public PlayerStats playerStats;
    void Start(){
        playerStats.currentHP = playerStats.maxHP;
    }
    // Called when we click the "Play" button.
    public void OnPlayButton ()
    {
        SceneManager.LoadScene(2);
    }
    // Called when we click the "Quit" button.
    public void OnQuitButton ()
    {
        Application.Quit();
    }
}
