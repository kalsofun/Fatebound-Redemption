using System.Collections;
using UnityEngine;

public class WaitSecondsPing : MonoBehaviour
{
    [SerializeField] float DelayTime;
    [SerializeField] GameObject[] PingObjects;

    void Start()
    {
        StartCoroutine(Ping());
    }

    private IEnumerator Ping()
    {
        yield return new WaitForSeconds(DelayTime);
        foreach (GameObject obj in PingObjects)
        {
            obj.SetActive(true);
            Debug.Log("Item Available: " + obj);
        }
        this.gameObject.SetActive(false);
    }
}
