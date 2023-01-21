using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupContent : MonoBehaviour
{
    public event UnityAction ClickTake;

    [SerializeField] private Text _name;
    [SerializeField] private Image _image;

    [SerializeField] private Button _take;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _take.onClick.AddListener(OnClickTake);
    }

    private void OnDisable()
    {
        _take.onClick.RemoveListener(OnClickTake);
    }

    public void SetValue(ItemShopSO itemShopsSO)
    {
        Managers.SoundController.Instance.GetItem();

        _name.text = itemShopsSO.Name;
        _image.sprite = itemShopsSO.Sprite;
    }

    private void OnClickTake()
    {
        ClickTake?.Invoke();
    }
}
