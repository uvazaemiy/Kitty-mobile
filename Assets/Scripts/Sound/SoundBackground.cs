using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundBackground : MonoBehaviour
{
    [SerializeField] private SpawnerCoin _spawnerCoin;
    [SerializeField] private AvatarAnimation _avatarAnimation;
    [Space]
    [SerializeField] private Image SoundImg;
    [SerializeField] private Sprite SoundOff;
    [SerializeField] private Sprite SoundOn;

    private bool mute = false;

    private void Start()
    {
        Managers.SoundController.Instance.CatBackground.Play();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Managers.SoundController.Instance.CatBackground.Play();
            if (mute)
                Managers.SoundController.Instance.CatBackground.VolumeChange(0);
        }
    }

    //private void OnEnable()
    //{
    //    _spawnerCoin.MoneyAdd += OnPlayBackground;
    //    //_avatarAnimation.Animation += OnPauseBackgound;
    //}

    //private void OnDisable()
    //{
    //    _spawnerCoin.MoneyAdd -= OnPlayBackground;
    //    //_avatarAnimation.Animation -= OnPauseBackgound;
    //}

    //private void OnApplicationPause(bool pause)
    //{
    //    if (pause)
    //    {
    //        Managers.SoundController.Instance.CatBackground.Play();
    //    }
    //}

    public void SoundOffOn()
    {
        if (mute)
        {
            Managers.SoundController.Instance.CatBackground.VolumeChange(0.3f);
            SoundImg.sprite = SoundOn;
            mute = false;
        }
        else
        {
            Managers.SoundController.Instance.CatBackground.VolumeChange(0);
            SoundImg.sprite = SoundOff;
            mute = true;
        }
    }
}
