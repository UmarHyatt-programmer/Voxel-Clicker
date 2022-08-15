using System.Collections;
using UnityEngine;

public class CivilianController : MonoBehaviour
{
    public Transform destinationObject;
    public Transform[] raws;
    public LayerMask layer;
    public float deltaDistance, visibleTime, rayRange = 10;
    bool b = true;
    bool isDetect = true;
    private void OnEnable()
    {
        destinationObject = transform;
        int x = Random.Range(0, CivilianSpawner.instance.spawnPoint.Length);
        transform.position = CivilianSpawner.instance.spawnPoint[x].transform.position;
        transform.position = new Vector3(transform.position.x, 2.47f, transform.position.z);
        destinationObject = CivilianSpawner.instance.DistanceFind(transform);
        transform.LookAt(destinationObject);
    }
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
    public void Update()
    {
        transform.position += transform.forward * deltaDistance * Time.deltaTime;
        if (Mathf.Abs(transform.position.z - destinationObject.position.z) < 2)
        {
            MoveOut(ref b);
        }
          RayDetector();
    }
    void MoveOut(ref bool x)
    {
        if (x)
        {
            //      print(Random.Range(0,4));
            transform.eulerAngles = Vector3.zero;
            transform.eulerAngles = new Vector3(0, Random.Range(0, 4) * 90, 0);
            //transform.localRotation=new Quaternion(0,Random.Range(0,4)*90,0,0);
        }
        x = false;
        // transform.Rotate(0,Random.Range(0,4)*90,0);
        // transform.rotation=Random.rotation;
        // transform.position=Vector3.MoveTowards(transform.position,destinationObject.position,deltaDistance*Time.deltaTime);
    }
    IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(visibleTime);
        gameObject.SetActive(false);
    }
    void OnDisable()
    {
        b = true;
        isDetect=true;
        //  print("OnDisable Called ");
        //  gameObject.SetActive(true);
    }


    RaycastHit hit;

    public void RayDetector()
    {
        Debug.DrawRay(transform.position + Vector3.up, transform.forward * hit.distance, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayRange, layer))
        {
            if (hit.collider.CompareTag("Player"))
            {
              if(isDetect)
              {
                isDetect=false;
               transform.eulerAngles = new Vector3(transform.eulerAngles.x, -transform.eulerAngles.y, transform.eulerAngles.z);
              }
              else
              {
                transform.eulerAngles += Vector3.up * 50;
              }
               // isDetect = false;
                //transform.eulerAngles += Vector3.up * 50;
            }
           if (hit.collider.GetComponent<BuildingHealth>())
            {
               // isDetect = false;
                transform.eulerAngles += Vector3.up * 50;
              //  transform.eulerAngles = new Vector3(transform.eulerAngles.x, -transform.eulerAngles.y, transform.eulerAngles.z);
            }
//            Debug.Log(hit.collider.name);
        }
        else
        {
         //   Debug.Log("Not hit anywhere");
        }
    }
}
