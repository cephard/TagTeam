using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowTaskDetail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject taskdetail;
    [SerializeField]
    private Text detailText;
    private string taskName;
    private string taskDuration;
    private string taskDeadline;
    private string taskPriority;

    void Start()
    {
        if (taskdetail != null)
        {
            taskdetail.SetActive(false);
        }
    }

    public void SetTaskName(string specificName)
    {
        taskName = specificName;
    }
    public void SetTaskDuration(string specificDuration)
    {
        taskDuration = specificDuration;
    }
    public void SettaskDeadline(string specificDeadline)
    {
        taskDeadline = specificDeadline;
    }

    public void SetTaskPriority(string specificPriority)
    {
        taskPriority = specificPriority;
    }

    public void SetDetailText()
    {
        detailText.text = taskName + "\n"
            + taskDuration + "\n"
            + taskDeadline + "\n"
            + taskPriority;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        detailText.text = "Cick on the task to See more details !";
        taskdetail.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (taskdetail != null)
        {
            taskdetail.SetActive(false);
        }
    }
}
