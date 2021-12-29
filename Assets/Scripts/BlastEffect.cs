using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastEffect : MonoBehaviour
{
    public float Speed = 10;
    public float Size = 10;
    private float startSize;

    private void Start()
    {
        startSize = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < Size * startSize)
        {
            var scaleUp = Time.deltaTime * Speed;
            transform.localScale += new Vector3(scaleUp, scaleUp, scaleUp);
            GetComponentInChildren<Renderer>().material.SetColor("_Color", new Color(255, 255, 255, 1 - transform.localScale.x / Size));
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}
