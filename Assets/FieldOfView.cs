using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
   public float viewRadius;
   [Range(0,360)]
   public float viewAngle;
   public LayerMask targetMask,obstacleMask;
   void FindVisibleTargets()
   {
       Collider[] targetsViewRadius=Physics.OverlapSphere(transform.position,viewRadius,targetMask);
   }
  public Vector3 DirFormAngle(float angleIndegree,bool angleIsGlobal)
  {
      if(!angleIsGlobal)
      {
        angleIndegree+=transform.eulerAngles.y;
      }
    return new Vector3(Mathf.Sin(angleIndegree*Mathf.Deg2Rad),0,Mathf.Cos(angleIndegree*Mathf.Deg2Rad));
  }
}
