using UnityEngine;

public class FindingsPickup : MonoBehaviour
{
    public string FindingsName;
    public int FindingsIndex;

    void Start()
    {
        SaveData data = SaveManager.Instance.Load();
        if (data != null)
            gameObject.SetActive(!data.Findings[FindingsIndex]);
    }
}
