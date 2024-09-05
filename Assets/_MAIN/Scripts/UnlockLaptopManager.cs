using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockLaptopManager : UnityEngine.MonoBehaviour
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

    private void Start()
    {
        clueManager = GetComponent<ClueManager>();
        coinManager = GetComponent<CoinManager>();
        mainMenuController = GetComponent<MainMenuController>();
        analyticsManager = GetComponent<AnalyticsManager>();
        analyticsManager.TrackEvent(CURRENT_EVENT_NAME);
    }

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
