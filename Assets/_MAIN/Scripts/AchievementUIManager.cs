using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the display of achievement UI elements based on the player's achievements.
/// </summary>
public class AchievementUIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] achievements = new GameObject[18];

    // Constants for index boundaries and calculation multipliers
    private const int INDEX_OUT_OF_BOUNDS = -1;
    private const int FIRST_INDEX = 0;
    private const int SECOND_INDEX = 1;
    private const int THIRD_INDEX = 2;
    private const int FOURTH_INDEX = 3;
    private const int SEVENTH_INDEX = 6;
    private const int TENTH_INDEX = 9;
    private const int THIRTEENTH_INDEX = 12;
    private const int SIXTEENTH_INDEX = 15;

    /// <summary>
    /// Initializes the achievement UI by showing the appropriate stars.
    /// </summary>
    private void Start()
    {
        ShowStars();
    }

    /// <summary>
    /// Determines the index of the star to be displayed based on the expected and achieved scores.
    /// </summary>
    /// <param name="expectedScore">The score required to achieve a star.</param>
    /// <param name="achievedScore">The score the player has achieved.</param>
    /// <returns>The index of the star to be displayed, or <see cref="INDEX_OUT_OF_BOUNDS"/> if no stars should be displayed.</returns>
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

    /// <summary>
    /// Awards achievements by activating the corresponding stars based on the achieved score.
    /// </summary>
    /// <param name="skill">The name of the skill for which achievements are being awarded.</param>
    /// <param name="achievedScore">The score achieved by the player for the given skill.</param>
    private void AwardAchievement(string skill, int achievedScore)
    {
        int expectedScore = AchievementDataManager.GetExpectedScore(skill);
        int starIndex = DetermineIndexOfStar(expectedScore, achievedScore);
        if (starIndex != INDEX_OUT_OF_BOUNDS)
        {
            ActivateAchievement(skill, starIndex);
        }
    }

    /// <summary>
    /// Activates the achievement UI elements for a given skill based on the star index.
    /// </summary>
    /// <param name="skill">The name of the skill for which achievements are being activated.</param>
    /// <param name="starIndex">The index of the highest star to be activated.</param>
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

    /// <summary>
    /// Maps a skill name to the base index in the achievements array where its stars begin.
    /// </summary>
    /// <param name="skill">The name of the skill to map.</param>
    /// <returns>The base index for the skill in the achievements array, or <see cref="INDEX_OUT_OF_BOUNDS"/> if the skill is not recognized.</returns>
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

    /// <summary>
    /// Hides all achievement UI elements and then shows the appropriate stars based on the player's achievements.
    /// </summary>
    private void ShowStars()
    {
        // Hide all achievement UI elements initially
        for (int i = FIRST_INDEX; i < achievements.Length; i++)
        {
            if (achievements[i] != null)
            {
                achievements[i].SetActive(false);
            }
        }

        // Show stars for each skill based on the achieved score
        foreach (var skill in AchievementDataManager.GetSkills())
        {
            int achievedScore = AchievementDataManager.GetAchievedScore(skill);
            AwardAchievement(skill, achievedScore);
        }
    }
}
