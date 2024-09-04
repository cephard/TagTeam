using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

/// <summary>
/// Manages the email-related task, including checking responses and triggering scene changes.
/// </summary>
public class EmailTaskManager : MonoBehaviour
{
    private const int TIME_REQUIRED_FOR_TASK = 180;
    private const string TASK_NAME = "Ann'sTask";

    [SerializeField] private GameObject computer;

    private MainMenuController mainMenuController;
    private ClueManager clueManager;
    private TimerManager timerManager;
    private string responseName;

    // Dictionary to hold predefined feedback messages based on user choice.
    private Dictionary<string, string> responseFeedback;

    /// <summary>
    /// Initializes components and sets up task requirements.
    /// </summary>
    void Start()
    { 
        mainMenuController = GetComponent<MainMenuController>();
        clueManager = GetComponent<ClueManager>();
        timerManager = GetComponent<TimerManager>();
        timerManager.SetTimer(TIME_REQUIRED_FOR_TASK);
        computer.SetActive(false);

        responseFeedback = new Dictionary<string, string>()
        {
            { "client email", "You cannot resend this email back to the sender!" },
            { "response one", "Retail Corp is considering to cancel all future contracts due to lack of clear communication and professionalism!" },
            { "response two", "Cathy does not trust the team with the project and has decided to escalate to the CEO!" },
            { "response three", "You have expressed sincere apology, and reassured Cathy with a specific and timely plan of action. This approach demonstrates strong problem-solving skills and a commitment to customer satisfaction." }
        };
    }

    /// <summary>
    ///  refreshes the scene when ten timer runs out
    /// </summary>
    private void Update()
    {
        mainMenuController.RefreshScene(timerManager.GetTimer(), TASK_NAME, TIME_REQUIRED_FOR_TASK);
    }

    /// <summary>
    /// Opens the email task by enabling the computer UI in unity.
    /// </summary>
    public void OpenEmailTask()
    {
        computer.SetActive(true);
    }

    /// <summary>
    /// Sets the name of the response selected by the user.
    /// </summary>
    /// <param name="newResponseName">The name of the user's response.</param>
    public void SetResponseName(string newResponseName)
    {
        responseName = newResponseName;
    }

    /// <summary>
    /// Checks if the user's selected response is valid, shows the corresponding feedback,
    /// and transitions to the next scene if correct.
    /// </summary>
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
