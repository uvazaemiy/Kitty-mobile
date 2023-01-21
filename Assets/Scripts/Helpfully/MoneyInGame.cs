using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyInGame : MonoBehaviour
{
    public Text Money;
    public UserData User;

    private void Start()
    {
        User = JSON.From<UserData>(PlayerPrefs.GetString("User"));
        Money.text = User.Money.ToString();
    }

    public void PlusMoney()
    {
        Money.text = (int.Parse(Money.text) + 100).ToString();
        User.Money = int.Parse(Money.text);
        PlayerPrefs.SetString("User", JSON.To(User));
    }
}
