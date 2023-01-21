using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public Type TypeOfBorder;
    private void OnTriggerExit2D(Collider2D other)
    {
        transform.parent.GetComponent<GameManager_Circle>().BorderExit(other.gameObject, (int)TypeOfBorder);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((int)TypeOfBorder == 2)
            transform.parent.GetComponent<GameManager_Circle>().border = !transform.parent.GetComponent<GameManager_Circle>().border;
    }
}

public enum Type
{
    Rigth,
    Left,
    UpOrDown
}
