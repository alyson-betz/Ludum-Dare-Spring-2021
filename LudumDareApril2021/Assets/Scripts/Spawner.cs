using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies;
    public TileManager tileManager;

    public int minEnemiesToSpawn;
    public int maxEnemiesToSpawn;

    public void Reset()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        string holderName = "Generated Enemies";
        int enemiesToSpawn = (int)Random.Range(minEnemiesToSpawn, maxEnemiesToSpawn);

        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        Transform enemyHolder = new GameObject (holderName).transform;
        enemyHolder.parent = transform;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy().transform.parent = enemyHolder;
        }
    }

    private GameObject SpawnEnemy()
    {
        GameObject enemy;
        int enemyToInstantiate = Random.Range(0, enemies.Length);

        enemy = Instantiate(enemies[enemyToInstantiate]) as GameObject;
        //enemy.transform.SetParent(transform);
        enemy.transform.position = tileManager.GetRandomVectorPosition();

        return enemy;
    }
}