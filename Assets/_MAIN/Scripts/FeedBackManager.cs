using System.Collections.Generic;
using UnityEngine;

public class FeedBackManager : MonoBehaviour
{
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


    void Start()
    {
        HideStars();
        coinManager = GetComponent<CoinManager>();
        chapterManager = GetComponent<ChapterManager>();

        ChapterFeedBackDeterminant();
    }

    // Define gem requirements for each chapter
    private void ChapterFeedBackDeterminant()
    {
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter1"), FOUR_GEMS);
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter2"), FOUR_GEMS);
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter3"), EIGHT_GEMS);
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter4"), SIX_GEMS);
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter5"), FOUR_GEMS);
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter6"), TWO_GEMS);
    }

    public void AwardStar(int availableGem)
    {
        string currentChapterName = chapterManager.GetCurrentChapterName();

        // Check if the current chapter has a gem requirement defined
        if (chapterGemRequirements.ContainsKey(currentChapterName))
        {
            int requiredGems = chapterGemRequirements[currentChapterName];
            int starsToShow = ZERO_STARS;
            UpdateArchievedScore(availableGem);


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
            ShowStars(starsToShow);
        }
    }

    // Use set the required gems based on the chapter name
    public void UpdateArchievedScore(int availableGem)
    {
        string currentChapterName = chapterManager.GetCurrentChapterName();
        //int requiredGems = ZERO_STARS;
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

    // Loop through the stars array and set active state based on the count
    private void ShowStars(int count)
    {
        for (int i = ZERO_STARS; i < stars.Length; i++)
        {
            stars[i].SetActive(i < count);
        }
    }

    private void HideStars()
    {
        foreach (var star in stars)
        {
            star.SetActive(false);
        }
    }
}
