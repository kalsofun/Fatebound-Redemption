using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailUI : MonoBehaviour
{
    Image icon;
    TextMeshProUGUI nameText;
    TextMeshProUGUI descText;
    Button useButton;
    
    PlayerInventory inventory;

    int currentIndex;

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
        nameText.text = item.name;
        descText.text = item.description;

        currentIndex = index;
        inventory = PlayerInventory.Instance;

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
