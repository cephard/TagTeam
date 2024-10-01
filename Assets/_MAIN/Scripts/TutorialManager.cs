using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    [SerializeField] private GameObject audioGameObject;
    private AudioManager audioManager;
    private void Start()
    {
        audioManager = GetComponent<AudioManager>();
        audioManager.PlayBackgroundAudio();
    }

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


