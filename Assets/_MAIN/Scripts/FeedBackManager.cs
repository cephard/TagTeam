using System.Collections.Generic;
using UnityEngine;

public class FeedBackManager : MonoBehaviour
{
    [SerializeField] private GameObject[] stars = new GameObject[3];
  
    private CoinManager coinManager;
    private ChapterManager chapterManager;
    private Dictionary<string, int> chapterGemRequirements = new Dictionary<string, int>();


    void Start()
    {
        HideStars();
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

    }

    public void AwardStar(int availableGem)
    {
        string currentChapterName = chapterManager.GetCurrentChapterName();

        // Check if the current chapter has a gem requirement defined
        if (chapterGemRequirements.ContainsKey(currentChapterName))
        {
            int requiredGems = chapterGemRequirements[currentChapterName];
            int starsToShow = 0;
            if (availableGem == 0)
            {

                starsToShow = 0;

            }
            else if (availableGem >= requiredGems)
            {
                starsToShow = 3;
            }
            else if (availableGem == requiredGems - 1)
            {
                starsToShow = 2;
            }
            else if (availableGem == requiredGems - 2)
            {
                starsToShow = 1;
            }
            ShowStars(starsToShow);
        }
    }

    // Loop through the stars array and set active state based on the count
    private void ShowStars(int count)
    {
        for (int i = 0; i < stars.Length; i++)
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
