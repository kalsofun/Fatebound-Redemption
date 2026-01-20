using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SwitchRoom : MonoBehaviour
{
    LayerMask PlayerLayer;
    GameObject player;
    
    public string interactID;

    [SerializeField] Vector2 Range = new Vector2(10f, 10f);
    [SerializeField] Vector2 Offset = Vector2.zero;
    [SerializeField] string SwitchRoomName;
    [SerializeField] Vector2 PlayerNewPos = Vector2.zero;
    [SerializeField] bool DoorSoundSwitch = false;
    [SerializeField] bool InteractSwitch = true;
    public bool DoorLocked = false;
    [SerializeField] bool ImmediateSwitch = false;
    [SerializeField] bool FadeSwitch = false;

    void Start()
    {
        PlayerLayer = LayerMask.GetMask("Player");
        player = GameObject.Find("Player");
        if (ImmediateSwitch)
        {
            SwitchScene();
        }
    }

    void Update()
    {
        Collider2D Hit = Physics2D.OverlapBox((Vector2)transform.position + Offset, Range, 0f, PlayerLayer.value);
        if (Hit != null)
            if (InteractSwitch)
            {
                if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
                    if (!CanvasManager.Instance.AnyUIActive())
                        SwitchScene();
            }
            else
            {
                SwitchScene();
            }
    }

    void SwitchScene()
    {
        if (DoorSoundSwitch)
        {
            AudioManager.Instance.PlaySFX(0);
        }

        if (!DoorLocked)
        {
            if (!FadeSwitch)
            {
                Debug.Log("Switch Scene to " + SwitchRoomName);
                SceneManager.sceneLoaded += OnSceneLoaded;
                SceneManager.LoadScene(SwitchRoomName);
            }
            else
            {
                Debug.Log("Fade Switch Scene to " + SwitchRoomName);
                MenuManager.Instance.PlayerPos = PlayerNewPos;
                MenuManager.Instance.LoadScene(SwitchRoomName);
            }
        }
        else
        {
            CanvasManager.Instance.DoorLockedMessage.SetActive(true);
            Debug.Log("The Door is Locked.");
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player.transform.position = PlayerNewPos;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((Vector2)transform.position + Offset, Range);
    }
}
