using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the player's available chances for completing tasks. Inherits from ItemDropManager and handles saving, loading, and reducing the player's chances.
/// </summary>
public class PlayerChanceManager : ItemDropManager
{
    private const int AVAILABLE_CHANCE = 3;
    private const string PLAY_CHANCE_KEY = "Chance";
    private static int playerChance = AVAILABLE_CHANCE;

    /// <summary>
    /// Returns the remaining chances the player has.
    /// </summary>
    /// <returns>The number of remaining chances.</returns>
    public int GetRemainingChance()
    {
        return playerChance;
    }

    /// <summary>
    /// Saves the player's remaining chances to PlayerPrefs to persist between sessions.
    /// </summary>
    public void SaveRemainingChance()
    {
        PlayerPrefs.SetInt(PLAY_CHANCE_KEY, playerChance);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Loads the player's remaining chances from PlayerPrefs. If no saved data is found, it resets the chances to the default value.
    /// </summary>
    public void LoadRemainingChance()
    {
        if (PlayerPrefs.HasKey(PLAY_CHANCE_KEY))
        {
            playerChance = PlayerPrefs.GetInt(PLAY_CHANCE_KEY);
        }
        else
        {
            ResetChance();
        }
    }

    /// <summary>
    /// Resets the player's chances to the default value and saves this state.
    /// </summary>
    public void ResetChance()
    {
        playerChance = AVAILABLE_CHANCE;
        SaveRemainingChance();
    }

    /// <summary>
    /// Reduces the player's remaining chances by one and saves the updated value.
    /// </summary>
    public void ReduceRemainingChance()
    {
        playerChance--;
        SaveRemainingChance();
    }
}
