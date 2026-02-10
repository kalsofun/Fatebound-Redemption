using UnityEngine;
using UnityEngine.UI;

public class FindingsDataLoad : MonoBehaviour
{
    public GameObject[] findingsObject;
    [SerializeField] FindingsDetailUI FDUI;

    void Start()
    {
        SaveData data = SaveManager.Instance.Load();
        if (data != null)
            for (int i = 0; i < 11; i++)
                findingsObject[i].SetActive(data.Findings[i]);

        gameObject.SetActive(false);
    }
}
