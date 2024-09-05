using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ReadDialogue : MonoBehaviour
{
    private const int DIVIDE_LINE = 2;
    private const int INITIAL_LINE_INDEX = 0;
    private const int LINE_INCREMENT = 1;
    private const int FIRST_OPTION = 1;
    private const int SECOND_OPTION = 2;
    private const int THIRD_OPTION = 3;
    private const int FOURTH_OPTION = 4;
    private const int PLAYER_OPTIONS = 4;

    private CoinManager coinManager;
    private TaskProgressManager taskProgressManager;
    private AvatarManager avatarManager;
    private ChapterManager chapterManager;
    private MainMenuController mainMenuController;
    private PlayerDecisionManager playerDecisionManager;
    private FeedBackManager feedBackManager;
    private ClueManager clueManager;
    private AnalyticsManager analyticsManager;
    private TypeWritterEffectManager typeWritter;
    private ConversationUIManager conversationUIManager;
    private PlayerResponseManager playerResponceManager;

    private int playerChoice;
    private static int currentLine = INITIAL_LINE_INDEX;
    private string[] lines;
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
        analyticsManager = GetComponent<AnalyticsManager>();
        typeWritter = GetComponent<TypeWritterEffectManager>();
        conversationUIManager = GetComponent<ConversationUIManager>();
        playerResponceManager = GetComponent<PlayerResponseManager>();
    }

    private void Start()
    {
        InitializeCustomObjects();
        avatarManager.InitializeAvatar();
        avatarManager.DeactivateAvatars();
        LoadDialogueForScene();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && !conversationUIManager.IsPlayerSpeaking())
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
        //playerResponse.SetActive(false);
        conversationUIManager.HidePlayerResponce();
        if (textAsset == null)
        {
            conversationUIManager.SetDialogueText("Oops! Sorry I'll get back to you soon I have an urgent meeting!");
            return;
        }

        lines = textAsset.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        SetNameAndDialogue(currentLine);
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

    public void NextLine()
    {
        if (lines != null && currentLine < lines.Length - LINE_INCREMENT)
        {
            currentLine++;
            coinManager.RefreshCoinState();
            string coins = coinManager.GetCoins().ToString();
            SetNameAndDialogue(currentLine);
        }
        else
        {
            conversationUIManager.SwitchActiveObject(conversationUIManager.GetAvatarDialogue(), conversationUIManager.GetPlayerResponse());
        }
    }

    private void SetNameAndDialogue(int lineIndex)
    {
        PlaceHolderDialogue(lineIndex);
        string line = lines[lineIndex];
        string[] sentenceParts = line.Split(new[] { ':' }, DIVIDE_LINE);
        SplitSentence(line, sentenceParts);
    }

    private void PlaceHolderDialogue(int lineIndex)
    {
        if (lines == null || lineIndex >= lines.Length)
        {
            conversationUIManager.SetAvatarName("Office");
            conversationUIManager.SetDialogueText("There is no one in the office!");
        }
    }

    public void SplitSentence(string line, string[] sentenceParts)
    {
        typeWritter.StopTypeWritter();
        if (sentenceParts.Length == DIVIDE_LINE)
        {
            conversationUIManager.SetAvatarName(sentenceParts[INITIAL_LINE_INDEX].Trim());
            LoadTaskScene(sentenceParts);
            InovkeResponse();
            chapterManager.IntroduceChapter(conversationUIManager.GetAvatarName(), sentenceParts[LINE_INCREMENT].Trim());
            avatarManager.ActivateAvatar(conversationUIManager.GetAvatarName());
            typeWritter.StartTypeWritter(sentenceParts[LINE_INCREMENT].Trim(), conversationUIManager.GetDialogue());
        }
        else
        {
            typeWritter.StartTypeWritter(line, conversationUIManager.GetDialogue());

        }

    }

    public void LoadTaskScene(string[] sentenceParts)
    {
        if (String.Equals("Task", conversationUIManager.GetAvatarName()))
        {
            conversationUIManager.SetDialogueText(sentenceParts[LINE_INCREMENT].Trim());
            SaveNextLine();
            taskProgressManager.SetTaskProgress(conversationUIManager.GetDialogueText(), currentLine);
            mainMenuController.LoadNextScene(conversationUIManager.GetDialogueText());
        }
    }

    public void InovkeResponse()
    {
        if (String.Equals("Player", conversationUIManager.GetAvatarName()))
        {
            conversationUIManager.SwitchActiveObject(conversationUIManager.GetAvatarDialogue(), conversationUIManager.GetPlayerResponse());
            LoadPlayerResponses(currentLine + LINE_INCREMENT);
        }
    }


    public void LoadPlayerResponses(int startLine)
    {
        if (startLine < lines.Length)
        {
            conversationUIManager.SetPlayerResponceOne(GetLine(startLine));
            conversationUIManager.SetPlayerResponceTwo(GetLine(startLine + FIRST_OPTION));
            conversationUIManager.SetPlayerResponceThree(GetLine(startLine + SECOND_OPTION));
            conversationUIManager.SetPlayerResponceFour(GetLine(startLine + THIRD_OPTION));
        }
        else
        {

            conversationUIManager.HidePlayerResponce();
        }
    }


    public void PlayerDecision(int playerChoice)
    {
        currentLine += playerChoice;
        int nextLine = SkipRemainingChoice(currentLine, playerChoice);
        playerDecisionManager.SeekAdvice(GetLine(currentLine), GetLine(nextLine), conversationUIManager.GetAvatarDialogue(), conversationUIManager.GetPlayerResponse());
        PlayerReport.UpdateDecisions(GetLine(currentLine));
        PlayFabDataManager.SavePlayerResponse(GetLine(currentLine), currentLine);
        coinManager.ExtractExpenditure(GetLine(currentLine));
        playerDecisionManager.GetPlayerChoice(GetLine(currentLine));
        currentLine = nextLine;
        feedBackManager.AwardStar(coinManager.GetChapterGem());
        conversationUIManager.SwitchActiveObject(conversationUIManager.GetAvatarDialogue(), conversationUIManager.GetPlayerResponse());
        NextLine();
    }

    private int SkipRemainingChoice(int currentLine, int playerChoice)
    {
        return currentLine += (PLAYER_OPTIONS - playerChoice);
    }

    public void SaveNextLine()
    {
        PlayerPrefs.SetInt("CurrentLine", currentLine += SECOND_OPTION);
        PlayerPrefs.Save();
    }
}
