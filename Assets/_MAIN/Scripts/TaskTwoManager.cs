using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTwoManager : AssignmentOneLogic
{
    void Update()
    {
        mainMenuController.RefreshScene(timerManager.GetTimer(), "TaskTwo", timeRequiredForTask);
    }

}
