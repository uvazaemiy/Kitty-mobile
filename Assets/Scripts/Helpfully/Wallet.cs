using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Wallet
{
    public static event UnityAction<int> ChangetValue;
    private static int _value;

    public static void LoadValue(int value)
    {
        _value = value;

        ChangetValue?.Invoke(_value);
    }

    public static void Add(int value)
    {
        _value += value;
        ChangetValue?.Invoke(_value);
        DataJSON.Save();
    }

    public static bool TrySubtract(int value)
    {
        if (_value >= value)
        {
            _value -= value;
            ChangetValue?.Invoke(_value);
            DataJSON.Save();

            return true;
        }

        return false;
    }

    public static int GetValue => _value;
}
