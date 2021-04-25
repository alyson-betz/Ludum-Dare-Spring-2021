using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies;
    public TileManager tileManager;

    public int enemiesToSpawn;

    void Start()
    {
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy;
        int enemyToInstantiate = Random.Range(0, enemies.Length);

        enemy = Instantiate(enemies[enemyToInstantiate]) as GameObject;
        enemy.transform.SetParent(transform);
        enemy.transform.position = tileManager.GetRandomVectorPosition();
    }
}