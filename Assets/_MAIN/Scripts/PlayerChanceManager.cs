using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChanceManager : ItemDropManager
{
    private const int AVAILABLE_CHANCE = 3;
    private const string PLAY_CHANCE_KEY = "Chance";
    private static int playerChance = AVAILABLE_CHANCE;
    


    public int GetRemainingChance()
    {

        return playerChance;
    }

    public void SaveRemainingChance()
    {
        PlayerPrefs.SetInt(PLAY_CHANCE_KEY, playerChance);
        PlayerPrefs.Save();
    }

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

    public void ResetChance()
    {
        playerChance = AVAILABLE_CHANCE;
        SaveRemainingChance();
    }

    public void ReduceRemainingChance()
    {
        playerChance--;
        SaveRemainingChance();
    }
}
