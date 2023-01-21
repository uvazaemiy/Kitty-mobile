using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExclusionSO", menuName = "Shop/ExclusionSO")]
public class ExclusionSO : ScriptableObject
{
    public TypePage PageType;
    public List<ItemShopSO> ItemShopSO = new List<ItemShopSO>();
}
