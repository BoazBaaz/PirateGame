using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        cannonballPool = new ObjectPool(m_CannonballParent, m_CannonballPrefab, m_CannonballPoolSize, m_CannonballOverflow);
        enemyPool = new ObjectPool(m_EnemyParent, m_EnemyPrefab, m_EnemyPoolSize, m_EnemyOverflow);
        lootBoxPool = new ObjectPool(m_LootBoxParent, m_LootBoxPrefab, m_LootBoxPoolSize, m_LootBoxOverflow);
    }

    [Header("GameOver")]
    public GameObject m_Shipwreak;
    public int m_CorpseDespawnTime = 2;
    public int m_CrashDamage = 5;

    [Header("Cannonball Objectpool Info")]
    public GameObject m_CannonballParent;
    public GameObject m_CannonballPrefab;
    public int m_CannonballPoolSize;
    public int m_CannonballOverflow;
    public ObjectPool cannonballPool;

    [Header("Enemy Objectpool Info")]
    public GameObject m_EnemyParent;
    public GameObject m_EnemyPrefab;
    public int m_EnemyPoolSize;
    public int m_EnemyOverflow;
    public ObjectPool enemyPool;

    [Header("LootBox Objectpool Info")]
    public GameObject m_LootBoxParent;
    public GameObject m_LootBoxPrefab;
    public int m_LootBoxPoolSize;
    public int m_LootBoxOverflow;
    public ObjectPool lootBoxPool;

    [HideInInspector] public GameObject playerRef;

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
