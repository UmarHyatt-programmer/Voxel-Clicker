using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Button continueButton;
    public GameObject backPanel;
    public GameObject[] tutorialObject;
    public Text tutorialText;
    [TextArea(3, 3)]
    public string[] Topic;
    public int index = 0;
    void OnEnable()
    {
        MenuHandler.Instance.isStart=false;
        CallWriter();
    }

    public void CallWriter()
    {
        if (index >= Topic.Length)
        {
            MenuHandler.Instance.isStart=true;
            this.gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(Writer(Topic[index]));
            OutLine();
        }
        index++;
    }
    IEnumerator Writer(string txt)
    {
        tutorialText.text = "";
        continueButton.interactable=false;
        foreach (char item in txt)
        {
            tutorialText.text += item.ToString();
            yield return new WaitForSecondsRealtime(0.05f);
        }
        continueButton.interactable=true;
    }
    public void OutLine()
    {
       for (int i = 0; i < tutorialObject.Length; i++)
       {
           if(i==index)
           {
            tutorialObject[index].SetActive(true);
           }
           else
           {
           tutorialObject[i].SetActive(false);
           }
       }
    }

}
