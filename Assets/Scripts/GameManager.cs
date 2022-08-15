using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public int numberOfLevel;

    public int test;

    public bool bgMusic = true;
    public bool vibration = true;
    public bool sfx = true;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
       // PlayFabManager.Instance.sv = new SaveData();
       //u* GF_SaveLoad.LoadProgress();
        if (PlayFabManager.Instance.sv.numberOfLevel <= 49)
        {
            numberOfLevel = PlayFabManager.Instance.sv.numberOfLevel;
        }
        else
        {
            numberOfLevel = 0;
            UIManager.Instance.comingSoonPanel.SetActive(true);
            Time.timeScale=0;
        }
    }
}
