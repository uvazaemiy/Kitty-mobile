using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIAnswerButton : MonoBehaviour
{
    public event UnityAction<Answer> ClickAnswer;

    [SerializeField] private Button _button;
    [SerializeField] private Text _text;
    [SerializeField] private Image _image;

    private Answer _answer;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    public void SetValue(Answer answer)
    {
        _text.text = answer.Text;
        _text.fontSize = answer.SizeText;
        _image.sprite = answer.Sprite;
        _answer = answer;
    }

    private void OnClick()
    {
        ClickAnswer?.Invoke(_answer);
    }

    public void SetCanClick(bool value)
    {
        _button.interactable = value;
    }
}
