using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class ItemDropManager : MonoBehaviour, IDropHandler
{
    [SerializeField] private Text slotText;
    [SerializeField] private Text motivationText;
    private static int correctTask = 0;


    private string GetCorrectTaskCount(int taskCount)
    {
        return taskCount.ToString();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
        Text draggedText = eventData.pointerDrag.GetComponentInChildren<Text>();
        if (draggedText.text == slotText.text)
        {
            correctTask++;
            motivationText.text = "Correct tasks "! + GetCorrectTaskCount(correctTask);
        }
        else
        {
            if(correctTask < 0)
            {
                correctTask = 0;
            }
            correctTask--;
            motivationText.text = "Correct tasks "! + GetCorrectTaskCount(correctTask);

        }
    }
}