using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    [Header("Permanent")]
    public GameObject InventoryUI;
    public ItemDetailUI ItemDetailUI;
    public GameObject FindingsUI;
    public GameObject FindingsDetailUI;

    [Header("Dynamic")]
    public GameObject DialogueBox;
    public GameObject DoorLockedMessage;
    public GameObject PickupItemMessage;
    public GameObject InvFullMessage;
    public GameObject ItemCantUseMessage;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        ItemDetailUI.Hide();
        InventoryUI.SetActive(false);
    }

    void Update()
    {
        if (PauseManager.Instance.InMenuScene()) return;

        if (DialogueBox == null)
        {
            DialogueBox = GameObject.Find("Dialogue Box");
            DialogueBox.SetActive(false);
        }

        if (DoorLockedMessage == null)
        {
            DoorLockedMessage = GameObject.Find("DoorLockedMessage");
            DoorLockedMessage.SetActive(false);
        }
        if (PickupItemMessage == null)
        {
            PickupItemMessage = GameObject.Find("PickupItemMessage");
            PickupItemMessage.SetActive(false);
        }
        if (InvFullMessage == null)
        {
            InvFullMessage = GameObject.Find("InvFullMessage");
            InvFullMessage.SetActive(false);
        }
        if (ItemCantUseMessage == null)
        {
            ItemCantUseMessage = GameObject.Find("ItemCantUseMessage");
            ItemCantUseMessage.SetActive(false);
        }
    }

    public bool DialogueBoxActive()
    {
        return DialogueBox.activeSelf;
    }

    public bool InventoryUIActive()
    {
        return InventoryUI.activeSelf;
    }

    public bool FindingsUIActive()
    {
        return FindingsUI.activeSelf;
    }

    public bool AnyUIActive()
    {
        return DialogueBox.activeSelf || InventoryUI.activeSelf || FindingsUI.activeSelf;
    }
}
