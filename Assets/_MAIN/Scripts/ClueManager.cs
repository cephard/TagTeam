using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueManager : UnityEngine.MonoBehaviour
{
    private const int PAUSED_TIME = 0;
    private const int CONTINOUS_TIME = 1;
    [SerializeField] private Text clueText;
    [SerializeField] private GameObject clueDisplay;
    private AudioManager audioManager;
    private bool isPaused = true;

    private void Start()
    {
        audioManager = GetComponent<AudioManager>();
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

        if (clueDisplay.activeSelf == true)
        {
            PauseGame();
        }
    }

    public GameObject GetClue()
    {
        return clueDisplay;
    }

    public void SetClue(string clue)
    {
        clueText.text = clue;
        ShowClue();
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

    public void OverRideClueOnStart(GameObject activeObject)
    {
        if (activeObject.activeSelf == false)
        {
            HideClue();
        }
    }

    public void ShowWinOrLooseClue(string clueName)
    {
        ShowClue();
        clueText.text = clueName;
        ResumeGame();
    }

    public void PauseGame()
    {
        Time.timeScale = PAUSED_TIME;
        isPaused = true;
        audioManager.PauseAudio();
    }

    public void ResumeGame()
    {
        Time.timeScale = CONTINOUS_TIME;
        isPaused = false;

        if (audioManager != null)
        {
            audioManager.ResumeAudio();
        }
    }
}
