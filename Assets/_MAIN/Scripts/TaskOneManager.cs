using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the functionality related to Task One, including UI slot-task matching,
/// timers, and analytics tracking. Inherits from the ItemDropManager class.
/// </summary>
public class TaskOneManager : ItemDropManager
{
    // UI slots for task placement
    [SerializeField] private Image SlotOne;
    [SerializeField] private Image SlotTwo;
    [SerializeField] private Image SlotThree;
    [SerializeField] private Image SlotFour;
    [SerializeField] private Image SlotFive;
    [SerializeField] private Image SlotSix;
    [SerializeField] private Image SlotSeven;

    // UI images representing tasks
    [SerializeField] private Image TaskOne;
    [SerializeField] private Image TaskTwo;
    [SerializeField] private Image TaskThree;
    [SerializeField] private Image TaskFour;
    [SerializeField] private Image TaskFive;
    [SerializeField] private Image TaskSix;
    [SerializeField] private Image TaskSeven;

    // External managers and constants
    protected MainMenuController mainMenuController;
    private AnalyticsManager analyticsManager;
    protected TimerManager timerManager;
    protected PlayerChanceManager playerChanceManager;

    private const int TIME_REQUIRED_FOR_TASK = 90;
    private const string TASK_ONE_NAME = "TaskOne";

    /// <summary>
    /// Initializes the necessary managers and sets up the timer and player chances.
    /// Tracks the task event through analytics.
    /// </summary>
    new void Start()
    {
        // Get references to other necessary components
        mainMenuController = GetComponent<MainMenuController>();
        timerManager = GetComponent<TimerManager>();
        playerChanceManager = GetComponent<PlayerChanceManager>();
        analyticsManager = GetComponent<AnalyticsManager>();

        // Initialize task-related data
        timerManager.SetTimer(TIME_REQUIRED_FOR_TASK);
        playerChanceManager.LoadRemainingChance();

        // Compare UI text between slots and tasks
        CompareTexts();

        // Track the event with analytics
        analyticsManager.TrackEvent(TASK_ONE_NAME);
    }

    /// <summary>
    /// Updates the scene, checks for incorrect task assignments, and refreshes the scene UI.
    /// </summary>
    void Update()
    {
        // Check if an incorrect task has been assigned to this task's slot
        CheckWrongTask(TASK_ONE_NAME);

        // Refresh the scene's UI based on the current timer
        mainMenuController.RefreshScene(timerManager.GetTimer(), TASK_ONE_NAME, TIME_REQUIRED_FOR_TASK);
    }

    /// <summary>
    /// Compares the text of task slots with their corresponding tasks.
    /// </summary>
    void CompareTexts()
    {
        // Get the Text components of slot UI elements
        Text slotTextOne = GetTextComponent(SlotOne);
        Text slotTextTwo = GetTextComponent(SlotTwo);
        Text slotTextThree = GetTextComponent(SlotThree);
        Text slotTextFour = GetTextComponent(SlotFour);
        Text slotTextFive = GetTextComponent(SlotFive);
        Text slotTextSix = GetTextComponent(SlotSix);
        

        // Get the Text components of task UI elements
        Text taskTextOne = GetTextComponent(TaskOne);
        Text taskTextTwo = GetTextComponent(TaskTwo);
        Text taskTextThree = GetTextComponent(TaskThree);
        Text taskTextFour = GetTextComponent(TaskFour);
        Text taskTextFive = GetTextComponent(TaskFive);
        Text taskTextSix = GetTextComponent(TaskSix);
        Text taskTextSeven = GetTextComponent(TaskSeven);
    }

    /// <summary>
    /// Retrieves the Text component from an Image's child object.
    /// </summary>
    /// <param name="image">The Image component containing the Text component.</param>
    /// <returns>The Text component inside the Image.</returns>
    private Text GetTextComponent(Image image)
    {
        return image.GetComponentInChildren<Text>();
    }
}
