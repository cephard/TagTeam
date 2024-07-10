using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ConfidentialFileManager : MonoBehaviour
{
    private const int SECRET_VALUE = 14;
    [SerializeField] private Text puzzleClue;
    [SerializeField] private Text motivationText;
    [SerializeField] private GameObject cluePanel;
    [SerializeField] private InputField secretValue;
    private MainMenuController mainMenuController;

    private void Start()
    {
        mainMenuController = GetComponent<MainMenuController>();
        HideClue();
    }

    public void HideClue()
    {
        cluePanel.SetActive(false);
    }
    public void RevealFileClue()
    {
        cluePanel.SetActive(true);
        puzzleClue.text = "Help Ann to unlock the confidential file.\n" +
            "Every Key has its own value. Private keys are RED and Public keys are BLUE.\n" +
            "Both keys and files are encoded with a secret value.";
    }

    public void SubmitValue()
    {
        if (secretValue.text == null) {
            motivationText.text = "Value cannot be null!";
        }
        int enteredValue;
        bool isInteger = int.TryParse(secretValue.text, out enteredValue);

        if (!isInteger) {
            motivationText.text = "Please enter a number!";
            return;
        }
        if(SECRET_VALUE != enteredValue)
        {
            motivationText.text = "Try again!";
        }
        else
        {
            mainMenuController.LoadNextScene("Conversation");
            mainMenuController.UpdateSceneName("Ann'sTask");
        }
    }
}


