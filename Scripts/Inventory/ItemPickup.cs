using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData;
    public bool isPickedUp = false;

    void Awake() => itemData.ID = gameObject.name;
}
