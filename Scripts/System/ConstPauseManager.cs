using UnityEngine;

public class ConstPauseManager : MonoBehaviour
{
    public void ResumeGame()
    {
        PauseManager.Instance.ResumeGame();
    }

    public void MainMenu()
    {
        PauseManager.Instance.MainMenu();
    }
}
