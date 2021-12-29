using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 10;
    public float Damage = 1;
    public float LifeTime = 4;
    public float KnockBack = 1;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        transform.position += transform.forward * Speed * Time.deltaTime;
        if(timer > LifeTime)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().TakeDamage(Damage);
            other.GetComponent<Rigidbody>().AddExplosionForce(KnockBack * 200, transform.position, transform.localScale.x);
            Destroy(gameObject);
        }
        else
        {
            if(other.tag == "Wall")
            {
                Destroy(gameObject);
            }
        }
    }
}
