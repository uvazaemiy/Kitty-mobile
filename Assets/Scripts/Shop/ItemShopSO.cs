using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemShopSO", menuName = "Shop/ItemShopSO")]
public class ItemShopSO : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public int Price;
    public bool OnlyCode;
    public GameObject Template;
    public Vector3 Position;
    public Vector3 Rotation;
    [HideInInspector] 
    public int Index;
}
