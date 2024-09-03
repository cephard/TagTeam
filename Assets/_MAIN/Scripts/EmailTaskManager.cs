using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class EmailTaskManager : MonoBehaviour
{
    private const int TIME_REQUIRED_FOR_TASK = 180;
    private const string TASK_NAME = "Ann'sTask";

    [SerializeField] private GameObject computer;

    private MainMenuController mainMenuController;
    private ClueManager clueManager;
    TimerManager timerManager;
    private string responseName;

    private Dictionary<string, string> responseFeedback;

    void Start()
    {
        mainMenuController = GetComponent<MainMenuController>();
        clueManager = GetComponent<ClueManager>();
        timerManager = GetComponent<TimerManager>();
        timerManager.SetTimer(TIME_REQUIRED_FOR_TASK);
        computer.SetActive(false);

        // Initialize feedback dictionary
        responseFeedback = new Dictionary<string, string>()
        {
            { "client email", "You cannot resend this email back to the sender!" },
            { "response one", "Retail Corp is considering to cancel all future contracts due to lack of clear communication and professionalism !" },
            { "response two", "Cathy does not trust the team with the project and has decided to escalate to the CEO!" },
            { "response three", "You have expressed sincere apology, and reassured Cathy with a specific and timely plan of action. This approach demonstrates strong problem-solving skills and a commitment to customer satisfaction." }
        };
    }

    private void Update()
    {
        // mainMenuController.RefreshScene(timerManager.GetTimer(), TASK_NAME, TIME_REQUIRED_FOR_TASK);
    }

    public void OpenEmailTask()
    {
        computer.SetActive(true);
    }

    public void SetResponseName(string newResponseName)
    {
        responseName = newResponseName;
    }

    public async void CheckCorrectResponse()
    {
        if (responseFeedback.ContainsKey(responseName))
        {
            clueManager.ShowWinOrLoseClue(responseFeedback[responseName]);
            computer.SetActive(false);
            await Task.Delay(10000);
            mainMenuController.LoadNextScene("Conversation");
        }
        else
        {
            Debug.LogWarning("Unknown response name: " + responseName);
        }

        Debug.Log(responseName);
    }
}
