using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class MopubMediation : MonoBehaviour
{
    public static MopubMediation Instance;

    [Header("Banner")]
    public string[] _bannerAdUnits_Android;
    public string[] _bannerAdUnits_IOS;
    public MoPub.AdPosition bannerPosition;
    public MoPub.MaxAdSize bannerSize;

    [Header("Interstitial")]
    public string[] _interstitialAdUnits_Android;
    public string[] _interstitialAdUnits_IOS;

    [Header("Rewarded Video")]
    public string[] _rewardedVideoAdUnits_Android;
    public string[] _rewardedVideoAdUnits_IOS;

    /// <summary>
    /// Delete th LogText variable when in production
    /// </summary>
    //public Text LogText;

    void Awake()
    {
        Instance = this;
    }
    private void Start() {
        StartCoroutine("AdsTimer");
    }
    IEnumerator AdsTimer()
    {
        yield return new WaitForSeconds(2);
        RequestInterstitial();
    }
    public void LoadPluginsForAdUnits()
    {

#if UNITY_ANDROID
        if (_bannerAdUnits_Android.Length > 0)
            MoPub.LoadBannerPluginsForAdUnits(_bannerAdUnits_Android);

        if (_interstitialAdUnits_Android.Length > 0)
            MoPub.LoadInterstitialPluginsForAdUnits(_interstitialAdUnits_Android);

        if (_rewardedVideoAdUnits_Android.Length > 0)
            MoPub.LoadRewardedVideoPluginsForAdUnits(_rewardedVideoAdUnits_Android);
#elif UNITY_IOS

        if (_bannerAdUnits_IOS.Length > 0)
            MoPub.LoadBannerPluginsForAdUnits(_bannerAdUnits_IOS);

        if (_interstitialAdUnits_IOS.Length > 0)
            MoPub.LoadInterstitialPluginsForAdUnits(_interstitialAdUnits_IOS);

        if (_rewardedVideoAdUnits_IOS.Length > 0)
            MoPub.LoadRewardedVideoPluginsForAdUnits(_rewardedVideoAdUnits_IOS);

#endif

      //  RequestInterstitial();
      //  RequestRewardedAd();
    }

    private void OnEnable()
    {
        MoPubManager.OnInterstitialLoadedEvent += OnInterstitialLoadedEvent;
        MoPubManager.OnInterstitialFailedEvent += OnInterstitialFailedEvent;
        MoPubManager.OnInterstitialDismissedEvent += OnInterstitialDismissedEvent;

        MoPubManager.OnRewardedVideoLoadedEvent += OnRewardedVideoLoaded;
        MoPubManager.OnRewardedVideoFailedEvent += OnRewardedVideoFailed;
        MoPubManager.OnRewardedVideoShownEvent += OnRewardedVideoShown;
        //MoPubManager.OnConsentDialogLoadedEvent += OnConsentDialogLoaded;

    }

    private void OnDisable()
    {
        MoPubManager.OnInterstitialLoadedEvent -= OnInterstitialLoadedEvent;
        MoPubManager.OnInterstitialFailedEvent -= OnInterstitialFailedEvent;
        MoPubManager.OnInterstitialDismissedEvent -= OnInterstitialDismissedEvent;

        MoPubManager.OnRewardedVideoLoadedEvent -= OnRewardedVideoLoaded;
        MoPubManager.OnRewardedVideoFailedEvent -= OnRewardedVideoFailed;
        MoPubManager.OnRewardedVideoShownEvent -= OnRewardedVideoShown;
        //MoPubManager.OnConsentDialogLoadedEvent -= OnConsentDialogLoaded;
    }

    #region Banner
    public void RequestBanner()
    {
        #if UNITY_ANDROID
                MoPub.RequestBanner(_bannerAdUnits_Android[0], bannerPosition, bannerSize);
        #elif UNITY_IOS
                MoPub.RequestBanner(_bannerAdUnits_IOS[0], bannerPosition, bannerSize);
        #endif
    }

    public void ShowBanner(bool status)
    {
        #if UNITY_ANDROID
                MoPub.ShowBanner(_bannerAdUnits_Android[0], status);
        #elif UNITY_IOS
                MoPub.ShowBanner(_bannerAdUnits_IOS[0], status);
        #endif
    }

    public void DestroyBanner(bool status)
    {
        #if UNITY_ANDROID
                MoPub.DestroyBanner(_bannerAdUnits_Android[0]);
        #elif UNITY_IOS
                MoPub.DestroyBanner(_bannerAdUnits_IOS[0]);
        #endif
    }

    #endregion

    #region Interstitial
    public void RequestInterstitial()
    {
        #if UNITY_ANDROID
                MoPub.RequestInterstitialAd(_interstitialAdUnits_Android[0]);

        #elif UNITY_IOS
                         MoPub.RequestInterstitialAd(_interstitialAdUnits_IOS[0]);
        #endif
    }

    public void ShowInterstitial()
    {
#if UNITY_ANDROID
              MoPub.ShowInterstitialAd(_interstitialAdUnits_Android[0]);
#elif UNITY_IOS
              MoPub.ShowInterstitialAd(_interstitialAdUnits_IOS[0]);
#endif
        RequestInterstitial();
    }

    void OnInterstitialLoadedEvent(string adUnitId)
    {
        Debug.Log("OnInterstitialLoadedEvent " + adUnitId);
      //  LogText.text = "OnInterstitialLoadedEvent " + adUnitId;
    }

    void OnInterstitialFailedEvent(string adUnitId, string error)
    {
        Debug.Log("OnInterstitialFailedEvent " + error + " " + adUnitId);
       // LogText.text = "OnInterstitialFailedEvent " + error + " " + adUnitId;
    }

    void OnInterstitialDismissedEvent(string adUnitId)
    {
        Debug.Log("OnInterstitialDismissedEvent " + adUnitId);
      //  LogText.text = "OnInterstitialDismissedEvent " + adUnitId;
    }

#endregion

#region RewardedAd
    public void RequestRewardedAd()
    {
        #if UNITY_ANDROID
                MoPub.RequestRewardedVideo(_rewardedVideoAdUnits_Android[0]);
        #elif UNITY_IOS
                MoPub.RequestRewardedVideo(_rewardedVideoAdUnits_IOS[0]);
        #endif
    }
     
    public void ShowRewardedAd()
    {
        if (IsRewardedVideoReady())
        {
            #if UNITY_ANDROID
                    MoPub.ShowRewardedVideo(_rewardedVideoAdUnits_Android[0]);
            #elif UNITY_IOS
                    MoPub.ShowRewardedVideo(_rewardedVideoAdUnits_IOS[0]);
            #endif
        }
        else
        {
          //  LogText.text = "Failed to show rewarded not ready";
        }
    }

    public bool IsRewardedVideoReady()
    {
        #if UNITY_ANDROID
        if (MoPub.HasRewardedVideo(_rewardedVideoAdUnits_Android[0]))
            return true;
        #elif UNITY_IOS
        if (MoPub.HasRewardedVideo(_rewardedVideoAdUnits_IOS[0]))
        return true;
        #endif

        RequestRewardedAd();
        return false;
    }

    void OnRewardedVideoLoaded(string adUnitId)
    {
        Debug.Log("OnRewardedVideoLoaded " + adUnitId);
       // LogText.text = "OnRewardedVideoLoaded " + adUnitId;
    }
    void OnRewardedVideoFailed(string adUnitId, string error)
    {
        Debug.Log("OnRewardedVideoFailed " + adUnitId);
     //   LogText.text = "OnRewardedVideoFailed " + adUnitId + " " + error;
    }

    void OnRewardedVideoShown(string adUnitId)
    {
        Debug.Log("OnRewardedVideoShown " + adUnitId);
      //  LogText.text = "OnRewardedVideoShown " + adUnitId;
    }
#endregion
}
