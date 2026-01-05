using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 3f;

    private void Start() => rb = GetComponent<Rigidbody2D>();

    private void Update()
    {
        Vector2 move = Vector2.zero;

        // Prefer gamepad left stick when available
        if (Gamepad.current != null)
        {
            move = Gamepad.current.leftStick.ReadValue();
        }
        else if (Keyboard.current != null)
        {
            float x = (Keyboard.current.dKey.isPressed ? 1f : 0f) - (Keyboard.current.aKey.isPressed ? 1f : 0f);
            float y = (Keyboard.current.wKey.isPressed ? 1f : 0f) - (Keyboard.current.sKey.isPressed ? 1f : 0f);
            move = new Vector2(x, y);
        }

        rb.linearVelocity = move.sqrMagnitude > 0f ? (move.normalized * speed) : Vector2.zero;
    }
}