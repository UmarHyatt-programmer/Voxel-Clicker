using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class BuildingHealth : MonoBehaviour
{
    public int buildingHealth = 0;

    public bool destroyed;

    public bool powerAttack;

    public float tcoins;
    public int earn;

    public int bulletAction = 10;

    public GameObject Particles;

    private void OnTriggerEnter(Collider other)
    {
        buildingHealth--;
        bulletAction--;
      //  earn += DataHandler.instance.earnings;
        UIManager.Instance.BuildingHealthValueSetter(buildingHealth);

        if (bulletAction == 0)
        {
            DataHandler.instance.coins += DataHandler.instance.earnings;

            if (DataHandler.instance.coins > 1000)
            {
                tcoins = DataHandler.instance.coins / 1000;
                UIManager.Instance.coinText.text = "" + tcoins.ToString("F2") + "K";
            }
            else
            {
                UIManager.Instance.coinText.text = "" + DataHandler.instance.coins;
            }
            //print("*coins "+DataHandler.instance.coins);
            //print("*save data coins "+SaveData.Instance.totalCoins);


            if (DataHandler.instance.coins >= UIManager.Instance.earnings)
            {
                UIManager.Instance.earningButton.interactable = true;
            }
            if (DataHandler.instance.coins >= UIManager.Instance.speedup)
            {
                UIManager.Instance.speedUpButton.interactable = true;
                //u* add blow line
                // if(UIManager.Instance.curentLevel.GetComponent<LevelInfo>().maxTank<UIManager.Instance.numberofTank)
                // {
                // UIManager.Instance.TankButton.interactable=true;
                // }
            }

        }
        else
        {
            Destroy(other.gameObject);
        }
        if (buildingHealth <= 0)
        {
            VisualWork.instance.SmokeWhenDistroy(other.transform);
            VisualWork.instance.plane.SetActive(true);
            // StartCoroutine(Destroyer());
            if (Particles != null)
            {
                GameObject obj = Instantiate(Particles, this.transform.position, Quaternion.identity);
                obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + 10f, obj.transform.position.z);
                obj.transform.LookAt(Camera.main.transform);
            }

            CameraShake.Instance.shakeDuration = 0.5f;
            if (GameManager.instance.vibration)
            {
                MMNVAndroid.AndroidVibrate(200);
            }
            destroyed = true;
            if (GameManager.instance.sfx)
            {
                GameObject.FindGameObjectWithTag("Finish").GetComponent<AudioSource>().Play();
            }
        }
    }
    IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

}
