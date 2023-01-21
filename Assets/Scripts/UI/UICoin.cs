using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICoin : MonoBehaviour
{
    public UnityAction MoneyAdd;

    [SerializeField] private Text _text;

    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] private float _timeWait = 2f;
    [SerializeField] private float _timeToUp = 1f;
    [SerializeField] private float _timeAlpha = 1f;

    [SerializeField] private Transform _targetPosition;

    private int _value;
    public void SetValue(int value)
    {
        _value = value;
        _text.text = "+" + _value.ToString();
    }

    public void SetTargetPosition(Transform position)
    {
        _targetPosition = position;
    }

    public void StartAnimation()
    {
        StartCoroutine(AnimStep0());
    }

    private IEnumerator AnimStep0()
    {
        yield return new WaitForSeconds(_timeWait);
        StartCoroutine(AnimStep1(transform, _targetPosition, _timeToUp, _canvasGroup, _timeAlpha));
    }

    private IEnumerator AnimStep1(Transform transform, Transform target, float time, CanvasGroup canvasGroup, float timeCanvas)
    {
        Vector3 s = target.position - transform.position;
        float xstep = s.x / time;
        float ystep = s.y / time;
        float zstep = s.z / time;

        float step = 1f / timeCanvas;

        float t = 0f;

        while (t < time)
        {
            t += Time.deltaTime;

            transform.position += new Vector3(xstep * Time.deltaTime, ystep * Time.deltaTime, zstep * Time.deltaTime);

            canvasGroup.alpha -= step * Time.deltaTime;

            yield return null;
        }

        Wallet.Add(_value);

        MoneyAdd?.Invoke();
        MoneyAdd = null;

        Managers.SoundController.Instance.GetMoney();

        Destroy(this.gameObject);
    }
}
