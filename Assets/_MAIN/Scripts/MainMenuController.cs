using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages scene navigation, player chances, and game flow in the main menu. Handles transitioning between scenes, updating the current scene name, and handling game exits.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    private ReadDialogue readDialogue;
    private PlayerChanceManager playerChanceManager;

    private static string currentScene;

    private const float CHAPTER_LOAD_DELAY = 2.5f;
    private const int NO_MORE_CHANCE = 0;
    private const int ONE_SECOND = 1;

    /// <summary>
    /// Initializes necessary components at the start of the game.
    /// </summary>
    void Start()
    {
        readDialogue = GetComponent<ReadDialogue>();
        playerChanceManager = GetComponent<PlayerChanceManager>();
    }

    /// <summary>
    /// Loads the next scene immediately.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Starts a coroutine to load the next scene with a delay, allowing for transitions.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    public void LoadNextChapter(string sceneName)
    {
        StartCoroutine(DelayedLoadScene(sceneName, CHAPTER_LOAD_DELAY));
    }

    /// <summary>
    /// Coroutine to load the next scene after a delay.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    /// <param name="delay">The amount of delay in seconds before loading the scene.</param>
    private IEnumerator DelayedLoadScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadNextScene(sceneName);
    }

    /// <summary>
    /// Updates the stored name of the current scene to facilitate transitions between levels.
    /// </summary>
    /// <param name="sceneName">The name of the current scene.</param>
    public void UpdateSceneName(string sceneName)
    {
        currentScene = sceneName;
    }

    /// <summary>
    /// Retrieves the name of the current scene.
    /// </summary>
    /// <returns>The name of the current scene.</returns>
    public string GetSceneName()
    {
        return currentScene;
    }

    /// <summary>
    /// Handles the failure of a level when the player has no more chances left. Resets the player's chances and loads the conversation scene.
    /// </summary>
    /// <param name="scene">The current scene name.</param>
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

    /// <summary>
    /// Refreshes the scene if the timer runs out and reduces the player's remaining chances. Resets the timer if below one second and reloads the current scene.
    /// </summary>
    /// <param name="timer">The current value of the timer.</param>
    /// <param name="scene">The current scene name.</param>
    /// <param name="taskTime">The task time to reset the timer to if it runs out.</param>
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

    /// <summary>
    /// Exits the game. If in the Unity Editor, stops the play mode. Otherwise, closes the application.
    /// </summary>
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
