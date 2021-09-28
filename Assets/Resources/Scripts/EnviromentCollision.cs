using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //For if the object hits an object with an IDamageble.
        IDamageble collider = gameObject.GetComponent<IDamageble>();
        if (collider != null)
        {
            if (collision.gameObject.layer == 10)
                collider.ModifyHealth(-gameObject.GetComponent<Health>().hitpoints);
        }
    }
}
