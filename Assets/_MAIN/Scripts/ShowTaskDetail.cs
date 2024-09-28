using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ShowTaskDetail handles the behavior of showing task details 
/// when the user hovers over a task item in the UI.
/// </summary>
public class ShowTaskDetail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private const string HOVER_TEXT = "Click on task to see details!";
    // Fields for displaying text and task details in the UI.

    /// <summary>
    /// The original text of the task item before hovering.
    /// </summary>
    [SerializeField] private Text originalText;

    /// <summary>
    /// The UI element that displays detailed information about the task.
    /// </summary>
    [SerializeField] private GameObject taskdetail;

    /// <summary>
    /// The UI text element that will show the detailed information.
    /// </summary>
    [SerializeField] private Text detailText;

    // Task-specific details: name, duration, deadline, and priority.
    private string taskName;
    private string taskDuration;
    private string taskDeadline;
    private string taskPriority;

    /// <summary>
    /// Initializes the task detail UI element and ensures it is hidden initially.
    /// </summary>
    void Start()
    {
        if (taskdetail != null)
        {
            taskdetail.SetActive(true);
        }
    }

    /// <summary>
    /// Sets the name of the task.
    /// </summary>
    /// <param name="specificName">The name of the task.</param>
    public void SetTaskName(string specificName)
    {
        taskName = specificName;
    }

    /// <summary>
    /// Sets the duration of the task.
    /// </summary>
    /// <param name="specificDuration">The duration of the task.</param>
    public void SetTaskDuration(string specificDuration)
    {
        taskDuration = specificDuration;
    }

    /// <summary>
    /// Sets the deadline of the task.
    /// </summary>
    /// <param name="specificDeadline">The deadline of the task.</param>
    public void SettaskDeadline(string specificDeadline)
    {
        taskDeadline = specificDeadline;
    }

    /// <summary>
    /// Sets the priority of the task.
    /// </summary>
    /// <param name="specificPriority">The priority of the task.</param>
    public void SetTaskPriority(string specificPriority)
    {
        taskPriority = specificPriority;
    }

    /// <summary>
    /// Sets the detailed text from an external source.
    /// </summary>
    /// <param name="taskDetail">The text component containing task details.</param>
    public void SetDetail(Text taskDetail)
    {
        detailText.text = taskDetail.text;
    }

    /// <summary>
    /// Concatenates and sets the detailed text based on task properties.
    /// </summary>
    public void SetDetailText()
    {
        detailText.text = taskName + "\n"
            + taskDuration + "\n"
            + taskDeadline + "\n"
            + taskPriority;
    }

    /// <summary>
    /// Triggered when the mouse pointer enters the task area. 
    /// It makes the task detail panel visible.
    /// </summary>
    /// <param name="eventData">Data related to the pointer event.</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        taskdetail.SetActive(true);
    }

    /// <summary>
    /// Triggered when the mouse pointer exits the task area.
    /// Hides the task detail panel if needed.
    /// </summary>
    /// <param name="eventData">Data related to the pointer event.</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (taskdetail != null)
        {
           taskdetail.SetActive(false);
            detailText.text = HOVER_TEXT;
        }
    }
}
