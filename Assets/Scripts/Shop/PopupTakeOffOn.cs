using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupTakeOffOn : MonoBehaviour
{
    public event UnityAction TakeOff;
    public event UnityAction TakeOn;

    [SerializeField] private Text _name;
    [SerializeField] private Image _image;

    [SerializeField] private Button _takeOff;
    [SerializeField] private Button _takeOn;

    private void Start()
    {
        gameObject.SetActive(false);
        if (PlayerPrefs.GetInt("Clothes") == 0)
        {
            GameObject.Find("Nightgown").GetComponent<SkinnedMeshRenderer>().enabled = false;
            GameObject.Find("jaket").GetComponent<SkinnedMeshRenderer>().enabled = false;
            GameObject.Find("pant").GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
        else if (PlayerPrefs.GetInt("Clothes") == 1)
        {
            GameObject.Find("Nightgown").GetComponent<SkinnedMeshRenderer>().enabled = true;
            GameObject.Find("jaket").GetComponent<SkinnedMeshRenderer>().enabled = false;
            GameObject.Find("pant").GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
        else if (PlayerPrefs.GetInt("Clothes") == 2)
        {
            GameObject.Find("jaket").GetComponent<SkinnedMeshRenderer>().enabled = true;
            GameObject.Find("pant").GetComponent<SkinnedMeshRenderer>().enabled = true;
            GameObject.Find("Nightgown").GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
    }

    public void SetValue(ItemShopSO itemShopsSO, bool takeOn)
    {
        _name.text = itemShopsSO.Name;
        _image.sprite = itemShopsSO.Sprite;

        UpdateButton(takeOn);
    }

    private void OnEnable()
    {
        _takeOff.onClick.AddListener(OnClickTakeOff);
        _takeOn.onClick.AddListener(OnClickTakeOn);
    }

    private void OnDisable()
    {
        _takeOff.onClick.RemoveListener(OnClickTakeOff);
        _takeOn.onClick.RemoveListener(OnClickTakeOn);
    }

    private void OnClickTakeOff()
    {
        if (_name.text == "Пiжама" || _name.text == "Коктейльний костюм")
        { 
            GameObject.Find("Nightgown").GetComponent<SkinnedMeshRenderer>().enabled = false;
            GameObject.Find("jaket").GetComponent<SkinnedMeshRenderer>().enabled = false;
            GameObject.Find("pant").GetComponent<SkinnedMeshRenderer>().enabled = false;
            PlayerPrefs.SetInt("Clothes", 0);
        }
        TakeOff?.Invoke();
        UpdateButton(true);
        Managers.SoundController.Instance.RemoveItem();
    }

    private void OnClickTakeOn()
    {
        if (_name.text == "Пiжама")
        {
            PlayerPrefs.SetInt("Clothes", 1);
            GameObject.Find("Nightgown").GetComponent<SkinnedMeshRenderer>().enabled = true;
            GameObject.Find("jaket").GetComponent<SkinnedMeshRenderer>().enabled = false;
            GameObject.Find("pant").GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
        else if (_name.text == "Коктейльний костюм")
        {
            PlayerPrefs.SetInt("Clothes", 2);
            GameObject.Find("jaket").GetComponent<SkinnedMeshRenderer>().enabled = true;
            GameObject.Find("pant").GetComponent<SkinnedMeshRenderer>().enabled = true;
            GameObject.Find("Nightgown").GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
        TakeOn?.Invoke();
        UpdateButton(false);
        Managers.SoundController.Instance.UseItem();
    }

    private void UpdateButton(bool value)
    {
        _takeOff.gameObject.SetActive(!value);
        _takeOn.gameObject.SetActive(value);
    }
}
