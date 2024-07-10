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

    public void UpdateClue(string clue)
    {
        if (clueText == null)
        {
            clueDisplay.SetActive(false);
        }
        clueDisplay.SetActive(true);
        clueText.text = clue;
    }

    public void HideClue()
    {
        clueDisplay.SetActive(false);
    }

    public void UnlockLaptop()
    {
        if (hiddenEntry.text == "En4te@#r")
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
