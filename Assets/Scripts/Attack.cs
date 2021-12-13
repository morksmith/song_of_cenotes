using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>()!= null)
        {
            other.GetComponent<Rigidbody>().AddExplosionForce(200, transform.position, 2);
        }
    }
}

