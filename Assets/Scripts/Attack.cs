using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float KnockBack = 2;
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>()!= null)
        {
            other.GetComponent<Rigidbody>().AddExplosionForce(KnockBack * 200, transform.position, 2);
        }
    }
}

