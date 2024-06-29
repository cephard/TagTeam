using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private ReadDialogue readDialogue;
    private static string currentScene;

    void Start()
    {
        readDialogue = GetComponent<ReadDialogue>();
    }

    //mehod to navigate between scenes
    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void UpdateSceneName(string sceneName)
    {
        currentScene = sceneName;
    }

    public string GetSceneName()
    {
        return currentScene;
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

