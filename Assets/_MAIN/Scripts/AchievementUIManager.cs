using System.Collections.Generic;
using UnityEngine;

public class AchievementUIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] achievements = new GameObject[15];


    private void Start()
    {

        ShowStars();
    }

    private int DetermineIndexOfStar(int expectedScore, int achievedScore)
    {
        if (achievedScore == 0)
        {
            return -1;
        }
        else if (achievedScore <= expectedScore / 3)
        {
            return 0; // One star
        }
        else if (achievedScore <= 2 * expectedScore / 3)
        {
            return 1; // Two stars
        }
        else
        {
            return 2; // Three stars
        }
    }

    private void AwardAchievement(string skill, int achievedScore)
    {
        int expectedScore = AchievementDataManager.GetExpectedScore(skill);
        int starIndex = DetermineIndexOfStar(expectedScore, achievedScore);
        if (starIndex != -1)
        {
            ActivateAchievement(skill, starIndex);
        }
    }

    private void ActivateAchievement(string skill, int starIndex)
    {
        int baseIndex = GetBaseIndexForSkill(skill);

        if (baseIndex != -1)
        {
            for (int i = 0; i <= starIndex; i++)
            {
                int achievementIndex = baseIndex + i;
                if (achievementIndex >= 0 && achievementIndex < achievements.Length && achievements[achievementIndex] != null)
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
            case "Self Efficacy": return 0;
            case "Leadership": return 3;
            case "Communication Skills": return 6;
            case "Decision Making": return 9;
            case "Emotional Intelligence": return 12;
            default: return -1;
        }
    }

    private void ShowStars()
    {
        for (int i = 0; i < achievements.Length; i++)
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
