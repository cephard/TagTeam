using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class UserAuthenticationManager : MonoBehaviour
{
    const string LAST_EMAIL = "LAST_EMAIL";
    const string LAST_PASSWORD = "LAST_PASSWORD";

    #region Register
    [SerializeField] private InputField registerEmail;
    [SerializeField] private InputField regName;
    [SerializeField] private InputField regPassword;
    [SerializeField] private InputField confrimPassword;
    [SerializeField] private GameObject loginGameObject;
    [SerializeField] private GameObject signupGameObject;
    [SerializeField] private Text authenticationType;
    [SerializeField] private Text switchAuthType;
    [SerializeField] InputField loginMail;
    [SerializeField] InputField loginPassword;
    private MainMenuController mainMenuController;
    private static bool authenticationScreen;

    private void Start()
    {
        loginGameObject.SetActive(authenticationScreen);
        mainMenuController = GetComponent<MainMenuController>();
    }

    public void OnRegPressed()
    {
        if (string.IsNullOrEmpty(registerEmail.text) || string.IsNullOrEmpty(regPassword.text) || string.IsNullOrEmpty(regName.text))
        {
            authenticationType.text = "Registration fields cannot be empty!";
        }
        if (regPassword.text != confrimPassword.text)
        {
            authenticationType.text = "Password mismatch!";
        }
        Register(registerEmail.text, regName.text, regPassword.text, confrimPassword.text);
    }

    private void Register(string email, string username, string password, string confirmPass)
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = email,
            DisplayName = username,
            Password = password,
            RequireBothUsernameAndEmail = false,
        };
        SwitchScreen();
    }
    #endregion

    #region Login
    public void OnLogInPressed()
    {
        if (string.IsNullOrEmpty(loginMail.text) || string.IsNullOrEmpty(loginPassword.text))
        {
            Debug.LogError("Login fields cannot be empty.");
            return;
        }
        LogIn(loginMail.text, loginPassword.text);
    }

    private void LogIn(string email, string password)
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };

        PlayFabClientAPI.LoginWithEmailAddress(request,
            successResult =>
            {
                PlayerPrefs.SetString(LAST_EMAIL, email);
                PlayerPrefs.SetString(LAST_PASSWORD, password);
                PlayerPrefs.SetString("Username", successResult.InfoResultPayload.PlayerProfile.DisplayName);
                Debug.Log("Successfully Logged In User: " + PlayerPrefs.GetString("Username"));
                mainMenuController.LoadNextScene("WelcomeScene");
                mainMenuController.UpdateSceneName("MainMenu");
            },
            PlayFabFailure
        );
    }

    private void PlayFabFailure(PlayFabError error)
    {
        Debug.LogError(error.Error + " : " + error.GenerateErrorReport());
        authenticationType.text = "An error occurd please try again!";
    }
    #endregion
    public void SwitchScreen()
    {
        authenticationScreen = !authenticationScreen;
        loginGameObject.SetActive(authenticationScreen);
        signupGameObject.SetActive(!authenticationScreen);
        SwitchLogInScreen(loginGameObject);
    }

    private void SwitchLogInScreen(GameObject logIn)
    {
        if (loginGameObject.activeSelf)
        {
            authenticationType.text = "Log In";
            switchAuthType.text = "New player? Sign up!";
        }
        else
        {
            authenticationType.text = "Sign Up";
            switchAuthType.text = "Already have an account? Log in!";
        }
    }
}
