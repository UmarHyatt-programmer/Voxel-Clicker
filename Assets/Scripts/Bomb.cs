using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject boomEffect;
    public bool effect;
    // Start is called before the first frame update

    private void Update()
    {
        // transform.rotation = Quaternion.Euler(0f, Mathf.Lerp(transform.rotation.eulerAngles.y, 0, Time.deltaTime * 20), 0f);
        transform.position = Vector3.MoveTowards(transform.position, UIManager.Instance.mainSoldier.GetComponent<PlayerAIController>().building.transform.position, Time.deltaTime * 80);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("building"))
        {
            //other.transform.GetChild(other.transform.childCount - 2).GetComponent<VoxelBomb>().triggered = true;
            GameObject bmb = new GameObject("Bomb");
            bmb.transform.position = other.gameObject.transform.GetChild(1).transform.position;
            bmb.AddComponent<SphereCollider>();
            VoxelBomb bb =  bmb.AddComponent<VoxelBomb>();
            bb.explosionRadius = 8;
            bb.triggered = true;

            if (effect)
            {
                Instantiate(boomEffect, this.transform.position, Quaternion.identity);
                if (GameManager.instance.sfx)
                {
                    GameObject.Find("BoomEffect").GetComponent<AudioSource>().Play();
                }
            }
            //VoxelBomb bomb = this.gameObject.GetComponent<VoxelBomb>();
            //if (bomb != null)
            //{
            //    bomb.triggered = true;
            //}
            Destroy(this.gameObject, 1);
        }
    }

}
