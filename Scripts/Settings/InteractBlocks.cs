using UnityEngine;
using UnityEngine.InputSystem;

public class InteractBlocks : MonoBehaviour
{
    LayerMask PlayerLayer;

    public GameObject[] PingItems;
    [SerializeField] Vector2 Range = new Vector2(10f, 10f);

    void Start()
    {
        PlayerLayer = LayerMask.GetMask("Player");
    }

    void Update()
    {
        Collider2D Hit = Physics2D.OverlapBox((Vector2)transform.position, Range, 0f, PlayerLayer.value);
        if (Hit != null && Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            foreach (GameObject item in PingItems)
            {
                item.SetActive(true);
                Debug.Log("Item available: " + item);
            }
            this.gameObject.SetActive(false);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Range);
    }
}
