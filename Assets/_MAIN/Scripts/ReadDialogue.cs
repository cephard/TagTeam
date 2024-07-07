/*
Class that takes care of the storyline of the game by loading scripts and avatars to their correct responces
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    private int playerChoice;
    private static int currentLine = 0;
    private string[] lines;
    private MainMenuController mainMenuController;
    private static Dictionary<string, int> taskProgress;


    /*loading the text file with the dialogues and setting the eponsences to wait for the NPC dialogues
    skipping blank lines to ensure seamless conversation
    */
    private void Start()
    {
        taskProgress = new Dictionary<string, int>();
        taskProgress["pause"] = 0;
        taskProgress["TaskOne"] = 13;
        taskProgress["Ann'sTask"] = 49;
        taskProgress["UnlockLaptop"] = 127;
        mainMenuController = GetComponent<MainMenuController>();
        avatarManager = GetComponent<AvatarManager>();
        coinManager.GetComponent<CoinManager>();
        avatarManager.InitiliseAvatar();
        avatarManager.DeactivateAvatars();
        LoadDialogueForScene(mainMenuController.GetSceneName());
    }

    public void LoadDialogueForScene(string sceneName)
    {
        if (taskProgress.ContainsKey(sceneName))
        {
            currentLine = taskProgress[sceneName];
        }
        else
        {
            currentLine = 0;

        }
        LoadScript("General2");
    }

    public void LoadScript(string scriptName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(scriptName);
        playerResponse.SetActive(false);
        if (textAsset == null)
        {
            //in case the file response is not found this text will be displayed insted
            dialogue.text = "Oops! Sorry I'll get back to you soon I have an urgent meeting!";
            return;
        }

        lines = textAsset.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        SetNameAndDialogue(currentLine);
    }

    //returns the line on the specific index
    private string GetLine(int lineIndex)
    {
        if (lines == null || lineIndex >= lines.Length)
        {
            mainMenuController.LoadNextScene("Stats");
            return "The End";
        }
        return lines[lineIndex];
    }

    //reading the next line and dsabling the parent game object if there are no lines to read.
    public void NextLine()
    {
        if (lines != null && currentLine < lines.Length - 1)
        {
            currentLine++;
            Debug.Log(currentLine);
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
        if (lines == null || lineIndex >= lines.Length)
        {
            avatarName.text = "Office";
            dialogue.text = "There is no one in the office!";
        }
        string line = lines[lineIndex];
        string[] sentenceParts = line.Split(new[] { ':' }, 2);
        SplitSentence(line, sentenceParts);
    }

    //split the sentence in a  way that the first part is the avatar's name and the other is the dialogue
    public void SplitSentence(string line, string[] sentenceParts)
    {
        if (sentenceParts.Length == 2)
        {
            avatarName.text = sentenceParts[0].Trim();
            dialogue.text = sentenceParts[1].Trim();
            InovkeResponse();
            avatarManager.ActivateAvatar(avatarName.text);
        }
        else
        {
            dialogue.text = line;
        }
        LoadTaskScene();
    }

    public void LoadTaskScene()
    {
        if (String.Equals("Task", avatarName.text))
        {
            SaveNextLine();
            taskProgress[dialogue.text] = currentLine;
            mainMenuController.LoadNextScene(dialogue.text);
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
            playerReponseOne.text = GetLine(startLine);
            playerReponseTwo.text = GetLine(startLine + 1);
            playerReponseThree.text = GetLine(startLine + 2);
            playerReponseFour.text = GetLine(startLine + 3);
        }
        else
        {
            // If there are not enough lines left, deactivate player responses
            playerResponse.SetActive(false);
        }
    }

    //based on the players response the player avatar will skip to that response then skip ahead
    // Extract the integer from the current line
    public void PlayerDecision(int playerChoice)
    {
        currentLine += playerChoice;
        PlayerReport.UpdateDecisions(GetLine(currentLine));
        coinManager.ExtractExpenditure(GetLine(currentLine));
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
