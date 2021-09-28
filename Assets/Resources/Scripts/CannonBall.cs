using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CannonBall : MonoBehaviour
{
    [HideInInspector]
    private CannonballData objectData;
    private Rigidbody rb;
    private float lifeTime;
    private float speed;

    public void Initialize(CannonballData _projectileData)
    {
        objectData = _projectileData;
        rb = gameObject.GetComponent<Rigidbody>();

        gameObject.layer = (int)objectData.ownerLayer;
        lifeTime = objectData.lifeTime;
        speed = objectData.speed;
        StartCoroutine(Lifetime());
    }

    IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(lifeTime);

        GameManager.instance.cannonballPool.DeactivateObject(gameObject);

        yield return null;
    }

    private void Update()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageble collider = collision.gameObject.GetComponent<IDamageble>();
        if (collider != null)
        {
            collider.ModifyHealth(-objectData.damageDealt);
        }

        //Explotion

        GameManager.instance.cannonballPool.DeactivateObject(gameObject);
    }
}
