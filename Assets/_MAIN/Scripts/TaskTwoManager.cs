using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TaskTwoManager : TaskOneManager
{
    private const int TIME_REQUIRED_FOR_TASK = 60;
    private const string TASK_TWO_NAME = "TaskTwo";
   
    private AnalyticsManager analyticsManager;
    private void Awake()
    {
        analyticsManager = GetComponent<AnalyticsManager>();
        analyticsManager.TrackEvent(TASK_TWO_NAME);
    }
   
    void Update()
    {
        CheckWrongTask(TASK_TWO_NAME);
        mainMenuController.RefreshScene(timerManager.GetTimer(), TASK_TWO_NAME, TIME_REQUIRED_FOR_TASK);
  
    }

}
