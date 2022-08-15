using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataHandler : MonoBehaviour
{

    public static DataHandler instance;
    public List<GameObject> Tanks;

    public float coins = 100;

    public int earnings = 1;
    public Text earningText;

    public int speedUp = 1;
    public Text speedupText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
        Debug.Log("new save data "+PlayFabManager.Instance.sv.numberOfLevel);
        }
    }

    bool levelComplete = false;
    public void tankLevelCheck()
    {
        //for(int i =0; i < Tanks.Count; i++)
        //{
        //    if (Tanks[i].GetComponent<PlayerAIController>().levelComplete)
        //    {
        //        levelComplete = true;
        //    }
        //    else
        //    {
        //        levelComplete = false;
        //        break;
        //    }
        //}
       

        if(/*levelComplete*/ Tanks[0].GetComponent<PlayerAIController>().levelComplete)
        {
            //mychange if condition because its run on update
            if(!UIManager.Instance.levelCompletePanel.activeInHierarchy)
            {
            MopubMediation.Instance.ShowInterstitial();
            UIManager.Instance.levelCompletePanel.SetActive(true);
            UIManager.Instance.levelPanel.SetActive(false);
            UIManager.Instance.endLevel.text = "Level " + (UIManager.Instance.levelNumber + 1).ToString();
           // UIManager.Instance.finalcoins.text = UIManager.Instance.coinText.text;
            UIManager.Instance.finalcoins.text = "You Got "+coins.ToString()+" Coins";
            UIManager.Instance.particleEffects.SetActive(true);
            }
//            print("level Complete Called");
        }

    }
}
