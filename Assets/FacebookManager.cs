using Facebook.Unity;
using System.Collections.Generic;
using UnityEngine;

public class FacebookManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {

        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            Debug.Log("facebook already Activated");

        }
        else
        {
            Debug.Log("facebook Initializing");
            //Handle FB.Init
            FB.Init(() =>
            {
                FB.ActivateApp();

                Debug.Log("facebook Initialized Complete");
                FB.LogAppEvent(
                AppEventName.ActivatedApp,
                null,
                new Dictionary<string, object>()
                {
                        { AppEventParameterName.Description, "User activates the App" }
                });
            });
        }
    }


    void OnHideUnity(bool isGameShown)
    {
        if (isGameShown)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    void ActivateAppCallback()
    {
        Debug.Log("facebook Activated");
    }
    void OnApplicationPause(bool pauseStatus)
    {
        // Check the pauseStatus to see if we are in the foreground
        // or background
        if (!pauseStatus)
        {
            //app resume
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            else
            {
                //Handle FB.Init
                FB.Init(() =>
                {
                    FB.ActivateApp();
                });
            }
        }
    }
}
