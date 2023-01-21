using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using NotificationSamples;
using System;

public class UIButton : MonoBehaviour
{
    Animator Cat_Anim;
    
    public event UnityAction<TypePopup> Click;
    public event UnityAction<TypePopup, float> EndAnimation;

    [SerializeField] private TypePopup _typePopup;

    [SerializeField] private Notifications _Notifications;

    [SerializeField] private Button _button;
    [SerializeField] private GameObject Popup;
    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private GameObject Warning;
    [SerializeField] private PopupBable _PopupBable;

    [SerializeField] private float _minValueClick = 0.25f;

    private float _fillValue;
    private bool scaleanim = true;
    private AnimatorClipInfo[] CurrentClip;

    private void Update()
    {
        CurrentClip = Cat_Anim.GetCurrentAnimatorClipInfo(0);
        if (_progressBar.Fill == 1)
            Warning.SetActive(true);
        if (_progressBar.Fill >= 0.75)
        {
            Cat_Anim.SetBool("pre" + _typePopup.ToString(), true);
            if (CurrentClip[0].clip.name == "CatPreeat")
                _PopupBable.ShowBable(0);
            if (CurrentClip[0].clip.name == "CatPreplay")
                _PopupBable.ShowBable(1);
            if (CurrentClip[0].clip.name == "CatPresleep")
                _PopupBable.ShowBable(2);
            if (CurrentClip[0].clip.name == "CatPretoilet")
                _PopupBable.ShowBable(3);
        }
        else
            Cat_Anim.SetBool("pre" + _typePopup.ToString(), false);
    }

    private void OnEnable()
    {
        Cat_Anim = GameObject.Find("Cat").GetComponent<Animator>();
        _button.onClick.AddListener(OnClick);
        _progressBar.EndReduce += OnEndReduce;
        Invoke("ScaleAnim", 1);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
        _progressBar.EndReduce -= OnEndReduce;
    }

    private void OnEndReduce()
    {
        EndAnimation?.Invoke(_typePopup, _fillValue);
    }

    private void OnClick()
    {
        if (_progressBar.Fill >= _minValueClick)
        {
            //_Notifications.InitNotifications();
            Warning.SetActive(false);
            scaleanim = false;
            transform.localScale = Vector3.one;
            _fillValue = _progressBar.Fill;
            _progressBar.Reduce();
            Click?.Invoke(_typePopup);
        }
        else
            _PopupBable.ShowBable(4 + (int)_typePopup, 3);
    }

    private void ScaleAnim()
    {
        if (_progressBar.Fill >= _minValueClick && scaleanim)
            GetComponent<Animation>().Play("ScaleWarning");
        else if (_progressBar.Fill < _minValueClick)
            scaleanim = true;
        else
            GetComponent<Animation>().Stop("ScaleWarning");
        Invoke("ScaleAnim", 0.02f);
    }
}
