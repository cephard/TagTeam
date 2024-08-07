using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ChapterManager : UnityEngine.MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Text chapterName;
    Dictionary<string, Color> backgroundColors = new Dictionary<string, Color>();

    private void Start()
    {
        backgroundColors.Add("The Takeover", new Color(0.93f, 0.76f, 0.83f));
        backgroundColors.Add("Teamleader", new Color(0.85f, 0.64f, 0.87f));
        backgroundColors.Add("Tonner Replacement", new Color(0.70f, 0.87f, 0.90f));
        backgroundColors.Add("Personal life", new Color(1.00f, 0.89f, 0.77f));
        backgroundColors.Add("The Conflict", new Color(0.75f, 0.85f, 0.65f));
        backgroundColors.Add("The Dilemma", new Color(0.69f, 0.77f, 0.87f));
    }

    public void IntroduceChapter(string avatarName, string currentChapter)
    {
        if (avatarName.Equals("Chapter"))
        {
            FindAnyObjectByType<CoinManager>().AwardCoinsByProgress();
            FindAnyObjectByType<AvatarManager>().ActivateAvatar(avatarName);
            ChangeChapterBackground(currentChapter);
        }
        else
        {
            HideChapterName();
        }
    }

    public void ChangeChapterBackground(string chapter)
    {
        if (backgroundColors.ContainsKey(chapter))
        {
            backgroundImage.color = backgroundColors[chapter];
            DisplayChapterName(chapter);
        }
    }

    private void DisplayChapterName(string chapter)
    {
        chapterName.text = chapter;
    }

    public void HideChapterName()
    {
        chapterName.text = "";
    }

}
