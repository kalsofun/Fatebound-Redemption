using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    private GameObject player;
    public float speed = 5f;

    private void Start() => player = GameObject.FindWithTag("Player");

    private void Update() => transform.position -= (transform.position - player.transform.position).normalized * Time.deltaTime * speed;
}