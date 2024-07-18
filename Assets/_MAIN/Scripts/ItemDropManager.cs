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
    private AudioManager audioManager;
    private ClueManager clueManager;
    private const int COMPLETE_TASK = 7;
    private const int BONUS_COIN  = 50;
    private int wrongTask = 0;

    public void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        mainMenuController = GetComponent<MainMenuController>();
        coinManager = GetComponent<CoinManager>();
        clueManager = GetComponent<ClueManager>();
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
    }

    private void CompareTask(PointerEventData eventData)
    {
        Text draggedText = eventData.pointerDrag.GetComponentInChildren<Text>();
        if (slotText.text != null && draggedText != null)
        {
            ChangeTaskState(draggedText);
            eventData.pointerDrag.SetActive(false);
        }
    }

    private void ChangeTaskState(Text draggedText)
    {
        if (draggedText.text == slotText.text)
        {
            audioManager.PlayGainCoinAudio();
            SetCompleteBackground(Color.blue);
            motivationText.text = "Correct task !";
            correctTask++;
        }
        else
        {
            audioManager.PlayWrongAnswerAudio();
            SetCompleteBackground(Color.red);
            motivationText.text = "Wrong task !";
            wrongTask++;
        }
    }

    //Helper method for player to differentiate between completed and incomplete task
    private void SetCompleteBackground(Color color)
    {
        Image imageComponent = GetComponent<Image>();
        imageComponent.color = color;
        motivationText.color = color;
    }

    //Allows player to load next scene only sfter all tasks are complete
    public void Proceed(string sceneName)
    {
        if (correctTask >= COMPLETE_TASK)
        {
            clueManager.ShowWinOrLooseClue("Perfect Eye for Detail !");
            audioManager.PlayWiningAudio();
            coinManager.AddCoins(BONUS_COIN);
            mainMenuController.LoadNextChapter(sceneName);
        }
        else
        {
            clueManager.ShowWinOrLooseClue("Please Try Again!");
        }

        correctTask = 0;
    }
}
