using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SwitchRoom : MonoBehaviour
{
    LayerMask PlayerLayer;

    [SerializeField] Vector2 Range = new Vector2(10f, 10f);
    [SerializeField] string SwitchRoomName;
    [SerializeField] bool ImmediateSwitch = false;

    void Start()
    {
        PlayerLayer = LayerMask.GetMask("Player");
        if (ImmediateSwitch)
        {
            Debug.Log("Switch Scene to " + SwitchRoomName);
            SceneManager.LoadScene(SwitchRoomName);
        }
    }

    void Update()
    {
        Collider2D Hit = Physics2D.OverlapBox((Vector2)transform.position, Range, 0f, PlayerLayer.value);
        if (Hit != null && Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            Debug.Log("Switch Scene to " + SwitchRoomName);
            SceneManager.LoadScene(SwitchRoomName);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Range);
    }
}
