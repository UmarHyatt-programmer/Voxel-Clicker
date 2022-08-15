using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class Loader : MonoBehaviour
{
    public float speed = 0.1f;
    float progress;
    public Slider loader;
    AsyncOperation op;

    float x = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadLevell());
      // loadingLevel();
    }

    IEnumerator LoadLevell()
    {
        op = SceneManager.LoadSceneAsync("Gameplay");
        while (!op.isDone)
        {
            progress = Mathf.Clamp01(op.progress / .9f);
            loader.value += progress;
            yield return null;
        }
    }
    async void loadingLevel()
    {
        await Task.Delay(500);
        x += 0.1f;
        loader.value += x;
        if (x < 1)
        {
            loadingLevel();
        }
        else
        {
            SceneManager.LoadScene("Gameplay");
           //SceneManager.LoadSceneAsync("GamePlay");
        }
    }
}
