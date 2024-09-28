using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    private const int LINE_INCREMENT = 1;
    private CurrentLineManager lineManager;
    private CoinManager coinManager;
    private ConversationUIManager conversationUIManager;
    private MainMenuController mainMenuController;

    private string[] lines;

    private void Awake()
    {
        lineManager = GetComponent<CurrentLineManager>();
        coinManager = GetComponent<CoinManager>();
        conversationUIManager = GetComponent<ConversationUIManager>();
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


    public void NextLine()
    {
        if (GetLines() != null && lineManager.GetCurrentLine() < GetLinesLength() - LINE_INCREMENT)
        {
            lineManager.IncrementCurrentLine();
            coinManager.RefreshCoinState();
           // SetNameAndDialogue(currentLineManager.GetCurrentLine());
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

