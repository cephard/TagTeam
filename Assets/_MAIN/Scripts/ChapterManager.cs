using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ChapterManager : UnityEngine.MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Text chapterName;
    Dictionary<string, Dictionary<string, Color>> chapters = new Dictionary<string, Dictionary<string, Color>>();

    private void Start()
    {
        AddChapter("Chapter1", "The Takeover", new Color(0.93f, 0.76f, 0.83f));
        AddChapter("Chapter2", "Teamleader", new Color(0.85f, 0.64f, 0.87f));
        AddChapter("Chapter3", "Tonner Replacement", new Color(0.70f, 0.87f, 0.90f));
        AddChapter("Chapter4", "Personal life", new Color(1.00f, 0.89f, 0.77f));
        AddChapter("Chapter5", "The Conflict", new Color(0.75f, 0.85f, 0.65f));
        AddChapter("Chapter6", "The Dilemma", new Color(0.69f, 0.77f, 0.87f));
    }

    private void AddChapter(string chapterKey, string chapterName, Color color)
    {
        // Add the chapter and its details to the nested dictionary
        chapters[chapterKey] = new Dictionary<string, Color>
        {
            { chapterName, color }
        };
    }

    public void IntroduceChapter(string avatarName, string chapterKey)
    {
        if (avatarName.Equals("Chapter"))
        {
            FindAnyObjectByType<CoinManager>().AwardCoinsByProgress();
            FindAnyObjectByType<AvatarManager>().ActivateAvatar(avatarName);
            ChangeChapterBackground(chapterKey);
        }
        else
        {
            HideChapterName();
        }
    }

    public void ChangeChapterBackground(string chapterKey)
    {
        if (chapters.ContainsKey(chapterKey))
        {
            // Assuming there's only one entry per chapterKey
            foreach (var chapter in chapters[chapterKey])
            {
                backgroundImage.color = chapter.Value;  
                DisplayChapterName(chapter.Key);
            }
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

    public string GetCurrentChapterName()
    {
        return chapterName.text;
    }

    // Method to get the chapter name by key (e.g., "Chapter1")
    public string GetChapterNameByKey(string chapterKey)
    {
        if (chapters.ContainsKey(chapterKey))
        {
            foreach (var chapter in chapters[chapterKey])
            {
                return chapter.Key;
            }
        }
        return null;
    }
}
