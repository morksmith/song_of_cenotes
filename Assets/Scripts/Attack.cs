using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float Damage;
    public float KnockBack = 2;
    public float Speed = 1;
    
    public void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>()!= null)
        {
            other.GetComponent<Rigidbody>().AddExplosionForce(KnockBack * 200, transform.position, 2);
            other.GetComponent<Enemy>().TakeDamage(Damage);
        }
    }
}

