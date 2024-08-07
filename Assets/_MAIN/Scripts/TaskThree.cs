using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TaskThree : UnityEngine.MonoBehaviour
{
    private bool privateKey;
    private bool publicKey;
    private bool confidentialFile;
    MainMenuController mainMenuController;
    [SerializeField] private Text message;

    public void CheckConfidentiality()
    {
        mainMenuController = GetComponent<MainMenuController>();
        if (publicKey == confidentialFile && privateKey != confidentialFile)
        {
            message.text = "The File has Been Hacked!";
        }

        if (privateKey == confidentialFile && publicKey != confidentialFile)
        {
            message.text = "The file private key has been exposed"; 
        }

        if (privateKey == confidentialFile && publicKey == confidentialFile) {
            mainMenuController.LoadNextScene("Conversation");
        }
    }

    public void UpdateSecurityState(bool securityState)
    {
        CheckConfidentiality();
    }

}
