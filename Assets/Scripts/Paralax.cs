using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    public Transform TargetTransform;
    public float Translation = 0.1f;
    void Update()
    {
        transform.position = new Vector3(TargetTransform.position.x * Translation, TargetTransform.position.y * Translation, transform.position.z);
    }
}
