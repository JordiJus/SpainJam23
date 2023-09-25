using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;

    [SerializeField]
    private List<TileData> tileDatas;

    private Dictionary<TileBase, TileData> dataFromTiles;

    public Transform ship;

    private PlayerStats playerStats;

    private int islandSize;

    private void Start(){
        playerStats = FindObjectOfType<PlayerStats>();
    }


    private void Update(){
        if(Input.GetKeyDown("z")){
            Vector2 cellPos = new Vector2(ship.position.x, ship.position.y);
            Vector3Int gridPosition = map.WorldToCell(cellPos);

            TileBase clickedTile = map.GetTile(gridPosition);

            if (clickedTile.IsUnityNull()) {
                islandSize = 0;
            } else {
                islandSize = dataFromTiles[clickedTile].islandSize;
            }

            playerStats.currentTile = islandSize;

            playerStats.posShipX = ship.transform.position.x;
            playerStats.posShipY = ship.transform.position.y;
            
            if (playerStats.supplies <= 0) {
                UnityEngine.SceneManagement.SceneManager.LoadScene(6);
            } else {
                if(!((islandSize == 4 && playerStats.isPenguDead) || (islandSize == 5 && playerStats.isDuckDead) || (islandSize == 6 && playerStats.isRaccDead))) {
                    WasteSupplies(islandSize);
                    UnityEngine.SceneManagement.SceneManager.LoadScene(5);
                }
            }
           
        }
    }

    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach(var tileData in tileDatas) {
            foreach(var tile in tileData.tiles) {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }

    private void WasteSupplies(int size){
        playerStats.supplies -= 5;

        if (size == 1) {
            playerStats.supplies += 2;
        } else if (size == 2) {
            playerStats.supplies += 5;
        } else if (size == 3) {
            playerStats.supplies += 10;
        } else if (size > 4) {
            playerStats.supplies += 20;
        }
    }
}
