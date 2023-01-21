using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

public class JSON : MonoBehaviour
{
    public static T From<T>(string json)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            Error = delegate (object sender, ErrorEventArgs args)
            {
                Debug.LogError("[JSON DESERIALIZING ERROR]" + args.ErrorContext.Error.Message + "; JSON: " + json);
                args.ErrorContext.Handled = true;
            },
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All
        };

        T obj = JsonConvert.DeserializeObject<T>(json, settings);
        return obj;
    }

    public static T From<T>(string json, System.Action<string> onErrorAction)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            Error = delegate (object sender, ErrorEventArgs args)
            {
                onErrorAction.Invoke(args.ErrorContext.Error.Message);
                Debug.LogError("[JSON DESERIALIZING ERROR]" + args.ErrorContext.Error.Message);
                args.ErrorContext.Handled = true;
            },
            TypeNameHandling = TypeNameHandling.All
        };

        T obj = JsonConvert.DeserializeObject<T>(json, settings);
        return obj;
    }

    public static string To(object obj)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            Error = delegate (object sender, ErrorEventArgs args)
            {
                Debug.LogError("[JSON SERIALIZING ERROR]" + args.ErrorContext.Error.Message);
                args.ErrorContext.Handled = true;
            },
            TypeNameHandling = TypeNameHandling.All
        };

        string json = JsonConvert.SerializeObject(obj, settings);
        //GameLog.Log("Success Serializing [ " + json + " ]");
        return json;
    }

    public static string To(object obj, bool prettyPrint)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            Error = delegate (object sender, ErrorEventArgs args)
            {
                Debug.LogError("[JSON SERIALIZING ERROR]" + args.ErrorContext.Error.Message);
                args.ErrorContext.Handled = true;
            },
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.All
        };

        string json = JsonConvert.SerializeObject(obj, settings);
        return json;
    }

    public static string To(object obj, System.Action<string> onErrorAction)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            Error = delegate (object sender, ErrorEventArgs args)
            {
                onErrorAction.Invoke(args.ErrorContext.Error.Message);
                Debug.LogError("[JSON SERIALIZING ERROR]" + args.ErrorContext.Error.Message);
                args.ErrorContext.Handled = true;
            },
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        string json = JsonConvert.SerializeObject(obj, settings);
        return json;
    }

    public static T UFrom<T>(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }

    public static string UTo(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    public static string ConvertToDataBase(string json)
    {
        return json.Replace("$type", "_type").Replace("$values", "_values");
    }

    public static string DeconvertFromDataBase(string json)
    {
        return json.Replace("_type", "$type").Replace("_values", "$values");
    }
}