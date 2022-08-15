using AppleAuth;
using AppleAuth.Enums;
using AppleAuth.Extensions;
using AppleAuth.Interfaces;
using AppleAuth.Native;
using System.Text;
using UnityEngine;

public class AppleUserLogin : MonoBehaviour
{
    public static AppleUserLogin instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private IAppleAuthManager _appleAuthManager;
    public string token = "";
    public string AppleUserIdKey = "AppleUserIdKey";
    private void Start()
    {
        AuthorizeAppleSignIn();
        AutoLoginApple();
        //initialized();
    }
    public void AutoLoginApple()
    {
        if (PlayerPrefs.HasKey(AppleUserIdKey))
        {
            var appleIdKey = PlayerPrefs.GetString(AppleUserIdKey);
            CheckCredentialStatusForUserId(appleIdKey);
        }
        if (PlayerPrefs.HasKey("IsLoginWithApple"))
        {
            if (PlayerPrefs.GetInt("IsLoginWithApple") == 1)
            {
                if (PlayerPrefs.HasKey("AppleIdentityToken"))
                {
                    Debug.Log("AutoLogin with idToken");
                    var idToken = PlayerPrefs.GetString("AppleIdentityToken");
                    PlayFabManager.Instance.AppleLoginWithPlayfab(idToken);
                    PlayFabManager.Instance.appleLoginButton.SetActive(false);
                }
            }
        }
    }

    public void AuthorizeAppleSignIn()
    {
        // If the current platform is supported
        if (AppleAuthManager.IsCurrentPlatformSupported)
        {
            // Creates a default JSON deserializer, to transform JSON Native responses to C# instances
            var deserializer = new PayloadDeserializer();
            // Creates an Apple Authentication manager with the deserializer
            this._appleAuthManager = new AppleAuthManager(deserializer);
        }
    }


    void initialized()
    {
/*        if (this._appleAuthManager == null)
        {
            Debug.Log("apple Authmanager is null ");
            return;
        }

        // If at any point we receive a credentials revoked notification, we delete the stored User ID, and go back to login
        this._appleAuthManager.SetCredentialsRevokedCallback(result =>
        {
            Debug.Log("Received revoked callback " + result);
            PlayerPrefs.DeleteKey(AppleUserIdKey);
        });
        // If we have an Apple User Id available, get the credential status for it
        if (PlayerPrefs.HasKey(AppleUserIdKey))
        {
            var storedAppleUserId = PlayerPrefs.GetString(AppleUserIdKey);
            this.CheckCredentialStatusForUserId(storedAppleUserId);
        }
        // If we do not have an stored Apple User Id, attempt a quick login
        else
        {
            this.AttemptQuickLogin();
        }*/
    }
    public void LogOutApple()
    {
        Debug.Log("Logout Apple call");
        this._appleAuthManager.SetCredentialsRevokedCallback(result =>
        {
            UIManager.Instance.logout = true;
            Debug.Log("Received revoked callback " + result);
            Debug.Log("Logout Apple Success");
        });
    }

    public void AttemptQuickLogin()
    {
        PlayFabManager.Instance.appleLoginButton.SetActive(false);
        PlayFabManager.Instance.loading.SetActive(true);
        var quickLoginArgs = new AppleAuthQuickLoginArgs();

        // Quick login should succeed if the credential was authorized before and not revoked
        this._appleAuthManager.QuickLogin(
            quickLoginArgs,
            credential =>
            {
                Debug.Log("Quick login success");
                // If it's an Apple credential, save the user ID, for later logins
                var appleIdCredential = credential as IAppleIDCredential;
                //if (appleIdCredential != null)
                //{
                //    Debug.Log("Apple id credential not null");
                //    Debug.LogWarning("Set Apple user id keys");
                //    PlayerPrefs.SetString(AppleUserIdKey, credential.User);
                //}
                print(appleIdCredential.IdentityToken);
                var idToken = Encoding.UTF8.GetString(appleIdCredential.IdentityToken, 0, appleIdCredential.IdentityToken.Length);
                PlayerPrefs.SetString("AppleIdentityToken", idToken);
                PlayFabManager.Instance.AppleLoginWithPlayfab(idToken);
                Debug.Log("idToken " + idToken);

            },
            error =>
            {
                SignInWithApple();
                Debug.Log("Quick login failed");
                // If Quick Login fails, we should show the normal sign in with apple menu, to allow for a normal Sign In with apple
                var authorizationErrorCode = error.GetAuthorizationErrorCode();
                Debug.LogWarning("Quick Login Failed " + authorizationErrorCode.ToString() + " " + error.ToString());
            });
    }

    private void SignInWithApple()
    {
        Debug.Log("SignIn with apple function call");
        var loginArgs = new AppleAuthLoginArgs(LoginOptions.IncludeEmail | LoginOptions.IncludeFullName);

        this._appleAuthManager.LoginWithAppleId(
            loginArgs,
            credential =>
            {
                Debug.Log("SignIn with apple function call");
                // If a sign in with apple succeeds, we should have obtained the credential with the user id, name, and email, save it
                PlayerPrefs.SetString(AppleUserIdKey, credential.User);
                var appleIdCredential = credential as IAppleIDCredential;
                print(appleIdCredential.IdentityToken);
                token = appleIdCredential.IdentityToken.ToString();
                var idToken = Encoding.UTF8.GetString(appleIdCredential.IdentityToken, 0, appleIdCredential.IdentityToken.Length);
                PlayerPrefs.SetString("AppleIdentityToken", idToken);
                PlayFabManager.Instance.AppleLoginWithPlayfab(idToken);
            },
            error =>
            {
                PlayFabManager.Instance.appleLoginButton.SetActive(true);
                Debug.Log("apple signin failed");
                PlayFabManager.Instance.errorLog.text = "login faild";
                var authorizationErrorCode = error.GetAuthorizationErrorCode();
                Debug.LogWarning("Sign in with Apple failed " + authorizationErrorCode.ToString() + " " + error.ToString());
            });
    }
    private void Update()
    {
        // Updates the AppleAuthManager instance to execute
        // pending callbacks inside Unity's execution loop
        if (this._appleAuthManager != null)
        {
            this._appleAuthManager.Update();
        }

    }
    public void CheckCredentialStatusForUserId(string appleUserId)
    {
        // If there is an apple ID available, we should check the credential state
        this._appleAuthManager.GetCredentialState(
            appleUserId,
            state =>
            {
                switch (state)
                {
                    // If it's authorized, login with that user id
                    case CredentialState.Authorized:
                        //  PlayFabManager.Instance.AppleLoginWithPlayfab("");
                        //  this.SetupGameMenu(appleUserId, null);
                        break;

                    // If it was revoked, or not found, we need a new sign in with apple attempt
                    // Discard previous apple user id
                    case CredentialState.Revoked:

                        break;
                    case CredentialState.NotFound:
                        //   this.SetupLoginMenuForSignInWithApple();
                        // PlayerPrefs.DeleteKey(AppleUserIdKey);
                        break;
                }
            },
            error =>
            {
                //  var authorizationErrorCode = error.GetAuthorizationErrorCode();
                //  Debug.LogWarning("Error while trying to get credential state " + authorizationErrorCode.ToString() + " " + error.ToString());
                // this.SetupLoginMenuForSignInWithApple();
            });
    }
    public void SignUpUser()
    {

    }
}
