using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorld : MonoBehaviour
{
    public static GenerateWorld instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public List<Transform> m_AvailableSpawnTiles;

    private Transform[] lootBoxSpawnTiles;
    private Transform[] enemySpawnTiles;

    void Start()
    {
        lootBoxSpawnTiles = new Transform[GameManager.instance.m_LootBoxPoolSize];
        enemySpawnTiles = new Transform[GameManager.instance.m_EnemyPoolSize];

        SpawnObjectsOnTiles(lootBoxSpawnTiles);
        SpawnObjectsOnTiles(enemySpawnTiles);
    }

    private void SpawnObjectsOnTiles(Transform[] spawnedObjects)
    {
        for (int i = 0; i < spawnedObjects.Length; i++)
        {
            int randomInt = Random.Range(0, m_AvailableSpawnTiles.Count);

            spawnedObjects[i] = m_AvailableSpawnTiles[randomInt];
            m_AvailableSpawnTiles.RemoveAt(randomInt);

            if (spawnedObjects == lootBoxSpawnTiles)
            {
                GameObject lootBoxOBJ = GameManager.instance.lootBoxPool.GetObjectFromPool();
                lootBoxOBJ.transform.position = spawnedObjects[i].position;
                lootBoxOBJ.transform.rotation = Quaternion.identity;
            }
            else if (spawnedObjects == enemySpawnTiles)
            {
                GameObject enemyOBJ = GameManager.instance.enemyPool.GetObjectFromPool();
                enemyOBJ.transform.position = new Vector3(spawnedObjects[i].position.x, -0.5f, spawnedObjects[i].position.z);
                enemyOBJ.transform.rotation = Quaternion.identity;
                enemyOBJ.GetComponent<Enemy>().Initialize();
            }
        }
    }
}
