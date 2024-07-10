using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    private static int availableCoins = 0;
    private static int diamondGem = 0;
    [SerializeField] private Text coinText;
    [SerializeField] private Text diamondText;

    void Start()
    {
        availableCoins = PlayerPrefs.GetInt("Coins", availableCoins);
        diamondGem = PlayerPrefs.GetInt("Diamonds", diamondGem);
        coinText.text = availableCoins.ToString();
        diamondText.text = diamondGem.ToString();
    }

    public void AddCoins(int coins)
    {
        availableCoins += (coins * 2);
        diamondGem += coins;
        SaveCoinsAndGems();
    }

    //when a player makes a decision to spend coins
    public void UseCoins(int bonusCoins)
    {
        availableCoins -= bonusCoins;
        SaveCoinsAndGems();
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
            StartCoroutine(CoinReductionEffect(expenditure));
        }
    }

    //a coroutine that makes an effect to make the players money spenditure purnishment feel more severe
    private IEnumerator CoinReductionEffect(int spentCoins)
    {
        int newAmount = availableCoins - spentCoins;
        if (spentCoins > availableCoins)
        {
            availableCoins = 0;
        }
        else
        {
            availableCoins -= spentCoins;
        }
        SaveCoinsAndGems();
        while (availableCoins > newAmount)
        {
            {
                availableCoins--;
                coinText.text = availableCoins.ToString();
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    /*
     * Awarding coins based on the progress of the player
     */
    public void AwardCoinsByProgress(int currentLine)
    {
        if (currentLine % 5 == 0)
        {
            AddCoins(20);
            SaveCoinsAndGems();
        }
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
