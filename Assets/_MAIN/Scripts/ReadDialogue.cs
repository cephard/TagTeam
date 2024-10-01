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
    private LoadDialogueManager loadDialogueManager;
    private DialogueManager dialogueManager;
    private CurrentLineManager currentLineManager;

    private int playerChoice;

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
        loadDialogueManager = GetComponent<LoadDialogueManager>();
        dialogueManager = GetComponent<DialogueManager>();
        currentLineManager = GetComponent<CurrentLineManager>();
    }

    private void Start()
    {
        InitializeCustomObjects();
        avatarManager.InitializeAvatar();
        avatarManager.DeactivateAvatars();
        LoadDialogueForScene();
        currentLineManager.SetCurrentLine(taskProgressManager.GetTaskProgress(mainMenuController.GetSceneName()));

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

        currentLineManager.SetCurrentLine(taskProgressManager.GetTaskProgress(mainMenuController.GetSceneName()));
        dialogueManager.SetLines(loadDialogueManager.LoadScript("Story"));

        SetNameAndDialogue(currentLineManager.GetCurrentLine());


    }

    public void NextLine()
    {

        dialogueManager.NextLine();
        SetNameAndDialogue(currentLineManager.GetCurrentLine());
    }

    public void SetNameAndDialogue(int lineIndex)
    {
        dialogueManager.PlaceHolderDialogue(lineIndex);
        string line = dialogueManager.GetLine(lineIndex);
        string[] sentenceParts = line.Split(new[] { ':' }, DIVIDE_LINE);
        SplitSentence(line, sentenceParts);
    }

    public void SplitSentence(string line, string[] sentenceParts)
    {
        typeWritter.StopTypeWritter();


        if (sentenceParts.Length == DIVIDE_LINE)
        {
            conversationUIManager.SetAvatarName(sentenceParts[INITIAL_LINE_INDEX].Trim());

            taskProgressManager.LoadTaskScene(sentenceParts, currentLineManager.GetCurrentLine());

            playerResponceManager.InovkePlayerResponse(currentLineManager.GetCurrentLine());

            chapterManager.IntroduceChapter(conversationUIManager.GetAvatarName(), sentenceParts[LINE_INCREMENT].Trim());
            avatarManager.ActivateAvatar(conversationUIManager.GetAvatarName());

            typeWritter.StartTypeWritter(sentenceParts[LINE_INCREMENT].Trim(), conversationUIManager.GetDialogue());
        }
        else
        {
            typeWritter.StartTypeWritter(line, conversationUIManager.GetDialogue());

        }
    }

    public void PlayerDecision(int playerChoice)
    {
        currentLineManager.AddUpCurrentLine(playerChoice);

        int nextLine = SkipRemainingChoice(currentLineManager.GetCurrentLine(), playerChoice);
        playerDecisionManager.SeekAdvice(dialogueManager.GetLine(currentLineManager.GetCurrentLine()), dialogueManager.GetLine(nextLine), conversationUIManager.GetAvatarDialogue(), conversationUIManager.GetPlayerResponse());
        playerResponceManager.SavePlayerChoice(currentLineManager.GetCurrentLine());


        coinManager.ExtractExpenditure(dialogueManager.GetLine(currentLineManager.GetCurrentLine()));

        playerDecisionManager.GetPlayerChoice(dialogueManager.GetLine(currentLineManager.GetCurrentLine()));


        currentLineManager.SetCurrentLine(nextLine);
        feedBackManager.AwardStar(coinManager.GetChapterGem());
        conversationUIManager.SwitchActiveObject();
        NextLine();
    }

    public int SkipRemainingChoice(int currentLine, int playerChoice)
    {
        return currentLine += (PLAYER_OPTIONS - playerChoice);
    }
}

