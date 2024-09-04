using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages and displays the player's decisions in a report format. Tracks the number of decisions made and updates the report accordingly.
/// </summary>
public class PlayerReport : MonoBehaviour
{
    [SerializeField] private Text report;
    private static string reportText = "";
    private static int choiceNumber;
    private AudioManager audioManager;

    /// <summary>
    /// Initializes the PlayerReport by playing background audio and updating the report text on the UI.
    /// </summary>
    public void Start()
    {
        audioManager = GetComponent<AudioManager>();
        audioManager.PlayBackgroundAudio();
        report.text = reportText;
    }

    /// <summary>
    /// Updates the player's report by adding their latest decision.
    /// </summary>
    /// <param name="playerChoice">The player's decision to add to the report.</param>
    public static void UpdateDecisions(string playerChoice)
    {
        choiceNumber++;
        reportText += choiceNumber.ToString() + " : " + playerChoice + "\n";
    }
}
