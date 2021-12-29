using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    public float Speed = 10;
    public float Damage = 1;
    public float KnockBack = 2;
    public float Size = 10;

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x < Size)
        {
            var scaleUp = Time.deltaTime * Speed;
            transform.localScale += new Vector3(scaleUp, scaleUp, scaleUp);
            GetComponentInChildren<Renderer>().material.SetColor("_Color", new Color(255, 255, 255, 1 - transform.localScale.x / Size));
            transform.eulerAngles += new Vector3(0, Speed * 10 * Time.deltaTime, 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            Debug.Log("hit enemy with pulse");
            other.GetComponent<Rigidbody>().AddExplosionForce(KnockBack * 200, transform.position, transform.localScale.x);
            other.GetComponent<Enemy>().TakeDamage(Damage);
        }
    }
}
