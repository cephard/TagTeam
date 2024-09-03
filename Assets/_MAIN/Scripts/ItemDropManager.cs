using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDropManager : UnityEngine.MonoBehaviour, IDropHandler
{
    [SerializeField] private Text slotText;
    [SerializeField] private Text motivationText;
    private const int COMPLETE_TASK = 7;
    private const int PASS_TASK_COUNT = 4;
    private const int BONUS_COIN = 10;
    private const int MAXIMUM_WRONG_TASKS = 3;
    private const int NO_TASK = 0;
    private static int correctTask = NO_TASK;
    private MainMenuController mainMenuController;
    private CoinManager coinManager;
    private AudioManager audioManager;
    private ClueManager clueManager;
    private static int wrongTask;
    private Stack<DroppedTaskManager> actionStack = new Stack<DroppedTaskManager>();


    public void Start()
    {
        correctTask = NO_TASK;
        wrongTask = NO_TASK;
        audioManager = FindAnyObjectByType<AudioManager>();
        mainMenuController = GetComponent<MainMenuController>();
        coinManager = GetComponent<CoinManager>();
        clueManager = GetComponent<ClueManager>();
    }

    protected bool CheckWrongTask(string sceneName)
    {
        if (wrongTask == MAXIMUM_WRONG_TASKS)
        {
            mainMenuController = new MainMenuController();
            mainMenuController.LoadNextScene(sceneName);
        }
        return false;
    }

    private string GetCorrectTaskCount(int taskCount)
    {
        return taskCount.ToString();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
        CompareTask(eventData);
    }

    private void CompareTask(PointerEventData eventData)
    {
        Text draggedText = eventData.pointerDrag.GetComponentInChildren<Text>();
        if (slotText.text != null && draggedText != null)
        {
            ChangeTaskState(draggedText);
            eventData.pointerDrag.SetActive(false);
        }
    }

    private void ChangeTaskState(Text draggedText)
    {
        if (draggedText.text == slotText.text)
        {
            audioManager.PlayGainCoinAudio();
            SetCompleteBackground(Color.blue);
            motivationText.text = "Correct task !";
            correctTask++;
        }
        else
        {
            audioManager.PlayWrongAnswerAudio();
            SetCompleteBackground(Color.red);
            motivationText.text = "Wrong task !";
            wrongTask++;
        }
    }

    //Helper method for player to differentiate between completed and incomplete task
    private void SetCompleteBackground(Color color)
    {
        Image imageComponent = GetComponent<Image>();
        imageComponent.color = color;
        motivationText.color = color;
    }

    //Allows player to load next scene only sfter all tasks are complete
    public void Proceed(string sceneName)
    {
        if (correctTask >= PASS_TASK_COUNT)
        { 
            CheckTaskCount(correctTask);
            audioManager.PlayWinningAudio();
            coinManager.AddCoins(BONUS_COIN * correctTask);
            mainMenuController.LoadNextChapter(sceneName);
        }
        else
        {
            clueManager.ShowWinOrLooseClue("Please Try Again!");
        }
        Debug.Log(correctTask.ToString());
    }

    private void CheckTaskCount(int checkCorrectTask)
    {
        if (checkCorrectTask == COMPLETE_TASK)
        {
            clueManager.ShowWinOrLooseClue("Perfect Eye for Detail !");
        }
        else
        {
            clueManager.ShowWinOrLooseClue("Nice Try !");
        }
    }
}
