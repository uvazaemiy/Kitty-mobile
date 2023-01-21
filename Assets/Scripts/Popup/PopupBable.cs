using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBable : MonoBehaviour
{
    [SerializeField] private Bable _bable;
    [SerializeField] private AvatarAnimation _avatarAnimation;

    [SerializeField] private List<ProgressBar> _progressBars = new List<ProgressBar>();

    private string _messageEat = "Ухх, я вже хочу їсти…";
    private string _messagePlay = "А де мій клубочок? Погнали гратися!";
    private string _messageSleep = "Час спати...";
    private string _messageToilet = "Потрібно до вбиральні. ОЙ-ой, скільки ще терпіти?";

    private string _messageNotEat = "Я не голодний.";
    private string _messageNotPlay = "Мені й так дуже весело.";
    private string _messageNotSleep = "Я ще не хочу спати.";
    private string _messageNotToilet = "Не хочу до вбиральні.";

    private string _messageMoney = "У тебе вже достатньо грошикiв, щоб купити щось у магазинi!";

    private List<string> _messageList = new List<string>();

    private void Awake()
    {
        _messageList.Add(_messageEat);
        _messageList.Add(_messagePlay);
        _messageList.Add(_messageSleep);
        _messageList.Add(_messageToilet);

        _messageList.Add(_messageNotEat);
        _messageList.Add(_messageNotPlay);
        _messageList.Add(_messageNotSleep);
        _messageList.Add(_messageNotToilet);

        _messageList.Add(_messageMoney);
    }

    //private void Start()
    //{
    //    CheckShowBable();
    //}

    //private void OnEnable()
    //{
    //    _avatarAnimation.Animation += OnHideBable;
    //    _avatarAnimation.EndBable += OnEndBable;

    //    for (int i = 0; i < _progressBars.Count; i++)
    //        _progressBars[i].EndIncrease += OnEndAnimation;

    //    _bable.EndAnimation += OnEndAnimation;
    //}

    //private void OnDisable()
    //{
    //    _avatarAnimation.Animation -= OnHideBable;
    //    _avatarAnimation.EndBable -= OnEndBable;

    //    for (int i = 0; i < _progressBars.Count; i++)
    //        _progressBars[i].EndIncrease -= OnEndAnimation;

    //    _bable.EndAnimation -= OnEndAnimation;
    //}

    public void OnHideBable()
    {
        _bable.Hide();
    }

    //private void OnEndAnimation()
    //{
    //    CheckShowBable();
    //}

    //private void OnEndBable()
    //{
    //    CheckShowBable();
    //}

    //private void CheckShowBable()
    //{
    //    for (int i = 0; i < _progressBars.Count; i++)
    //    {
    //        if (_progressBars[i].Fill == 1f)
    //        {
    //            ShowBable(i);
    //            break;
    //        }
    //    }
    //}

    public void ShowBable(int index, float time = float.MaxValue)
    {
        if (_avatarAnimation.CatAnimationPlay == false)
        {
            _bable.ShowBable(_messageList[index], time);
        }
    }
}
