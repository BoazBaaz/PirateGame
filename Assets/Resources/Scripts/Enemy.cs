using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum enemyStates : int
    {
        surveillance = 0,
        attack
    }
    private enemyStates enemyState;

    [Header("Cannon Info")]
    public CannonballData m_ProjectileData;
    public float m_CannonReloadFPS;
    public List<Cannon> m_Cannons;

    [Header("Movement")]
    public int m_MovementSpeed;


    [Header("")]
    public int m_DistanceOffset;
    public int m_EnemySight;

    private Rigidbody rb;
    private NavMeshAgent navMeshAgent;
    private float cannonReloadTimer;

    public void Initialize()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();

        int ranInt = UnityEngine.Random.Range(0, GenerateWorld.instance.m_AvailableSpawnTiles.Count);
        Transform destination = GenerateWorld.instance.m_AvailableSpawnTiles[ranInt];

        navMeshAgent.SetDestination(destination.position);
        enemyState = enemyStates.surveillance;
        cannonReloadTimer = m_CannonReloadFPS;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);

        switch (enemyState)
        {
            case enemyStates.surveillance:
                if (navMeshAgent.destination == null)
                {
                    int ranInt = UnityEngine.Random.Range(0, GenerateWorld.instance.m_AvailableSpawnTiles.Count);
                    Transform destination = GenerateWorld.instance.m_AvailableSpawnTiles[ranInt];

                    navMeshAgent.SetDestination(destination.position);
                }

                var distToDes = Vector3.Distance(transform.position, navMeshAgent.destination);

                if (distToDes <= m_DistanceOffset)
                {
                    int ranInt = UnityEngine.Random.Range(0, GenerateWorld.instance.m_AvailableSpawnTiles.Count);
                    Transform destination = GenerateWorld.instance.m_AvailableSpawnTiles[ranInt];

                    navMeshAgent.SetDestination(destination.position);
                }
                break;
            case enemyStates.attack:
                navMeshAgent.SetDestination(GameManager.instance.playerRef.transform.position);

                cannonReloadTimer = Mathf.Clamp(cannonReloadTimer - Time.deltaTime, 0f, m_CannonReloadFPS);
                if (cannonReloadTimer <= 0f)
                {
                    FireCannons(m_Cannons);
                    cannonReloadTimer = m_CannonReloadFPS;
                }
                break;
            default:
                break;
        }


        var distToPlayer = Vector3.Distance(transform.position, GameManager.instance.playerRef.transform.position);

        if (distToPlayer <= m_EnemySight && GameManager.instance.playerRef.activeSelf)
            enemyState = enemyStates.attack;
        else
            enemyState = enemyStates.surveillance;
    }

    private void FireCannons(List<Cannon> cannons)
    {
        foreach (Cannon cannon in cannons)
        {
            GameObject _cannon = GameManager.instance.cannonballPool.GetObjectFromPool();
            _cannon.transform.position = cannon.shootPoint.position;
            _cannon.transform.rotation = cannon.shootPoint.rotation;
            _cannon.GetComponent<CannonBall>().Initialize(m_ProjectileData);
        }
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
