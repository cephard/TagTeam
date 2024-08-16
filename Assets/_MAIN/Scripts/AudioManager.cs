using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : UnityEngine.MonoBehaviour
{

    private bool isMute = false;
    [SerializeField] private AudioSource chimeAudioSource;
    [SerializeField] private AudioSource timerAudioSource;

    [SerializeField] private AudioClip timerAudioClip;
    [SerializeField] private AudioClip backgroundAudioClip;
    [SerializeField] private AudioClip gainCoinAudioClip;
    [SerializeField] private AudioClip looseCoinAudioClip;
    [SerializeField] private AudioClip winAudioClip;
    [SerializeField] private AudioClip wrongAnswerAudioClip;
    [SerializeField] private AudioClip gainGemAudioClip;

    public void PlayTimerAudio()
    {
        TriggerClip(timerAudioClip, timerAudioSource);
    }

    public void PlayGainCoinAudio()
    {
        TriggerClip(gainCoinAudioClip, chimeAudioSource);
    }

    public void PlayGainGemAudio()
    {
        TriggerClip(gainGemAudioClip, chimeAudioSource);
    }

    public void PlayLooseCoinAudio()
    {
        TriggerClip(looseCoinAudioClip, chimeAudioSource);
    }
    public void PlayWiningAudio()
    {
        TriggerClip(winAudioClip, chimeAudioSource);
    }
    public void PlayBackgroundAudio()
    {
        TriggerClip(backgroundAudioClip, chimeAudioSource);
    }

    public void PlayWrongAnswerAudio()
    {
        TriggerClip(wrongAnswerAudioClip, chimeAudioSource);
    }

    private void TriggerClip(AudioClip clip, AudioSource audioSource)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PauseAudio()
    {
        AudioListener.pause = true;
    }
    public void ResumeAudio()
    {
        AudioListener.pause = false;
    }
    public void MuteAudio()
    {
        AudioListener.volume = 0;
    }

    public void UnmuteAudio()
    {
        AudioListener.volume = 1;
    }

    public bool GetAudioStatues()
    {
        return isMute;
    }
    public void MuteEntireGame()
    {
        if (isMute)
        {
            UnmuteAudio();
            isMute = false;
        }
        else
        {
            MuteAudio();
            isMute = true;
        }
    }

}

