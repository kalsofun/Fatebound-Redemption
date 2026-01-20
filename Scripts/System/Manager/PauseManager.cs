using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    public bool isPaused = false;
    GameObject pauseMenuUI;
    string currentScene;

    [SerializeField] List<string> NoPauseScene = new List<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Update()
    {
        if (pauseMenuUI == null)
        {
            pauseMenuUI = GameObject.Find("Pause Panel");
            if (pauseMenuUI != null)
                pauseMenuUI.SetActive(false);
        }

        if (CanPauseScene())
        {            
            if (Keyboard.current.escapeKey.wasPressedThisFrame && !CanvasManager.Instance.AnyUIActive())
                if (!isPaused) PauseGame();
                else ResumeGame();
        }
    }
    
    public bool InMenuScene()
    {
        return SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "SplashScreen";
    }

    public bool CanPauseScene()
    {
        return !NoPauseScene.Contains(SceneManager.GetActiveScene().name);
    }

    public bool CheckSceneSwitch()
    {
        string activeScene = SceneManager.GetActiveScene().name;

        if (string.IsNullOrEmpty(currentScene))
        {
            currentScene = activeScene;
            return false;
        }

        if (currentScene != activeScene)
        {
            currentScene = activeScene;
            return true;
        }
        return false;
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenuUI.SetActive(true);
        Debug.Log("Game Paused.");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Debug.Log("Game Resumed.");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        Debug.Log("Return to Main Menu.");
        SceneManager.LoadScene("MainMenu");
    }
}
