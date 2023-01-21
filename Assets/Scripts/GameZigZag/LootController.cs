using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.up * 50 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("GameManager").GetComponent<GameManager_ZigZag>().PickUpLoot();
        Destroy(gameObject);
    }
}
