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
    private const float TYPEWRITER_SPEED = 0.05f;

    [SerializeField] private TextMeshProUGUI dialogue;
    [SerializeField] private TextMeshProUGUI avatarName;
    [SerializeField] private GameObject avatarDialogue;
    [SerializeField] private GameObject playerResponse;
    [SerializeField] private Text playerReponseOne;
    [SerializeField] private Text playerReponseTwo;
    [SerializeField] private Text playerReponseThree;
    [SerializeField] private Text playerReponseFour;

    private CoinManager coinManager;
    private TaskProgressManager taskProgressManager;
    private AvatarManager avatarManager;
    private ChapterManager chapterManager;
    private MainMenuController mainMenuController;
    private PlayerDecisionManager playerDecisionManager;
    private FeedBackManager feedBackManager;
    private ClueManager clueManager;
    private AnalyticsManager analyticsManager;
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
    }

    private void Start()
    {
        InitializeCustomObjects();
        avatarManager.InitializeAvatar();
        avatarManager.DeactivateAvatars();
        LoadDialogueForScene();
        clueManager.OverRideClueOnStart(playerResponse);
    }

    private void Update()
    {
        HandleInput();
    }

    private bool IsPlayerSpeaking()
    {
        return String.Equals("Player", avatarName.text);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && !IsPlayerSpeaking())
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
            dialogue.text = "Oops! Sorry I'll get back to you soon I have an urgent meeting!";
            return;
        }

        lines = textAsset.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        SetNameAndDialogue(currentLine);
    }

    private string GetLine(int lineIndex)
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
            SwitchActiveObject(avatarDialogue, playerResponse);
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
            avatarName.text = "Office";
            dialogue.text = "There is no one in the office!";
        }
    }

    public void SplitSentence(string line, string[] sentenceParts)
    {
        StopTypeWritterEffect();
        if (sentenceParts.Length == DIVIDE_LINE)
        {
            avatarName.text = sentenceParts[INITIAL_LINE_INDEX].Trim();
            LoadTaskScene(sentenceParts);
            InovkeResponse();
            chapterManager.IntroduceChapter(avatarName.text, sentenceParts[LINE_INCREMENT].Trim());
            avatarManager.ActivateAvatar(avatarName.text);
            typingCoroutine = StartCoroutine(TypeSentence(sentenceParts[LINE_INCREMENT].Trim()));
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
            dialogue.text = sentenceParts[LINE_INCREMENT].Trim();
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
            LoadPlayerResponses(currentLine + LINE_INCREMENT);
        }
    }

    private void LoadPlayerResponses(int startLine)
    {
        if (startLine < lines.Length)
        {
            playerReponseOne.text = GetLine(startLine);
            playerReponseTwo.text = GetLine(startLine + LINE_INCREMENT);
            playerReponseThree.text = GetLine(startLine + SECOND_OPTION);
            playerReponseFour.text = GetLine(startLine + THIRD_OPTION);
        }
        else
        {
            playerResponse.SetActive(false);
        }
    }

    public void PlayerDecision(int playerChoice)
    {
        currentLine += playerChoice;
        int nextLine = SkipRemainingChoice(currentLine, playerChoice);
        playerDecisionManager.SeekAdvice(GetLine(currentLine), GetLine(nextLine), avatarDialogue, playerResponse);
        PlayerReport.UpdateDecisions(GetLine(currentLine));
        PlayFabDataManager.SavePlayerResponse(GetLine(currentLine), currentLine);
        coinManager.ExtractExpenditure(GetLine(currentLine));
        playerDecisionManager.GetPlayerChoice(GetLine(currentLine));
        currentLine = nextLine;
        feedBackManager.AwardStar(coinManager.GetChapterGem());
        SwitchActiveObject(avatarDialogue, playerResponse);
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
            yield return new WaitForSeconds(TYPEWRITER_SPEED);
        }
    }
}
