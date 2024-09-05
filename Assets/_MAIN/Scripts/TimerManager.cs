using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the countdown timer, including visual warnings and audio triggers
/// when the timer reaches critical thresholds.
/// </summary>
public class TimerManager : MonoBehaviour
{
    private const int TIMER_WARNING_THRESHOLD = 15;
    private const float TIMER_REDUCTION_INTERVAL = 1f;
    private static int TIME_OUT = 0;

    [SerializeField] private Text timerText;
    private AudioManager audioManager;
    private bool isTriggered;
    private int timer;

    /// <summary>
    /// Initializes the TimerManager and sets the initial state.
    /// </summary>
    private void Start()
    {
        isTriggered = false;
        audioManager = GetComponent<AudioManager>();
    }

    /// <summary>
    /// Updates the UI with the current timer value and checks for warning triggers.
    /// </summary>
    private void Update()
    {
        timerText.text = GetTimer().ToString();
        TimerEnding();
    }

    /// <summary>
    /// Sets the timer to the given value and starts the countdown coroutine.
    /// </summary>
    /// <param name="timerLength">The length of the timer in seconds.</param>
    public void SetTimer(int timerLength)
    {
        timer = timerLength;
        StartCoroutine(ReduceTimeCoroutine());
    }

    /// <summary>
    /// Returns the current timer value.
    /// </summary>
    /// <returns>The current timer value.</returns>
    public int GetTimer()
    {
        return timer;
    }

    /// <summary>
    /// Reduces the timer value by 1 second if the timer has not yet timed out.
    /// </summary>
    public void ReduceTime()
    {
        if (timer > TIME_OUT)
        {
            timer--;
        }
    }

    /// <summary>
    /// Checks if the timer has reached the warning threshold and triggers the warning.
    /// </summary>
    public void TimerEnding()
    {
        if (!isTriggered)
        {
            TriggerTimer();
        }
    }

    /// <summary>
    /// Changes the UI to indicate the warning state and plays a warning sound when
    /// the timer reaches the warning threshold.
    /// </summary>
    private void TriggerTimer()
    {
        if (timer == TIMER_WARNING_THRESHOLD)
        {
            timerText.color = Color.red;
            audioManager.PlayTimerAudio();
            isTriggered = true;
        }
    }

    /// <summary>
    /// Coroutine that reduces the timer at regular intervals until it reaches zero.
    /// </summary>
    /// <returns>IEnumerator for coroutine handling.</returns>
    private IEnumerator ReduceTimeCoroutine()
    {
        while (timer > TIME_OUT)
        {
            yield return new WaitForSeconds(TIMER_REDUCTION_INTERVAL);
            ReduceTime();
        }
    }
}
