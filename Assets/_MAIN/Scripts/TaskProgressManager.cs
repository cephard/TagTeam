using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages task progress by tracking various triggers across different tasks.
/// Stores progress for different scenes and tasks in a dictionary.
/// </summary>
public class TaskProgressManager : MonoBehaviour
{
    private const int LINE_INCREMENT = 1;

    // Constants representing the starting points or triggers for various tasks.
    private const int NO_PROGRESS = 0;
    private const int PAUSE_MENU_TRIGGER = 0;
    private const int TASK_ONE_TRIGGER = 24;
    private const int ANNS_TASK_TRIGGER = 46;
    private const int PRINTER_SERIAL_TRIGGER = 80;
    private const int TASK_TWO_TRIGGER = 111;
    private const int UNLOCK_LAPTOP_TRIGGER = 130;

    private ConversationUIManager conversationUIManager;
    private MainMenuController mainMenuController;
    private CurrentLineManager currentLineManager;

    // Dictionary to store task progress for each task or scene by name.
    private Dictionary<string, int> taskProgress;


    private void InitializeCustomObjects()
    {
        conversationUIManager = GetComponent<ConversationUIManager>();
        mainMenuController = GetComponent<MainMenuController>();
        currentLineManager = GetComponent<CurrentLineManager>();
    }
    /// <summary>
    /// Initializes the taskProgress dictionary with predefined values
    /// for different tasks and their associated triggers.
    /// </summary>
    private void Awake()
    {
        InitializeCustomObjects();
        taskProgress = new Dictionary<string, int>
        {
            { "pause", PAUSE_MENU_TRIGGER },
            { "TaskOne", TASK_ONE_TRIGGER },
            { "Ann'sTask", ANNS_TASK_TRIGGER },
            { "PrinterSerial", PRINTER_SERIAL_TRIGGER },
            { "TaskTwo", TASK_TWO_TRIGGER },
            { "UnlockLaptop", UNLOCK_LAPTOP_TRIGGER }
        };
    }

    /// <summary>
    /// Retrieves the progress value of the specified task or scene.
    /// </summary>
    /// <param name="sceneName">The name of the task or scene.</param>
    /// <returns>
    /// The progress value associated with the task or scene, 
    /// or 0 if the task is not found.
    /// </returns>
    public int GetTaskProgress(string sceneName)
    {
        if (taskProgress.ContainsKey(sceneName))
        {
            return taskProgress[sceneName];
        }
        else
        {
            return NO_PROGRESS;
        }
    }

    /// <summary>
    /// Updates the progress for a specific task or scene.
    /// If the task already exists, its progress is updated to the current value.
    /// </summary>
    /// <param name="dialogue">The name of the task or dialogue.</param>
    /// <param name="currentLine">The current line or point of progress in the task.</param>
    public void SetTaskProgress(string dialogue, int currentLine)
    {
        taskProgress[dialogue] = currentLine;
    }

    public void LoadTaskScene(string[] sentenceParts, int currentLine)
    {
        if (String.Equals("Task", conversationUIManager.GetAvatarName()))
        {
            conversationUIManager.SetDialogueText(sentenceParts[LINE_INCREMENT].Trim());
            currentLineManager.SaveNextLine();
            SetTaskProgress(conversationUIManager.GetDialogueText(), currentLine);
            Debug.Log(currentLine);
            mainMenuController.LoadNextScene(conversationUIManager.GetDialogueText());
        }
    }
}
