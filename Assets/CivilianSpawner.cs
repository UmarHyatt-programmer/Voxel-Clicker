using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianSpawner : MonoBehaviour
{
    public static CivilianSpawner instance;
    public GameObject[] civilian, spawnPoint;
    public int activateTime;
    public Transform[] raws;
    int y=1;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Function(out y);
    }
   static void Function(out int x)
    {
        x=10;
    }
    private void Start()
    {
        StartCoroutine(OnObjects());
    }
    IEnumerator OnObjects()
    {
        yield return new WaitForSeconds(2);
        spawnPoint = GameObject.FindGameObjectsWithTag("simpleBuilding");
        while (true)
        {
            int x = 0;
            x = Random.Range(0, civilian.Length);
          //  y = Random.Range(0, civilian.Length);
            for(int i=0;i<3;i++)
            {
                if(!civilian[x].activeInHierarchy)
                {
                civilian[x].SetActive(true);
                break;
                }
                x = Random.Range(0, civilian.Length);
                // if(!civilian[y].activeInHierarchy)
                // {
                // yield return new WaitForSeconds(0.2f);
                // civilian[y].SetActive(true);
                // }
               // y = Random.Range(0, civilian.Length);
            }
            //civilian[x].SetActive(true);
            yield return new WaitForSeconds(activateTime);
        }
        // for(int i=0;i<civilian.Length;i++)
        // {
        //     if(!civilian[i].activeInHierarchy)
        //     {
        //       civilian[i].SetActive(true);
        //     }
        //     yield return new WaitForSeconds(activateTime);
        // }
    }
    public void CivilianEnable()
    {

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
                nearestObject = nearestRaw.GetChild(0);
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
