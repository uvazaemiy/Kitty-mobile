using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemInShop : MonoBehaviour
{
    public event UnityAction<ItemShopSO, string, ItemInShop> Click;

    [SerializeField] private Image _body;
    [SerializeField] private Image _spriteWithPrice;
    [SerializeField] private Image _spriteWithoutPrice;
    [SerializeField] private Image _noClikcable;

    [SerializeField] private Button _button;

    [SerializeField] private GameObject _buttonPrice;
    [SerializeField] private Text _price;

    [Header("prefabs")]
    [SerializeField] private Sprite _normal;
    [SerializeField] private Sprite _selected;

    private ItemShopSO _itemShopSO;
    private string _saveData;

    private void Start()
    {
        _button.onClick.AddListener(() =>
        {
            Click?.Invoke(_itemShopSO, _saveData,this);
            //UpdateItem();
        });
    }

    //0 - none
    //1 - buy
    //2 - buy and set

    public void SetValue(ItemShopSO itemShopSO, string saveData)
    {
        _itemShopSO = itemShopSO;
        _saveData = saveData;

        UpdateItem();
    }

    public void UpdateItem()
    {
        _spriteWithPrice.sprite = _spriteWithoutPrice.sprite = _itemShopSO.Sprite;
        _price.text = _itemShopSO.Price.ToString();

        _noClikcable.gameObject.SetActive(false);

        if (PlayerPrefs.HasKey(_saveData))
        {
            int n = PlayerPrefs.GetInt(_saveData);

            _buttonPrice.SetActive(false);
            _spriteWithPrice.gameObject.SetActive(false);
            _spriteWithoutPrice.gameObject.SetActive(true);

            if (n == 1)
                _body.sprite = _normal;

            if (n == 2)
                _body.sprite = _selected;
        }
        else
        {
            if (_itemShopSO.OnlyCode)
            {
                _buttonPrice.SetActive(false);
                _spriteWithPrice.gameObject.SetActive(false);
                _spriteWithoutPrice.gameObject.SetActive(true);
                _noClikcable.gameObject.SetActive(true);
            }
            else
            {
                _spriteWithoutPrice.gameObject.SetActive(false);
                _buttonPrice.SetActive(true);
            }
        }
    }
}
