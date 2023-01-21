using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{    
    public event UnityAction EndReduce;
    public event UnityAction EndIncrease;

    [SerializeField] private TypePopup _typePopup;

    public Image _image;

    [SerializeField] private float _reduceTime;
    public float _increaseTime;

    private float _increaseTimeValue;

    private float _targetValue;
    private StateBar _stateBar;
    private Coroutine _smoothStep;
    public float Fill => _image.fillAmount;

    private void Awake()
    { 
        PreLoad();
        Load();
        ConnectToData();
        Increase();
    }

    private void PreLoad()
    {
        _increaseTimeValue = _increaseTime;

        if (DataJSON.Instance.Debag)
            _increaseTimeValue = DataJSON.Instance.IncreaseTime;
    }

    private void Load()
    {
        DateTime loadTime = DataJSON.User.DateTime;
        ProgressItem temp = DataJSON.User.GetProgressItem(_typePopup);
        float step = 1f / _increaseTimeValue;
        if (PlayerPrefs.GetInt("FirstLaunch") <= 3)
        {
            _image.fillAmount = 0.3f;
            PlayerPrefs.SetInt("FirstLaunch", PlayerPrefs.GetInt("FirstLaunch") + 1);
        }
        else
            _image.fillAmount = temp.Fill + (step * (float)(DateTime.Now - loadTime).TotalSeconds);
    }

    private void ConnectToData()
    {
        DataJSON.User.AddToDictionary(_typePopup, this);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause == true)
        {
            _stateBar = StateBar.Wait;
            Increase();
        }
        if (pause == false)
        {
            Load();
        }
    }

    public void Reduce(float value = 0f)
    {
        _targetValue = value;
        _stateBar = StateBar.Reduce;

        if (_smoothStep != null)
            StopCoroutine(_smoothStep);

        _smoothStep = StartCoroutine(SmoothStep(_image, _targetValue, _reduceTime,
            () =>
            {
                _stateBar = StateBar.Wait;
                Increase();
                EndReduce?.Invoke();
            }));
    }

    public void Increase(float value = 1f)
    {
        if (_stateBar == StateBar.Reduce) return;

        _targetValue = value;
        _stateBar = StateBar.Increase;

        if (_smoothStep != null)
            StopCoroutine(_smoothStep);

        _smoothStep = StartCoroutine(SmoothStep(_image, _increaseTimeValue,
            () =>
            {
                _stateBar = StateBar.Wait;
                EndIncrease?.Invoke();
            }));
    }

    private IEnumerator SmoothStep(Image image, float targetValue, float time, UnityAction end)
    {
        float s = targetValue - image.fillAmount;
        float step = s / time;

        while (image.fillAmount != targetValue)
        {
            image.fillAmount += step * Time.deltaTime;
            yield return null;
        }

        end?.Invoke();
    }

    private IEnumerator SmoothStep(Image image, float time, UnityAction end, float targetValue = 1f)
    {
        float step = targetValue / time;

        while (image.fillAmount != targetValue)
        {
            image.fillAmount += step * Time.deltaTime;
            yield return null;
        }

        end?.Invoke();
    }
}

public enum StateBar
{
    Wait,
    Increase,
    Reduce
}