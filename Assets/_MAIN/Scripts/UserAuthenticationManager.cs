using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;


public class UserAuthenticationManager : MonoBehaviour
{
    const string LAST_EMAIL = "LAST_EMAIL";
    const string LAST_PASSORD = "LAST_PASSWORD";

    #region Register
    [SerializeField] InputField registerEmail;
    [SerializeField] InputField regPassword;
    [SerializeField] InputField regName;
    [SerializeField] GameObject mygameObject;
    private static bool state;

    public void OnRegPressed()
    {
        Register(registerEmail.text, regName.text, regPassword.text);
    }

    private void Register(string email, string username, string password)
    {
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest()
        {
            Email = email,
            DisplayName = username,
            Password = password,
            RequireBothUsernameAndEmail = false,
        },

        successResult => LogIn(email, password),
        PlayFabFailure
        );
    }
    #endregion

    #region Login

    [SerializeField] InputField loginMail;
    [SerializeField] InputField loginPassword;

    public void OnLogInPressed()
    {
        LogIn(loginMail.text, loginPassword.text);
    }

    private void LogIn(string email, string password)
    {
        PlayFabClientAPI.LoginWithEmailAddress(new LoginWithEmailAddressRequest()
        {
            Email = email,
            Password = password,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams()
            {
                GetPlayerProfile = true
            }
        },
            successResult =>
            {
                PlayerPrefs.SetString(LAST_EMAIL, email);
                PlayerPrefs.SetString(LAST_PASSORD, password);
                PlayerPrefs.SetString("Username", successResult.InfoResultPayload.PlayerProfile.DisplayName);

                Debug.Log("Successfully Logged In User: " + PlayerPrefs.GetString("Username"));
            },
            PlayFabFailure);
    }
    private void PlayFabFailure(PlayFabError error)
    {
        Debug.Log(error.Error + " : " + error.GenerateErrorReport());
    }
    #endregion

    public void SwitchScreen()
    {
        state = !state;
        mygameObject.SetActive(state);
        Debug.Log(state);
    }
    private void Start()
    {
        mygameObject.SetActive(state);
    }
}

}
