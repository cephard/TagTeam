using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    CoinManager coinManager;
    AudioManager audioManager;
    private void Start()
    {
        audioManager = GetComponent<AudioManager>();
        coinManager = GetComponent<CoinManager>();
        coinManager.ResetChapterGem();
        settingsPanel.SetActive(false);
        audioManager.PlayBackgroundAudio();
    }
}

