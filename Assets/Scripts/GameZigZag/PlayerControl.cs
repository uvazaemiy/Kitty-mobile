using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody rb;
    private bool dir = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        MoveSphere();
    }

    private void MoveSphere()
    {
        if (dir)
            transform.Translate(Vector3.right * Time.deltaTime);
        else
            transform.Translate(Vector3.forward * Time.deltaTime);
        Invoke("MoveSphere", 0);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dir = !dir;
            CancelInvoke("MoveSphere");
            MoveSphere();
        }
        Vector3 target = new Vector3(transform.position.x - 10, 15, transform.position.z - 10);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, target, 2 * Time.deltaTime);
    }
}
