using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public event UnityAction<Answer> ClickPopup;

    [SerializeField] private GameObject happy;
    [Space]

    [SerializeField] private List<UIAnswerButton> _answerButtons = new List<UIAnswerButton>();

    [SerializeField] private CanvasAnimation _popupCanvasAnimation;
    [SerializeField] private CanvasAnimation _canvasAnimation;
    [SerializeField] private ScaleAnimation _scaleAnimation;

    public void Show()
    {
        happy.SetActive(false);
        _popupCanvasAnimation.Show();
        _canvasAnimation.Show();
        _scaleAnimation.Show();
        SetCanClick(true);
    }

    public void Hide()
    {
        happy.SetActive(true);
        _popupCanvasAnimation.Hide();
        _canvasAnimation.Hide();
        _scaleAnimation.Hide();
        SetCanClick(false);
    }

    private void OnEnable()
    {
        _answerButtons.ForEach(x =>
        {
            x.ClickAnswer += OnClickAnswer;
        });
    }

    private void OnDisable()
    {
        _answerButtons.ForEach(x =>
        {
            x.ClickAnswer -= OnClickAnswer;
        });
    }

    private void OnClickAnswer(Answer value)
    {
        ClickPopup?.Invoke(value);
    }

    public void ShowQuestion(QuestionSO question)
    {
        int countAnswer = question.Answers.Count;
        List<int> randomIndexList = new List<int>();

        for (int i = 0; i < countAnswer; i++)
            randomIndexList.Add(i);

        List<int> randomButton = new List<int>();

        for (int i = 0; i < countAnswer; i++)
            randomButton.Add(0);

        int n;

        for (int i = 0; i < countAnswer; i++)
        {
            n = Random.Range(0, randomIndexList.Count);
            randomButton[i] = randomIndexList[n];
            randomIndexList.RemoveAt(n);
        }

        for (int i = 0; i < _answerButtons.Count; i++)
            _answerButtons[i].SetValue(question.Answers[randomButton[i]]);
    }

    public void SetCanClick(bool value)
    {
        _answerButtons.ForEach(x => x.SetCanClick(value));
    }

}
