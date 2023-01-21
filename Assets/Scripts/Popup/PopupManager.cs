using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private bool _cutVersion;

    [Space]
    [SerializeField] private Bable _bable;
    [SerializeField] private PopupBable _PopupBable;

    [SerializeField] private GameObject _panel;
    [SerializeField] private Popup _popup;
    [SerializeField] private SpawnerCoin _spawnerCoin;

    [SerializeField] private List<UIButton> _UIButtons = new List<UIButton>();
    [SerializeField] private List<QuestionSO> _questions = new List<QuestionSO>();

    [SerializeField] private List<CalculatePolicy> _calculatePolicies = new List<CalculatePolicy>();

    private Dictionary<TypePopup, QuestionSO> _questionsDictionary = new Dictionary<TypePopup, QuestionSO>();

    private float _fill;

    private void Start()
    {
        foreach (var x in _questions)
            _questionsDictionary.Add(x.TypePopup, x);
    }

    private void OnEnable()
    {
        _UIButtons.ForEach(x =>
        {
            x.EndAnimation += OnShowPopup;
            x.Click += OnClickButton;
            x.Click += OnClickButtonSound;
        });
        _popup.ClickPopup += OnClickPopup;
    }

    private void OnDisable()
    {
        _UIButtons.ForEach(x =>
        {
            x.EndAnimation -= OnShowPopup;
            x.Click -= OnClickButton;
            x.Click -= OnClickButtonSound;
        });
        _popup.ClickPopup -= OnClickPopup;
    }

    //cut version
    //private void OnShowPopup(TypePopup type, float fillValue)
    //{
    //    _fill = fillValue;

    //    int coin = (int)(CalculateMoney(_fill));

    //    _spawnerCoin.SpawnCoin(coin);
    //    _spawnerCoin.MoneyAdd += OnAddMoney;
    //}

    private void OnShowPopup(TypePopup type, float fillValue)
    {
        if (_cutVersion)
        {
            _fill = fillValue;
            int coin = (int)(CalculateMoney(_fill));
            _spawnerCoin.SpawnCoin(coin);
            _spawnerCoin.MoneyAdd += OnAddMoney;
        }
        else
        {
            _fill = fillValue;
            _popup.Show();
            _popup.ShowQuestion(_questionsDictionary[type]);
        }
    }

    private void OnClickPopup(Answer answer)
    {
        _popup.Hide();

        float cof = answer.RewardCoefficient / 100f;
        int coin = (int)(CalculateMoney(_fill) * cof);

        _spawnerCoin.SpawnCoin(coin);
        _spawnerCoin.MoneyAdd += OnAddMoney;

        _bable.SetAnswer(answer);
    }

    private void OnClickButtonSound(TypePopup type)
    {
        switch (type)
        {
            case TypePopup.eat:
                Managers.SoundController.Instance.Eat();
                break;
            case TypePopup.play:
                Managers.SoundController.Instance.Game();
                break;
            case TypePopup.sleep:
                Managers.SoundController.Instance.Sleep();
                break;
            case TypePopup.toilet:
                Managers.SoundController.Instance.Toilet();
                break;
        }
        _PopupBable.OnHideBable();
    }

    private void OnClickButton(TypePopup type)
    {
        _panel.SetActive(true);
    }

    private void OnAddMoney()
    {
        _panel.SetActive(false);
    }

    private int CalculateMoney(float fill)
    {
        fill *= 100f;

        for (int i = 0; i < _calculatePolicies.Count; i++)
        {
            if (fill >= _calculatePolicies[i].From && fill <= _calculatePolicies[i].To)
            {
                return Mathf.RoundToInt(fill * _calculatePolicies[i].Coefficient);
            }
        }

        return Mathf.RoundToInt(fill);
    }

    private void OnApplicationPause(bool pause)
    {
        _panel.SetActive(false);
        _popup.Hide();
        _bable.Hide();
    }
}

public enum TypePopup
{
    eat,
    play,
    sleep,
    toilet,
    happy
}

[Serializable]
public class CalculatePolicy
{
    public float From;
    public float To;
    public float Coefficient;
}