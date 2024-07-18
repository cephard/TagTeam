using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskProgressManager : MonoBehaviour
{
    private Dictionary<string, int> taskProgress;
    private void Awake()
    {
        taskProgress = new Dictionary<string, int>
        {
            { "pause", 0 },
            { "TaskOne", 24 },
            { "Ann'sTask", 46 },
            { "PrinterSerial", 80 },
            { "TaskTwo", 111 },
            { "UnlockLaptop", 130 }
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
            return 0;

        }
    }

    public void SetTaskProgress(string dialogue, int currentLine)
    {
        taskProgress[dialogue] = currentLine;
    }
}
