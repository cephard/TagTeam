using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrinterSerialNumberManager : UnityEngine.MonoBehaviour
{
    private const int NO_ERROR = 0;
    [SerializeField] private Text printedErrorCount;
    [SerializeField] private Text correctErrorCount;
    [SerializeField] private InputField[] errorsFound;
    private AudioManager audioManager;
    private int[] expectedAnswer = { 2, 3, 0, 2, 2 };
    private const int EXPECTED_ERRORS = 9;
    private MainMenuController mainMenuController;
    private PlayerChanceManager playerChanceManager;
    private ClueManager clueManager;
    private TimerManager timerManager;
    private int timeRequiredForTask = 60;
    private AnalyticsManager analyticsManager;


    private void Start()
    {
        mainMenuController = GetComponent<MainMenuController>();
        timerManager = GetComponent<TimerManager>();
        clueManager = GetComponent<ClueManager>();
        audioManager = GetComponent<AudioManager>();
        playerChanceManager = GetComponent<PlayerChanceManager>();
        analyticsManager = GetComponent<AnalyticsManager>();
        timerManager.SetTimer(timeRequiredForTask);
        playerChanceManager.LoadRemainingChance();
        analyticsManager.TrackEvent("Printer Serial Task");
    }

    void Update()
    {
        mainMenuController.RefreshScene(timerManager.GetTimer(), "PrinterSerial", timeRequiredForTask);
    }


    public void Proceed(string sceneName)
    {
        if (EXPECTED_ERRORS == ValidatePlayerAnswers())
        {
            audioManager.PlayWinningAudio();
            clueManager.ShowWinOrLooseClue("Congratulations! You Rock!");
            mainMenuController.LoadNextChapter(sceneName);
        }
        else
        {
            audioManager.PlayWrongAnswerAudio();
            clueManager.ShowWinOrLooseClue("Please Try Again!");
        }
    }

    //check exact answer for each error spotted
    private int ValidatePlayerAnswers()
    {
        int errorFoundByPlayer = NO_ERROR;

        for (int i = NO_ERROR; i < errorsFound.Length; i++)
        {
            if (ParseInputField(errorsFound[i].text) == expectedAnswer[i])
            {
                errorFoundByPlayer += ParseInputField(errorsFound[i].text);
            }
            else
            {
                return errorFoundByPlayer;
            }
        }
        return errorFoundByPlayer;
    }

    public void SetErrorCount(int errorCount)
    {
        printedErrorCount.text = errorCount.ToString();
    }

    private int ParseInputField(string errorsFound)
    {
        int result;
        if (int.TryParse(errorsFound, out result))
        {
            return result;
        }
        else
        {
            return NO_ERROR;
        }
    }

}
