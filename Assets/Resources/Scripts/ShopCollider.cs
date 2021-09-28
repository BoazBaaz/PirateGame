using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
          ShopManager.instance.m_ShopInteractKeyUI.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            ShopManager.instance.OpenShop();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ShopManager.instance.m_ShopInteractKeyUI.SetActive(false);
        }
    }
}
