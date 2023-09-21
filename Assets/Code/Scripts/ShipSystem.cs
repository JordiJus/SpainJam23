using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ShipSystem : MonoBehaviour
{

    public TMP_Text dialogue;
    public PlayerStats playerStats;

    void Start(){
        playerStats.station1 = ShipStations.NONE;
        playerStats.station2 = ShipStations.NONE;
        playerStats.station3 = ShipStations.NONE;

        dialogue.SetText("Place the 3 sailors in any station");
    }

    public IEnumerator PlaceSailor(ShipStations station){

        Debug.Log("place CLick");

        if(playerStats.station1 == ShipStations.NONE) {
            Debug.Log("place 1 CLick");
            playerStats.station1 = station;
        } else if (playerStats.station2 == ShipStations.NONE) {
            Debug.Log("place 2 CLick");
            playerStats.station2 = station;
        } else if (playerStats.station3 == ShipStations.NONE) {
            Debug.Log("place 3 CLick");
            playerStats.station3 = station;
        } else {
            Debug.Log("place 4 CLick");
            dialogue.SetText("All sailors are already in a station");
        }
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator GoToCards(){
        if(playerStats.station1 != ShipStations.NONE && playerStats.station2 != ShipStations.NONE && playerStats.station3 != ShipStations.NONE){
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(3);
        }
    }

    public void OnCabinButton(){
        StartCoroutine(GoToCards());
    }

    public void OnCannonsButton(){
        StartCoroutine(PlaceSailor(ShipStations.CANNONS));
    }

    public void OnCellarButton(){
        StartCoroutine(PlaceSailor(ShipStations.CELLAR));
    }

    public void OnArmoryButton(){
        StartCoroutine(PlaceSailor(ShipStations.ARMORY));
    }

    public void OnKitchenButton(){
        StartCoroutine(PlaceSailor(ShipStations.KITCHEN));
    }

    public void OnInfirmaryButton(){
        StartCoroutine(PlaceSailor(ShipStations.INFIRMARY));
    }

    public void OnSTWheelButton(){
        StartCoroutine(PlaceSailor(ShipStations.STWHEEL));
    }

    public void OnTopButton(){
        StartCoroutine(PlaceSailor(ShipStations.TOP));
    }

}
