using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class PlayFabManager : MonoBehaviour
{
    public GameObject appleLoginButton, loading;
    public static PlayFabManager Instance;
    public Text errorLog;
    public string titleid;
    public string playerPlayfabId;
    public SaveData sv;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayFabSettings.TitleId = titleid;
        LoainWithPlayFAb();
    }
    private void LoainWithPlayFAb()
    {
        loginPlayFab();
    }
    void OnError(PlayFabError error)
    {
        Debug.LogError(error);
    }
    public void loginPlayFab()
    {
#if UNITY_EDITOR

        print("login Editor");
        var request = new LoginWithCustomIDRequest { CustomId = SystemInfo.deviceUniqueIdentifier, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, success, OnError);
#elif UNITY_IOS
        print("loginWithApple");
#endif
    }


    public void AppleLoginWithPlayfab(string identityToken)
    {
        var req = new LoginWithAppleRequest
        {
            IdentityToken = identityToken,
            CreateAccount = true,
            TitleId = PlayFabSettings.TitleId
        };
        PlayFabClientAPI.LoginWithApple(req, res =>
        {
            PlayerPrefs.SetInt("IsLoginWithApple", 1);
            Debug.Log("Apple login with playfab success");
            success(res);
        }, error =>
         {
             PlayerPrefs.SetInt("IsLoginWithApple", 0);
             appleLoginButton.SetActive(true);
             AppleUserLogin.instance.LogOutApple();
             Debug.Log("Apple login with playfab failed");
             errorLog.text = "Login Failed";
         });
    }
    private void success(LoginResult obj)
    {
        playerPlayfabId = obj.PlayFabId;

        if (obj.NewlyCreated)
        {
            print("* newly created");
            setPlayerData();
            StartCoroutine(LoadScene());
        }
        else
        {
            //            print("* get old data created");
            getPlayerData();
        }
    }
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }
    public void getPlayerData()
    {
        var request = new GetUserDataRequest
        {
            PlayFabId = playerPlayfabId
        };
        PlayFabClientAPI.GetUserData(request, OnDataRecieved, OnError);
        //    await Task.Delay(5000);
    }
    private void OnDataRecieved(GetUserDataResult obj)
    {
        if (obj.Data != null)
        {
            if (obj.Data.ContainsKey("SaveData"))
            {
                sv = JsonUtility.FromJson<SaveData>(obj.Data["SaveData"].Value);
                //SaveData.Instance = JsonUtility.FromJson<SaveData>(obj.Data["SaveData"].Value);                
                //                Debug.Log("has Data");
            }
        }
        SceneManager.LoadScene(1);
    }
    public void setPlayerData()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                //{"SaveData", JsonUtility.ToJson(SaveData.Instance)}
                {"SaveData", JsonUtility.ToJson(sv)}
            },
            Permission = UserDataPermission.Public
        };
        //        print("* Set player data");
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }
    private void OnDataSend(UpdateUserDataResult obj)
    {
        Debug.Log("Data sent");
    }

    public void LogoutPlayFab()
    {
        PlayFabClientAPI.ForgetAllCredentials();
    }
}

