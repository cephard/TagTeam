using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] private GameObject[] achievements = new GameObject[15];

    private Dictionary<string, (int expected, int achieved)> targetGoals;

    private int[] archvievedScore = new int[5];


    private void AddScore(int index, int score)
    {
        archvievedScore[index] = score;
    }

    private void Start()
    {
        // Initialize the dictionary with soft skills and their expected and initial archived values
        targetGoals = new Dictionary<string, (int expected, int achieved)>
        {
            { "Self Efficacy", (12, 8) },
            { "Leadership", (6, 5) },
            { "Communication Skills", (6, 5) },
            { "Decision Making", (10, 3) },
            { "Emotional Intelligence", (6, 2) }
        };
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
        if (targetGoals.ContainsKey(skill))
        {
            var (expectedScore, _) = targetGoals[skill];
            int starIndex = DetermineIndexOfStar(expectedScore, achievedScore);
            if (starIndex != -1)
            {
                ActivateAchievement(skill, starIndex);
            }
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
                if (achievementIndex >= 0 && achievementIndex < achievements.Length)
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

    public void UpdateAchievedGoals(string skill, int newValue)
    {
        if (targetGoals.ContainsKey(skill))
        {
            var (expected, achieved) = targetGoals[skill];
            targetGoals[skill] = (expected, newValue);
            AwardAchievement(skill, newValue);
        }
    }

    private void Update()
    {
        for (int i = 0; i < achievements.Length; i++)
        {
            achievements[i].SetActive(false);
        }

        // Example of updating the visibility of achievements based on achieved values
        foreach (var skill in targetGoals.Keys)
        {
            var (expected, achieved) = targetGoals[skill];
            AwardAchievement(skill, achieved);
        }
    }
}
