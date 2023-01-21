using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupNoMoney : MonoBehaviour
{
    [SerializeField] private Text _name;
    [SerializeField] private Text _money;
    [SerializeField] private Text _price;
    [SerializeField] private Image _image;

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
    }
}
