using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeSceneManager : UnityEngine.MonoBehaviour
{
    [SerializeField] private Text welcome;
    [SerializeField] private GameObject audioPlayer;
    private AudioManager audioManager;

    void Awake()
    {
        audioManager = GetComponent<AudioManager>();
        audioManager.PlayBackgroundAudio();
        welcome.text = "Welcome " + PlayerPrefs.GetString("Username");
        DontDestroyOnLoad(audioPlayer);
    }
}

