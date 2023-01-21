using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopupAnimate : MonoBehaviour
{
    public event UnityAction HidePopup;

    [SerializeField] private CanvasAnimation _canvasAnimation;
    [SerializeField] private ScaleAnimation _scaleAnimation;

    [SerializeField] private CanvasAnimation ExtraScaleAnimation;

    private void OnHide()
    {
        _canvasAnimation.EndAnimation -= OnHide;

        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);

        _canvasAnimation.Show();
        _scaleAnimation.Show();
    }

    public void Hide()
    {
        HidePopup?.Invoke();
        _canvasAnimation.EndAnimation += OnHide;

        _canvasAnimation.Hide();
        _scaleAnimation.Hide();
        if (ExtraScaleAnimation != null)
            ExtraScaleAnimation.Hide();
    }

    public void HideNoEvent()
    {
        _canvasAnimation.EndAnimation += OnHide;
        _canvasAnimation.Hide();
        _scaleAnimation.Hide();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && transform.name != "PopupConten" && GameObject.Find("Code") == null)
            Hide();
    }
}
