using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    private GameObject player;
    public float speed = 5f;

    private void Start() => player = GameObject.FindWithTag("Player");

    private void Update() => transform.position -= (transform.position - player.transform.position).normalized * Time.deltaTime * speed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with Player.");
        }
    }
}