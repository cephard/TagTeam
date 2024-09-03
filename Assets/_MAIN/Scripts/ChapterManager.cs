using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

/// <summary>
/// Manages the game's chapters, including background color changes, chapter introductions, and chapter name display.
/// </summary>
public class ChapterManager : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Text chapterName;
    private string currentChapterName;

    // Dictionary to store chapter details, with each chapter having a name and associated color.
    private Dictionary<string, Dictionary<string, Color>> chapters = new Dictionary<string, Dictionary<string, Color>>();

    /// <summary>
    /// Initializes the chapters with their corresponding names and background colors.
    /// </summary>
    private void Start()
    {
        AddChapter("Chapter1", "The Takeover", new Color(0.93f, 0.76f, 0.83f));
        AddChapter("Chapter2", "Teamleader", new Color(0.85f, 0.64f, 0.87f));
        AddChapter("Chapter3", "Tonner Replacement", new Color(0.70f, 0.87f, 0.90f));
        AddChapter("Chapter4", "Personal life", new Color(1.00f, 0.89f, 0.77f));
        AddChapter("Chapter5", "The Conflict", new Color(0.75f, 0.85f, 0.65f));
        AddChapter("Chapter6", "The Dilemma", new Color(0.69f, 0.77f, 0.87f));
    }

    /// <summary>
    /// Adds a new chapter to the chapter dictionary.
    /// </summary>
    /// <param name="chapterKey">The unique key for the chapter.</param>
    /// <param name="chapterName">The name of the chapter.</param>
    /// <param name="color">The background color associated with the chapter.</param>
    private void AddChapter(string chapterKey, string chapterName, Color color)
    {
        chapters[chapterKey] = new Dictionary<string, Color>
        {
            { chapterName, color }
        };
    }

    /// <summary>
    /// Introduces a chapter by updating the avatar, changing the background, and displaying the chapter name.
    /// </summary>
    /// <param name="avatarName">The name of the avatar triggering the chapter introduction.</param>
    /// <param name="chapterKey">The key identifying the chapter.</param>
    public void IntroduceChapter(string avatarName, string chapterKey)
    {
        if (avatarName.Equals("Chapter"))
        {
            FindAnyObjectByType<CoinManager>().AwardCoinsByProgress();
            FindAnyObjectByType<AvatarManager>().ActivateAvatar(avatarName);
            ChangeChapterBackground(chapterKey);
            currentChapterName = chapterKey;
            Debug.Log(currentChapterName);
            DisplayChapterName(currentChapterName);
        }
        else
        {
            HideChapterName();
        }
    }

    /// <summary>
    /// Changes the background color and displays the name of the specified chapter.
    /// </summary>
    /// <param name="chapterKey">The key identifying the chapter to display.</param>
    public void ChangeChapterBackground(string chapterKey)
    {
        if (chapters.ContainsKey(chapterKey))
        {
            foreach (var chapter in chapters[chapterKey])
            {
                backgroundImage.color = chapter.Value;
                DisplayChapterName(chapter.Key);
            }
        }
    }

    /// <summary>
    /// Displays the name of the specified chapter.
    /// </summary>
    /// <param name="chapter">The name of the chapter to display.</param>
    private void DisplayChapterName(string chapter)
    {
        chapterName.text = chapter;
    }

    /// <summary>
    /// Hides the currently displayed chapter name.
    /// </summary>
    public void HideChapterName()
    {
        chapterName.text = "";
    }

    /// <summary>
    /// Gets the name of the currently active chapter.
    /// </summary>
    /// <returns>The name of the current chapter.</returns>
    public string GetCurrentChapterName()
    {
        return currentChapterName;
    }

    /// <summary>
    /// Retrieves the name of a chapter based on its key.
    /// </summary>
    /// <param name="chapterKey">The key identifying the chapter.</param>
    /// <returns>The name of the chapter, or null if the key is not found.</returns>
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
