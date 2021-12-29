using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColour : MonoBehaviour
{
    public Color Colour1;
    public Color Colour2;

    private Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.SetColor("_Color", Color.Lerp(Colour1, Colour2, Random.value));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
