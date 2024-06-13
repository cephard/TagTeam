using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadDialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogue;
    [SerializeField] private TextMeshProUGUI avatarName;
    [SerializeField] private GameObject avatarDialogue;
    [SerializeField] private GameObject playerResponce;
    private int currentLine = 0;
    private string[] lines;

    private void Start()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Conversation");
        //avatarDialogue.SetActive(false);
        playerResponce.SetActive(false);
        if (textAsset != null)
        {
            lines = textAsset.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            dialogue.text = GetLineIndex(currentLine);
        }
        else
        {
            dialogue.text = "File not found!";
        }
    }

    private string GetLineIndex(int lineIndex)
    {
        if (lines != null && lineIndex < lines.Length)
        {
            return lines[lineIndex];
        }
        else
        {
            return "No Lines To Read";
        }
    }

    //reading the next line and dsabling the parent game object if there are no lines to read.
    public void NextLine()
    {
        if (lines != null && currentLine < lines.Length - 1)
        {
            currentLine++;
            dialogue.text = GetLineIndex(currentLine);
        }
        else
        {
            avatarDialogue.SetActive(false);
            playerResponce.SetActive(true);
        }
    }
}
