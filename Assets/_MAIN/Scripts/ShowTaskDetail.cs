using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowTaskDetail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject taskdetail;
    [SerializeField]
    private Text detailText;

    void Start()
    {
        if (taskdetail != null)
        {
            taskdetail.SetActive(false);
        }
    }

    public void SetDetailText(string specificDetail)
    {
        detailText.text = specificDetail;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        detailText.text = "Cick to See more details";
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
