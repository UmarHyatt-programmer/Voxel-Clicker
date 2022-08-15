using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuHandler : MonoBehaviour
{
    public GameObject tutorialPanel, tapTutorialPanel;
    public static MenuHandler Instance;

    public GameObject playPanel;

    public Toggle soundFX;
    public Toggle bgFX;
    public Toggle vibration;
    public Text playerId;
    public bool isStart;

    public Image sfxImage, bgSoundImage, vibrationImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void Start()
    {
        if (UIManager.Instance.levelNumber == 0)
        {
            tapTutorialPanel.SetActive(true);
        }
        //Time.timeScale = 0;
        GameManager.instance.sfx = PlayFabManager.Instance.sv.sfx;
        GameManager.instance.bgMusic = PlayFabManager.Instance.sv.bgMusic;
        GameManager.instance.vibration = PlayFabManager.Instance.sv.vibration;

        soundFX.isOn = GameManager.instance.sfx;
        bgFX.isOn = GameManager.instance.bgMusic;
        vibration.isOn = GameManager.instance.vibration;

        GameManager.instance.sfx = soundFX.isOn;
        GameManager.instance.bgMusic = bgFX.isOn;
        GameManager.instance.vibration = vibration.isOn;

        sfxImage.gameObject.GetComponent<Button>().interactable = soundFX.isOn;
        bgSoundImage.gameObject.GetComponent<Button>().interactable = bgFX.isOn;
        vibrationImage.gameObject.GetComponent<Button>().interactable = vibration.isOn;

        LevelManager.instance.bgMusic.SetActive(bgFX.isOn);

    }
    public void SetPlayerID()
    {
        playerId.text = "id: " + PlayFabManager.Instance.playerPlayfabId;
    }
    public void LoadGame()
    {
        print("Load Game Call");
        isStart = true;
        //  tutorialPanel.SetActive(true);
        //Time.timeScale=1;
        //  ReferenceManager.Instance.isFirstLoad = false;
        PlayFabManager.Instance.sv.sfx = GameManager.instance.sfx;
        PlayFabManager.Instance.sv.vibration = GameManager.instance.vibration;
        PlayFabManager.Instance.sv.bgMusic = GameManager.instance.bgMusic;


        //u*   GF_SaveLoad.SaveProgress();
        // SceneManager.LoadScene("Gameplay");
        //SceneManager.LoadScene("Loading");
        UIManager.Instance.back();
    }

    public void SoundFX()
    {
        if (GameManager.instance.sfx)
        {
            GameManager.instance.sfx = false;
            sfxImage.gameObject.GetComponent<Button>().interactable = false;
            PlayFabManager.Instance.sv.sfx = false;


        }
        else
        {
            GameManager.instance.sfx = true;
            sfxImage.gameObject.GetComponent<Button>().interactable = true;
            PlayFabManager.Instance.sv.sfx = true;
        }
        PlayFabManager.Instance.setPlayerData();
    }

    public void Vibration()
    {
        if (GameManager.instance.vibration)
        {
            GameManager.instance.vibration = false;
            vibrationImage.gameObject.GetComponent<Button>().interactable = false;
            PlayFabManager.Instance.sv.vibration = false;
        }
        else
        {
            GameManager.instance.vibration = true;
            vibrationImage.gameObject.GetComponent<Button>().interactable = true;
            PlayFabManager.Instance.sv.vibration = true;

        }
        PlayFabManager.Instance.setPlayerData();
    }

    public void BGMusic()
    {
        if (GameManager.instance.bgMusic)
        {
            LevelManager.instance.bgMusic.SetActive(false);
            GameManager.instance.bgMusic = false;
            bgSoundImage.gameObject.GetComponent<Button>().interactable = false;
            PlayFabManager.Instance.sv.bgMusic = false;
            print("BGMusic goes to false");
        }
        else
        {
            LevelManager.instance.bgMusic.SetActive(true);
            GameManager.instance.bgMusic = true;
            bgSoundImage.gameObject.GetComponent<Button>().interactable = true;
            PlayFabManager.Instance.sv.bgMusic = true;
        }
      //  PlayFabManager.Instance.sv.bgMusic = false;
        PlayFabManager.Instance.setPlayerData();
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

}
