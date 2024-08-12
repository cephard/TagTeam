using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueManager : UnityEngine.MonoBehaviour
{
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
            //PauseGame();
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
