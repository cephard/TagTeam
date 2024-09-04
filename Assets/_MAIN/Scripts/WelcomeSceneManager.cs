using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the welcome scene by playing background audio and displaying a personalized welcome message.
/// </summary>
public class WelcomeSceneManager : MonoBehaviour
{

    [SerializeField] private Text welcome;
    [SerializeField] private GameObject audioPlayer;

    private AudioManager audioManager;

    /// <summary>
    /// Sets up the audio and welcome message.
    /// </summary>
    void Awake()
    {
        audioManager = GetComponent<AudioManager>();
        audioManager.PlayBackgroundAudio();

        welcome.text = "Welcome " + PlayerPrefs.GetString("Username");
    }
}
