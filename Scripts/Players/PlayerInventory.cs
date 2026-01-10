using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    private PlayerMovement pm;
    public List<ItemSlotUI> slots = new List<ItemSlotUI>();

    private List<ItemData> items = new List<ItemData>();
    private bool isOpen = false;
    [SerializeField] private float useItemRange = 1.2f;
    private string currentInteractID = "";

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        for (int i = 0; i < 12; i++)
            slots.Add(GameObject.Find("SlotItem_" + i).GetComponent<ItemSlotUI>());
    }

    void Update()
    {
        if (PauseManager.Instance.InMenuScene() == false)
            if (!PauseManager.Instance.isPaused && Keyboard.current.shiftKey.wasPressedThisFrame)
            {
                if (!DialogueManager.Instance.DialogueBoxActive())
                {
                    isOpen = !isOpen;
                    DialogueManager.Instance.InventoryUI.SetActive(isOpen);
                    if (!isOpen) DialogueManager.Instance.ItemDetailUI.Hide();
                    pm.canMove = !isOpen;
                    Time.timeScale = isOpen ? 0f : 1f;
                }
            }
    }

    public bool AddItem(ItemData item)
    {
        if (items.Count >= slots.Count) return false;

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
            Debug.Log("Item can't used: " + item.itemName);
            CloseUI();
            DialogueManager.Instance.ItemCantUseMessage.SetActive(true);
            return;
        }

        Debug.Log("Item used: " + item.itemName);
        if (item.useItem != null) item.useItem.SetActive(true);

        items.RemoveAt(index); //後面自動補位
        RefreshUI();
        CloseUI();
    }

    void CloseUI()
    {
        isOpen = false;
        DialogueManager.Instance.InventoryUI.SetActive(false);
        DialogueManager.Instance.ItemDetailUI.Hide();
        pm.canMove = true;
        Time.timeScale = 1f;
    }

    public void RefreshUI()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < items.Count)
            {
                slots[i].Set(items[i], i, this);
            }
            else
            {
                slots[i].Clear();
            }
        }
    }
}
