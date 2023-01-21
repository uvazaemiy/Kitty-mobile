using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanvasAnimation : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] private float _time;

    public event UnityAction EndAnimation;
    public event UnityAction EndAnimationHide;

    private Coroutine _coroutine;

    private void Start()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
    }

    public void Show()
    {
        gameObject.SetActive(true);

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Anim(_canvasGroup, 1f, _time));
    }

    public void Hide()
    {
        if (gameObject.activeSelf == false) return;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Anim(_canvasGroup, 0f, _time));
    }

    private IEnumerator Anim(CanvasGroup canvasGroup, float targetValue, float time)
    {
        float step = (targetValue - canvasGroup.alpha) / time;

        while (canvasGroup.alpha != targetValue)
        {
            canvasGroup.alpha += step * Time.deltaTime;
            yield return null;
        }

        bool tr = _canvasGroup.interactable = targetValue == 1;
        gameObject.SetActive(tr);
        EndAnimation?.Invoke();

        if (tr == false)
        {
            EndAnimationHide?.Invoke();
        }
    }
}
