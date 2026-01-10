using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    [SerializeField] private GameObject LockedDoor;
    [SerializeField] private GameObject UnlockedMessage;

    void Start()
    {
        LockedDoor.GetComponent<SwitchRoom>().DoorLocked = false;
        UnlockedMessage.SetActive(true);
        AudioManager.Instance.PlaySFX(1);
        gameObject.SetActive(false);
    }
}
