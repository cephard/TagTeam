using System.Collections.Generic;
using UnityEngine;

public static class AchievementDataManager
{
    private static Dictionary<string, (int expected, int achieved)> targetGoals;

    static AchievementDataManager()
    {
        targetGoals = new Dictionary<string, (int expected, int achieved)>
        {
            { "Self Efficacy", (12, 0) },
            { "Leadership", (6, 0) },
            { "Communication Skills", (6, 0) },
            { "Decision Making", (10, 0) },
            { "Emotional Intelligence", (6, 0) }
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
        return 0;
    }

    public static int GetExpectedScore(string skill)
    {
        if (targetGoals.TryGetValue(skill, out var values))
        {
            return values.expected;
        }
        return 0;
    }

    public static IEnumerable<string> GetSkills()
    {
        return targetGoals.Keys;
    }
}
