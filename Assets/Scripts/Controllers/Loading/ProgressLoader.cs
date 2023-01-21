using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class ProgressLoader : MonoBehaviour
{
    [Header("Parameters")]
    [Range(0,1)] public float Value;
    public string ProgressValueFormat = "{0}%";
    public float ProgressSpeed = 12;

    [Header("References")]
    public Image ProgressImage;
    public Text ProgressValue;
    
    void Update()
    {
        ProgressImage.fillAmount = Mathf.SmoothStep(ProgressImage.fillAmount, Value, ProgressSpeed * Time.deltaTime);
        ProgressValue.text = string.Format(ProgressValueFormat, (ProgressImage.fillAmount * 100).ToString("0"));
    }
}
