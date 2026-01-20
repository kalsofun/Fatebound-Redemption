using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFindings : MonoBehaviour
{
    PlayerMovement pm;
    LayerMask findingsLayer;

    [SerializeField] float useItemRange = 1.2f;

    bool isOpen = false;
    string currentInteractID = "";

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        findingsLayer = LayerMask.GetMask("Findings");
    }

    void Update()
    {
        if (PauseManager.Instance.CanPauseScene())
            if (!PauseManager.Instance.isPaused && Keyboard.current.rKey.wasPressedThisFrame)
                if (!CanvasManager.Instance.DialogueBoxActive() && !CanvasManager.Instance.InventoryUIActive())
                {
                    isOpen = !isOpen;
                    CanvasManager.Instance.FindingsUI.SetActive(isOpen);
                    CanvasManager.Instance.FindingsDetailUI.SetActive(false);
                    pm.canMove = !isOpen;
                    Time.timeScale = isOpen ? 0f : 1f;
                }

        if (Keyboard.current.fKey.wasPressedThisFrame)
            if (!CanvasManager.Instance.AnyUIActive())
                PickUp();
    }

    void PickUp()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, useItemRange, findingsLayer);
        if (hit == null) return;

        FindingsPickup fd = hit.GetComponent<FindingsPickup>();

        Debug.Log("Picked up Findings: " + hit);
        AudioManager.Instance.PlaySFX(2);
        CanvasManager.Instance.PickupItemMessage.GetComponent<DialogueText>().textEffects[0].fullDialogue = "獲得物件" + fd.FindingsName + "。";
        CanvasManager.Instance.PickupItemMessage.SetActive(true);

        SaveManager.Instance.CurrentData.Findings[fd.FindingsIndex] = true;
        SaveManager.Instance.Save();

        FindingsDataLoad fdl = CanvasManager.Instance.FindingsUI.GetComponent<FindingsDataLoad>();
        fdl.findingsObject[fd.FindingsIndex].SetActive(true);

        Destroy(hit.gameObject);
    }

    bool CanUseItem(FindingsData fd)
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
        return currentInteractID == fd.requiredInteractID;
    }

    public void UseItem(int index)
    {
        FindingsData fd = CanvasManager.Instance.FindingsDetailUI.GetComponent<FindingsDetailUI>().findingsData[index];

        isOpen = false;
        CanvasManager.Instance.FindingsUI.SetActive(false);
        CanvasManager.Instance.FindingsDetailUI.SetActive(false);
        pm.canMove = true;
        Time.timeScale = 1f;

        if (!CanUseItem(fd))
        {
            Debug.Log("Findings can't used: " + fd.name);
            CanvasManager.Instance.ItemCantUseMessage.SetActive(true);
            return;
        }

        Debug.Log("Findings used: " + fd.name);
        if (fd.useFindings != null) fd.useFindings.SetActive(true);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, useItemRange);
    }
}
