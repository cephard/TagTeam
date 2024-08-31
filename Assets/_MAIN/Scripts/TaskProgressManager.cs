using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskProgressManager : UnityEngine.MonoBehaviour
{
    private const int NO_PROGRESS = 0;
    private const int PAUSE_MENU_TRIGGER = 0;
    private const int TASK_ONE_TRIGGER = 24;
    private const int ANNS_TASK_TRIGGER = 46;
    private const int PRINTER_SERIAL_TRIGGER = 80;
    private const int TASK_TWO_TRIGGER = 111;
    private const int UNLOCK_LAPTOP_TRIGGER = 130;

    private Dictionary<string, int> taskProgress;
    private void Awake()
    {
        taskProgress = new Dictionary<string, int>
        {
            { "pause", PAUSE_MENU_TRIGGER },
            { "TaskOne", TASK_ONE_TRIGGER },
            { "Ann'sTask", ANNS_TASK_TRIGGER },
            { "PrinterSerial", PRINTER_SERIAL_TRIGGER },
            { "TaskTwo",TASK_TWO_TRIGGER },
            { "UnlockLaptop", UNLOCK_LAPTOP_TRIGGER }
        };
    }


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

    public void SetTaskProgress(string dialogue, int currentLine)
    {
        taskProgress[dialogue] = currentLine;
    }
}
