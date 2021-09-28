using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public int m_Coins { get; private set; }
    public int m_PlayerAmmo { get; private set; }
    public int m_EnemiesKilled { get; private set; }

    public void ModifyCoins(int amount)
    {
        m_Coins += amount;

        InterfaceManager.instance.m_CoinsTMP.text = string.Format("Coins: {0}", m_Coins);
    }

    public void ModifyPlayerAmmo(int amount)
    {
        m_PlayerAmmo += amount;

        InterfaceManager.instance.m_PlayerAmmoTMP.text = string.Format("Ammo: {0}", m_PlayerAmmo);
    }

    public void ModifyEnemiesKilled(int amount)
    {
        m_EnemiesKilled += amount;

        InterfaceManager.instance.m_ScoreTMP.text = string.Format("Enemies Defeated: {0}/{1}", m_EnemiesKilled, GameManager.instance.m_EnemyPoolSize);

        if (m_EnemiesKilled == GameManager.instance.m_EnemyPoolSize)
        {
            InterfaceManager.instance.ShowVictoryUI();
        }
    }
}
