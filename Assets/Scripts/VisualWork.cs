using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualWork : MonoBehaviour
{
    public GameObject plane,heli,burnPartical;
    public static VisualWork instance;
    private void Awake() {
        if(instance==null)
        {
            instance=this;
            var n=gameObject.name;
            var n1=name;
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
    }
    public void SmokeWhenDistroy(Transform t)
    {
        Instantiate(burnPartical,new Vector3(t.position.x,2.7f,t.position.z),Quaternion.identity);
    }
}
