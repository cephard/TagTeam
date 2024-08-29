using System.Collections.Generic;
using UnityEngine;

public static class AchievementDataManager
{
    private const int NO_ACHIEVEMENT = 0;
    private const int SIXTH_ACHIEVEMENT = 6;
    private const int TENTH_ACHIEVEMENT = 10;
        private const int TWELVETH_ACHIEVEMENT = 12;
    private const int FIFTEENTH_ACHIEVEMENT = 15;
    private static Dictionary<string, (int expected, int achieved)> targetGoals;

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

    public static void UpdateAchievedGoals(string skill, int newValue)
    {
        if (targetGoals.ContainsKey(skill))
        {
            var (expected, _) = targetGoals[skill];
            targetGoals[skill] = (expected, newValue);
        }
    }

    public static int GetAchievedScore(string skill)
    {
        if (targetGoals.TryGetValue(skill, out var values))
        {
            return values.achieved;
        }
        return NO_ACHIEVEMENT;
    }

    public static int GetExpectedScore(string skill)
    {
        if (targetGoals.TryGetValue(skill, out var values))
        {
            return values.expected;
        }
        return NO_ACHIEVEMENT;
    }

    public static IEnumerable<string> GetSkills()
    {
        return targetGoals.Keys;
    }
}
