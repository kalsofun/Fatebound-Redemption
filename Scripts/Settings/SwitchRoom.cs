using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SwitchRoom : MonoBehaviour
{
    LayerMask PlayerLayer;

    [SerializeField] Vector2 Range = new Vector2(10f, 10f);
    [SerializeField] string SwitchRoomName;
    [SerializeField] bool ImmediateSwitch = false;
    [SerializeField] bool DoorSoundSwitch = false;
    [SerializeField] bool FadeSwitch = false;

    void Start()
    {
        PlayerLayer = LayerMask.GetMask("Player");
        if (ImmediateSwitch)
        {
            SwitchScene();
        }
    }

    void Update()
    {
        Collider2D Hit = Physics2D.OverlapBox((Vector2)transform.position, Range, 0f, PlayerLayer.value);
        if (Hit != null && Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            SwitchScene();
        }
    }

    void SwitchScene()
    {
        if (!FadeSwitch)
        {
            Debug.Log("Switch Scene to " + SwitchRoomName);
            if (DoorSoundSwitch)
            {
                AudioManager.Instance.PlaySFX(0);
            }
            SceneManager.LoadScene(SwitchRoomName);
        }
        else
        {
            Debug.Log("Fade Switch Scene to " + SwitchRoomName);
            MenuManager.Instance.LoadScene(SwitchRoomName);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Range);
    }
}
