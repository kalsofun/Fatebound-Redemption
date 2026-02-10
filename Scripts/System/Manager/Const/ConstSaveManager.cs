using UnityEngine;

public class ConstSaveManager : MonoBehaviour
{
    public void ReadSave()
    {
        SaveManager.Instance.ShowSaved();
    }

    public void ResetSave()
    {
        SaveManager.Instance.ResetSave();
    }
}
