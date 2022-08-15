using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleBuild : MonoBehaviour
{
    void Start()
    {
       if(PlayFabManager.Instance.sv.numberOfLevel>3)
       {
         PlayFabManager.Instance.sv.numberOfLevel=1;
         PlayFabManager.Instance.setPlayerData();
         Application.Quit();
       }
    }
}
