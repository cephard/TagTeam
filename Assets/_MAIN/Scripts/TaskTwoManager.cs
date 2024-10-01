using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages Task Two, inheriting functionality from TaskOneManager.
/// Tracks task events via analytics and manages task-related UI and logic.
/// </summary>
public class TaskTwoManager : TaskOneManager
{
    private const int TIME_REQUIRED_FOR_TASK = 120;
    private const string TASK_TWO_NAME = "TaskTwo";

    private AnalyticsManager analyticsManager;

    /// <summary>
    /// Initializes the TaskTwoManager and tracks the TaskTwo event using analytics.
    /// Called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        analyticsManager = GetComponent<AnalyticsManager>();
        analyticsManager.TrackEvent(TASK_TWO_NAME);
    }

    /// <summary>
    /// Updates the task status by checking for incorrect task assignments
    /// and refreshing the scene based on the timer and task name.
    /// </summary>
    void Update()
    {
        CheckWrongTask(TASK_TWO_NAME);
        mainMenuController.RefreshScene(timerManager.GetTimer(), TASK_TWO_NAME, TIME_REQUIRED_FOR_TASK);
    }
}
