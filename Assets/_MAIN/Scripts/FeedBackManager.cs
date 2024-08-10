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
        chapterGemRequirements.Add("The Takeover", 2); 
        chapterGemRequirements.Add("Teamleader", 1);   
        chapterGemRequirements.Add("Tonner Replacement", 3); 
        chapterGemRequirements.Add("Personal life", 2); 
        chapterGemRequirements.Add("The Conflict", 1);  
        chapterGemRequirements.Add("The Dilemma", 3);
    }


    public void AwardStar(int availableGem)
    {
        if (availableGem < 2 && chapterManager.GetCurrentChapterName() == "")
        {
        }
    }

    private void HideStars()
    {
        starOne.SetActive(false);
        starTwo.SetActive(false);
        starThree.SetActive(false);
    }


}
