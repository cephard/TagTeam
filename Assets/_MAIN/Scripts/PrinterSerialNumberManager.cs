using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrinterSerialNumberManager : MonoBehaviour
{
    [SerializeField] private Text printedErrorCount;
    [SerializeField] private Text correctErrorCount;
    [SerializeField] private InputField[] errorsFound;
    private AudioManager audioManager;
    private int[] expectedAnswer = { 2, 3, 0, 2, 2 };
    private const int EXPECTED_ERRORS = 9;
    private MainMenuController mainMenuController;
    private ClueManager clueManager;
    private TimerManager timerManager;
    private int timeRequiredForTask = 60;


    private void Start()
    {
        mainMenuController = GetComponent<MainMenuController>();
        timerManager = GetComponent<TimerManager>();
        clueManager = GetComponent<ClueManager>();
        audioManager = GetComponent<AudioManager>();
        timerManager.SetTimer(timeRequiredForTask);
        mainMenuController.LoadCounter();
    }

    void Update()
    {
        mainMenuController.RefreshScene(timerManager.GetTimer(), "PrinterSerial", timeRequiredForTask);
    }


    public void Proceed(string sceneName)
    {
        if (EXPECTED_ERRORS == ValidatePlayerAnswers())
        {
            audioManager.PlayWiningAudio();
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
        int errorFoundByPlayer = 0;

        for (int i = 0; i < errorsFound.Length; i++)
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
            return 0;
        }
    }

}
