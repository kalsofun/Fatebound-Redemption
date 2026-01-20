using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Transform playerT;

    [SerializeField] private float speed = 5f;
    public bool canMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerT = transform;
        MenuManager.RegisterPlayer(transform);

        SaveData data = SaveManager.Instance.Load();
        if (data != null)
            playerT.position = new Vector2(data.PlayerPosX, data.PlayerPosY);
    }

    private void Update()
    {
        if (canMove && PauseManager.Instance.CanPauseScene())
        {
            Vector2 move = Vector2.zero;

            if (Keyboard.current != null)
            {
                float x = Keyboard.current.dKey.ReadValue() - Keyboard.current.aKey.ReadValue();
                float y = Keyboard.current.wKey.ReadValue() - Keyboard.current.sKey.ReadValue();
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