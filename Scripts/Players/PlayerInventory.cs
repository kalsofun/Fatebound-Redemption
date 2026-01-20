using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    PlayerMovement pm;

    public List<ItemSlotUI> slots = new List<ItemSlotUI>();

    [SerializeField] List<ItemData> items = new List<ItemData>();
    [SerializeField] float useItemRange = 1.2f;

    bool isOpen = false;
    string currentInteractID = "";

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
        pm = GetComponent<PlayerMovement>();
        slots.Add(null);
    }

    void Update()
    {
        if (PauseManager.Instance.CanPauseScene())
        {
            if (!PauseManager.Instance.isPaused && Keyboard.current.eKey.wasPressedThisFrame)
            {
                if (!CanvasManager.Instance.DialogueBoxActive() && !CanvasManager.Instance.FindingsUIActive())
                {
                    isOpen = !isOpen;
                    CanvasManager.Instance.InventoryUI.SetActive(isOpen);
                    if (!isOpen) CanvasManager.Instance.ItemDetailUI.Hide();
                    pm.canMove = !isOpen;
                    Time.timeScale = isOpen ? 0f : 1f;
                }
            }

            if (slots[0] == null)
            {
                if (GameObject.Find("SlotItem_0") == null) return;
                slots.Clear();
                for (int i = 0; i < 12; i++)
                    slots.Add(GameObject.Find("SlotItem_" + i).GetComponent<ItemSlotUI>());
                if (items.Count > 0) RefreshUI();
            }
        }
    }

    public bool AddItem(ItemData item)
    {
        if (items.Count >= 12) return false;

        items.Add(item);
        RefreshUI();
        return true;
    }

    bool CanUseItem(ItemData item)
    {
        currentInteractID = "";
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, useItemRange);
        foreach (var hit in hits)
        {
            InteractBlocks blocks = hit.GetComponent<InteractBlocks>();
            if (blocks != null)
            {
                currentInteractID = blocks.interactID;
                blocks.Interacted();
            }
            SwitchRoom room = hit.GetComponent<SwitchRoom>();
            if (room != null)
            {
                currentInteractID = room.interactID;
            }
        }
        if (currentInteractID == "") return false;
        return currentInteractID == item.requiredInteractID;
    }

    public void UseItem(int index)
    {
        if (index < 0 || index >= items.Count) return;

        ItemData item = items[index];

        if (!CanUseItem(item))
        {
            Debug.Log("Item can't used: " + item.name);
            CloseUI();
            CanvasManager.Instance.ItemCantUseMessage.SetActive(true);
            return;
        }

        Debug.Log("Item used: " + item.name);
        if (item.useItem != null) item.useItem.SetActive(true);

        items.RemoveAt(index); //後面自動補位
        RefreshUI();
        CloseUI();
    }

    void CloseUI()
    {
        isOpen = false;
        CanvasManager.Instance.InventoryUI.SetActive(false);
        CanvasManager.Instance.ItemDetailUI.Hide();
        pm.canMove = true;
        Time.timeScale = 1f;
    }

    public void RefreshUI()
    {
        if (slots.Count == 0 || slots[0] == null) return;

        for (int i = 0; i < slots.Count; i++)
        {
            if (i < items.Count)
                slots[i].Set(items[i], i, this);
            else
                slots[i].Clear();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, useItemRange);
    }
}
