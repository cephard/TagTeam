using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the coin and gem system in the game, including awarding, spending, and displaying coins and gems.
/// </summary>
public class CoinManager : MonoBehaviour
{
    private const int ZERO = 0;
    private const int ONE = 1;
    private const int TWO = 2;
    private const int FIVE = 5;
    private const int CHAPTER_BONUS = 100;

    private static int availableCoinCount = ZERO;
    private static int specialGem = ZERO;
    private static int gemPerChapter = ZERO;

    private AudioManager audioManager;

    [SerializeField] private Text coinText;
    [SerializeField] private Text diamondText;

    /// <summary>
    /// Initializes the CoinManager, setting the initial coin and gem counts in the UI.
    /// </summary>
    void Start()
    {
        audioManager = GetComponent<AudioManager>();
        coinText.text = availableCoinCount.ToString();
        diamondText.text = specialGem.ToString();
    }

    /// <summary>
    /// Resets the gem count for the current chapter.
    /// </summary>
    public void ResetChapterGem()
    {
        gemPerChapter = ZERO;
    }

    /// <summary>
    /// Adds coins to the player's total, awarding a special gem if the added amount is 5 or more.
    /// </summary>
    /// <param name="coin">The number of coins to add.</param>
    public void AddCoins(int coin)
    {
        if (coin >= FIVE)
        {
            AwardSpecialGem();
        }
        availableCoinCount += coin;
    }

    /// <summary>
    /// Returns the total number of available coins.
    /// </summary>
    /// <returns>The number of available coins.</returns>
    public int GetCoins()
    {
        return availableCoinCount;
    }

    /// <summary>
    /// Returns the total number of special gems.
    /// </summary>
    /// <returns>The number of special gems.</returns>
    public int GetSpecialGem()
    {
        return specialGem;
    }

    /// <summary>
    /// Returns the number of gems earned in the current chapter.
    /// </summary>
    /// <returns>The number of gems earned in the current chapter.</returns>
    public int GetChapterGem()
    {
        return gemPerChapter;
    }

    /// <summary>
    /// Extracts and deducts expenditure from the player's total coins based on a formatted string.
    /// </summary>
    /// <param name="line">The formatted string containing the expenditure (e.g., "Cost: 10").</param>
    public void ExtractExpenditure(string line)
    {
        string[] parts = line.Split(':');
        if (parts.Length == TWO && int.TryParse(parts[ONE].Trim(), out int expenditure))
        {
            RemoveExpenditure(expenditure);
        }
    }

    /// <summary>
    /// Removes a specified number of coins from the player's total, ensuring the total does not go below zero.
    /// </summary>
    /// <param name="spentCoins">The number of coins to deduct.</param>
    private void RemoveExpenditure(int spentCoins)
    {
        if (spentCoins > availableCoinCount)
        {
            availableCoinCount = ZERO;
        }
        else
        {
            availableCoinCount -= spentCoins;
        }
    }

    /// <summary>
    /// Awards coins based on the player's progress, adding a chapter completion bonus.
    /// </summary>
    public void AwardCoinsByProgress()
    {
        availableCoinCount += CHAPTER_BONUS;
    }

    /// <summary>
    /// Awards special gems to the player, updates the chapter gem count, and plays a sound effect.
    /// </summary>
    private void AwardSpecialGem()
    {
        gemPerChapter += TWO;
        specialGem += TWO;
        audioManager.PlayGainGemAudio();
    }

    /// <summary>
    /// Refreshes the coin and gem counts in the UI.
    /// </summary>
    public void RefreshCoinState()
    {
        coinText.text = availableCoinCount.ToString();
        diamondText.text = specialGem.ToString();
    }

    /// <summary>
    /// Saves the current coin and gem counts to player preferences, ensuring persistence across sessions.
    /// </summary>
    private void SaveCoinsAndGems()
    {
        PlayerPrefs.SetInt("Coins", availableCoinCount);
        PlayerPrefs.SetInt("Diamonds", specialGem);
        PlayerPrefs.Save();
    }
}
