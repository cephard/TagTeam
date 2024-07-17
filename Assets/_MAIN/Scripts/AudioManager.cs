using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip timerAudioClip;
    [SerializeField] public AudioClip backgroundAudioClip;
    [SerializeField] public AudioClip gainCoinAudioClip;
    [SerializeField] public AudioClip looseCoinAudioClip;
    [SerializeField] public AudioClip winAudioClip;

    public void PlayTimerAudio()
    {
        TriggerClip(timerAudioClip);
    }

    public void PlayGainCoinAudio()
    {
        TriggerClip(gainCoinAudioClip);
    }

    public void PlayLooseCoinAudio()
    {
        TriggerClip(looseCoinAudioClip);
    }
    public void PlayWiningAudio()
    {
        TriggerClip(winAudioClip);
    }
    public void PlayBackgroundAudio()
    {
        TriggerClip(backgroundAudioClip);
    }

    private void TriggerClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}

