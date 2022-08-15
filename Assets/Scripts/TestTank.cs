using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTank : MonoBehaviour
{
    public float moveSpeed;
    public float bodyRotationSpeed;
    public GameObject turret;
    public float speed;

    public GameObject lookRef;
    public GameObject enemy;

    bool testing;
    float temp;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.gameObject.transform.position += this.gameObject.transform.TransformVector(Vector3.forward) * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.gameObject.transform.position -= this.gameObject.transform.TransformVector(Vector3.forward) * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.Rotate(Vector3.up, -this.bodyRotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.Rotate(Vector3.up, this.bodyRotationSpeed * Time.deltaTime);
        }

        
        lookRef.transform.LookAt(enemy.transform);

        temp = lookRef.transform.rotation.y;

        //turret.transform.rotation = Quaternion.Euler(turret.transform.rotation.x, temp, turret.transform.rotation.z);
        turret.transform.Rotate(lookRef.transform.rotation.x, lookRef.transform.rotation.y, lookRef.transform.rotation.z);
    }
}
