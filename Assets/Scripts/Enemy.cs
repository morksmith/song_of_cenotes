using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform Player;
    public float MaxHealth;
    public float Health;
    public float KnockBack = 100;
    public float Damage = 5;
    public float MoveSpeed = 5;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        rb.AddRelativeForce((Player.position - transform.position) * (MoveSpeed * 10) * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.transform  == Player)
        {
            Player.GetComponent<Stats>().TakeDamage(Damage);
            Player.GetComponent<Rigidbody>().AddRelativeForce((Player.position - transform.position) * (KnockBack * 1000) * Time.deltaTime, ForceMode.VelocityChange);
        }
    }
}
