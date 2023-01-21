using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = System.Random;

public static class TransformExtensions
{
    public static List<Transform> GetChilds(this Transform transform) {
        List<Transform> result = new List<Transform>();

        foreach (Transform child in transform)
        {
            result.Add(child);
        }

        return result;
    }

    public static List<Transform> GetChilds(this Transform transform, Predicate<Transform> predicate)
    {
        List<Transform> result = new List<Transform>();

        foreach (Transform child in transform)
        {
            if (predicate.Invoke(child))
                result.Add(child);
        }

        return result;
    }

    public static List<Transform> GetAllChilds(this Transform transform)
    {
        List<Transform> result = new List<Transform>();

        foreach (Transform child in transform)
        {
            result.Add(child);
            if (child.childCount > 0)
                result.AddRange(child.GetAllChilds());
        }

        return result;
    }

    public static void DestroyChilds(this Transform transform) {
        transform.GetChilds().ForEach(x => MonoBehaviour.Destroy(x.gameObject));
    }
    public static void DestroyChildsFrom(this Transform transform, int lastChildIndex)
    {
        transform.GetChilds().ForEach(x => { if (x.GetSiblingIndex() > lastChildIndex) MonoBehaviour.Destroy(x.gameObject); });
    }
    public static void DestroyChildsExcept(this Transform transform, int childIndex)
    {
        transform.GetChilds().ForEach(x => { if (x.GetSiblingIndex() != childIndex) MonoBehaviour.Destroy(x.gameObject); });
    }

    public static void DisableChilds(this Transform transform)
    {
        transform.GetChilds().ForEach(x => x.gameObject.SetActive(false));
    }
    public static void DisableChildsExcept(this Transform transform, int childIndex)
    {
        transform.GetChilds().ForEach(x => { if (x.GetSiblingIndex() != childIndex) x.gameObject.SetActive(false); });
    }
    public static void DestroyImmidiateChilds(this Transform transform)
    {
        transform.GetChilds().ForEach(x => MonoBehaviour.DestroyImmediate(x.gameObject));
    }

    public static void SetLeft(this RectTransform rt, float left)
    {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }

    public static void SetRight(this RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }

    public static void SetTop(this RectTransform rt, float top)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }

    public static void SetBottom(this RectTransform rt, float bottom)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }

    public static void AsyncForeach<T>(this List<T> list, Action<T, ListDispatcher> onElement)
    {
        void InvokeByIndex(int index)
        {
            if (index >= list.Count)
                return;

            onElement.Invoke(list[index], new ListDispatcher(() =>
            {
                InvokeByIndex(++index);
            }, index == (list.Count - 1)));
            Debug.Log($"{index}/{list.Count}");
        }

        InvokeByIndex(0);
    }

    private static Random rng = new Random();
    public static IList<T> Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

        return list;
    }

}

public class ListDispatcher
{
    public bool IsLast = false;
    private Action onContinue = () => { };

    public ListDispatcher(Action onContinue, bool isLast)
    {
        this.onContinue = onContinue;
        this.IsLast = isLast;
    }

    public void Continue() => onContinue.Invoke();
}
