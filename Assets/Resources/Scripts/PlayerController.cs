using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Cannon Info")]
    public CannonballData projectileData;
    public int m_StartingAmmo = 20;
    public float m_CannonReloadFPS;
    public int m_UpgradePhase = 0;

    [Header("Cannon Objects")]
    public Cannon[] m_CannonsPhase0;
    public Cannon[] m_CannonsPhase1;
    public List<Cannon> m_ActiveCannons = new List<Cannon>();

    [Header("Movement")]
    public int m_MovementSpeed;
    public int m_TurnSpeed;

    [Header("")]

    private Rigidbody rb;
    private float cannonReloadTimer;

    void Start()
    {
        GameManager.instance.playerRef = gameObject;

        foreach (Cannon cannonObject in m_CannonsPhase0)
        {
            m_ActiveCannons.Add(cannonObject);
        }

        ResourceManager.instance.ModifyPlayerAmmo(m_StartingAmmo);
        rb = gameObject.GetComponent<Rigidbody>();
        cannonReloadTimer = m_CannonReloadFPS;
    }

    void Update()
    {
        if (InterfaceManager.instance.m_VictoryUI.activeSelf)
            return;

        cannonReloadTimer = Mathf.Clamp(cannonReloadTimer - Time.deltaTime, 0f, m_CannonReloadFPS);
        if (Input.GetKey(KeyCode.Space) && cannonReloadTimer <= 0)
        {
            FireCannons(m_ActiveCannons);
            cannonReloadTimer = m_CannonReloadFPS;
        }

        float xDir = Input.GetAxis("Horizontal");
        float zDir = Input.GetAxis("Vertical");
        if (zDir < 0)
            zDir = 0;

        rb.velocity = transform.forward * (zDir * m_MovementSpeed);
        rb.angularVelocity = new Vector3(0f, xDir * m_TurnSpeed, 0f);

        transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
    }

    public void UpgradeWeapon(int upgradeAmount)
    {
        if (m_UpgradePhase == 0)
        {
            foreach (Cannon _cannon in m_CannonsPhase1)
            {
                m_ActiveCannons.Add(_cannon);
                _cannon.cannon.SetActive(true);
            }
        }
        m_UpgradePhase += upgradeAmount;
    }

    private void FireCannons(List<Cannon> cannons)
    {
        foreach (Cannon cannon in cannons)
        {
            if (ResourceManager.instance.m_PlayerAmmo > 0)
            {
                GameObject _cannon = GameManager.instance.cannonballPool.GetObjectFromPool();
                _cannon.transform.position = cannon.shootPoint.position;
                _cannon.transform.rotation = cannon.shootPoint.rotation;
                _cannon.GetComponent<CannonBall>().Initialize(projectileData);
                ResourceManager.instance.ModifyPlayerAmmo(-1);
            }
        }

        //Sound???
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageble collider = collision.gameObject.GetComponent<IDamageble>();
        if (collider != null)
        {
            collider.ModifyHealth(-GameManager.instance.m_CrashDamage);
        }
    }

    [Serializable]
    public struct Cannon
    {
        public GameObject cannon;
        public Transform shootPoint;
    }
}
