using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDropManager : MonoBehaviour, IDropHandler
{
    public Text slotText;
    public static int correctDropCounter = 0;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null){
            eventData.pointerDrag.GetComponent<RectTransform> ().anchoredPosition = GetComponent<RectTransform> ().anchoredPosition;
        }
        Text draggedText = eventData.pointerDrag.GetComponentInChildren<Text>();
        if (draggedText.text == slotText.text)
        {
            correctDropCounter++;
            Debug.Log("Correct drop! Counter: " + correctDropCounter);
        }
    }
}  
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDropManager : MonoBehaviour, IDropHandler
{
    public Text slotText; // Assign this in the Inspector
    public static int correctDropCounter = 0;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Text draggedText = eventData.pointerDrag.GetComponentInChildren<Text>();
            if (draggedText != null && slotText != null)
            {
                if (draggedText.text == slotText.text)
                {
                    correctDropCounter++;
                    Debug.Log("Correct drop! Counter: " + correctDropCounter);
                }
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            }
        }
    }
}
*/
