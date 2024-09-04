using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages feedback for the current chapter by awarding stars based on the number of available gems.
/// The feedback is determined by comparing the player's gem count with the requirements for the chapter.
/// </summary>
public class FeedBackManager : MonoBehaviour
{
    // Constants defining the number of stars, gem divisions, and gem values for each chapter.
    private const int ZERO_STARS = 0;
    private const int ONE_STAR = 1;
    private const int TWO_STARS = 2;
    private const int THREE_STARS = 3;
    private const int HALF_MARKS = 2;
    private const int QUARTER_MARKS = 4;
    private const int TWO_GEMS = 2;
    private const int FOUR_GEMS = 4;
    private const int SIX_GEMS = 6;
    private const int EIGHT_GEMS = 8;

    [SerializeField] private GameObject[] stars = new GameObject[THREE_STARS];

    private CoinManager coinManager;
    private ChapterManager chapterManager;

    private Dictionary<string, int> chapterGemRequirements = new Dictionary<string, int>();

    /// <summary>
    /// Initializes the manager by hiding stars and setting up gem requirements for each chapter.
    /// </summary>
    void Start()
    {
        HideStars();
        coinManager = GetComponent<CoinManager>();
        chapterManager = GetComponent<ChapterManager>();

        ChapterFeedBackDeterminant();
    }

    /// <summary>
    /// Defines the gem requirements needed to achieve full stars for each chapter.
    /// </summary>
    private void ChapterFeedBackDeterminant()
    {
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter1"), FOUR_GEMS);
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter2"), FOUR_GEMS);
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter3"), EIGHT_GEMS);
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter4"), SIX_GEMS);
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter5"), FOUR_GEMS);
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter6"), TWO_GEMS);
    }

    // Determine the number of stars to award based on available gems.
    private void DetermineStarsToAward(int availableGem, int requiredGems, int starsToShow)
    {
        if (availableGem == ZERO_STARS)
        {
            starsToShow = ZERO_STARS;
        }
        else if (availableGem >= requiredGems)
        {
            starsToShow = THREE_STARS;
        }
        else if (availableGem >= requiredGems / HALF_MARKS)
        {
            starsToShow = TWO_STARS;
        }
        else if (availableGem >= requiredGems / QUARTER_MARKS)
        {
            starsToShow = ONE_STAR;
        }
    }

    /// <summary>
    /// Awards stars based on the player's available gems and the chapter's required gems.
    /// </summary>
    /// <param name="availableGem">The number of gems the player currently has.</param>
    public void AwardStar(int availableGem)
    {
        string currentChapterName = chapterManager.GetCurrentChapterName();

        if (chapterGemRequirements.ContainsKey(currentChapterName))
        {
            int requiredGems = chapterGemRequirements[currentChapterName];
            int starsToShow = ZERO_STARS;
            UpdateArchievedScore(availableGem);
            DetermineStarsToAward(availableGem, requiredGems, starsToShow);
            ShowStars(starsToShow);
        }
    }

    /// <summary>
    /// Updates the player's achieved goals based on the current chapter and available gems.
    /// </summary>
    /// <param name="availableGem">The number of gems the player has collected.</param>
    public void UpdateArchievedScore(int availableGem)
    {
        string currentChapterName = chapterManager.GetCurrentChapterName();

        // Update specific achievement categories based on the chapter name.
        switch (currentChapterName)
        {
            case "The Takeover":
                AchievementDataManager.UpdateAchievedGoals("Leadership", availableGem);
                AchievementDataManager.UpdateAchievedGoals("Self Efficacy", availableGem);
                break;
            case "Teamleader":
                AchievementDataManager.UpdateAchievedGoals("Leadership", availableGem);
                AchievementDataManager.UpdateAchievedGoals("Communication Skills", availableGem);
                break;
            case "Tonner Replacement":
                AchievementDataManager.UpdateAchievedGoals("Decision Making", availableGem);
                AchievementDataManager.UpdateAchievedGoals("Self Efficacy", availableGem);
                AchievementDataManager.UpdateAchievedGoals("Problem Solving", availableGem);
                break;
            case "Personal life":
                AchievementDataManager.UpdateAchievedGoals("Emotional Intelligence", availableGem);
                break;
            case "The Conflict":
                AchievementDataManager.UpdateAchievedGoals("Communication Skills", availableGem);
                AchievementDataManager.UpdateAchievedGoals("Problem Solving", availableGem);
                break;
            case "The Dilemma":
                AchievementDataManager.UpdateAchievedGoals("Decision Making", availableGem);
                break;
            default:
                Debug.LogWarning("Chapter name not found: " + currentChapterName);
                return;
        }
    }

    /// <summary>
    /// Displays the given number of stars by setting the active state of the star GameObjects.
    /// </summary>
    /// <param name="count">The number of stars to display.</param>
    private void ShowStars(int count)
    {
        for (int i = ZERO_STARS; i < stars.Length; i++)
        {
            stars[i].SetActive(i < count);
        }
    }

    /// <summary>
    /// Hides all stars by deactivating their corresponding GameObjects.
    /// </summary>
    private void HideStars()
    {
        foreach (var star in stars)
        {
            star.SetActive(false);
        }
    }
}
