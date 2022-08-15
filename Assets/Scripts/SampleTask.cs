using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SampleTask : MonoBehaviour
{
   public Button restartButton;

   private void Start() {
       restartButton.onClick.AddListener(Restart);
   }
   void Restart()
   {
    SceneManager.LoadScene(0);
   }
}
