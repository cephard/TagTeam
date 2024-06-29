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
    private MainMenuController mainMenuController;
    private CoinManager coinManager;
    private ConfidentialFileManager confidentialFileManager;    

    public void Start()
    {
        mainMenuController = GetComponent<MainMenuController>();
        coinManager = GetComponent<CoinManager>();
        confidentialFileManager = FindObjectOfType<ConfidentialFileManager>();
    }

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

        Image confidentialImage = eventData.pointerDrag.GetComponent<Image>();
        if (confidentialImage == null || confidentialFileManager == null)
        {
            Debug.Log("Item is null");
        }
        confidentialFileManager.ChangeState(confidentialImage);
        confidentialFileManager.StartTimer();
    }

    private void CompareTask(PointerEventData eventData)
    {
        Text draggedText = eventData.pointerDrag.GetComponentInChildren<Text>();
        if (slotText.text != null && draggedText != null)
        {
            
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
            mainMenuController.LoadNextScene(sceneName);
            coinManager.AddCoins();
        }
    }
}
