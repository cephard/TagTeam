using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ConfidentialFileManager : UnityEngine.MonoBehaviour
{
    private const int SECRET_VALUE = 14;
    [SerializeField] private Text puzzleClue;
    [SerializeField] private Text motivationText;
    [SerializeField] private GameObject cluePanel;
    [SerializeField] private InputField secretValue;
    private MainMenuController mainMenuController;
    private PlayerChanceManager playerChanceManager;
    private AudioManager audioManager;
    private ClueManager clueManager;
    private int timeRequiredForTask = 60;
    private TimerManager timerManager;

    private void Start()
    {
        mainMenuController = GetComponent<MainMenuController>();
        timerManager = GetComponent<TimerManager>();
        audioManager = GetComponent<AudioManager>();
        clueManager = GetComponent<ClueManager>();
        playerChanceManager = GetComponent<PlayerChanceManager>();
        timerManager.SetTimer(timeRequiredForTask);
        playerChanceManager.LoadRemainingChance();
    }

    void Update()
    {
        mainMenuController.RefreshScene(timerManager.GetTimer(), "Ann'sTask", timeRequiredForTask);
    }

    public void RevealFileClue()
    {
        clueManager.SetClue("Help Ann to unlock the confidential file.\n" +
            "Every Key has its own value. Private keys are RED and Public keys are BLUE.\n" +
            "Both keys and files are encoded with a secret value.");
    }

    public void SubmitValue(string sceneName)
    {
        if (secretValue.text == null)
        {
            Feedback("Value cannot be null!");
        }
        int enteredValue;
        bool isInteger = int.TryParse(secretValue.text, out enteredValue);

        if (!isInteger)
        {
            Feedback("Please enter a number!");
            return;
        }
        if (SECRET_VALUE != enteredValue)
        {

            Feedback("Try again!");
            audioManager.PlayWrongAnswerAudio();
        }
        else
        {
            Proceed(sceneName);
        }
    }


    //proceed with click
    public void Proceed(string sceneName)
    {
        audioManager.PlayWinningAudio();
        clueManager.ShowWinOrLooseClue("Congratulations! You Rock!");
        mainMenuController.LoadNextChapter(sceneName);
    }

    private void Feedback(string feedback)
    {
        motivationText.color = Color.red;
        motivationText.text = feedback;
    }

}


