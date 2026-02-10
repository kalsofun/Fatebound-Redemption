using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FindingsDetailUI : MonoBehaviour
{
    public FindingsData[] findingsData;

    Image icon;
    TextMeshProUGUI nameText;
    TextMeshProUGUI descText;
    Button useButton;

    PlayerFindings pf;

    void Start()
    {
        icon = transform.Find("Findings Image").GetComponent<Image>();
        nameText = transform.Find("FindingsName Text (TMP)").GetComponent<TextMeshProUGUI>();
        descText = transform.Find("Description Text (TMP)").GetComponent<TextMeshProUGUI>();
        useButton = transform.Find("Use Button").GetComponent<Button>();

        pf = GameObject.Find("Player").GetComponent<PlayerFindings>();

        gameObject.SetActive(false);
    }

    public void Show(int index)
    {
        gameObject.SetActive(true);

        icon.sprite = findingsData[index].icon;
        nameText.text = findingsData[index].name;
        descText.text = findingsData[index].description;

        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(() =>
        {
            pf.UseItem(findingsData[index].index);
        });
    }
}
