using System.Collections.Generic;
using UnityEngine;

public class FeedBackManager : MonoBehaviour
{
    [SerializeField] private GameObject[] stars = new GameObject[3];
    private CoinManager coinManager;
    private ChapterManager chapterManager;
    private Dictionary<string, int> chapterGemRequirements = new Dictionary<string, int>();
    private int[] feedBackScore = new int[5];

    void Start()
    {
        // Initialize and hide all stars
        HideStars();

        // Get references to other managers
        coinManager = GetComponent<CoinManager>();
        chapterManager = GetComponent<ChapterManager>();

        // Define gem requirements for each chapter
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter1"), 4);
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter2"), 4);
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter3"), 8);
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter4"), 6);
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter5"), 4);
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter6"), 2);
    }

    private void Update()
    {
        // Optionally call AwardStar with the latest gem count
        // AwardStar(coinManager.GetChapterGem());
    }

    public void AwardStar(int availableGem)
    {
        string currentChapterName = chapterManager.GetCurrentChapterName();

        // Check if the current chapter has a gem requirement defined
        if (chapterGemRequirements.ContainsKey(currentChapterName))
        {
            int requiredGems = chapterGemRequirements[currentChapterName];
            int starsToShow = 0;

            // Determine the number of stars to show based on available gems
            if (availableGem >= requiredGems)
            {
                starsToShow = 3; // Award 3 stars
            }
            else if (availableGem == requiredGems - 1)
            {
                starsToShow = 2; // Award 2 stars
            }
            else if (availableGem == requiredGems - 2)
            {
                starsToShow = 1; // Award 1 star
            }
            else
            {
                starsToShow = 0; // Award no stars
            }

            // Show or hide stars based on the starsToShow count
            ShowStars(starsToShow);
        }
    }

    private void ShowStars(int count)
    {
        // Loop through the stars array and set active state based on the count
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(i < count);
        }
    }

    private void HideStars()
    {
        // Hide all stars
        foreach (var star in stars)
        {
            star.SetActive(false);
        }
    }
}
