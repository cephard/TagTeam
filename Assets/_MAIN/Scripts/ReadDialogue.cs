/*
Class that takes care of the storyline of the game by loading scripts and avatars to their correct responces
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ReadDialogue : UnityEngine.MonoBehaviour
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
    private TaskProgressManager taskProgressManager;
    private AvatarManager avatarManager;
    private ChapterManager chapterManager;
    private int playerChoice;
    private static int currentLine = 0;
    private string[] lines;
    private MainMenuController mainMenuController;
    private PlayerDecisionManager playerDecisionManager;
    private FeedBackManager feedBackManager;
    private ClueManager clueManager;
    private bool clueHidden = false;


    private void InitializeCustomObjects()
    {
        mainMenuController = GetComponent<MainMenuController>();
        avatarManager = GetComponent<AvatarManager>();
        coinManager = GetComponent<CoinManager>();
        chapterManager = GetComponent<ChapterManager>();
        playerDecisionManager = GetComponent<PlayerDecisionManager>();
        taskProgressManager = GetComponent<TaskProgressManager>();
        clueManager = GetComponent<ClueManager>();
        feedBackManager = GetComponent<FeedBackManager>();
    }

    /*loading the text file with the dialogues and setting the eponsences to wait for the NPC dialogues
    skipping blank lines to ensure seamless conversation
    */
    private void Start()
    {
        InitializeCustomObjects();
        avatarManager.InitiliseAvatar();
        avatarManager.DeactivateAvatars();
        LoadDialogueForScene();
        clueManager.OverRideClueOnStart(playerResponse);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && !String.Equals("Player", avatarName.text))
        {
            NextLine();
        }
    }

    public void LoadDialogueForScene()
    {
        currentLine = taskProgressManager.GetTaskProgress(mainMenuController.GetSceneName());
        LoadScript("Story");
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
            mainMenuController.LoadNextScene("PlayStatistics");
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
            coinManager.RefreshCoinState();
            string coins = coinManager.GetCoins().ToString();
            //Debug.Log(currentLine);
            SetNameAndDialogue(currentLine);
        }
        else
        {
            SwitchActiveObject(avatarDialogue, playerResponse);
        }
    }


    // Sets the dialogue and avatar name based on the current line index
    private void SetNameAndDialogue(int lineIndex)
    {
        PlaceHolderDialogue(lineIndex);
        string line = lines[lineIndex];
        string[] sentenceParts = line.Split(new[] { ':' }, 2);
        SplitSentence(line, sentenceParts);
    }

    private void PlaceHolderDialogue(int lineIndex)
    {
        if (lines == null || lineIndex >= lines.Length)
        {
            avatarName.text = "Office";
            dialogue.text = "There is no one in the office!";
        }
    }


    //split the sentence in a  way that the first part is the avatar's name and the other is the dialogue
    public void SplitSentence(string line, string[] sentenceParts)
    {
        StopTypeWritterEffect();
        if (sentenceParts.Length == 2)
        {
            avatarName.text = sentenceParts[0].Trim();
            LoadTaskScene(sentenceParts);
            InovkeResponse();
            chapterManager.IntroduceChapter(avatarName.text, sentenceParts[1].Trim());
            avatarManager.ActivateAvatar(avatarName.text);
            typingCoroutine = StartCoroutine(TypeSentence(sentenceParts[1].Trim()));
        }
        else
        {
            typingCoroutine = StartCoroutine(TypeSentence(line));
        }

    }

    private void StopTypeWritterEffect()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
    }

    public void LoadTaskScene(string[] sentenceParts)
    {
        if (String.Equals("Task", avatarName.text))
        {
            dialogue.text = sentenceParts[1].Trim();
            SaveNextLine();
            taskProgressManager.SetTaskProgress(dialogue.text, currentLine);
            mainMenuController.LoadNextScene(dialogue.text);
        }
    }

    public void InovkeResponse()
    {
        if (String.Equals("Player", avatarName.text))
        {
            SwitchActiveObject(avatarDialogue, playerResponse);
            LoadPlayerResponses(currentLine + 1);
        }
    }

    //Load player responces
    // If there are not enough lines left, deactivate player responses
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
            playerResponse.SetActive(false);
        }
    }


    //based on the players response the player avatar will skip to that response then skip ahead
    // Extract the integer from the current line
    public void PlayerDecision(int playerChoice)
    {
        currentLine += playerChoice;
        int nextLine = SkipRemainingChoice(currentLine, playerChoice);
        PlayerReport.UpdateDecisions(GetLine(currentLine));
        coinManager.ExtractExpenditure(GetLine(currentLine));
        playerDecisionManager.GetPlayerChoice(GetLine(currentLine));
        playerDecisionManager.SeekAdvice(GetLine(currentLine), GetLine(nextLine), avatarDialogue, playerResponse);
        currentLine = nextLine;
        feedBackManager.AwardStar(coinManager.GetChapterGem());
        SwitchActiveObject(avatarDialogue, playerResponse);
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


    private Coroutine typingCoroutine;

    private IEnumerator TypeSentence(string sentence)
    {
        dialogue.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogue.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

}
