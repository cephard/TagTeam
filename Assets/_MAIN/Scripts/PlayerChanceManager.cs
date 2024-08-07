using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChanceManager : ItemDropManager
{
    private static int playerChance = 3;
    private const string playChanceKey = "Chance";


    public int GetRemainingChance()
    {

        return playerChance;
    }

    public void SaveRemainingChance()
    {
        PlayerPrefs.SetInt(playChanceKey, playerChance);
        PlayerPrefs.Save();
    }

    public void LoadRemainingChance()
    {
        if (PlayerPrefs.HasKey(playChanceKey))
        {
            playerChance = PlayerPrefs.GetInt(playChanceKey);
        }
        else
        {
            ResetChance();
        }
    }

    public void ResetChance()
    {
        playerChance = 3;
        SaveRemainingChance();
    }

    public void ReduceRemainingChance()
    {
        playerChance--;
        SaveRemainingChance();
    }
}
