using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour, IDamageble
{
    public TextMeshProUGUI m_HealthTMP;
    public int m_StartingHealth;

    private bool objectDied;

    public int hitpoints { get; set; }

    void Start()
    {
        hitpoints = m_StartingHealth;
        m_HealthTMP.text = hitpoints.ToString();
    }

    public void ModifyHealth(int amount)
    {
        if (!objectDied)
        {
            hitpoints += amount;

            m_HealthTMP.text = hitpoints.ToString();

            if (hitpoints <= 0)
            {
                KillSelf();
            }
        }
    }

    private void KillSelf()
    {
        objectDied = true;

        GameObject shipwreak = Instantiate(GameManager.instance.m_Shipwreak);
        shipwreak.transform.position = gameObject.transform.position;
        shipwreak.transform.rotation = gameObject.transform.rotation;
        Destroy(shipwreak, GameManager.instance.m_CorpseDespawnTime);

        if (gameObject.CompareTag("Player"))
        {
            InterfaceManager.instance.ShowGameOverUI();
            gameObject.SetActive(false);
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            ResourceManager.instance.ModifyEnemiesKilled(1);
            GameManager.instance.enemyPool.DeactivateObject(gameObject);
        }

    }
}
