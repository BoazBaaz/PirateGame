using System;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public enum shopItems : int
    {
        health = 0,
        ammo,
        upgrade
    }

    [Header("ShopItems")]
    public shopItem[] m_ShopItemList;

    [Header("Shop Buttons TMP")]
    public TextMeshProUGUI m_HealthButtonTMP;
    public TextMeshProUGUI m_AmmoButtonTMP;
    public TextMeshProUGUI m_UpgradeButtonTMP;
    public GameObject m_UpgradeButtonUI;

    [Header("")]
    public GameObject m_ShopInteractKeyUI;

    private void Start()
    {
        m_HealthButtonTMP.text = string.Format("Full Health - Price: {0}$", Mathf.Abs(m_ShopItemList[0].price));
        m_AmmoButtonTMP.text = string.Format("+{0} Ammo - Price: {1}$", m_ShopItemList[1].amount, Mathf.Abs(m_ShopItemList[1].price));
        m_UpgradeButtonTMP.text = string.Format("Upgrade Cannons - Price: {0}$", Mathf.Abs(m_ShopItemList[2].price));
    }

    public void BuyItem(int item)
    {
        foreach (shopItem _item in m_ShopItemList)
        {
            if ((int)_item.item == item)
            {
                if (AbleToBuy(_item.price))
                {
                    switch (_item.item)
                    {
                        case shopItems.health:
                            Health playerHealthScript = GameManager.instance.playerRef.GetComponent<Health>();
                            playerHealthScript.ModifyHealth(playerHealthScript.m_StartingHealth - playerHealthScript.hitpoints);
                            break;
                        case shopItems.ammo:
                            ResourceManager.instance.ModifyPlayerAmmo(_item.amount);
                            break;
                        case shopItems.upgrade:
                            PlayerController playerControllerScript = GameManager.instance.playerRef.GetComponent<PlayerController>();
                            playerControllerScript.UpgradeWeapon(_item.amount);
                            m_UpgradeButtonUI.SetActive(false);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    public bool AbleToBuy(int amount)
    {
        if (Mathf.Sign(amount) > 0)
        {
            Debug.LogWarning("Price needs to be a negative value.");
            return false;
        }
        else if (Mathf.Sign(amount) < 0)
        {
            if (Mathf.Abs(amount) <= ResourceManager.instance.m_Coins)
            {
                ResourceManager.instance.ModifyCoins(amount);
                return true;
            }
        }
        Debug.LogWarning("Something went wrong while trying to buy something.");
        return false;
    }

    public void OpenShop()
    {
        m_ShopInteractKeyUI.SetActive(false);
        InterfaceManager.instance.ShowShopUI(true);
        Time.timeScale = 0f;
    }

    public void CloseShop()
    {
        InterfaceManager.instance.ShowShopUI(false);
        m_ShopInteractKeyUI.SetActive(true);
        Time.timeScale = 1f;
    }

    [Serializable]
    public struct shopItem
    {
        public shopItems item;
        public int price;
        public int amount;
    }
}
