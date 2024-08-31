using UnityEngine;
using UnityEngine.UI;

public class TaskOneManager : ItemDropManager
{
    [SerializeField] private Image SlotOne;
    [SerializeField] private Image SlotTwo;
    [SerializeField] private Image SlotThree;
    [SerializeField] private Image SlotFour;
    [SerializeField] private Image SlotFive;
    [SerializeField] private Image SlotSix;
    [SerializeField] private Image SlotSeven;
    [SerializeField] private Image TaskOne;
    [SerializeField] private Image TaskTwo;
    [SerializeField] private Image TaskThree;
    [SerializeField] private Image TaskFour;
    [SerializeField] private Image TaskFive;
    [SerializeField] private Image TaskSix;
    [SerializeField] private Image TaskSeven;
    protected MainMenuController mainMenuController;
    private AnalyticsManager analyticsManager;
    protected TimerManager timerManager;
    protected PlayerChanceManager playerChanceManager;
    private const int TIME_REQUIRED_FOR_TASK = 60;
    private const string TASK_ONE_NAME = "TaskOne";

   // protected int timeRequiredForTask = 60;
   // private string taskOne = "TaskOne";


    new void Start()
    {
        mainMenuController = GetComponent<MainMenuController>();
        timerManager = GetComponent<TimerManager>();
        playerChanceManager = GetComponent<PlayerChanceManager>();
        analyticsManager = GetComponent<AnalyticsManager>();    
        timerManager.SetTimer(TIME_REQUIRED_FOR_TASK);
        playerChanceManager.LoadRemainingChance();
        CompareTexts();
        analyticsManager.TrackEvent(TASK_ONE_NAME);
    }

    void Update()
    {
        CheckWrongTask(TASK_ONE_NAME);
        mainMenuController.RefreshScene(timerManager.GetTimer(), TASK_ONE_NAME, TIME_REQUIRED_FOR_TASK);
    }
    void CompareTexts()
    {
        // Access the Text components of Slot images
        Text slotTextOne = GetTextComponent(SlotOne);
        Text slotTextTwo = GetTextComponent(SlotTwo);
        Text slotTextThree = GetTextComponent(SlotThree);
        Text slotTextFour = GetTextComponent(SlotFour);
        Text slotTextFive = GetTextComponent(SlotFive);
        Text slotTextSix = GetTextComponent(SlotSix);
        Text slotTextSeven = GetTextComponent(SlotSeven);

        // Access the Text components of Task images
        Text taskTextOne = GetTextComponent(TaskOne);
        Text taskTextTwo = GetTextComponent(TaskTwo);
        Text taskTextThree = GetTextComponent(TaskThree);
        Text taskTextFour = GetTextComponent(TaskFour);
        Text taskTextFive = GetTextComponent(TaskFive);
        Text taskTextSix = GetTextComponent(TaskSix);
        Text taskTextSeven = GetTextComponent(TaskSeven);
    }

    private Text GetTextComponent(Image image)
    {
        return image.GetComponentInChildren<Text>();
    }
}