using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private ReadDialogue readDialogue;
    private static string currentScene;
    private const float CHAPTER_LOAD_DELAY= 2.5f;
    private static int playChance = 3;
    private static int counter = 3;
    private const string CounterKey = "Counter";

    void Start()
    {
        readDialogue = GetComponent<ReadDialogue>();
    }

    //mehod to navigate between scenes
    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Method to start the coroutine for delayed scene loading
    public void LoadNextChapter(string sceneName)
    {
        StartCoroutine(DelayedLoadScene(sceneName, CHAPTER_LOAD_DELAY));
    }

    // Coroutine for delayed scene loading
    private IEnumerator DelayedLoadScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadNextScene(sceneName);
    }

    //Update the name of the previous scene. helps to transition between levels
    public void UpdateSceneName(string sceneName)
    {
        currentScene = sceneName;
    }

    public string GetSceneName()
    {
        return currentScene;
    }

    //player fails when they have used all chances
    public void FailedLevel(string scene)
    {
        if (counter <= 0)
        {
            ResetCounter();
            SaveCounter();
            LoadNextScene("Conversation");
            UpdateSceneName(scene);
        }
    }
        
    private void SaveCounter()
    {
        PlayerPrefs.SetInt(CounterKey, counter);
        PlayerPrefs.Save();
    }

    public void LoadCounter()
    {
        if (PlayerPrefs.HasKey(CounterKey))
        {
            counter = PlayerPrefs.GetInt(CounterKey);
        }
        else
        {
            ResetCounter();
        }
    }

    private void ResetCounter()
    {
        counter = 3;
        SaveCounter();
    }

    //When timer hits below one it resets then will ens when counter is 3
    public void RefreshScene(int timer, string scene, int taskTime)
    {
        FailedLevel(scene);
        if (timer < 1)
        {
            counter--;
            SaveCounter();
            timer = taskTime;
            Debug.Log(counter);
            LoadNextScene(scene);
        }
    }


    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

