using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

/// <summary>
/// Manages user authentication by handling registration, login, and screen switching between login and signup views.
/// Utilizes PlayFab API to handle user data and authentication.
/// </summary>
public class UserAuthenticationManager : MonoBehaviour
{
    // Constants for player preferences keys, error messages, and default values.
    private const string LAST_EMAIL = "LAST_EMAIL";
    private const string LAST_PASSWORD = "LAST_PASSWORD";
    private const string DEFAULT_ERROR_MESSAGE = "An unknown error occurred.";
    private const string WAIT_MESSAGE = "Please wait...";
    private const string LOGIN_SCREEN_TEXT = "Log In";
    private const string SIGNUP_SCREEN_TEXT = "Sign Up";
    private const string LOGIN_PROMPT_TEXT = "Already have an account? Log in!";
    private const string SIGNUP_PROMPT_TEXT = "New player? Sign up!";
    private const string PLAYER_PREFS_KEY = "Username";
    private const string EMPTY_REG_FIELD_MESSAGE = "Registration fields cannot be empty!";
    private const string PASSWORD_MISSMATCH_MESSAGE = "Password mismatch!";
    private const string MAIN_MENU_SCENE_NAME = "MainMenu";
    private const string WELCOME_SCENE_NAME = "WelcomeScene";
    private const string EMPTY_TEXT = "";
    private const int BASE_SEGMENT = 0;
    private const int EXTRA_SEGMENT = 1;

    // Static references to the main menu controller and state tracking for authentication screen.
    private static MainMenuController mainMenuController;
    private static bool authenticationScreen;

    // Serialized UI elements.
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

    // Constants for error report parsing.
    private const char ERROR_REPORT_SEPARATOR_DOT = '.';
    private const char ERROR_REPORT_SEPARATOR_COLON = ':';
    private const char ERROR_REPORT_SEPARATOR_NEWLINE = '\n';

    /// <summary>
    /// Initializes the authentication manager and switches the screen to the correct initial view.
    /// </summary>
    private void Start()
    {
        mainMenuController = FindObjectOfType<MainMenuController>();
        SwitchScreen();
    }

    /// <summary>
    /// Validates the registration input fields, ensuring they are not empty.
    /// </summary>
    private void CheckSignInEntries()
    {
        if (string.IsNullOrEmpty(registrationEmail.text) ||
            string.IsNullOrEmpty(registrationPassword.text) ||
            string.IsNullOrEmpty(registrationName.text))
        {
            authenticationType.text = EMPTY_REG_FIELD_MESSAGE;
            return;
        }
    }

    /// <summary>
    /// Ensures the registration password matches the confirmation password.
    /// </summary>
    private void CheckPassWordConfirmation()
    {
        if (registrationPassword.text != confirmationPassword.text)
        {
            authenticationType.text = PASSWORD_MISSMATCH_MESSAGE;
            return;
        }
    }

    /// <summary>
    /// Called when the Register button is pressed. Validates the inputs and registers the user.
    /// </summary>
    public void OnRegPressed()
    {
        CheckSignInEntries();
        CheckPassWordConfirmation();
        Register(registrationEmail.text, registrationName.text, registrationPassword.text);
    }


    /// <summary>
    /// Registers a new PlayFab user with the provided email, username, and password.
    /// </summary>
    private void Register(string email, string username, string password)
    {
        pleaseWait.text = WAIT_MESSAGE;

        // Create a request to register a new user with PlayFab.
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

    /// <summary>
    /// Validates the login input fields, ensuring they are not empty.
    /// </summary>
    private void CheckLoginInEntries()
    {
        if (string.IsNullOrEmpty(loginMail.text) || string.IsNullOrEmpty(loginPassword.text))
        {
            Debug.LogError("Login fields cannot be empty.");
            return;
        }
    }

    /// <summary>
    /// Called when the Login button is pressed. Validates the inputs and logs the user in.
    /// </summary>
    public void OnLogInPressed()
    {
        CheckLoginInEntries();
        LogIn(loginMail.text, loginPassword.text);
    }

    /// <summary>
    /// Logs in an existing PlayFab user with the provided email and password.
    /// </summary>
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

    /// <summary>
    /// Makes the PlayFab API call for login and transitions to the next scene upon success.
    /// </summary>
    private void MakeAPICall(LoginWithEmailAddressRequest request, string email, string password)
    {
        pleaseWait.text = WAIT_MESSAGE;

        PlayFabClientAPI.LoginWithEmailAddress(request,
           successResult =>
           {
               // Save login details to PlayerPrefs upon successful login.
               PlayerPrefs.SetString(LAST_EMAIL, email);
               PlayerPrefs.SetString(LAST_PASSWORD, password);
               PlayerPrefs.SetString(PLAYER_PREFS_KEY, successResult.InfoResultPayload.PlayerProfile.DisplayName);

               Debug.Log("Successfully Logged In User: " + PlayerPrefs.GetString(PLAYER_PREFS_KEY));

               // Transition to the main menu and welcome scene.
               mainMenuController.UpdateSceneName(MAIN_MENU_SCENE_NAME);
               mainMenuController.LoadNextScene(WELCOME_SCENE_NAME);
           },
           PlayFabFailure
       );
    }

    /// <summary>
    /// Handles PlayFab API call failure by parsing the error and displaying a user-friendly message.
    /// </summary>
    private void PlayFabFailure(PlayFabError error)
    {
        string errorReport = error.GenerateErrorReport();
        string[] separators = { ERROR_REPORT_SEPARATOR_DOT.ToString(), ERROR_REPORT_SEPARATOR_COLON.ToString(), ERROR_REPORT_SEPARATOR_NEWLINE.ToString() };
        string[] segments = errorReport.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        string lastSegment = segments.Length > BASE_SEGMENT ? segments[segments.Length - EXTRA_SEGMENT].Trim() : DEFAULT_ERROR_MESSAGE;
        pleaseWait.text = EMPTY_TEXT;
        authenticationType.text = lastSegment;
    }

    /// <summary>
    /// Switches between the login and signup screens.
    /// </summary>
    public void SwitchScreen()
    {
        authenticationScreen = !authenticationScreen;
        loginGameObject.SetActive(authenticationScreen);
        signupGameObject.SetActive(!authenticationScreen);
        UpdateAuthenticationScreenText();
    }

    /// <summary>
    /// Updates the text displayed on the authentication screen based on whether it's login or signup.
    /// </summary>
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
