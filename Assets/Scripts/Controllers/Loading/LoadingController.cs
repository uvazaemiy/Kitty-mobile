using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NotificationSamples;
using System;

public class LoadingController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ProgressLoader _loader;

    private void Start()
    {
        _loader.Value = 0;
        TimeService.Delay(1, () =>
        {
            TimeService.Interval(0.5f, 5, i =>
            {
                _loader.Value += 0.2f;

                if (_loader.Value == 1)
                    TimeService.Delay(1, () => SceneManager.LoadScene(1));
            });
        });
    }
}
