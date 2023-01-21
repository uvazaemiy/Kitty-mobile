using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WalletUI : MonoBehaviour
{
    //[SerializeField] private TMP_Text _text;
    [SerializeField] private Text _text;
    [SerializeField] private PopupBable _PopupBable;

    private void Start()
    {
        OnChangetValue(Wallet.GetValue);
        Invoke("MessageMoney", 30);
    }

    private void OnEnable()
    {
        Wallet.ChangetValue += OnChangetValue;
    }

    private void OnDisable()
    {
        Wallet.ChangetValue -= OnChangetValue;
    }

    private void OnChangetValue(int value)
    {
        _text.text = value.ToString();
    }

    private void MessageMoney()
    {
        if (int.Parse(_text.text) >= 200 && Random.Range(0, 5) == 1)
            _PopupBable.ShowBable(8, 5);
        Invoke("MessageMoney", 30);
    }
}
