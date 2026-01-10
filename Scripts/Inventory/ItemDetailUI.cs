using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailUI : MonoBehaviour
{
    private Image icon;
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI descText;
    private Button useButton;

    private int currentIndex;
    private PlayerInventory inventory;

    void Awake()
    {
        icon = transform.Find("Item Image").GetComponent<Image>();
        nameText = transform.Find("ItemName Text (TMP)").GetComponent<TextMeshProUGUI>();
        descText = transform.Find("Description Text (TMP)").GetComponent<TextMeshProUGUI>();
        useButton = transform.Find("Use Button").GetComponent<Button>();
    }

    public void Show(ItemData item, int index)
    {
        gameObject.SetActive(true);

        icon.sprite = item.icon;
        nameText.text = item.itemName;
        descText.text = item.description;

        currentIndex = index;
        inventory = FindFirstObjectByType<PlayerInventory>();

        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(() =>
        {
            inventory.UseItem(currentIndex);
        });
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
