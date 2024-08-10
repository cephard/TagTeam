using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MainMenuController
{
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        settingsPanel.SetActive(false);
    }
}

