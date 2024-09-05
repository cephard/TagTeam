using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the process of unlocking a laptop using clues and a password input. 
/// Handles updating the clue display, validating input, and loading the next scene.
/// </summary>
public class UnlockLaptopManager : MonoBehaviour
{
    private const string CURRENT_EVENT_NAME = "Unlock Laptop";
    private const string MAIN_CLUE = "mainClue";
    private const string MAIN_CLUE_TEXT = "The password is has eight characters." +
                 "\n It begins with a vowel and ends with a consonant." +
                 "\n After every two letters its a symbol or a number." +
                 "\n Only the first half follows alphabetical order." +
                 "\n The last three start with a symbol. ";
    private const string EXPECTED_INPUT = "En4te@#r";
    private const string WRONG_INPUT_MESSAGE = "Wrong input please try again!";
    private const string NEXT_SCENE_NAME = "PlayerStatistics";
    private const string CURRENT_SCENE_NAME = "UnlockLaptop";

    [SerializeField] private Text clueText;
    [SerializeField] private GameObject clueDisplay;
    [SerializeField] private InputField hiddenEntry;
    private MainMenuController mainMenuController;
    private ClueManager clueManager;
    private CoinManager coinManager;
    private char[] passwordCharacters = { 'E', 'n', '4', 't', 'e', '@', '#', 'r' };
    private AnalyticsManager analyticsManager;

    /// <summary>
    /// Initializes references to other game managers and tracks the event using analytics.
    /// </summary>
    private void Start()
    {
        clueManager = GetComponent<ClueManager>();
        coinManager = GetComponent<CoinManager>();
        mainMenuController = GetComponent<MainMenuController>();
        analyticsManager = GetComponent<AnalyticsManager>();
        analyticsManager.TrackEvent(CURRENT_EVENT_NAME);
    }

    /// <summary>
    /// Updates the clue displayed to the player based on the provided clue string.
    /// If the clue is not found, it displays the main clue.
    /// </summary>
    /// <param name="clue">The clue identifier to display.</param>
    public void UpdateClue(string clue)
    {
        if (clueText == null)
        {
            clueDisplay.SetActive(false);
        }
        clueDisplay.SetActive(true);

        if (clue == MAIN_CLUE)
        {
            clueText.text = MAIN_CLUE_TEXT;
        }
        else
        {
            clueText.text = clue;
        }
    }

    /// <summary>
    /// Checks if the player's input matches the expected password. If correct, it adds coins and loads the next scene.
    /// If incorrect, it shows a wrong input message.
    /// </summary>
    public void UnlockLaptop()
    {
        if (string.Equals(hiddenEntry.text, EXPECTED_INPUT, StringComparison.OrdinalIgnoreCase))
        {
            coinManager.AddCoins(100);
            mainMenuController.LoadNextScene(NEXT_SCENE_NAME);
            mainMenuController.UpdateSceneName(CURRENT_SCENE_NAME);
        }
        else
        {
            clueManager.SetClue(WRONG_INPUT_MESSAGE);
        }

        Debug.Log(hiddenEntry.text);
    }
}
