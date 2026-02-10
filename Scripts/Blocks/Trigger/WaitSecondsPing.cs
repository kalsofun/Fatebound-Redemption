using System.Collections;
using UnityEngine;

public class WaitSecondsPing : MonoBehaviour
{
    [SerializeField] float DelayTime;
    [SerializeField] GameObject[] PingObjects;
    [SerializeField] GameObject[] DisableObjects;

    void Start()
    {
        StartCoroutine(Ping());
    }

    private IEnumerator Ping()
    {
        yield return new WaitForSeconds(DelayTime);
        if (PingObjects != null)
            foreach (GameObject obj in PingObjects)
            {
                obj.SetActive(true);
                Debug.Log("Item Available: " + obj);
            }
        if (DisableObjects != null)
            foreach (GameObject obj in DisableObjects)
            {
                obj.SetActive(false);
                Debug.Log("Item Disabled: " + obj);
            }
        this.gameObject.SetActive(false);
    }
}
