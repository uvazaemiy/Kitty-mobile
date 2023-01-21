using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PageSO", menuName = "Shop/PageSO")]
public class PageSO : ScriptableObject
{
    public string Name;
    public TypePage PageType;
    public List<ItemShopSO> ItempPage = new List<ItemShopSO>();

    private void OnValidate()
    {
        for (int i = 0; i < ItempPage.Count; i++)
        {
            ItempPage[i].Index = i;
        }
    }
}

public enum TypePage
{
    Hairstyles, 
    Clothes, 
    Badges, 
    Decor, 
    Accessories
}