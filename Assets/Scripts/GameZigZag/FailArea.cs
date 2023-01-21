using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailArea : MonoBehaviour
{
    private void Update()
    {
        transform.position = new Vector3(GameObject.Find("Sphere").transform.position.x, -10, GameObject.Find("Sphere").transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("GameManager").GetComponent<GameManager_ZigZag>().EndGame();
    }
}
