using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages audio playback and volume control for the game.
/// </summary>
public class AudioManager : MonoBehaviour
{
    private const int MUTE = 0;
    private const int UNMUTE = 1;
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

    /// <summary>
    /// Plays the timer audio clip using the timer audio source.
    /// </summary>
    public void PlayTimerAudio()
    {
        TriggerClip(timerAudioClip, timerAudioSource);
    }

    /// <summary>
    /// Plays the gain coin audio clip using the chime audio source.
    /// </summary>
    public void PlayGainCoinAudio()
    {
        TriggerClip(gainCoinAudioClip, chimeAudioSource);
    }

    /// <summary>
    /// Plays the gain gem audio clip using the chime audio source.
    /// </summary>
    public void PlayGainGemAudio()
    {
        TriggerClip(gainGemAudioClip, chimeAudioSource);
    }

    /// <summary>
    /// Plays the loose coin audio clip using the chime audio source.
    /// </summary>
    public void PlayLooseCoinAudio()
    {
        TriggerClip(looseCoinAudioClip, chimeAudioSource);
    }

    /// <summary>
    /// Plays the win audio clip using the chime audio source.
    /// </summary>
    public void PlayWinningAudio()
    {
        TriggerClip(winAudioClip, chimeAudioSource);
    }

    /// <summary>
    /// Plays the background audio clip using the chime audio source.
    /// </summary>
    public void PlayBackgroundAudio()
    {
        TriggerClip(backgroundAudioClip, chimeAudioSource);
    }

    /// <summary>
    /// Plays the wrong answer audio clip using the chime audio source.
    /// </summary>
    public void PlayWrongAnswerAudio()
    {
        TriggerClip(wrongAnswerAudioClip, chimeAudioSource);
    }

    /// <summary>
    /// Triggers playback of the specified audio clip using the provided audio source.
    /// </summary>
    /// <param name="clip">The audio clip to play.</param>
    /// <param name="audioSource">The audio source to use for playback.</param>
    private void TriggerClip(AudioClip clip, AudioSource audioSource)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    /// <summary>
    /// Pauses all audio playback in the game.
    /// </summary>
    public void PauseAudio()
    {
        AudioListener.pause = true;
    }

    /// <summary>
    /// Resumes audio playback in the game.
    /// </summary>
    public void ResumeAudio()
    {
        AudioListener.pause = false;
    }

    /// <summary>
    /// Mutes all audio in the game.
    /// </summary>
    public void MuteAudio()
    {
        AudioListener.volume = MUTE;
    }

    /// <summary>
    /// Unmutes all audio in the game.
    /// </summary>
    public void UnmuteAudio()
    {
        AudioListener.volume = UNMUTE;
    }

    /// <summary>
    /// Retrieves the current audio mute status.
    /// </summary>
    /// <returns>True if audio is muted, otherwise false.</returns>
    public bool GetAudioStatus()
    {
        return isMute;
    }

    /// <summary>
    /// Toggles the audio mute status for the entire game.
    /// </summary>
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
