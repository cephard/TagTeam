/*
Class that takes care of the storyline of the game by loading scripts and avatars to their correct responces
*/

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
    private AvatarManager avatarManager;
    int playerChoice;


    private int currentLine = 0;
    private string[] lines;

    /*loading the text file with the dialogues and setting the eponsences to wait for the NPC dialogues
    skipping blank lines to ensure seamless conversation
    */
    private void Start()
    {
        avatarManager = GetComponent<AvatarManager>();
        avatarManager.InitiliseAvatar();
        avatarManager.DeactivateAvatars();
        LoadScript("General");
    }

    public void LoadScript(string scriptName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(scriptName);
        playerResponce.SetActive(false);
        if (textAsset != null)
        {
            lines = textAsset.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            SetNameAndDialogue(currentLine);

        }
        else
        {
            //in case the file responce is not found this text will be displayed insted
            dialogue.text = "Oops! Sorry I'll get back to you soon I have an urgent meeting!";
        }
    }

    //returns the line on the specifi index
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
            SetNameAndDialogue(currentLine);

        }
        else
        {
            avatarDialogue.SetActive(false);
            playerResponce.SetActive(true);
        }
    }

    // Sets the dialogue and avatar name based on the current line index
    private void SetNameAndDialogue(int lineIndex)
    {
        if (lines != null && lineIndex < lines.Length)
        {
            string line = lines[lineIndex];
            string[] sentenceParts = line.Split(new[] { ':' }, 2);
            SplitSentence(line, sentenceParts);

        }
        else
        {
            avatarName.text = "Office";
            dialogue.text = "There is no one in the office!";
        }
    }

    public void SplitSentence(string line, string[] sentenceParts)
    {
        if (sentenceParts.Length == 2)
        {
            avatarName.text = sentenceParts[0].Trim();
            dialogue.text = sentenceParts[1].Trim();
            InovkeResponce();
            avatarManager.ActivateAvatar(avatarName.text);
        }
        else
        {
            dialogue.text = line;
        }
    }
    public void InovkeResponce()
    {
        if (String.Equals("Player", avatarName.text))
        {
            avatarDialogue.SetActive(false);
            playerResponce.SetActive(true);
        }
    }

    //based on the players responce the player avatar will skip to that response then skip ahead
    public void PlayerDecision(int playerChoice)
    {

        currentLine += playerChoice;
        avatarDialogue.SetActive(true);
        playerResponce.SetActive(false);
        SetNameAndDialogue(currentLine);
        currentLine = SkipRemainingChoice(currentLine, playerChoice);
    }

    //The scripts avoids the multiple choice options after the player selects one to ensure a seamless storyline
    private int SkipRemainingChoice(int currentLine, int playerChoice)
    {
        int playerOptions = 4;
        return currentLine += (playerOptions - playerChoice);
    }
}
