using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRoot : MonoBehaviour
{
    [SerializeField] private GameManager_Circle _GameManaging;
    [SerializeField] private float speed;
    Transform Ring;

    private void Start()
    {
        Ring = GameObject.Find("Ring").transform;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && transform.name == "Ring" && Time.timeScale != 0)
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * speed * 1000);
        else if (transform.name == "Ring(back)")
            transform.position = new Vector3(Ring.position.x - 158, Ring.position.y, Ring.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _GameManaging.EndGame();
    }
}
