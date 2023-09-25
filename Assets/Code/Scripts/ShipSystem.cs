using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ShipSystem : MonoBehaviour
{

    public TMP_Text dialogue;
    private PlayerStats playerStats;

    public GameObject crew1;
    public GameObject crew2;
    public GameObject crew3;

    public Transform infirmaryCrew;
    public Transform kitchenCrew;
    public Transform cannonsCrew;
    public Transform armoryCrew;
    public Transform cellarCrew;
    public Transform topCrew;
    public Transform stWheelCrew;

    public Transform crew1Initial;
    public Transform crew2Initial;
    public Transform crew3Initial;

    void Start(){
        playerStats = FindObjectOfType<PlayerStats>();
        
        playerStats.station1 = ShipStations.NONE;
        playerStats.station2 = ShipStations.NONE;
        playerStats.station3 = ShipStations.NONE;

        playerStats.maxHP = 10;
        if (playerStats.currentHP > 10) { playerStats.currentHP = 10; }
        playerStats.damage = 2;
        playerStats.mana = 5;

        dialogue.SetText("Place the 3 sailors in any station");
    }

    public IEnumerator PlaceSailor(ShipStations station, Transform crewPlace){

        if(playerStats.station1 == ShipStations.NONE) {
            playerStats.station1 = station;
            crew1.transform.position = crewPlace.transform.position;
        } else if (playerStats.station2 == ShipStations.NONE) {
            playerStats.station2 = station;
            crew2.transform.position = crewPlace.transform.position;
        } else if (playerStats.station3 == ShipStations.NONE) {
            playerStats.station3 = station;
            crew3.transform.position = crewPlace.transform.position;
        } else {
            dialogue.SetText("All sailors are already in a station");
            yield return new WaitForSeconds(1f);
            dialogue.SetText("Place the 3 sailors in any station");
        }
    }

    public IEnumerator GoToCards(){
        if(playerStats.station1 != ShipStations.NONE && playerStats.station2 != ShipStations.NONE && playerStats.station3 != ShipStations.NONE){
            yield return new WaitForSeconds(1f);
            StationWorking(playerStats.station1);
            StationWorking(playerStats.station2);
            StationWorking(playerStats.station3);
            SceneManager.LoadScene(3);
        } else {
            dialogue.SetText("You have to place all sailors before sailing.");
            yield return new WaitForSeconds(1f);
            dialogue.SetText("Place the 3 sailors in any station");
        }
    }

    public void ReturnCrew(Transform station){
        if(crew1.transform.position == station.transform.position) {
            crew1.transform.position = crew1Initial.transform.position;
            playerStats.station1 = ShipStations.NONE;
        } else if (crew2.transform.position == station.transform.position) {
            crew2.transform.position = crew2Initial.transform.position;
            playerStats.station2 = ShipStations.NONE;
        } else if (crew3.transform.position == station.transform.position) {
            crew3.transform.position = crew3Initial.transform.position;
            playerStats.station3 = ShipStations.NONE;
        }


    }

    public void OnCabinButton(){
        StartCoroutine(GoToCards());
    }

    public void OnCannonsButton(){
        if(playerStats.station1 == ShipStations.CANNONS || playerStats.station2 == ShipStations.CANNONS || playerStats.station3 == ShipStations.CANNONS) {
            ReturnCrew(cannonsCrew);
        } else {
            StartCoroutine(PlaceSailor(ShipStations.CANNONS, cannonsCrew));
        }
    }

    public void OnCellarButton(){
        if(playerStats.station1 == ShipStations.CELLAR || playerStats.station2 == ShipStations.CELLAR || playerStats.station3 == ShipStations.CELLAR) {
            ReturnCrew(cellarCrew);
        } else {
            StartCoroutine(PlaceSailor(ShipStations.CELLAR, cellarCrew));
        }
    }

    public void OnArmoryButton(){
        if(playerStats.station1 == ShipStations.ARMORY || playerStats.station2 == ShipStations.ARMORY || playerStats.station3 == ShipStations.ARMORY) {
            ReturnCrew(armoryCrew);
        } else {
            StartCoroutine(PlaceSailor(ShipStations.ARMORY, armoryCrew));
        }
    }

    public void OnKitchenButton(){
        if(playerStats.station1 == ShipStations.KITCHEN || playerStats.station2 == ShipStations.KITCHEN || playerStats.station3 == ShipStations.KITCHEN) {
            ReturnCrew(kitchenCrew);
        } else {
            StartCoroutine(PlaceSailor(ShipStations.KITCHEN, kitchenCrew));
        }
    }

    public void OnInfirmaryButton(){
        if(playerStats.station1 == ShipStations.INFIRMARY || playerStats.station2 == ShipStations.INFIRMARY || playerStats.station3 == ShipStations.INFIRMARY) {
            ReturnCrew(infirmaryCrew);
        } else {
            StartCoroutine(PlaceSailor(ShipStations.INFIRMARY, infirmaryCrew));
        }
    }

    public void OnSTWheelButton(){
        if(playerStats.station1 == ShipStations.STWHEEL || playerStats.station2 == ShipStations.STWHEEL || playerStats.station3 == ShipStations.STWHEEL) {
            ReturnCrew(stWheelCrew);
        } else {
            StartCoroutine(PlaceSailor(ShipStations.STWHEEL, stWheelCrew));
        }
        
    }

    public void OnTopButton(){
        if(playerStats.station1 == ShipStations.TOP || playerStats.station2 == ShipStations.TOP || playerStats.station3 == ShipStations.TOP) {
            ReturnCrew(topCrew);
        } else {
            StartCoroutine(PlaceSailor(ShipStations.TOP, topCrew));
        }
    }

    public void StationWorking(ShipStations station){
        if (station == ShipStations.INFIRMARY) {
            playerStats.currentHP = 10;
        } else if (station == ShipStations.CELLAR) {
            playerStats.maxHP += 4;
            playerStats.currentHP += 4;
        } else if (station == ShipStations.ARMORY) {
            playerStats.damage += 1;
        } else if (station == ShipStations.KITCHEN) {
            playerStats.mana = 7;
        }
    }
}
