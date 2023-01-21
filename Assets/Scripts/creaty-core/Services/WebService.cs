using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebService : Service<WebService>
{
    public int DaysOffset = 1;
    public DateTime webTime = DateTime.Now;

    public override void Execute()
    {
        GetServerTime(time =>
        {
            webTime = time;
            StartCoroutine(Clock());
        });
    }

    public static void GetRequest(string uri, Action<string> onResponse)
    {
        GetInstance().StartCoroutine(GetInstance().GetRequestCoroutine(uri, onResponse));
    }

    public static void AssetBundleRequest(string uri, Action<float> onProgress, Action<AssetBundle> onResponse, Action<byte[]> onResponseData)
    {
        GetInstance().StartCoroutine(GetInstance().AssetBundleRequestCoroutine(uri, onProgress, onResponse, onResponseData));
    }

    public static void GetServerTime(Action<DateTime> onServerTime) {
        GetRequest("https://us-central1-pc-creator-test.cloudfunctions.net/timestamp", response =>
        {
            double ticks = double.Parse(response);
            TimeSpan span = TimeSpan.FromMilliseconds(ticks);
            DateTime serverTime = new DateTime(span.Ticks);

            onServerTime.Invoke(serverTime.AddDays(GetInstance().DaysOffset));
        });
    }

    private IEnumerator GetRequestCoroutine(string uri, Action<string> onResponse)
    {
        using (var webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            var pages = uri.Split('/');
            var page = pages.Length - 1;

            if (webRequest.isNetworkError)
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            else
                onResponse.Invoke(webRequest.downloadHandler.text);
        }
    }

    private IEnumerator Clock()
    {
        //Debug.LogError("Coroutine started");
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            webTime = webTime.AddSeconds(1);
        }
    }

    IEnumerator AssetBundleRequestCoroutine(string uri, Action<float> onProgress, Action<AssetBundle> onResponse, Action<byte[]> onResponseData = null)
    {
        using (var uwr = UnityWebRequest.Get(uri))
        {
            var operation = uwr.SendWebRequest();
            

            while (!operation.isDone)
            {
                onProgress.Invoke(uwr.downloadProgress);
                yield return null;
            }
            
            AssetBundle bundle = AssetBundle.LoadFromMemory(uwr.downloadHandler.data);

            onResponse.Invoke(bundle);
            onResponseData?.Invoke(uwr.downloadHandler.data);
        }
}
}
