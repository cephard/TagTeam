using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentLineManager : DialogueManager
{
    private int currentLine;

    public void SetCurrentLine(int newLineIndex)
    {
        currentLine = newLineIndex;
    }

    public int GetCurrentLine()
    {
        return currentLine;
    }

    public int IncrementCurrentLine()
    {
        currentLine++;
        return currentLine;
    }

    public int AddUpCurrentLine(int newLineIndex)
    {
        currentLine += newLineIndex;
        return currentLine;
    }

    public void SaveNextLine()
    {
        PlayerPrefs.SetInt("CurrentLine", AddUpCurrentLine(2));
        PlayerPrefs.Save();
    }
}
