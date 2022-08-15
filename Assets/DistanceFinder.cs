using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceFinder : MonoBehaviour
{
    public static DistanceFinder instance;
    public Transform[] raws;
    public Transform rawnear;
    private void Awake()
    {
        if (instance = null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        rawnear = DistanceFind(transform);
    }
    public Transform DistanceFind(Transform TransObject)
    {
        Transform nearestObject = null;
        float nearestDistance = Vector3.Distance(TransObject.position, raws[0].position);
        Transform nearestRaw = raws[0];
        int i = 0;
        for (i = 0; i < raws.Length; i++)
        {
            var dis = Vector3.Distance(TransObject.position, raws[i].position);
            if (dis < nearestDistance)
            {
                nearestDistance = dis;
                nearestRaw = raws[i];
            }
            if (i >= raws.Length - 1)
            {
                nearestDistance = Vector3.Distance(TransObject.position, nearestRaw.GetChild(0).position);
                for (int j = 0; j < nearestRaw.childCount; j++)
                {
                    dis = Vector3.Distance(TransObject.position, nearestRaw.GetChild(j).position);
                    if (dis < nearestDistance)
                    {
                        nearestDistance = dis;
                        nearestObject = nearestRaw.GetChild(j);
//                        print("nearest object name is " + nearestRaw.GetChild(j).name);
                    }
                }
            }
        }
        return nearestObject;
    }
}
