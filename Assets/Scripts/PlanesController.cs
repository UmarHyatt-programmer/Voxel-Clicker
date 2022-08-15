using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanesController : MonoBehaviour
{
    public float speed;
   public Transform p2;
   public Vector3 offset,initPos;
   public bool move,addOffSet,rotate;
    void Start()
    {
       if(addOffSet)
      offset=transform.position-p2.transform.position;
    }
    private void OnEnable() {
        initPos=transform.position;
    }
    void Update()
    {
        if(move)
        {
            transform.position=Vector3.MoveTowards(transform.position,p2.position+offset,speed*Time.deltaTime);   
            if(transform.position==p2.position)
            {
                gameObject.SetActive(false);
            }    
        }
        if(rotate)
        {
            transform.rotation=new Quaternion(transform.rotation.x,Mathf.Lerp(transform.rotation.y,p2.rotation.y,1),transform.rotation.z,0);
        }
    }
    private void OnDisable() 
    {
        transform.position=initPos;
    }
}
