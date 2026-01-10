using System.Collections;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 6f;
    [SerializeField] private float chaseDelay = 2f;
    private bool StartChasing = false;
    public bool CanChase = true;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(StartChaseAfterDelay());
    }

    private void Update()
    {
        if (!StartChasing || !CanChase)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 dir = (player.transform.position - transform.position).normalized;
        rb.linearVelocity = dir * speed;
    }

    private IEnumerator StartChaseAfterDelay()
    {
        yield return new WaitForSeconds(chaseDelay);
        StartChasing = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with Player.");
        }
    }
}