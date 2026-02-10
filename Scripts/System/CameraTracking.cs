using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Vector2 min;
    [SerializeField] private Vector2 max;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    
    private void Update()
    {
        transform.position = new Vector3
        (
            Mathf.Clamp(player.transform.position.x, min.x, max.x),
            Mathf.Clamp(player.transform.position.y, min.y, max.y),
            -10
        );
    }
}
