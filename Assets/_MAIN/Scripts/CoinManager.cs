using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : UnityEngine.MonoBehaviour
{
    private const int ZERO = 0;
    private static int availableCoinCount = ZERO;
    private static int specialGem = ZERO;
    private const int CHAPTER_BONUS = 100;
    private AudioManager audioManager;
    [SerializeField] private Text coinText;
    [SerializeField] private Text diamondText;

    void Start()
    {
        //availableCoins = PlayerPrefs.GetInt("Coins", availableCoins);
        //diamondGem = PlayerPrefs.GetInt("Diamonds", diamondGem);
        audioManager = GetComponent<AudioManager>();    
        coinText.text = availableCoinCount.ToString();
        diamondText.text = specialGem.ToString();
    }

    public void AddCoins(int coin)
    {
        if (coin >= 5) {
            AwardSpecialGem();
        }
        availableCoinCount += coin;
    }

    //return available coins
    public int GetCoins()
    {
        return availableCoinCount;
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
        int newAmount = availableCoinCount - spentCoins;
        if (spentCoins > availableCoinCount)
        {
            availableCoinCount = ZERO;
        }
        else
        {
            availableCoinCount -= spentCoins;
        }
    }

    /*
     * Awarding coins based on the progress of the player
     */
    public void AwardCoinsByProgress()
    {
        availableCoinCount += CHAPTER_BONUS;

    }

    private void AwardSpecialGem()
    {
        specialGem++;
        audioManager.PlayGainGemAudio();
    }
    //refresh coin UI on the gems prefab
    public void RefreshCoinState()
    {
        coinText.text = availableCoinCount.ToString();
        diamondText.text = specialGem.ToString();
    }

    //saving coins and diamond gems to persist even after exit
    private void SaveCoinsAndGems()
    {
        PlayerPrefs.SetInt("Coins", availableCoinCount);
        PlayerPrefs.SetInt("Diamonds", specialGem);
        PlayerPrefs.Save();
    }
}
