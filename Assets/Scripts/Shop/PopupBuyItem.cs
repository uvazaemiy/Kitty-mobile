using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupBuyItem : MonoBehaviour
{
    public event UnityAction ClickBuy;

    [SerializeField] private Text _name;    
    [SerializeField] private Text _money;
    [SerializeField] private Text _price;
    [SerializeField] private Image _image;

    [SerializeField] private Button _buttonBuy;

    private int _value;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void SetValue(ItemShopSO itemShopsSO)
    {
        _name.text = itemShopsSO.Name;
        _price.text = itemShopsSO.Price.ToString();
        _image.sprite = itemShopsSO.Sprite;
        _money.text = Wallet.GetValue.ToString();
        _value = itemShopsSO.Price;
    }

    private void OnEnable()
    {
        _buttonBuy.onClick.AddListener(ClickBuyItem);
    }

    private void OnDisable()
    {
        _buttonBuy.onClick.RemoveListener(ClickBuyItem);
    }

    private void ClickBuyItem()
    {
        Wallet.TrySubtract(_value);
        ClickBuy?.Invoke();
    }
}
