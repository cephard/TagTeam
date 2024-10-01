using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Manages the drag-and-drop functionality for tasks, comparing dropped tasks to the target slots and providing feedback based on the result.
/// </summary>
public class ItemDropManager : MonoBehaviour, IDropHandler
{
    [SerializeField] private Text slotText;
    [SerializeField] private Text motivationText;
    [SerializeField]  private int completeTaskCount;
    
    private const int PASS_TASK_COUNT = 3;
    private const int BONUS_COIN = 10;
    private const int MAXIMUM_WRONG_TASKS = 3;
    private const int NO_TASK = 0;

    private static int correctTask = NO_TASK;
    private static int wrongTask = NO_TASK;

    private MainMenuController mainMenuController;
    private PlayerChanceManager playerChanceManager;
    private CoinManager coinManager;
    private AudioManager audioManager;
    private ClueManager clueManager;

    private Stack<DroppedTaskManager> actionStack = new Stack<DroppedTaskManager>();

    private void InitializeGenericManagers()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        mainMenuController = GetComponent<MainMenuController>();
        playerChanceManager = GetComponent<PlayerChanceManager>();
        coinManager = GetComponent<CoinManager>();
        clueManager = GetComponent<ClueManager>();
    }
    /// <summary>
    /// Initializes the ItemDropManager, setting initial values and getting required components.
    /// </summary>
    public void Start()
    {
        correctTask = NO_TASK;
        wrongTask = NO_TASK;
        InitializeGenericManagers();
    }

    /// <summary>
    /// Checks if the maximum number of wrong tasks has been reached and loads the next scene if so.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    /// <returns>Returns false to indicate no further action is required from this check.</returns>
    protected bool CheckWrongTask(string sceneName)
    {
        if (wrongTask == MAXIMUM_WRONG_TASKS)
        {
            mainMenuController = new MainMenuController();
            mainMenuController.LoadNextScene(sceneName);
        }
        return false;
    }

    /// <summary>
    /// Handles the drop event when an ftask is dropped onto a slot. Compares the dropped task to the expected task.
    /// </summary>
    /// <param name="eventData">PointerEventData related to the drag-and-drop action.</param>
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
        CompareTask(eventData);
    }

    /// <summary>
    /// Compares the dropped task with the expected task in the slot and updates the task state accordingly.
    /// </summary>
    /// <param name="eventData">PointerEventData containing the details of the dropped item.</param>
    private void CompareTask(PointerEventData eventData)
    {
        Text draggedText = eventData.pointerDrag.GetComponentInChildren<Text>();
        if (slotText.text != null && draggedText != null)
        {
            ChangeTaskState(draggedText);
            eventData.pointerDrag.SetActive(false);
        }
    }

    /// <summary>
    /// Updates the game state when a correct task is completed. Plays a sound and updates the UI accordingly.
    /// </summary>
    private void DetectCorrectTask()
    {
        audioManager.PlayGainCoinAudio();
        SetCompleteBackground(Color.blue);
        motivationText.text = "Correct task!";
        correctTask++;
    }

    /// <summary>
    /// Updates the game state when a wrong task is detected. Plays a sound and updates the UI accordingly.
    /// </summary>
    private void DetectWrongTask()
    {
        audioManager.PlayWrongAnswerAudio();
        SetCompleteBackground(Color.red);
        motivationText.text = "Wrong task!";
        wrongTask++;
    }

    /// <summary>
    /// Changes the task state based on whether the dropped item matches the expected task.
    /// </summary>
    /// <param name="draggedText">The text from the dropped item.</param>
    private void ChangeTaskState(Text draggedText)
    {
        if (draggedText.text == slotText.text)
        {
            DetectCorrectTask();
        }
        else
        {
            DetectWrongTask();
        }
    }

    /// <summary>
    /// Changes the background color of the task to indicate whether the task was completed correctly or incorrectly.
    /// </summary>
    /// <param name="color">The color to set as the background, indicating task state.</param>
    private void SetCompleteBackground(Color color)
    {
        Image imageComponent = GetComponent<Image>();
        imageComponent.color = color;
        motivationText.color = color;
    }

    /// <summary>
    /// Proceeds to the next chapter if the player has completed enough correct tasks. Otherwise, shows a clue for retrying.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load next.</param>
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
            clueManager.ShowWinOrLoseClue("You missed the mark this time. The task assignments could have been better distributed, and some deadlines were too tight, which affected the team's performance. Remember, successful leadership involves careful planning, effective delegation, and clear communication. Focus on improving your organizational skills for next time.!");
        }
        Debug.Log(correctTask.ToString());
    }

    /// <summary>
    /// Displays a clue based on the number of correct tasks completed.
    /// </summary>
    /// <param name="checkCorrectTask">The number of correct tasks completed by the player.</param>
    private void CheckTaskCount(int checkCorrectTask)
    {
        if (checkCorrectTask == completeTaskCount)
        {
            clueManager.ShowWinOrLoseClue("Great work! You demonstrated excellent time management and leadership skills by assigning tasks efficiently and ensuring that the team had enough time to complete them. Your ability to balance delegation and follow-up shows a solid understanding of teamwork and communication. Keep it up!");
        }
        else if(checkCorrectTask >= PASS_TASK_COUNT/2)
        {
            clueManager.ShowWinOrLoseClue("Nice effort! You showed a good grasp of delegation and task management. While the timing wasn’t perfect for every task, you still demonstrated strong decision-making skills. With a bit more attention to deadlines, you’ll be able to lead the team more effectively. Keep learning and improving!");
        }
    }
}
