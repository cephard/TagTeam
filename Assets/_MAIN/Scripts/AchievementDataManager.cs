using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages achievement data, including tracking and updating goals for different skills.
/// </summary>
public static class AchievementDataManager
{
    // Constants representing achievement milestones.
    private const int NO_ACHIEVEMENT = 0;
    private const int SIXTH_ACHIEVEMENT = 6;
    private const int TENTH_ACHIEVEMENT = 10;
    private const int TWELVETH_ACHIEVEMENT = 12;
    private const int FIFTEENTH_ACHIEVEMENT = 15;

    // Dictionary to store target goals and achieved progress for each skill.
    private static Dictionary<string, (int expected, int achieved)> targetGoals;

    /// <summary>
    /// Static constructor to initialize the target goals for different skills.
    /// </summary>
    static AchievementDataManager()
    {
        targetGoals = new Dictionary<string, (int expected, int achieved)>
        {
            { "Self Efficacy", (TWELVETH_ACHIEVEMENT, NO_ACHIEVEMENT) },
            { "Leadership", (SIXTH_ACHIEVEMENT, NO_ACHIEVEMENT) },
            { "Communication Skills", (SIXTH_ACHIEVEMENT, NO_ACHIEVEMENT) },
            { "Decision Making", (TENTH_ACHIEVEMENT, NO_ACHIEVEMENT) },
            { "Emotional Intelligence", (SIXTH_ACHIEVEMENT, NO_ACHIEVEMENT) },
            { "Problem Solving", (FIFTEENTH_ACHIEVEMENT, NO_ACHIEVEMENT) }
        };
    }

    /// <summary>
    /// Updates the achieved score for a specified skill.
    /// </summary>
    /// <param name="skill">The name of the skill to update.</param>
    /// <param name="newValue">The new achieved score for the skill.</param>
    public static void UpdateAchievedGoals(string skill, int newValue)
    {
        if (targetGoals.ContainsKey(skill))
        {
            var (expected, _) = targetGoals[skill];
            targetGoals[skill] = (expected, newValue);
        }
    }

    /// <summary>
    /// Retrieves the achieved score for a specified skill.
    /// </summary>
    /// <param name="skill">The name of the skill to query.</param>
    /// <returns>The achieved score for the skill, or <see cref="NO_ACHIEVEMENT"/> if the skill is not found.</returns>
    public static int GetAchievedScore(string skill)
    {
        if (targetGoals.TryGetValue(skill, out var values))
        {
            return values.achieved;
        }
        return NO_ACHIEVEMENT;
    }

    /// <summary>
    /// Retrieves the expected score for a specified skill.
    /// </summary>
    /// <param name="skill">The name of the skill to query.</param>
    /// <returns>The expected score for the skill, or <see cref="NO_ACHIEVEMENT"/> if the skill is not found.</returns>
    public static int GetExpectedScore(string skill)
    {
        if (targetGoals.TryGetValue(skill, out var values))
        {
            return values.expected;
        }
        return NO_ACHIEVEMENT;
    }

    /// <summary>
    /// Retrieves a collection of all skill names for which achievement data is tracked.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{String}"/> containing the names of all tracked skills.</returns>
    public static IEnumerable<string> GetSkills()
    {
        return targetGoals.Keys;
    }
}
