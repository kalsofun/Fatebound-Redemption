using UnityEngine;
using UnityEngine.InputSystem;

public class InteractBlocks : MonoBehaviour
{
    LayerMask PlayerLayer;
    
    public string interactID;

    [SerializeField] GameObject[] PingItems;
    [SerializeField] Vector2 Range = new Vector2(10f, 10f);
    [SerializeField] Vector2 Offset = Vector2.zero;

    void Start()
    {
        PlayerLayer = LayerMask.GetMask("Player");
    }

    void Update()
    {
        Collider2D Hit = Physics2D.OverlapBox((Vector2)transform.position + Offset, Range, 0, PlayerLayer.value);
        if (Hit != null && Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            Debug.Log("Interacted Item: " + this.gameObject);
            if (PingItems != null)
                foreach (GameObject item in PingItems)
                {
                    item.SetActive(true);
                    Debug.Log("Item Available: " + item);
                }
            this.gameObject.SetActive(false);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((Vector2)transform.position + Offset, Range);
    }
}
