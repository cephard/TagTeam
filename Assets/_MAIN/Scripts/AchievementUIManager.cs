using System.Collections.Generic;
using UnityEngine;

public class AchievementUIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] achievements = new GameObject[18];
    private const int INDEX_OUT_OF_BOUNDS = -1;
    private const int FIRST_INDEX = 0;
    private const int SECOND_INDEX = 1;
    private const int THIRD_INDEX = 2;
    private const int FOURTH_INDEX = 3;
    private const int SEVENTH_INDEX = 6;
    private const int TENTH_INDEX = 9;
    private const int THIRTEENTH_INDEX = 12;
    private const int SIXTEENTH_INDEX = 15;

    private void Start()
    {
        ShowStars();
    }

    private int DetermineIndexOfStar(int expectedScore, int achievedScore)
    {
        if (achievedScore == FIRST_INDEX)
        {
            return INDEX_OUT_OF_BOUNDS;
        }
        else if (achievedScore <= expectedScore / FOURTH_INDEX)
        {
            return FIRST_INDEX;
        }
        else if (achievedScore <= THIRD_INDEX * expectedScore / FOURTH_INDEX)
        {
            return SECOND_INDEX;
        }
        else
        {
            return THIRD_INDEX;
        }
    }

    private void AwardAchievement(string skill, int achievedScore)
    {
        int expectedScore = AchievementDataManager.GetExpectedScore(skill);
        int starIndex = DetermineIndexOfStar(expectedScore, achievedScore);
        if (starIndex != INDEX_OUT_OF_BOUNDS)
        {
            ActivateAchievement(skill, starIndex);
        }
    }

    private void ActivateAchievement(string skill, int starIndex)
    {
        int baseIndex = GetBaseIndexForSkill(skill);

        if (baseIndex != INDEX_OUT_OF_BOUNDS)
        {
            for (int i = FIRST_INDEX; i <= starIndex; i++)
            {
                int achievementIndex = baseIndex + i;
                if (achievementIndex >= FIRST_INDEX && achievementIndex < achievements.Length && achievements[achievementIndex] != null)
                {
                    achievements[achievementIndex].SetActive(true);
                }
            }
        }
    }

    private int GetBaseIndexForSkill(string skill)
    {
        switch (skill)
        {
            case "Self Efficacy": return FIRST_INDEX;
            case "Leadership": return FOURTH_INDEX;
            case "Communication Skills": return SEVENTH_INDEX;
            case "Decision Making": return TENTH_INDEX;
            case "Emotional Intelligence": return THIRD_INDEX;
            case "Problem Solving": return SIXTEENTH_INDEX;
            default: return INDEX_OUT_OF_BOUNDS;
        }
    }

    private void ShowStars()
    {
        for (int i = FIRST_INDEX; i < achievements.Length; i++)
        {
            if (achievements[i] != null)
            {
                achievements[i].SetActive(false);
            }
        }

        foreach (var skill in AchievementDataManager.GetSkills())
        {
            int achievedScore = AchievementDataManager.GetAchievedScore(skill);
            AwardAchievement(skill, achievedScore);
        }
    }
}
