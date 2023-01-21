using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Bable : MonoBehaviour
{
    public event UnityAction EndAnimation;

    [SerializeField] private Text _text;
    [SerializeField] private TMP_Text _textTmp;
    [SerializeField] private float _timeShowMessage;

    [SerializeField] private CanvasAnimation _canvasAnimation;

    private Coroutine _coroutine;

    private bool _answer;

    private bool anim = false;

    private void Start()
    {
        _canvasAnimation.EndAnimationHide += ()=> { if(anim) EndAnimation?.Invoke(); };
        if (PlayerPrefs.GetInt("FirstLaunch") == 4)
        {
            SetText("Привiтики! Мене звуть Дивинка. Давай вчитися разом як за собою доглядати!");
            Show(true, 5);
            PlayerPrefs.SetInt("FirstLaunch", 5);
        }
    }

    public void Show(bool customTime = false, float time = 1f)
    {
        gameObject.SetActive(true);

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (customTime)
        {
            anim = false;
            _coroutine = StartCoroutine(Wait(time, () => { Hide(); }));
        }
        else
        {
            anim = true;
            _coroutine = StartCoroutine(Wait(_timeShowMessage, () => { Hide(); }));
        }
        _canvasAnimation.Show();
    }

    public void Hide()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _canvasAnimation.Hide();
    }

    public void SetText(string message)
    {
        _text.text = message;
        _textTmp.text = message;
    }

    public void SetAnswer(Answer answer)
    {
        _answer = true;
        SetText(answer.Message);
        Show();
    }

    public void ShowBable(string message, float time)
    {
        _answer = false;
        SetText(message);
        Show(true, time);
    }

    private IEnumerator Wait(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }
}
