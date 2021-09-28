using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public TextMeshProUGUI m_CoinsTMP;
    public TextMeshProUGUI m_PlayerAmmoTMP;
    public TextMeshProUGUI m_ScoreTMP;
    public GameObject m_GameOverUI;
    public GameObject m_VictoryUI;
    public GameObject m_ShopUI;

    void Start()
    {
        m_CoinsTMP.text = string.Format("Coins: {0}", ResourceManager.instance.m_Coins);
        m_ScoreTMP.text = string.Format("Enemies Defeated: {0}/{1}", ResourceManager.instance.m_EnemiesKilled, GameManager.instance.m_EnemyPoolSize);
        m_GameOverUI.SetActive(false);
        m_VictoryUI.SetActive(false);
        m_ShopUI.SetActive(false);

    }

    public void ShowGameOverUI()
    {
        m_GameOverUI.SetActive(true);
    }

    public void ShowVictoryUI()
    {
        m_VictoryUI.SetActive(true);
        GameManager.instance.playerRef.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void ShowShopUI(bool value)
    {
        m_ShopUI.SetActive(value);
    }
}
