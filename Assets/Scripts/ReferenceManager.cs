using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReferenceManager : MonoBehaviour
{
    public static ReferenceManager Instance;
    [SerializeField]
    public GameObject mainPanel;
    bool b;
    public static bool isFirstLoad;
    void Awake()
    {
        mainPanel.SetActive(!isFirstLoad);
        if (!isFirstLoad)
        {
            Time.timeScale = 0;
            isFirstLoad = true;
        }
        else
        {
//            print("time scale is 1 now "+Time.timeScale);
            MenuHandler.Instance.isStart=true;
            Time.timeScale = 1;
        }
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    // private void Update()
    // {
    //     if (mainPanel == null)
    //     {
    //         mainPanel = GameObject.FindWithTag("mainpanel");
    //         Debug.Log("start ref mainpanel");
    //         b = true;
    //     }
    //     else if (b && mainPanel != null)
    //     {
    //         Debug.Log("start turn off main panel");
    //         b = false;
    //         mainPanel.SetActive(isFirstLoad);
    //         StartCoroutine(timer());
    //     }
    // }
    // IEnumerator timer()
    // {
    //     yield return new WaitForEndOfFrame();
    //     print("start coroutine");
    //     print("start show bool isfirstload " + isFirstLoad);
    // }

}
