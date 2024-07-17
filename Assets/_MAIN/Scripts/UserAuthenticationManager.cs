/*
 * Class that handles user aunthentication using playfab API
 * The sensitive credentials are handled and stored by playfab API 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using System;

public class UserAuthenticationManager : MonoBehaviour
{
    private const string LAST_EMAIL = "LAST_EMAIL";
    private const string LAST_PASSWORD = "LAST_PASSWORD";
    private static MainMenuController mainMenuController;
    private static bool authenticationScreen;
    [SerializeField] private GameObject loginGameObject;
    [SerializeField] private GameObject signupGameObject;
    [SerializeField] private InputField registrationEmail;
    [SerializeField] private InputField registrationName;
    [SerializeField] private InputField registrationPassword;
    [SerializeField] private InputField confrimationPassword;
    [SerializeField] private InputField loginMail;
    [SerializeField] private InputField loginPassword;
    [SerializeField] private Text authenticationType;
    [SerializeField] private Text switchAuthenticationType;

    private void Start()
    {
        SwitchScreen();
        loginGameObject.SetActive(authenticationScreen);
        mainMenuController = FindObjectOfType<MainMenuController>();
    }

    // Called when Register button is pressed
    // Check if passwords match
    // Validate input fields
    public void OnRegPressed()
    {
        if (string.IsNullOrEmpty(registrationEmail.text) || string.IsNullOrEmpty(registrationPassword.text) || string.IsNullOrEmpty(registrationName.text))
        {
            authenticationType.text = "Registration fields cannot be empty!";
            return;
        }

        if (registrationPassword.text != confrimationPassword.text)
        {
            authenticationType.text = "Password mismatch!";
            return;
        }

        Register(registrationEmail.text, registrationName.text, registrationPassword.text, confrimationPassword.text);
    }

    // Register a new PlayFab user using PlayFab API
    private void Register(string email, string username, string password, string confirmPass)
    {
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
        PlayFabClientAPI.LoginWithEmailAddress(request,
           successResult =>
           {
               PlayerPrefs.SetString(LAST_EMAIL, email);
               PlayerPrefs.SetString(LAST_PASSWORD, password);

               PlayerPrefs.SetString("Username", successResult.InfoResultPayload.PlayerProfile.DisplayName);

               // Log success and proceed to main menu
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
        Debug.LogError(error.Error + " : " + error.GenerateErrorReport());
        authenticationType.text = "An error occurred, please try again!";
    }

    // Switch between login and signup screens
    public void SwitchScreen()
    {
        authenticationScreen = !authenticationScreen; 
        loginGameObject.SetActive(authenticationScreen); 
        signupGameObject.SetActive(!authenticationScreen);
        SwitchLogInScreen(loginGameObject);
    }

    // Update UI text based on current screen
    private void SwitchLogInScreen(GameObject logIn)
    {
        if (loginGameObject.activeSelf)
        {
            authenticationType.text = "Log In";
            switchAuthenticationType.text = "New player? Sign up!";
        }
        else
        {
            authenticationType.text = "Sign Up";
            switchAuthenticationType.text = "Already have an account? Log in!";
        }
    }
}
