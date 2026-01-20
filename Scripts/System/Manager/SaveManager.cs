using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public SaveData CurrentData { get; private set; }

    Transform playerPos;
    string savePath;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        savePath = Path.Combine(Application.persistentDataPath, "fr_data.json");

        CurrentData = Load() ?? new SaveData();
    }

    void Start()
    {
        Save();
    }

    void Update()
    {
        if (playerPos == null) playerPos = GameObject.Find("Player").transform;
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(CurrentData, true);
        File.WriteAllText(savePath, json);
    }

    public void SaveEvery()
    {
        Debug.Log("Game Saved.");
        CurrentData.PlayerPosX = playerPos.position.x;
        CurrentData.PlayerPosY = playerPos.position.y;
        CurrentData.PlayerRoom = SceneManager.GetActiveScene().name;

        CurrentData.MasterVolume = AudioManager.Instance.MasterVol;
        CurrentData.BGMVolume = AudioManager.Instance.BGMVol;
        CurrentData.SFXVolume = AudioManager.Instance.SFXVol;

        Save();
    }

    public SaveData Load()
    {
        if (!File.Exists(savePath))
            return null;

        string json = File.ReadAllText(savePath);
        return JsonUtility.FromJson<SaveData>(json);
    }

    public void ShowSaved()
    {
        string json = File.ReadAllText(savePath);
        Debug.Log(json);
    }

    public void ResetSave()
    {
        Debug.Log("Data Reset.");
        CurrentData = new SaveData();
        Save();
    }
}
