using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the interaction and logic for unlocking a confidential file in the game, including validating input, providing feedback, and transitioning between game scenes.
/// </summary>
public class ConfidentialFileManager : MonoBehaviour
{
    private const int SecretValue = 14;
    private const int DefaultTimeRequiredForTask = 60;

    [SerializeField] private Text puzzleClueText;
    [SerializeField] private Text feedbackText;
    [SerializeField] private GameObject cluePanel;
    [SerializeField] private InputField secretValueInputField;

    private MainMenuController mainMenuController;
    private PlayerChanceManager playerChanceManager;
    private AudioManager audioManager;
    private ClueManager clueManager;
    private TimerManager timerManager;

    /// <summary>
    /// Initializes the component references and sets up the initial game state.
    /// </summary>
    private void Start()
    {
        InitializeComponents();
        SetupInitialGameState();
    }

    /// <summary>
    /// Retrieves and initializes the required components.
    /// </summary>
    private void InitializeComponents()
    {
        mainMenuController = GetComponent<MainMenuController>();
        timerManager = GetComponent<TimerManager>();
        audioManager = GetComponent<AudioManager>();
        clueManager = GetComponent<ClueManager>();
        playerChanceManager = GetComponent<PlayerChanceManager>();
    }

    /// <summary>
    /// Sets up the initial game state, such as the timer and player's remaining chances.
    /// </summary>
    private void SetupInitialGameState()
    {
        timerManager.SetTimer(DefaultTimeRequiredForTask);
        playerChanceManager.LoadRemainingChance();
    }

    /// <summary>
    /// Regularly updates the main menu to reflect the current game state.
    /// </summary>
    private void Update()
    {
        UpdateMainMenu();
    }

    /// <summary>
    /// Updates the main menu with the current timer status and task name.
    /// </summary>
    private void UpdateMainMenu()
    {
        mainMenuController.RefreshScene(timerManager.GetTimer(), "Ann'sTask", DefaultTimeRequiredForTask);
    }

    /// <summary>
    /// Reveals a clue to help the player unlock the confidential file.
    /// </summary>
    public void RevealFileClue()
    {
        clueManager.SetClue("Help Ann to unlock the confidential file.\n" +
                            "Every Key has its own value. Private keys are RED and Public keys are BLUE.\n" +
                            "Both keys and files are encoded with a secret value.");
    }

    /// <summary>
    /// Submits the value entered by the player, checks if it matches the secret value, and provides feedback or proceeds to the next chapter accordingly.
    /// </summary>
    /// <param name="nextSceneName">The name of the next scene to load if the value is correct.</param>
    public void SubmitValue(string nextSceneName)
    {
        if (IsInputValueValid(out int enteredValue))
        {
            if (enteredValue == SecretValue)
            {
                ProceedToNextChapter(nextSceneName);
            }
            else
            {
                ProvideFeedback("Try again!", isError: true);
                audioManager.PlayWrongAnswerAudio();
            }
        }
    }

    /// <summary>
    /// Validates the player's input to ensure it is a non-null integer.
    /// </summary>
    /// <param name="enteredValue">The integer value parsed from the player's input.</param>
    /// <returns>True if the input is valid; otherwise, false.</returns>
    private bool IsInputValueValid(out int enteredValue)
    {
        enteredValue = 0;

        if (string.IsNullOrEmpty(secretValueInputField.text))
        {
            ProvideFeedback("Value cannot be null!", isError: true);
            return false;
        }

        if (!int.TryParse(secretValueInputField.text, out enteredValue))
        {
            ProvideFeedback("Please enter a number!", isError: true);
            return false;
        }

        return true;
    }

    /// <summary>
    /// Proceeds to the next chapter if the player has entered the correct value.
    /// </summary>
    /// <param name="nextSceneName">The name of the next scene to load.</param>
    private void ProceedToNextChapter(string nextSceneName)
    {
        audioManager.PlayWinningAudio();
        clueManager.ShowWinOrLoseClue("Congratulations! You Rock!");
        mainMenuController.LoadNextChapter(nextSceneName);
    }

    /// <summary>
    /// Provides feedback to the player, changing the color of the feedback text based on whether the message is an error.
    /// </summary>
    /// <param name="message">The feedback message to display.</param>
    /// <param name="isError">True if the feedback is for an error; otherwise, false.</param>
    private void ProvideFeedback(string message, bool isError)
    {
        feedbackText.color = isError ? Color.red : Color.green;
        feedbackText.text = message;
    }
}
