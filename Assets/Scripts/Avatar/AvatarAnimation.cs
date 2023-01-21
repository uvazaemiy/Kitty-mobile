using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AvatarAnimation : MonoBehaviour
{
    public event UnityAction Animation;
    public event UnityAction EndBable;
    
    [SerializeField] private Animator _animator;
    [Space]
    [SerializeField] private Bable _bable;
    [SerializeField] private List<UIButton> _UIButtons = new List<UIButton>();

    public bool CatAnimationPlay;
    private bool _temp = false;

    private void OnEnable()
    {
        _UIButtons.ForEach(x =>
        {
            x.Click += OnClickButton;
        });

        _bable.EndAnimation += OnEndAnimation;
    }

    private void OnDisable()
    {
        _UIButtons.ForEach(x =>
        {
            x.Click -= OnClickButton;
        });

        _bable.EndAnimation -= OnEndAnimation;
    }

    private void OnEndAnimation()
    {
        CatAnimationPlay = _temp;
        _temp = false;

        EndBable?.Invoke();
    }

    private void OnClickButton(TypePopup type)
    {
        if (CatAnimationPlay)
            _temp = true;
        
        CatAnimationPlay = true;

        Animation?.Invoke();
        _animator.SetTrigger(type.ToString());
        
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            _animator.SetTrigger("idle");
        }
    }

    public void StartAnimHappy()
    {
        _animator.SetBool("happy", true);
        Invoke("CatSound", 0.02f);
    }

    private void CatSound()
    {
        Managers.SoundController.Instance.Happy();
        Invoke("CatSound", 2.7f);
    }

    public void StopAnimHappy()
    {
        _animator.SetBool("happy", false);
        CancelInvoke("CatSound");
    }
}
