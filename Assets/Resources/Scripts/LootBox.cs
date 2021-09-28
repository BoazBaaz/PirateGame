using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    public RandomValue m_CoinsGiven;
    public RandomValue m_AmmoGiven;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int ranCoins = UnityEngine.Random.Range(m_CoinsGiven.min, m_CoinsGiven.max + 1);
            int ranAmmo = UnityEngine.Random.Range(m_AmmoGiven.min, m_AmmoGiven.max + 1);

            ResourceManager.instance.ModifyCoins(ranCoins);
            ResourceManager.instance.ModifyPlayerAmmo(ranAmmo);

            GameManager.instance.lootBoxPool.DeactivateObject(gameObject);
        }
    }

    [Serializable]
    public struct RandomValue
    {
        public int min;
        public int max;
    }
}
