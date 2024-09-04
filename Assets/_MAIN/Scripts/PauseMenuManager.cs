using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the functionality of the pause menu, including audio control and interaction with the CoinManager and AudioManager.
/// </summary>
public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject audioGameObject;

    private CoinManager coinManager;
    private AudioManager audioManager;

    /// <summary>
    /// Initializes the PauseMenuManager by resetting chapter gems, and playing background audio.
    /// </summary>
    private void Start()
    {
        audioManager = GetComponent<AudioManager>();
        coinManager = GetComponent<CoinManager>();
        coinManager.ResetChapterGem();
        audioManager.PlayBackgroundAudio();
    }

    /// <summary>
    /// Updates the state of the audio-related icon based on the current audio status.
    /// </summary>
    private void Update()
    {
        if (audioManager.GetAudioStatus())
        {
            audioGameObject.SetActive(false);
        }
        else
        {
            audioGameObject.SetActive(true);
        }
    }
}
