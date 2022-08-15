using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public float rotationSpeed=1;
    public Vector3 angle;
    void Update()
    {
        transform.Rotate(angle*Time.deltaTime*rotationSpeed);
    }
}
