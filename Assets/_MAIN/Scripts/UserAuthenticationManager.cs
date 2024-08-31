using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class UserAuthenticationManager : MonoBehaviour
{
    private const string LAST_EMAIL = "LAST_EMAIL";
    private const string LAST_PASSWORD = "LAST_PASSWORD";
    private const string DEFAULT_ERROR_MESSAGE = "An unknown error occurred.";
    private const string WAIT_MESSAGE = "Please wait...";
    private const string LOGIN_SCREEN_TEXT = "Log In";
    private const string SIGNUP_SCREEN_TEXT = "Sign Up";
    private const string LOGIN_PROMPT_TEXT = "Already have an account? Log in!";
    private const string SIGNUP_PROMPT_TEXT = "New player? Sign up!";

    private static MainMenuController mainMenuController;
    private static bool authenticationScreen;

    [SerializeField] private GameObject loginGameObject;
    [SerializeField] private GameObject signupGameObject;
    [SerializeField] private InputField registrationEmail;
    [SerializeField] private InputField registrationName;
    [SerializeField] private InputField registrationPassword;
    [SerializeField] private InputField confirmationPassword;
    [SerializeField] private InputField loginMail;
    [SerializeField] private InputField loginPassword;
    [SerializeField] private Text authenticationType;
    [SerializeField] private Text switchAuthenticationType;
    [SerializeField] private Text pleaseWait;

    private const char ERROR_REPORT_SEPARATOR_DOT = '.';
    private const char ERROR_REPORT_SEPARATOR_COLON = ':';
    private const char ERROR_REPORT_SEPARATOR_NEWLINE = '\n';

    private void Start()
    {
        mainMenuController = FindObjectOfType<MainMenuController>();
        SwitchScreen();
    }

    // Called when Register button is pressed
    public void OnRegPressed()
    {
        if (string.IsNullOrEmpty(registrationEmail.text) ||
            string.IsNullOrEmpty(registrationPassword.text) ||
            string.IsNullOrEmpty(registrationName.text))
        {
            authenticationType.text = "Registration fields cannot be empty!";
            return;
        }

        if (registrationPassword.text != confirmationPassword.text)
        {
            authenticationType.text = "Password mismatch!";
            return;
        }

        Register(registrationEmail.text, registrationName.text, registrationPassword.text);
    }

    // Register a new PlayFab user using PlayFab API
    private void Register(string email, string username, string password)
    {
        pleaseWait.text = WAIT_MESSAGE;
        var request = new RegisterPlayFabUserRequest
        {
            Email = email,
            DisplayName = username,
            Password = password,
            RequireBothUsernameAndEmail = false,
        };

        PlayFabClientAPI.RegisterPlayFabUser(request,
            successResult => LogIn(email, password),
            PlayFabFailure
        );
    }

    // Called when Login button is pressed
    public void OnLogInPressed()
    {
        if (string.IsNullOrEmpty(loginMail.text) || string.IsNullOrEmpty(loginPassword.text))
        {
            Debug.LogError("Login fields cannot be empty.");
            return;
        }

        LogIn(loginMail.text, loginPassword.text);
    }

    // Log in an existing PlayFab user
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

        MakeAPICall(request, email, password);
    }

    // Make API call to PlayFab for login
    private void MakeAPICall(LoginWithEmailAddressRequest request, string email, string password)
    {
        pleaseWait.text = WAIT_MESSAGE;

        PlayFabClientAPI.LoginWithEmailAddress(request,
           successResult =>
           {
               PlayerPrefs.SetString(LAST_EMAIL, email);
               PlayerPrefs.SetString(LAST_PASSWORD, password);
               PlayerPrefs.SetString("Username", successResult.InfoResultPayload.PlayerProfile.DisplayName);

               Debug.Log("Successfully Logged In User: " + PlayerPrefs.GetString("Username"));
               mainMenuController.UpdateSceneName("MainMenu");
               mainMenuController.LoadNextScene("WelcomeScene");
           },
           PlayFabFailure
       );
    }

    // Handle PlayFab API call failure
    private void PlayFabFailure(PlayFabError error)
    {
        string errorReport = error.GenerateErrorReport();
        string[] separators = { ERROR_REPORT_SEPARATOR_DOT.ToString(), ERROR_REPORT_SEPARATOR_COLON.ToString(), ERROR_REPORT_SEPARATOR_NEWLINE.ToString() };
        string[] segments = errorReport.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        string lastSegment = segments.Length > 0 ? segments[segments.Length - 1].Trim() : DEFAULT_ERROR_MESSAGE;
        pleaseWait.text = "";
        authenticationType.text = lastSegment;
    }

    // Switch between login and signup screens
    public void SwitchScreen()
    {
        authenticationScreen = !authenticationScreen;
        loginGameObject.SetActive(authenticationScreen);
        signupGameObject.SetActive(!authenticationScreen);
        UpdateAuthenticationScreenText();
    }

    // Update UI text based on current screen
    private void UpdateAuthenticationScreenText()
    {
        if (authenticationScreen)
        {
            authenticationType.text = LOGIN_SCREEN_TEXT;
            switchAuthenticationType.text = SIGNUP_PROMPT_TEXT;
        }
        else
        {
            authenticationType.text = SIGNUP_SCREEN_TEXT;
            switchAuthenticationType.text = LOGIN_PROMPT_TEXT;
        }
    }
}
