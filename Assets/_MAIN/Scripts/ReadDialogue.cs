using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadDialogue : MonoBehaviour
{
    //making fields serialissable to enable access in unity
    [SerializeField] private TextMeshProUGUI dialogue;
    [SerializeField] private TextMeshProUGUI avatarName;
    [SerializeField] private GameObject avatarDialogue;
    [SerializeField] private GameObject playerResponce;
    private int currentLine = 0;
    private string[] lines;

    /*loading the text file with the dialogues and setting the eponsences to wait for the NPC dialogues
    skipping blank lines to ensure seamless conversation
    */private void Start()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Conversation");
        playerResponce.SetActive(false);
        if (textAsset != null)
        {
            lines = textAsset.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            dialogue.text = GetLineIndex(currentLine);
        }
        else
        {
            //in case i the file responce is not found this text will be displayed insted
            dialogue.text = "Oops! Sorry I'll get back to you soon I have an urgent meeting!";
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
