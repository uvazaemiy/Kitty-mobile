using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePopup : MonoBehaviour
{
    [SerializeField] private CanvasAnimation _popupCanvasAnimation;
    [SerializeField] private CanvasAnimation _canvasAnimation;
    [SerializeField] private ScaleAnimation _scaleAnimation;

    [SerializeField] private GameObject _panel;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnHide()
    {
        _canvasAnimation.EndAnimation -= OnHide;

        gameObject.SetActive(false);
        _panel.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _panel.SetActive(true);

        _popupCanvasAnimation.Show();
        _canvasAnimation.Show();
        _scaleAnimation.Show();
    }

    public void Hide()
    {
        _canvasAnimation.EndAnimation += OnHide;

        _popupCanvasAnimation.Hide();
        _canvasAnimation.Hide();
        _scaleAnimation.Hide();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Hide();
    }
}
