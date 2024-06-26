using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{

    private static int availableCoins = 1000;
    private static int diamondBadge = 20;
    [SerializeField] private Text coinText;
    [SerializeField] private Text diamondText;


    void Start()
    {
        coinText.text = availableCoins.ToString();
        diamondText.text = diamondBadge.ToString();
    }

    public void AddCoins()
    {
        availableCoins += 100;
        diamondBadge += 10;
    }

    public void UseCoins(int bonusCoins)
    {
        availableCoins -= bonusCoins;
    }

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

        for (int i = availableCoins; i >= newAmount; i--)
        {
            coinText.text = i.ToString();
            yield return new WaitForSeconds(0.0000001f);
        }

    }




}


