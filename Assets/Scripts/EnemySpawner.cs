using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //tablica punktow spawnu
    //enumerator z losowym czasem i pkt spawnu spawniacy wroga
    [SerializeField] private Spawn[] spawnPoints;
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private int spawnTime;
    //[SerializeField] private int spawnTimeMax;

    private int indexRand;

   // public int SpawnTimeMax { get => spawnTimeMax; set => spawnTimeMax = value; }

    private void Awake()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i].Occupied = false;
            spawnPoints[i].SpawnID = i;
        }
        
    }
    private void FixedUpdate()
    {
        if(Time.frameCount % 15 == 0)
        {
            for(int i =0;i< enemies.Count; i++)
            {
                if (enemies[i].HitByFlashlight)
                {
                    spawnPoints[enemies[i].SpawnID].Occupied = false;
                    enemies.Remove(enemies[i]);
                }
            }
        }
        if (Time.frameCount % (spawnTime*50) == 0)
        {
            if (Space())
            {
                StartCoroutine(SpawnEnemy(ChooseSpawnPoint()));
            }
        }

    }
    private bool Space()
    {
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            if(!spawnPoints[i].Occupied)
            return true;
        }
        return false;
    }
    private Spawn ChooseSpawnPoint()
    {
        do
        {
            indexRand = Random.Range(0, spawnPoints.Length);
        } while (spawnPoints[indexRand].Occupied);

        spawnPoints[indexRand].Occupied = true;
        return spawnPoints[indexRand];
    }
    IEnumerator SpawnEnemy (Spawn spawnPoint)
    {
        yield return new WaitForSeconds(1);
        enemies.Add(Instantiate(enemyPrefab, spawnPoint.transform));
        enemies[enemies.Count-1].SpawnID = spawnPoint.SpawnID;

    }
}
