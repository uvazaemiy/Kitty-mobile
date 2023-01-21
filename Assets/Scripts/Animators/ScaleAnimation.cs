using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    [SerializeField] private float _time;

    [SerializeField] private Vector3 _showTarget;
    [SerializeField] private Vector3 _hideTarget;

    private Coroutine _coroutine;

    private void Start()
    {
        transform.localScale = _hideTarget;
    }

    public void Show()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(Anim(_showTarget, _time, new MoreA()));
    }

    public void Hide()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Anim(_hideTarget, _time, new MoreB()));
    }

    private IEnumerator Anim( Vector3 targetValue, float time, IPolicy policy)
    {
        float stepx = (targetValue.x - transform.localScale.x) / time;
        float stepy = (targetValue.y - transform.localScale.y) / time;
        float stepz = (targetValue.z - transform.localScale.z) / time;

        while (true)
        {
            transform.localScale += new Vector3(stepx, stepy, stepz) * Time.deltaTime;
            if (policy.Check(transform.localScale.x, targetValue.x))
            {
                transform.localScale = targetValue;
                break;
            }

            yield return null;
        }
    }
}

public interface IPolicy
{
    public bool Check(float a, float b);
}

public class MoreA : IPolicy
{
    public bool Check(float a, float b)
    {
        return a > b;
    }
}

public class MoreB : IPolicy
{
    public bool Check(float a, float b)
    {
        return a < b;
    }
}