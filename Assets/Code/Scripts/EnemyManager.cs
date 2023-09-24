using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerStats playerStats;

    public GameObject penguinBossPrefab;
    public GameObject duckBossPrefab;
    public GameObject tanukiBossPrefab;
    public GameObject ghostPrefab;
    public GameObject slimePrefab;
    public GameObject skeletonPrefab;

    public GameObject CreateEnemy(Transform enemyPlace, int islandSize){
        if(islandSize == 4) {
            return Instantiate(penguinBossPrefab, enemyPlace);
        } else if (islandSize == 5) {
            return Instantiate(duckBossPrefab, enemyPlace);
        } else if (islandSize == 6) {
            return Instantiate(tanukiBossPrefab, enemyPlace);
        } else {
            int random = Random.Range(0,3);
            if (random == 0) {
                return Instantiate(ghostPrefab, enemyPlace);
            } else if (random == 1) {
                return Instantiate(slimePrefab, enemyPlace);
            } else {
                return Instantiate(skeletonPrefab, enemyPlace);
            }
            
        }
        
    }

    public bool IAEnemy(Unit playerUnit, Unit enemyUnit, int islandSize) {
        if(islandSize < 4) {
            int random = Random.Range(0,4);

            if(random == 0) {
                enemyUnit.RecoverLife(2);
                if (enemyUnit.damage > 1) {
                    return playerUnit.TakeDamage(enemyUnit.damage - 1);
                } else {
                    return playerUnit.TakeDamage(enemyUnit.damage);
                }
                
            } else {
                return playerUnit.TakeDamage(enemyUnit.damage);
            }
        } else {
            int random = Random.Range(0,4);

            if(random == 0) {
                enemyUnit.RecoverLife(4);
                return playerUnit.TakeDamage(enemyUnit.damage - 2);
            } else {
                return playerUnit.TakeDamage(enemyUnit.damage);
            }            
        }
    }
}
