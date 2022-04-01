using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform Target;
    public float Speed;
    public float Damage;
    public float KnockBack;

    private GameManager gm;

    // Update is called once per frame

    private void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if(gm.State != GameManager.GameState.Playing)
        {
            return;
        }
        transform.position += transform.forward * Speed * Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.transform == Target)
        {
            Target.GetComponent<Stats>().TakeDamage(Damage);
            Target.GetComponent<Rigidbody>().AddRelativeForce((Target.position - transform.position) * (KnockBack * 1000) * Time.deltaTime, ForceMode.VelocityChange);
            Destroy(gameObject);
        }
        else if (other.transform.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (other.transform.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}

