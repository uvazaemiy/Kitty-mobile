using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFps : MonoBehaviour
{
    [SerializeField] private int _value;
    private void Awake()
    {
        Application.targetFrameRate = _value;
        DontDestroyOnLoad(this);
    }
}
