using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    private const int TIMER_WARNING_THRESHOLD = 15;
    private const float TIMER_REDUCTION_INTERVAL = 1f;
    private static int TIME_OUT = 0;

    [SerializeField] private Text timerText;
    private AudioManager audioManager;
    private bool isTriggered;
    private int timer;

    private void Start()
    {
        isTriggered = false;
        audioManager = GetComponent<AudioManager>();
    }

    private void Update()
    {
        timerText.text = GetTimer().ToString();
        TimerEnding();
    }

    public void SetTimer(int timerLength)
    {
        timer = timerLength;
        StartCoroutine(ReduceTimeCoroutine());
    }

    public int GetTimer()
    {
        return timer;
    }

    public void ReduceTime()
    {
        if (timer > TIME_OUT)
        {
            timer--;
        }
    }

    public void TimerEnding()
    {
        if (!isTriggered)
        {
            TriggerTimer();
        }
    }

    private void TriggerTimer()
    {
        if (timer == TIMER_WARNING_THRESHOLD)
        {
            timerText.color = Color.red;
            audioManager.PlayTimerAudio();
            isTriggered = true;
        }
    }

    private IEnumerator ReduceTimeCoroutine()
    {
        while (timer > TIME_OUT)
        {
            yield return new WaitForSeconds(TIMER_REDUCTION_INTERVAL);
            ReduceTime();
        }
    }
}
