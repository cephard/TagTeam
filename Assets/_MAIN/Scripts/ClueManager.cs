using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the display and interaction with clues in the game, including handling the game state (pause/resume) based on clue visibility.
/// </summary>
public class ClueManager : MonoBehaviour
{
    private const int PAUSED_TIME = 0;
    private const int CONTINUOUS_TIME = 1;

    [SerializeField] private Text clueText;
    [SerializeField] private GameObject clueDisplay;

    private AudioManager audioManager;
    private bool isPaused = true;

    /// <summary>
    /// Initializes the ClueManager and retrieves the AudioManager component.
    /// </summary>
    private void Start()
    {
        audioManager = GetComponent<AudioManager>();
    }

    /// <summary>
    /// Monitors for input to toggle the clue display and pauses the game if a clue is being shown.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchClueBasedOnGameState();
        }

        if (clueDisplay.activeSelf)
        {
            PauseGame();
        }
    }

    /// <summary>
    /// Returns the current clue display GameObject.
    /// </summary>
    /// <returns>The GameObject used for displaying clues.</returns>
    public GameObject GetClue()
    {
        return clueDisplay;
    }

    /// <summary>
    /// Switches the clue display based on the current game state (paused or not).
    /// </summary>
    public void SwitchClueBasedOnGameState()
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

    /// <summary>
    /// Sets the clue text and displays the clue.
    /// </summary>
    /// <param name="clue">The text of the clue to display.</param>
    public void SetClue(string clue)
    {
        clueText.text = clue;
        ShowClue();
    }

    /// <summary>
    /// Activates the clue display and pauses the game.
    /// </summary>
    public void ShowClue()
    {
        clueDisplay.SetActive(true);
        PauseGame();
    }

    /// <summary>
    /// Deactivates the clue display and resumes the game.
    /// </summary>
    public void HideClue()
    {
        clueDisplay.SetActive(false);
        ResumeGame();
    }

    /// <summary>
    /// Overrides the clue display based on the active state of another GameObject.
    /// </summary>
    /// <param name="activeObject">The GameObject whose active state determines whether to hide the clue.</param>
    public void OverRideClueOnStart(GameObject activeObject)
    {
        if (!activeObject.activeSelf)
        {
            HideClue();
        }
    }

    /// <summary>
    /// Displays a clue with a specific message and resumes the game afterward.
    /// </summary>
    /// <param name="clueName">The text to display as a clue.</param>
    public void ShowWinOrLoseClue(string clueName)
    {
        ShowClue();
        clueText.text = clueName;
        ResumeGame();
    }

    /// <summary>
    /// Pauses the game and audio, setting the game to a paused state.
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = PAUSED_TIME;
        isPaused = true;
        if (audioManager != null)
        {
            audioManager.PauseAudio();
        }
    }

    /// <summary>
    /// Resumes the game and audio, returning the game to its normal state.
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = CONTINUOUS_TIME;
        isPaused = false;

        if (audioManager != null)
        {
            audioManager.ResumeAudio();
        }
    }
}
