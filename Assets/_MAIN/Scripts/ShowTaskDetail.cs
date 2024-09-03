using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowTaskDetail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // private const string HOVER_TEXT = "Click on the task to see more details!";
    [SerializeField] private Text originalText;
    [SerializeField] private GameObject taskdetail;
    [SerializeField] private Text detailText;

    private string taskName;
    private string taskDuration;
    private string taskDeadline;
    private string taskPriority;

    void Start()
    {
        if (taskdetail != null)
        {
            taskdetail.SetActive(true);
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

    public void SetDetail(Text taskDetail)
    {
        detailText.text = taskDetail.text;
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
        //detailText.text = originalText.text;
        taskdetail.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (taskdetail != null)
        {
            //taskdetail.SetActive(false);
            //detailText.text = HOVER_TEXT;
        }
    }
}
