using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the Printer Serial Number Task where the player must identify and input errors. 
/// It checks the player's input against expected answers and provides feedback.
/// </summary>
public class PrinterSerialNumberManager : UnityEngine.MonoBehaviour
{
    private const int NO_ERROR = 0;

    [SerializeField] private Text printedErrorCount;
    [SerializeField] private Text correctErrorCount;
    [SerializeField] private InputField[] errorsFound;

    private AudioManager audioManager;
    private const int EXPECTED_ERRORS = 9;
    private int[] expectedAnswer = { 2, 3, 0, 2, 2 };

    private MainMenuController mainMenuController;
    private PlayerChanceManager playerChanceManager;
    private ClueManager clueManager;
    private TimerManager timerManager;
    private int timeRequiredForTask = 60;
    private AnalyticsManager analyticsManager;

    // Get required components for task management.
    private void InitializeCustomObjects()
    {
        mainMenuController = GetComponent<MainMenuController>();
        timerManager = GetComponent<TimerManager>();
        clueManager = GetComponent<ClueManager>();
        audioManager = GetComponent<AudioManager>();
        playerChanceManager = GetComponent<PlayerChanceManager>();
        analyticsManager = GetComponent<AnalyticsManager>();
    }

    /// <summary>
    /// Initializes the task by setting up timers, loading remaining player chances, and tracking the event.
    /// </summary>
    private void Start()
    {
        timerManager.SetTimer(timeRequiredForTask);
        playerChanceManager.LoadRemainingChance();
        analyticsManager.TrackEvent("Printer Serial Task");
    }

    /// <summary>
    /// Updates the task scene with the remaining time.
    /// </summary>
    void Update()
    {
        mainMenuController.RefreshScene(timerManager.GetTimer(), "PrinterSerial", timeRequiredForTask);
    }

    /// <summary>
    /// Proceeds to the next chapter if the player has correctly identified all expected errors.
    /// Provides feedback based on the result.
    /// </summary>
    /// <param name="sceneName">The name of the next scene or chapter to load if the task is successful.</param>
    public void Proceed(string sceneName)
    {
        if (EXPECTED_ERRORS == ValidatePlayerAnswers())
        {
            AllowPlayerToProceed(sceneName);
        }
        else
        {
            StopPlayerFromProceed();
        }
    }

    // If the player has identified the exact number of errors, provide success feedback and load the next chapter.
    private void AllowPlayerToProceed(string sceneName)
    {
        audioManager.PlayWinningAudio();
        clueManager.ShowWinOrLoseClue("Congratulations! You Rock!");
        mainMenuController.LoadNextChapter(sceneName);
    }

    // Otherwise, provide feedback to try again and play an incorrect answer audio cue.
    private void StopPlayerFromProceed()
    {
        audioManager.PlayWrongAnswerAudio();
        clueManager.ShowWinOrLoseClue("Please Try Again!");
    }

    /// <summary>
    /// Validates the player's answers by comparing input with the expected answers.
    /// </summary>
    /// <returns>The total number of errors correctly identified by the player.</returns>
    private int ValidatePlayerAnswers()
    {
        int errorFoundByPlayer = NO_ERROR;

        for (int i = NO_ERROR; i < errorsFound.Length; i++)
        {
            if (ParseInputField(errorsFound[i].text) == expectedAnswer[i])
            {
                errorFoundByPlayer += ParseInputField(errorsFound[i].text);
            }
            else
            {
                return errorFoundByPlayer;
            }
        }

        return errorFoundByPlayer;
    }

    /// <summary>
    /// Updates the displayed error count on the UI.
    /// </summary>
    /// <param name="errorCount">The current number of errors found.</param>
    public void SetErrorCount(int errorCount)
    {
        printedErrorCount.text = errorCount.ToString();
    }

    /// <summary>
    /// Parses the input field to retrieve the number of errors as an integer.
    /// </summary>
    /// <param name="errorsFound">The text from the input field.</param>
    /// <returns>The parsed number of errors, or 0 if parsing fails.</returns>
    private int ParseInputField(string errorsFound)
    {
        int result;
        if (int.TryParse(errorsFound, out result))
        {
            return result;
        }
        else
        {
            return NO_ERROR;
        }
    }
}
