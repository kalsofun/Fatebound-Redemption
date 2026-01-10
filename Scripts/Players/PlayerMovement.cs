using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed = 5f;
    public bool canMove = true;

    private void Start() => rb = GetComponent<Rigidbody2D>();

    private void Update()
    {
        if (canMove)
        {
            Vector2 move = Vector2.zero;

            if (Keyboard.current != null)
            {
                float x = (Keyboard.current.dKey.isPressed ? 1f : 0f) - (Keyboard.current.aKey.isPressed ? 1f : 0f);
                float y = (Keyboard.current.wKey.isPressed ? 1f : 0f) - (Keyboard.current.sKey.isPressed ? 1f : 0f);
                move = new Vector2(x, y);
            }

            rb.linearVelocity = move.sqrMagnitude > 0f ? (move.normalized * speed) : Vector2.zero;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}