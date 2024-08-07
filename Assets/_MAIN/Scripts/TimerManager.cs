using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : UnityEngine.MonoBehaviour
{
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
        if (timer > 0)
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
        if (timer == 15)
        {
            timerText.color = Color.red;
            audioManager.PlayTimerAudio();
            isTriggered = true;

        }
    }

    private IEnumerator ReduceTimeCoroutine()
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            ReduceTime();
        }
    }
}
