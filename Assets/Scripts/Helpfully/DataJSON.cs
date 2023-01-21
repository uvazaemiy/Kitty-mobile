using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataJSON : MonoBehaviour
{
    public static DataJSON Instance;

    public static UserData User;

    public bool Debag;
    public float IncreaseTime;

    private void Awake()
    {
        Instance = this;

        Load();

        DontDestroyOnLoad(this);
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey("User"))
        {
            User = JSON.From<UserData>(PlayerPrefs.GetString("User"));
            User.DataLoaded();
        }
        else
        {
            User = new UserData();
            Wallet.LoadValue(100);            
        }
    }

    public static void Save()
    {
        User.RefreshValue();
        PlayerPrefs.SetString("User", JSON.To(User));
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}

[Serializable]
public class UserData
{
    public DateTime DateTime;
    public List<ProgressItem> ProgressItems;

    public int Money;

    [NonSerialized] private Dictionary<int, ProgressBar> _progressBars;

    public UserData()
    {
        DateTime = DateTime.Now;
        ProgressItems = new List<ProgressItem>();
        _progressBars = new Dictionary<int, ProgressBar>();
        Money = 100;
    }

    public ProgressItem GetProgressItem(TypePopup typePopup)
    {
        for (int i = 0; i < ProgressItems.Count; i++)
        {
            if (ProgressItems[i].TypePopup == typePopup)
                return ProgressItems[i];
        }

        ProgressItem temp = new ProgressItem() { TypePopup = typePopup };

        ProgressItems.Add(temp);

        return temp;
    }

    public void AddToDictionary(TypePopup typePopup, ProgressBar progressBar)
    {
        for (int i = 0; i < ProgressItems.Count; i++)
        {
            if (ProgressItems[i].TypePopup == typePopup)
                _progressBars.Add(i, progressBar);
        }
    }

    public void RefreshValue()
    {
        for (int i = 0; i < ProgressItems.Count; i++)
        {
            ProgressItems[i].Fill = _progressBars[i].Fill;
        }

        DateTime = DateTime.Now;

        Money = Wallet.GetValue;
    }

    public void DataLoaded()
    {
        Wallet.LoadValue(Money);
    }
}

[Serializable]
public class ProgressItem
{
    public TypePopup TypePopup;
    public float Fill;

    public ProgressItem()
    {
        Fill = 0;
    }
}
