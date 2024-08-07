using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTwoManager : TaskOneManager
{
    private string taskTwo = "TaskTwo";
    void Update()
    {
        CheckWrongTask(taskTwo);
        mainMenuController.RefreshScene(timerManager.GetTimer(), taskTwo, timeRequiredForTask);
  
    }

}
