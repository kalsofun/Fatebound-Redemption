using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    private EnemyChase[] enemyChases;
    [SerializeField] public GameObject DialogueBox;
    [SerializeField] public GameObject InventoryUI;
    [SerializeField] public ItemDetailUI ItemDetailUI;
    [SerializeField] public GameObject DoorLockedMessage;
    [SerializeField] public GameObject PickupItemMessage;
    [SerializeField] public GameObject InvFullMessage;
    [SerializeField] public GameObject ItemCantUseMessage;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (ItemDetailUI != null) ItemDetailUI.Hide();
        if (InventoryUI != null) InventoryUI.SetActive(true);
        if (GameObject.FindFirstObjectByType<PlayerInventory>() != null) GameObject.FindFirstObjectByType<PlayerInventory>().RefreshUI();
        if (InventoryUI != null) InventoryUI.SetActive(false);
        enemyChases = FindObjectsByType<EnemyChase>(FindObjectsSortMode.None);
    }

    void Update()
    {
        if (enemyChases != null)
            foreach (var enemy in enemyChases)
            {
                enemy.CanChase = !DialogueBoxActive();
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
}
