using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    public bool isPaused = false;
    private GameObject pauseMenuUI;

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
        if (pauseMenuUI == null && InMenuScene() == false)
        {
            pauseMenuUI = GameObject.Find("Pause Canvas");
            if (pauseMenuUI != null)
                pauseMenuUI.SetActive(false);
        }
        
        if (InMenuScene() == false && Keyboard.current.escapeKey.wasPressedThisFrame)
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
    }
    
    public bool InMenuScene()
    {
        return SceneManager.GetActiveScene().name == "MainMenu";
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
