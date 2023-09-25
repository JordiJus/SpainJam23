using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Transactions;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float yPos;
    float xPos;
    private PlayerStats playerStats;
    public TMP_Text dialogue;
    public TMP_Text suppliesText;

    public void Start(){
        playerStats = FindObjectOfType<PlayerStats>();
        if(playerStats.station1 == ShipStations.STWHEEL || playerStats.station2 == ShipStations.STWHEEL || playerStats.station3 == ShipStations.STWHEEL) {
            playerStats.movements = 12;
        } else {
            playerStats.movements = 8;
        }
        suppliesText.SetText("Supplies left: " + playerStats.supplies);
        transform.position = new Vector3(playerStats.posShipX, playerStats.posShipY, transform.position.z);
    }

    void Update()
    {
        if(Input.GetKeyDown("p")){
            playerStats.movements = 100;
        }

        xPos = transform.position.x;
        yPos = transform.position.y;

        if (playerStats.movements > 0) {
            if(Input.GetKeyDown("up")) {
                yPos += 1;  
                if (yPos > 36.5f) yPos = 36.5f;
                playerStats.movements -= 1;
            }       

            if(Input.GetKeyDown("down")) {
                yPos -= 1;
                if (yPos < -3.5f) yPos = -3.5f;
                playerStats.movements -= 1;
            }

            if(Input.GetKeyDown("right")) {
                xPos += 1; 
                if (xPos > 14.5f) xPos = 14.5f;
                playerStats.movements -= 1;
            }

            if(Input.GetKeyDown("left")) {
                xPos -= 1;
                if (xPos < -13.5f) xPos = -13.5f;
                playerStats.movements -= 1;
            }
            
            transform.position = new Vector3(xPos, yPos, transform.position.z);
            dialogue.SetText("MOVEMENTS LEFT - " + playerStats.movements +"\nPress z to end day.");

            transform.position = new Vector3(xPos, yPos, transform.position.z);
        } else {
            dialogue.SetText("You have no more movements, press Z to end day.");
        }
;        
    }
}
