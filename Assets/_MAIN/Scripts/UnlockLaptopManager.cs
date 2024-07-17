using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockLaptopManager : MonoBehaviour
{
    [SerializeField] private Text clueText;
    [SerializeField] private GameObject clueDisplay;
    [SerializeField] private InputField hiddenEntry;
    private MainMenuController mainMenuController;
    private CoinManager coinManager;
    private char[] passwordCharacters = { 'E', 'n', '4', 't', 'e', '@', '#', 'r' };

    public void UpdateClue(string clue)
    {
        if (clueText == null)
        {
            clueDisplay.SetActive(false);
        }
        clueDisplay.SetActive(true);

        if (clue == "mainClue")
        {
            clueText.text = "The password is has eight characters." +
                 "\n It begins with a vowel and ends with a consonant." +
                 "\n After every two letters its a symbol or a number." +
                 "\n Only the first half follows alphabetical order." +
                 "\n The last three start with a symbol. ";
        }
        else
        {
            clueText.text = clue;
        }
    }

    public void HideClue()
    {
        clueDisplay.SetActive(false);
    }

    public void UnlockLaptop()
    {
        if (string.Equals(hiddenEntry.text, "En4te@#r", StringComparison.OrdinalIgnoreCase))
        {
            coinManager.AddCoins(100);
            mainMenuController.LoadNextScene("Stats");
            mainMenuController.UpdateSceneName("UnlockLaptop");
        }
    }

    private void Start()
    {
        coinManager = GetComponent<CoinManager>();
        mainMenuController = GetComponent<MainMenuController>();
    }
}
