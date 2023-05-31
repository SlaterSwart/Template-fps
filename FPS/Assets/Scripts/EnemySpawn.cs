using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform EnemySpawnPoint;
    public GameObject EnemyPrefab;

    public int spawnNumber;

    void Awake(){
    Spawn();
    }

    void Update(){
        if(spawnNumber > 0){
            Spawn();
            spawnNumber--;
        }
    }
    
    private void Spawn(){
        var Enemy = Instantiate(EnemyPrefab, EnemySpawnPoint.position, EnemySpawnPoint.rotation);
   }
}
