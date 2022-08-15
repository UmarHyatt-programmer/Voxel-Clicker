using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadProgress : MonoBehaviour
{
    public static LoadProgress instance;
    private void Awake() {
        if(instance==null)
        {
          //  GF_SaveLoad.LoadProgress();
            instance=this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.LoadScene(1);
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
