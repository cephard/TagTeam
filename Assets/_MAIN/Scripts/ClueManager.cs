using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueManager : MonoBehaviour
{
    [SerializeField] private Text clueText;
    [SerializeField] private GameObject clueDisplay;
    private AudioManager audioManager;
    private bool isPaused = true;

    private void Start()
    {
        audioManager = GetComponent<AudioManager>();
        ShowClue();
        Debug.Log(isPaused);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                HideClue();
            }
            else
            {
                ShowClue();
            }
        }
    }

    public void SetClue(string clue)
    {
        ShowClue();
        clueText.text = clue;
    }
    public void ShowClue()
    {
        clueDisplay.SetActive(true);
        PauseGame();
    }
    public void HideClue()
    {
        clueDisplay.SetActive(false);
        ResumeGame();
    }

    public void ShowWinOrLooseClue(string clueName)
    {
        ShowClue();
        clueText.text = clueName;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        audioManager.PauseAudio();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;

        if (audioManager != null)
        {
            audioManager.ResumeAudio();
        }
    }
}
