using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : UnityEngine.MonoBehaviour
{
    private ReadDialogue readDialogue;
    private PlayerChanceManager playerChanceManager;
    private static string currentScene;
    private const float CHAPTER_LOAD_DELAY= 2.5f;
    private const int NO_MORE_CHANCE = 0;
    private const int ONE_SECOND = 1;

    void Start()
    {
        readDialogue = GetComponent<ReadDialogue>();
        playerChanceManager = GetComponent<PlayerChanceManager>();
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
        if (playerChanceManager.GetRemainingChance() <= NO_MORE_CHANCE) 
        {
            playerChanceManager.ResetChance();
            playerChanceManager.SaveRemainingChance();
            LoadNextScene("Conversation");
            UpdateSceneName(scene);
        }
    }
       

    //When timer hits below one it resets then will ens when counter is 3
    public void RefreshScene(int timer, string scene, int taskTime)
    {
        FailedLevel(scene);
        if (timer < ONE_SECOND)
        {
            playerChanceManager.ReduceRemainingChance();
            timer = taskTime;
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

