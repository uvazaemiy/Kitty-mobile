using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupItem : MonoBehaviour
{
    public event UnityAction UpdateItem;

    [SerializeField] private CanvasAnimation _canvasAnimation;

    [SerializeField] private PopupAnimate _codePopup;
    [SerializeField] private CanvasAnimation _codeCanvasAnimation;

    [SerializeField] private PopupAnimate _buyItem;
    [SerializeField] private PopupBuyItem _popupBuyItem;

    [SerializeField] private PopupAnimate _noMoney;
    [SerializeField] private PopupNoMoney _popupNoMoney;

    [SerializeField] private PopupAnimate _takeOffOn;
    [SerializeField] private PopupTakeOffOn _popupTakeOffOn;

    [SerializeField] private PopupAnimate _content;
    [SerializeField] private PopupContent _popupContent;

    [SerializeField] private ObjectManager _objectManager;

    [SerializeField] private List<ExclusionSO> _exclusionSO = new List<ExclusionSO>();

    private ItemShopSO _itemShopSO;
    private string _saveData;
    private ItemInShop _itemInShop;

    private void OnEnable()
    {
        _popupBuyItem.ClickBuy += OnClickBuy;

        _buyItem.HidePopup += _canvasAnimation.Hide;
        _noMoney.HidePopup += _canvasAnimation.Hide;
        _takeOffOn.HidePopup += _canvasAnimation.Hide;
        _content.HidePopup += _canvasAnimation.Hide;

        _popupTakeOffOn.TakeOff += OnTakeOff;
        _popupTakeOffOn.TakeOn += OnTakeOn;

        _takeOffOn.HidePopup += _objectManager.UpdateObject;
    }

    private void OnDisable()
    {
        _popupBuyItem.ClickBuy -= OnClickBuy;

        _buyItem.HidePopup -= _canvasAnimation.Hide;
        _noMoney.HidePopup -= _canvasAnimation.Hide;
        _takeOffOn.HidePopup -= _canvasAnimation.Hide;
        _content.HidePopup -= _canvasAnimation.Hide;

        _popupTakeOffOn.TakeOff -= OnTakeOff;
        _popupTakeOffOn.TakeOn -= OnTakeOn;

        _takeOffOn.HidePopup -= _objectManager.UpdateObject;
    }

    private void OnTakeOff()
    {
        PlayerPrefs.SetInt(_saveData, 1);
        _itemInShop.UpdateItem();
    }

    private void OnTakeOn()
    {
        PlayerPrefs.SetInt(_saveData, 2);
        CheckExclusion();
        PlayerPrefs.SetInt(_saveData, 2);
        _itemInShop.UpdateItem();
        UpdateItem?.Invoke();
    }

    private void CheckExclusion()
    {
        string temp;
        List<string> listSaveData;

        for (int i = 0; i < _exclusionSO.Count; i++)
        {
            listSaveData = new List<string>();

            for (int j = 0; j < _exclusionSO[i].ItemShopSO.Count; j++)
            {
                temp = _exclusionSO[i].PageType.ToString() + _exclusionSO[i].ItemShopSO[j].Index.ToString();

                if (PlayerPrefs.GetInt(temp, 0) == 2)
                {
                    listSaveData.Add(temp);
                }
            }

            if (listSaveData.Count >= 2)
            {
                for (int j = 0; j < listSaveData.Count; j++)
                {
                    PlayerPrefs.SetInt(listSaveData[j], 1);
                }
            }
        }
    }

    public void ShowPopup(ItemShopSO itemShopSO, string saveData, ItemInShop itemInShop)
    {
        _itemShopSO = itemShopSO;
        _saveData = saveData;
        _itemInShop = itemInShop;

        switch (PlayerPrefs.GetInt(saveData, 0))
        {
            case 0:
                if (itemShopSO.OnlyCode)
                {
                    _codePopup.Show();
                    _codeCanvasAnimation.Show();
                    _codePopup.transform.GetChild(0).Find("Name").GetComponent<Text>().text = itemShopSO.Name;
                    return;
                }
        
                if (Wallet.GetValue >= itemShopSO.Price)
                {
                    _buyItem.Show();
                    _popupBuyItem.SetValue(itemShopSO);
                }
                else
                {
                    _noMoney.Show();
                    _popupNoMoney.SetValue(itemShopSO);
                }
                break;
        
            case 1:
                _takeOffOn.Show();
                _popupTakeOffOn.SetValue(itemShopSO, true);
                break;
        
            case 2:
                _takeOffOn.Show();
                _popupTakeOffOn.SetValue(itemShopSO, false);
                break;
        }
        
        _canvasAnimation.Show();
    }

    private void OnClickBuy()
    {
        _buyItem.HideNoEvent();
        _content.Show();
        _popupContent.SetValue(_itemShopSO);

        PlayerPrefs.SetInt(_saveData, 1);

        UpdateItem?.Invoke();
    }
}
