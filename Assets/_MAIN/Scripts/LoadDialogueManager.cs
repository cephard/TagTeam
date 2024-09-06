using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDialogueManager : MonoBehaviour
{
    private TextAsset storyLine;

    ConversationUIManager conversationUIManager;

    private void Awake()
    {
        conversationUIManager = GetComponent<ConversationUIManager>();

    }

    public void SetStoryLine(string storyLineFile)
    {
        storyLine = Resources.Load<TextAsset>(storyLineFile);
    }


    public TextAsset GetStoryLine()
    {
        return storyLine;
    }

    public string[] LoadScript(string scriptName)
    {
        SetStoryLine(scriptName);
        conversationUIManager.ReportEmptyStoryLine(storyLine);
        return storyLine.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

    }
}



