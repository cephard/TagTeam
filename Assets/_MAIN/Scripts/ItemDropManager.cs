using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDropManager : MonoBehaviour, IDropHandler
{
    [SerializeField] private Text slotText;
    [SerializeField] private Text motivationText;
    private static int correctTask = 0;
    MainMenuController mainMenuController;

    static CoinManager coinManager;


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
        CompareTask(eventData);
    }

    private void CompareTask(PointerEventData eventData)
    {
        Text draggedText = eventData.pointerDrag.GetComponentInChildren<Text>();
        if (draggedText.text == slotText.text)
        {
            SetCompleteBackground();
            motivationText.text = "Correct task "!;
            eventData.pointerDrag.SetActive(false);
            correctTask++;
        }
        else
        {
            motivationText.text = "Try Again!";

        }
    }

    //Helper method for player to differentiate between completed and incomplete task
    private void SetCompleteBackground()
    {
        Image imageComponent = GetComponent<Image>();
        imageComponent.color = Color.blue;
    }

    //Allows player to load next scene only sfter all tasks are complete
    public void Proceed(string sceneName)
    {
        if (correctTask >= 7)
        {
            mainMenuController = GetComponent<MainMenuController>();
            mainMenuController.LoadNextScene(sceneName);
            coinManager = GetComponent<CoinManager>();
            coinManager.AddCoins();
            
        }
    }
}
