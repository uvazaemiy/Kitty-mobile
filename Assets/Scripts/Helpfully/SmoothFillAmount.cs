using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmoothFillAmount : MonoBehaviour
{
    private Image image;

    public float Value;
    public float speed;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        image.fillAmount = Mathf.SmoothStep(image.fillAmount, Value, Time.deltaTime * speed);
    }
}
