using UnityEngine;

public class ConstMenuManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(MenuManager.Instance.StartMenu());
    }

    public void StartGame()
    {
        MenuManager.Instance.LoadScene("DreamScene");
    }

    public void ContinueGame()
    {
        SaveData data = SaveManager.Instance.Load();
        if (data != null)
        {
            MenuManager.Instance.PlayerPos = new Vector2(data.PlayerPosX, data.PlayerPosY);
            MenuManager.Instance.LoadScene(data.PlayerRoom);
        }
        else MenuManager.Instance.LoadScene("");
    }

    public void QuitGame()
    {
        MenuManager.Instance.QuitGame();
    }
}
