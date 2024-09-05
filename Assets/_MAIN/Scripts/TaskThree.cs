using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the logic for checking the confidentiality of files
/// based on the state of public and private keys.
/// Displays appropriate messages based on the file's security status
/// and loads the next scene if conditions are met.
/// </summary>
public class TaskThree : MonoBehaviour
{
    // Constant strings representing different status messages.
    private const string HACKED_MESSAGE = "The File has Been Hacked!";
    private const string EXPOSED_MESSAGE = "The file private key has been exposed";
    private const string NEXT_SCENE_NAME = "Conversation";

    // Booleans representing the state of the private key, public key, and confidential file.
    private bool privateKey;
    private bool publicKey;
    private bool confidentialFile;

    // Reference to the main menu controller for scene transitions.
    private MainMenuController mainMenuController;

    // The UI Text component for displaying messages.
    [SerializeField] private Text message;

    /// <summary>
    /// Checks the confidentiality of the file based on the states of the keys.
    /// Displays appropriate messages for exposed or hacked conditions.
    /// If both keys match the confidential file's state, the next scene is loaded.
    /// </summary>
    public void CheckConfidentiality()
    {
        // Get the MainMenuController component
        mainMenuController = GetComponent<MainMenuController>();

        // Check if the public key matches the confidential file, but the private key doesn't
        if (publicKey == confidentialFile && privateKey != confidentialFile)
        {
            message.text = HACKED_MESSAGE;
        }

        // Check if the private key matches the confidential file, but the public key doesn't
        if (privateKey == confidentialFile && publicKey != confidentialFile)
        {
            message.text = EXPOSED_MESSAGE;
        }

        // If both the public and private keys match the confidential file, load the next scene
        if (privateKey == confidentialFile && publicKey == confidentialFile)
        {
            mainMenuController.LoadNextScene(NEXT_SCENE_NAME);
        }
    }

    /// <summary>
    /// Updates the security state and checks the confidentiality based on the new state.
    /// </summary>
    /// <param name="securityState">The new security state (could represent key or file status).</param>
    public void UpdateSecurityState(bool securityState)
    {
        CheckConfidentiality();
    }
}
