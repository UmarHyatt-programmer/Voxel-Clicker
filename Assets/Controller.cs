using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Rigidbody rb;
    public float speed, maxSpeed, drag,rotationSpeed;
    Vector3 move;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        HorRotation();

    }
    void Movement()
    {
        if (rb.velocity.z < maxSpeed)
        {
            rb.velocity += Vector3.forward * speed * Time.deltaTime;
        }
        else
        {
            rb.velocity -= Vector3.forward * Time.deltaTime * drag;
        }
    }
    void HorRotation()
    {
        //if(horizontal)
        {
            rb.angularVelocity += Vector3.up * Time.deltaTime*rotationSpeed;
        }
        //if(-horizontal)
        {
            rb.angularVelocity -= Vector3.up * Time.deltaTime*rotationSpeed;
        }
    }
}
