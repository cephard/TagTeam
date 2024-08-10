using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBackManager : MonoBehaviour
{
    [SerializeField] private GameObject starOne;
    [SerializeField] private GameObject starTwo;
    [SerializeField] private GameObject starThree;
    private CoinManager coinManager;
    private ChapterManager chapterManager;


    private Dictionary<string, int> chapterGemRequirements = new Dictionary<string, int>();

    void Start()
    {
        HideStars();
        coinManager = GetComponent<CoinManager>();
        chapterManager = GetComponent<ChapterManager>();
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter1"), 2);
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter2"), 2);
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter3"), 4);
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter4"), 3);
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter5"), 2);
        chapterGemRequirements.Add(chapterManager.GetChapterNameByKey("Chapter6"), 1);
    }

    private void Update()
    {
        AwardStar(coinManager.GetSpecialGem());
        Debug.Log(coinManager.GetSpecialGem().ToString() + chapterManager.GetCurrentChapterName());
    }
    public void AwardStar(int availableGem)
    {
        string currentChapterName = chapterManager.GetCurrentChapterName();

        // Check if the current chapter has a gem requirement defined
        if (chapterGemRequirements.ContainsKey(currentChapterName))
        {
            // Compare available gems with required gems and award stars accordingly
            int requiredGems = chapterGemRequirements[currentChapterName];
            if (availableGem == 0) { 
                HideStars();
            }else if (availableGem >= requiredGems)
            {
                // Award 3 stars if available gems are equal or more than required
                starOne.SetActive(true);
                starTwo.SetActive(true);
                starThree.SetActive(true);
            }
            else if (availableGem == requiredGems - 1)
            {
                // Award 2 stars if available gems are one less than required
                starOne.SetActive(true);
                starTwo.SetActive(true);
                starThree.SetActive(false);
            }
            else if (availableGem == requiredGems - 2)
            {
                // Award 1 star if available gems are two less than required
                starOne.SetActive(true);
                starTwo.SetActive(false);
                starThree.SetActive(false);
            }
            else
            {
                // No stars awarded if available gems are less than required - 2
                HideStars();
            }
        }
        else
        {
            // No stars awarded if the chapter name is not found
            HideStars();
        }
    }


    private void HideStars()
    {
        starOne.SetActive(false);
        starTwo.SetActive(false);
        starThree.SetActive(false);
    }


}
