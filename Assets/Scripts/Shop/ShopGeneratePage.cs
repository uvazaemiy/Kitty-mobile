using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopGeneratePage : MonoBehaviour
{
    [SerializeField] private ShopSO _shopSO;

    private int _countItem = 12;

    [SerializeField] private Transform _parent;

    [SerializeField] private GameObject _emptyTemplate;
    [SerializeField] private ItemInShop _itemTemplate;

    [SerializeField] private PopupItem _popupItem;

    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Text _text;

    private List<List<GameObject>> _pages = new List<List<GameObject>>();
    private List<string> _pagesName = new List<string>();

    private int _countPage;
    private int _index = 0;

    private List<ItemInShop> _itemInShops = new List<ItemInShop>();

    public ShopSO ShopSO => _shopSO;
    private void OnEnable()
    {
        _leftButton.onClick.AddListener(LeftClick);
        _rightButton.onClick.AddListener(RightClick);
    }

    private void OnDisable()
    {
        _leftButton.onClick.RemoveListener(LeftClick);
        _rightButton.onClick.RemoveListener(RightClick);
    }

    private void Start()
    {
        _popupItem.UpdateItem += UpdateAllItem;

        Generate();
        HideAll();
        ShowActive();
    }

    private void LeftClick()
    {
        _index--;
        if (_index < 0)
            _index = _countPage - 1;

        HideAll();
        ShowActive();
    }

    private void RightClick()
    {
        _index++;
        if (_index >= _countPage)
            _index = 0;

        HideAll();
        ShowActive();
    }

    private void Generate()
    {
        _countPage = _shopSO.PagesSO.Count;

        for (int i = 0; i < _shopSO.PagesSO.Count; i++)
        {
            List<GameObject> temp = new List<GameObject>();

            for (int j = 0; j < _shopSO.PagesSO[i].ItempPage.Count; j++)
            {
                ItemInShop item = Instantiate(_itemTemplate, _parent);
                string saveData = _shopSO.PagesSO[i].PageType.ToString() + _shopSO.PagesSO[i].ItempPage[j].Index.ToString();
                item.SetValue(_shopSO.PagesSO[i].ItempPage[j], saveData);
                temp.Add(item.gameObject);
                _itemInShops.Add(item);
                item.Click += _popupItem.ShowPopup;
            }

            for (int j = 0; j < _countItem - _shopSO.PagesSO[i].ItempPage.Count; j++)
            {
                GameObject item = Instantiate(_emptyTemplate, _parent);
                temp.Add(item);
            }

            _pagesName.Add(_shopSO.PagesSO[i].Name);
            _pages.Add(temp);
        }
    }

    private void HideAll()
    {
        for (int i = 0; i < _pages.Count; i++)
        {
            for (int j = 0; j < _pages[i].Count; j++)
            {
                _pages[i][j].SetActive(false);
            }
        }
    }

    private void ShowActive()
    {
        for (int i = 0; i < _pages[_index].Count; i++)
            _pages[_index][i].SetActive(true);

        _text.text = _pagesName[_index];
    }

    public void UpdateAllItem()
    {
        for (int i = 0; i < _itemInShops.Count; i++)
        {
            _itemInShops[i].UpdateItem();
        }
    }
}
