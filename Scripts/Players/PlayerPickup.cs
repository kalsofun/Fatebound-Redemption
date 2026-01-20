using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickup : MonoBehaviour
{
    LayerMask itemLayer;
    PlayerInventory inventory;
    [SerializeField] float interactRange = 1.2f;

    void Start()
    {
        itemLayer = LayerMask.GetMask("Item");
        inventory = PlayerInventory.Instance;
    }

    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
            if (!CanvasManager.Instance.AnyUIActive())
                TryPickUp();
    }

    void TryPickUp()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRange, itemLayer);
        if (hits.Length == 0) return;

        Collider2D closest = null;
        float minDist = float.MaxValue;

        foreach (var hit in hits)
        {
            ItemPickup pickup = hit.GetComponent<ItemPickup>();
            if (pickup == null || pickup.isPickedUp) continue;

            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = hit;
            }
        }

        if (closest == null) return;

        ItemPickup finalPickup = closest.GetComponent<ItemPickup>();
        if (finalPickup == null) return;

        finalPickup.isPickedUp = true;
        Collider2D col = closest.GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        if (inventory.AddItem(finalPickup.itemData))
        {
            Debug.Log("Picked up Item: " + closest);
            AudioManager.Instance.PlaySFX(2);
            CanvasManager.Instance.PickupItemMessage.GetComponent<DialogueText>().textEffects[0].fullDialogue = "獲得道具" + finalPickup.itemData.name + "。";
            CanvasManager.Instance.PickupItemMessage.SetActive(true);
            Destroy(closest.gameObject);
        }
        else
        {
            Debug.Log("Inventory Full.");
            finalPickup.isPickedUp = false;
            if (col != null) col.enabled = true;
            CanvasManager.Instance.InvFullMessage.SetActive(true);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
