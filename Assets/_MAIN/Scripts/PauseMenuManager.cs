using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    CoinManager coinManager;
    private void Start()
    {
        coinManager = GetComponent<CoinManager>();
        coinManager.ResetChapterGem();
        settingsPanel.SetActive(false);
    }
}

