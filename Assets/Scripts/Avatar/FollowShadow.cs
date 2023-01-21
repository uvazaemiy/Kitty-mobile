using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowShadow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    //[SerializeField] private bool _x;
    //[SerializeField] private bool _y;
    //[SerializeField] private bool _z;

    private void Update()
    {
        transform.position = new Vector3(_target.position.x, transform.position.y, transform.position.z) + _offset;
    }
}

//12
//18
//13
//18