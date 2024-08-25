using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTwoManager : TaskOneManager
{
    private string taskTwo = "TaskTwo";
    private AnalyticsManager analyticsManager;
    private void Awake()
    {
        analyticsManager = GetComponent<AnalyticsManager>();
        analyticsManager.TrackEvent("Task Two");
    }
   
    void Update()
    {
        CheckWrongTask(taskTwo);
        mainMenuController.RefreshScene(timerManager.GetTimer(), taskTwo, timeRequiredForTask);
  
    }

}
