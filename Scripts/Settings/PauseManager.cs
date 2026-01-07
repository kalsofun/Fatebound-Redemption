using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenuUI;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
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
        Debug.Log("Return to Main Menu.");
        SceneManager.LoadScene("MainMenu");
    }
}
