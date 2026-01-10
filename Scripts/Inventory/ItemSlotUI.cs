using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public Image icon;
    private Button button;
    private PlayerInventory inventory;
    private int index;

    void Awake()
    {
        icon = GetComponent<Image>();
        button = GetComponent<Button>();
        Clear();
    }

    public void Set(ItemData item, int index, PlayerInventory inv)
    {
        this.index = index;
        inventory = inv;

        icon.sprite = item.icon;
        Color c = icon.color;
        c.a = 1f;
        icon.color = c;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            DialogueManager.Instance.ItemDetailUI.Show(item, index);
        });
    }

    public void Clear()
    {
        icon.sprite = null;
        Color c = icon.color;
        c.a = 0f;
        icon.color = c;
        button.onClick.RemoveAllListeners();
    }
}
