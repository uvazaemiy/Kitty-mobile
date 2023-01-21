using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeCheck : MonoBehaviour
{
    [SerializeField] private CodeDataSO _codeDataSO;
    [Space]
    [SerializeField] private InputField _input;
    [SerializeField] private Image _inputFieldImage;
    [SerializeField] private Sprite _inputFieldNormal;
    [SerializeField] private Sprite _inputFieldError;
    [Space]
    [SerializeField] private GameObject _error;
    [SerializeField] private Text _errorMessage;
    [SerializeField] private Button _button;
    [Space]
    [SerializeField] private string _errorCode = "Неправильний код!";
    [SerializeField] private string _errorIntroduced = "Цей код вже був введений.";
    [Space]
    [SerializeField] private CodePopup _codePopup;
    [Space]
    [SerializeField] private ShopGeneratePage _shopGeneratePage;
    [Space]
    [SerializeField] private PopupContent _popupContent;
    [SerializeField] private PopupAnimate _popupAnimate;

    private string _inputCode;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnCheck);

        InputNormalize();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnCheck);
    }

    private void OnCheck()
    {
        _inputCode = _input.text;

        InputNormalize(_inputCode);

        if (CheckInSave(_inputCode))
        {
            ShowError(_errorIntroduced);
        }
        else if (CheckInData(_inputCode))
        {
            Managers.SoundController.Instance.Click();

            _codePopup.Hide();

            _popupContent.SetValue(OpenRandomItem());
            _shopGeneratePage.UpdateAllItem();
            _popupAnimate.Show();

            PlayerPrefs.SetString(_inputCode, "1");
            PlayerPrefs.Save();
        }
        else
        {
            ShowError(_errorCode);
        }
    }


    private void ShowError(string message)
    {
        Managers.SoundController.Instance.ErrorCode();

        _error.SetActive(true);
        _errorMessage.text = message;
        _inputFieldImage.sprite = _inputFieldError;
    }

    private void InputNormalize(string message = "")
    {
        _error.SetActive(false);       
        _inputFieldImage.sprite = _inputFieldNormal;
        _input.text = message;
    }

    private bool CheckInData(string text)
    {
        DateTime date = DateTime.Now;

        for (int i = 0; i < _codeDataSO.CodeDatas.Count; i++)
        {
            if (_codeDataSO.CodeDatas[i].Code == text)
            {
                if (date.Year >= _codeDataSO.CodeDatas[i].From.Year && date.Year <= _codeDataSO.CodeDatas[i].To.Year)
                {
                    if (date.Year < _codeDataSO.CodeDatas[i].To.Year)
                        return true;
                    if (date.Month == _codeDataSO.CodeDatas[i].To.Month && date.Day <= _codeDataSO.CodeDatas[i].To.Day)
                        return true;
                    if (date.Month >= _codeDataSO.CodeDatas[i].From.Month && date.Month <= _codeDataSO.CodeDatas[i].To.Month)
                        return true;
                }
            }
        }

        return false;
    }

    private bool CheckInSave(string text)
    {
        return PlayerPrefs.HasKey(text);
    }

    public ItemShopSO OpenRandomItem()
    {
        Dictionary<string, ItemShopSO> keyValuePairs = new Dictionary<string, ItemShopSO>();
        List<string> temp = new List<string>();

        string saveData;
        ShopSO _shopSO = _shopGeneratePage.ShopSO;

        for (int i = 0; i < _shopSO.PagesSO.Count; i++)
        {
            for (int j = 0; j < _shopSO.PagesSO[i].ItempPage.Count; j++)
            {
                if (_shopSO.PagesSO[i].ItempPage[j].OnlyCode)
                {
                    saveData = _shopSO.PagesSO[i].PageType.ToString() + _shopSO.PagesSO[i].ItempPage[j].Index.ToString();

                    if (PlayerPrefs.GetInt(saveData, 0) == 0)
                    {
                        temp.Add(saveData);
                        keyValuePairs.Add(saveData, _shopSO.PagesSO[i].ItempPage[j]);
                    }
                }
            }
        }

        int n = UnityEngine.Random.Range(0, temp.Count);
        PlayerPrefs.SetInt(temp[n], 1);

        return keyValuePairs[temp[n]];
    }
}
