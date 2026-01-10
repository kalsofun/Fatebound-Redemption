using UnityEngine;

public class ConstMenuManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(MenuManager.Instance.StartMenu());
    }

    public void LoadScene(string sceneName)
    {
        MenuManager.Instance.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        MenuManager.Instance.QuitGame();
    }
}
