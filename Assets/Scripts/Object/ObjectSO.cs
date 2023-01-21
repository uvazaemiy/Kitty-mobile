using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectSO", menuName = "Object/ObjectSO")]
public class ObjectSO : ScriptableObject
{
    public TypePage PageType;
    public List<ItemShopSO> ItemShopSO = new List<ItemShopSO>();
}
