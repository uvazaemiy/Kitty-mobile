using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopup : MonoBehaviour
{
    [SerializeField] private CanvasAnimation _popupCanvasAnimation;
    [SerializeField] private CanvasAnimation _canvasAnimation;
    [SerializeField] private ScaleAnimation _scaleAnimation;

    [SerializeField] private GameObject GamePanel;
    [SerializeField] private CanvasAnimation GamePanelAnimation;
    [SerializeField] private ScaleAnimation ScaleGamePanel;

    [SerializeField] private GameObject _panel;

    [SerializeField] private Text _money;
    [SerializeField] private GameObject happy;

    private void Start()
    {
        gameObject.SetActive(false);
        Wallet.ChangetValue += OnWalletChange;
    }

    private void OnWalletChange(int value)
    {
        _money.text = value.ToString();
    }

    private void OnHide()
    {
        _canvasAnimation.EndAnimation -= OnHide;

        gameObject.SetActive(false);
        _panel.SetActive(false);
    }

    public void Show()
    {
        _money.text = Wallet.GetValue.ToString();

        happy.SetActive(false);
        gameObject.SetActive(true);
        _panel.SetActive(true);

        _popupCanvasAnimation.Show();
        _canvasAnimation.Show();
        _scaleAnimation.Show();
    }

    public void Hide()
    {
        _canvasAnimation.EndAnimation += OnHide;

        happy.SetActive(true);
        _popupCanvasAnimation.Hide();
        _canvasAnimation.Hide();
        _scaleAnimation.Hide();
    }

    public void ShowGamePanel()
    {
        GamePanel.SetActive(true);
        GamePanelAnimation.Show();
        ScaleGamePanel.Show();
    }

    public void HideGamePanel()
    {
        GamePanelAnimation.EndAnimation += OnHideGamePanel;

        GamePanelAnimation.Hide();
        ScaleGamePanel.Hide();
    }

    private void OnHideGamePanel()
    {
        GamePanelAnimation.EndAnimation -= OnHideGamePanel;

        GamePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && GameObject.Find("PopupBlock") == null && GameObject.Find("PopupBlock 2") == null && GameObject.Find("Code") == null)
            Hide();
    }
}
