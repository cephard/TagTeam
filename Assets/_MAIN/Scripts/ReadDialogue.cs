/*
Class that takes care of the storyline of the game by loading scripts and avatars to their correct responces
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ReadDialogue : MonoBehaviour
{
    //making fields serialissable to enable access in unity
    [SerializeField] private TextMeshProUGUI dialogue;
    [SerializeField] private TextMeshProUGUI avatarName;
    [SerializeField] private GameObject avatarDialogue;
    [SerializeField] private GameObject playerResponse;
    [SerializeField] private Text playerReponseOne;
    [SerializeField] private Text playerReponseTwo;
    [SerializeField] private Text playerReponseThree;
    [SerializeField] private Text playerReponseFour;
    [SerializeField] private CoinManager coinManager;
    private AvatarManager avatarManager;
    int playerChoice;

    private int currentLine = 0;
    private string[] lines;

    /*loading the text file with the dialogues and setting the eponsences to wait for the NPC dialogues
    skipping blank lines to ensure seamless conversation
    */
    private void Start()
    {
        currentLine = PlayerPrefs.GetInt("CurrentLine", 0);
        currentLine = 0; // this is temporaray for testing alone t paypass the saved line

        avatarManager = GetComponent<AvatarManager>();
        coinManager.GetComponent<CoinManager>();
        avatarManager.InitiliseAvatar();
        avatarManager.DeactivateAvatars();
        LoadScript("General");
        //SaveNextLine();
    }

    public void LoadScript(string scriptName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(scriptName);
        playerResponse.SetActive(false);
        if (textAsset != null)
        {
            lines = textAsset.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            SetNameAndDialogue(currentLine);

        }
        else
        {
            //in case the file response is not found this text will be displayed insted
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
            playerResponse.SetActive(true);
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

    //split the sentence in a  way that the first part is the avatar's name and the other is the dialogue
    public void SplitSentence(string line, string[] sentenceParts)
    {
        if (sentenceParts.Length == 2)
        {
            avatarName.text = sentenceParts[0].Trim();
            dialogue.text = sentenceParts[1].Trim();

            if (String.Equals("Player", avatarName.text))
            {
                avatarDialogue.SetActive(false);
                playerResponse.SetActive(true);

            }

            InovkeResponse();
            avatarManager.ActivateAvatar(avatarName.text);
        }
        else
        {
            dialogue.text = line;
        }

        if (String.Equals("Task", avatarName.text))
        {
            SaveNextLine();
        }
    }

    public void InovkeResponse()
    {
        if (String.Equals("Player", avatarName.text))
        {
            avatarDialogue.SetActive(false);
            playerResponse.SetActive(true);
            LoadPlayerResponses(currentLine + 1);
        }
    }


    private void LoadPlayerResponses(int startLine)
    {

        if (startLine < lines.Length)
        {
            playerReponseOne.text = GetLineIndex(startLine);
            playerReponseTwo.text = GetLineIndex(startLine + 1);
            playerReponseThree.text = GetLineIndex(startLine + 2);
            playerReponseFour.text = GetLineIndex(startLine + 3);
        }
        else
        {
            // If there are not enough lines left, deactivate player responses
            playerResponse.SetActive(false);
        }
    }

    //based on the players response the player avatar will skip to that response then skip ahead
    public void PlayerDecision(int playerChoice)
    {
        currentLine += playerChoice;
        // Extract the integer from the current line
    
        coinManager.ExtractExpenditure(GetLineIndex(currentLine));


        currentLine = SkipRemainingChoice(currentLine, playerChoice);
        avatarDialogue.SetActive(true);
        playerResponse.SetActive(false);
        NextLine();
    }

    //The scripts avoids the multiple choice options after the player selects one to ensure a seamless storyline
    private int SkipRemainingChoice(int currentLine, int playerChoice)
    {
        int playerOptions = 4;
        return currentLine += (playerOptions - playerChoice);
    }

    public void SaveNextLine()
    {
        PlayerPrefs.SetInt("CurrentLine", currentLine += 2);
        PlayerPrefs.Save();
    }


    //switch the actice layer based on the script
    private void SwitchActiveObject(GameObject avatarDialogue, GameObject playerResponse)
    {
        bool isAvatarDialogueActive = avatarDialogue.activeSelf;
        avatarDialogue.SetActive(!isAvatarDialogueActive);
        playerResponse.SetActive(isAvatarDialogueActive);
    }
}
