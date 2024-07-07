using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfidentialFileManager : MonoBehaviour
{
    [SerializeField] private Image privateKey;
    [SerializeField] private Image publicKey;
    [SerializeField] private Image secretMessage;
    [SerializeField] private Text feedback;
    [SerializeField] private Text timerMessage;
    private Dictionary<Image, bool> secretCode;
    private Coroutine timerCoroutine;
    private const int TIMER_DELAY = 1;
    private int timer = TIMER_DELAY;
    private MainMenuController mainMenuController;


    public void Start()
    {
        mainMenuController = GetComponent<MainMenuController>();
        timerMessage.text = timer.ToString();
        secretCode = new Dictionary<Image, bool>
        {
            { privateKey, false },
            { publicKey, false },
            { secretMessage, false }
        };
    }

    public void Update()
    {
        timerMessage.text = timer.ToString();

    }

    private void PrivateKeyExposed()
    {
        if (secretCode[secretMessage] && secretCode[publicKey] && !secretCode[privateKey])
        {
            feedback.color = Color.red;
            UpdateFeedBack("Private has been exposed ! ");
        }
    }

    private void PublicKeyExposed()
    {
        if (secretCode[secretMessage] && secretCode[privateKey] && !secretCode[publicKey])
        {
            feedback.color = Color.red;
            UpdateFeedBack("Public key has been exposed ! ");
        }
    }

    private void messageSentSuccessFully()
    {
        if (secretCode[secretMessage] && secretCode[publicKey] && secretCode[privateKey])
        {
            feedback.color = Color.white;
            UpdateFeedBack("Secret Message sent successfully! ");
        }
    }

    private void UpdateFeedBack(string feedbackText)
    {
        feedback.text = feedbackText;
    }

    public void ChangeState(Image image)
    {
        if (secretCode.ContainsKey(image))
        {
            secretCode[image] = !secretCode[image];

        }
        CheckFileIntegrity();
    }

    //when a private key and public key stay for 5 seconds player fails 
    public IEnumerator TimerCountDown()
    {
        if (secretCode[privateKey] == secretCode[publicKey])
        {
            ResetTimer();
        }
        else
        {
            while (timer > 0)
            {
                timer--;
                yield return new WaitForSeconds(1f);
            }
            CheckTimer();
        }
    }

    // Start the timer coroutine
    public void StartTimer()
    {
        ResetTimer();
        timerCoroutine = StartCoroutine(TimerCountDown());
    }


    private void CheckTimer()
    {
        if (timer <= 0)
        {
            feedback.color = Color.yellow;
            UpdateFeedBack("Too Late Confidential File Has Been Corrupted");
            mainMenuController.LoadNextScene("Ann'sTask");
        }
    }

    private void ResetTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        timer = TIMER_DELAY;
    }

    public void CheckFileIntegrity()
    {
        ResetTimer();
        messageSentSuccessFully();
        PublicKeyExposed();
        PrivateKeyExposed();
    }
}
