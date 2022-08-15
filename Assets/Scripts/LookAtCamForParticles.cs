using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamForParticles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(Camera.main.transform);
    }
    
}
