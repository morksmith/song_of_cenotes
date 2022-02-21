using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Transform player;
    private Vector3 newPosition;
    private bool moveToPlayer = false;
    public float StationaryTime = 2;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position + new Vector3(Random.Range(-5, 5), Random.Range(-5,5), 0);
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < StationaryTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            moveToPlayer = true;
        }
        if (!moveToPlayer)
        {
            if (Vector3.Distance(transform.position, newPosition) > 0.15f)
            {
                transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);
            }
            
        }
        else
        {
            if (Vector3.Distance(transform.position, player.position) > 0.25f)
            {
                transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * (10/ Vector3.Distance(transform.position, player.position)));
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
    }
}
