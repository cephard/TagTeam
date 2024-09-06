using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    private const int LINE_INCREMENT = 1;

    private CoinManager coinManager;
    private ConversationUIManager conversationUIManager;
    private ReadDialogue readDialogue;
    private MainMenuController mainMenuController;

    private string[] lines;

    private void Start()
    {
        coinManager = GetComponent<CoinManager>();
        conversationUIManager = GetComponent<ConversationUIManager>();
        readDialogue = GetComponent<ReadDialogue>();
        mainMenuController = GetComponent<MainMenuController>();
    }

    public void SetLines(string[] newLines)
    {
        lines = newLines;
    }

    public string[] GetLines()
    {
        return lines;
    }

    public string GetSpecificLine(int index)
    {
        return lines[index];
    }

    public string GetLine(int lineIndex)
    {
        if (lines == null || lineIndex >= lines.Length)
        {
            mainMenuController.LoadNextScene("PlayerStatistics");
            return "The End";
        }
        return lines[lineIndex];
    }

    public int GetLinesLength()
    {
        return lines.Length;
    }


    public void NextLine(int currentLinee)
    {
        if (lines != null && currentLinee < lines.Length - LINE_INCREMENT)
        {
            currentLinee++;
            coinManager.RefreshCoinState();
            string coins = coinManager.GetCoins().ToString();
            readDialogue.SetNameAndDialogue(currentLinee);
        }
        else
        {
            conversationUIManager.SwitchActiveObject();
        }
    }

    public void PlaceHolderDialogue(int lineIndex)
    {
        if (lines == null || lineIndex >= lines.Length)
        {
            conversationUIManager.SetAvatarName("Office");
            conversationUIManager.SetDialogueText("There is no one in the office!");
        }
    }
}

