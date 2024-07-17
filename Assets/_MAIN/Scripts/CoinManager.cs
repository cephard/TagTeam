using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    private const int ZERO = 0;
    private static int availableCoins = ZERO;
    private static int diamondGem = 3;
    private const int CHAPTER_BONUS = 100;
    [SerializeField] private Text coinText;
    [SerializeField] private Text diamondText;

    void Start()
    {
        //availableCoins = PlayerPrefs.GetInt("Coins", availableCoins);
        //diamondGem = PlayerPrefs.GetInt("Diamonds", diamondGem);
        coinText.text = availableCoins.ToString();
        diamondText.text = diamondGem.ToString();
    }

    public void AddCoins(int coins)
    {
        availableCoins += coins;

    }

    //return available coins
    public int GetCoins()
    {
        return availableCoins;
    }

    //Deducting the amount of coins based on the player decidion that requires money to be spent
    public void ExtractExpenditure(string line)
    {
        string[] parts = line.Split(':');
        if (parts.Length == 2 && int.TryParse(parts[1].Trim(), out int expenditure))
        {
           RemoveExpenditure(expenditure);
        }
    }

    
    private void RemoveExpenditure(int spentCoins)
    {
        int newAmount = availableCoins - spentCoins;
        if (spentCoins > availableCoins)
        {
            availableCoins = ZERO;
        }
        else
        {
            availableCoins -= spentCoins;
        }
    }

    /*
     * Awarding coins based on the progress of the player
     */
    public void AwardCoinsByProgress()
    {
        availableCoins += CHAPTER_BONUS;

    }

    //refresh coin UI on the gems prefab
    public void RefreshCoinState()
    {
        coinText.text = availableCoins.ToString();
        diamondText.text = diamondGem.ToString();
    }

    //saving coins and diamond gems to persist even after exit
    private void SaveCoinsAndGems()
    {
        PlayerPrefs.SetInt("Coins", availableCoins);
        PlayerPrefs.SetInt("Diamonds", diamondGem);
        PlayerPrefs.Save();
    }
}
