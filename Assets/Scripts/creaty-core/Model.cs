using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Model<T> where T : Model<T>, new()
{
    public static string GetModelName() => $"{typeof(T).Name}Model";

    public static T Load() {
        return PlayerPrefs.HasKey(GetModelName()) ? JsonConvert.DeserializeObject<T>(
            PlayerPrefs.GetString(GetModelName())) : new T().Create();
    }

    public T Save() {
        PlayerPrefs.SetString(GetModelName(), JsonConvert.SerializeObject(this));
        
        return (T)this;
    }

    public abstract T Create();

    public static bool Has() => PlayerPrefs.HasKey(GetModelName());
}