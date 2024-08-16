using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject audioGameObject;
    CoinManager coinManager;
    AudioManager audioManager;
    private void Start()
    {
        audioManager = GetComponent<AudioManager>();
        coinManager = GetComponent<CoinManager>();
        coinManager.ResetChapterGem();
        audioManager.PlayBackgroundAudio();
    }

    private void Update()
    {
        if (audioManager.GetAudioStatues())
        {
            audioGameObject.SetActive(false);
        }
        else
        {
            audioGameObject.SetActive(true);

        }
    }
}

