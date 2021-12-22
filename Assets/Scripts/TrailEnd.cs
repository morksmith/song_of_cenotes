using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEnd : MonoBehaviour
{
    public float Lifetime;
    private float timer;
    private TrailRenderer trail;
    // Start is called before the first frame update
    void Start()
    {
        trail = GetComponent<TrailRenderer>();
        trail.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < Lifetime)
        {
            timer+=Time.deltaTime;
        }
        else
        {
            trail.emitting = false;
        }
    }

    public void StartTrail()
    {
        timer = 0;
        trail.emitting = true;
    }
}
