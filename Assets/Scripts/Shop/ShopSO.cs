using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopSO", menuName = "Shop/ShopSO")]
public class ShopSO : ScriptableObject
{
    public List<PageSO> PagesSO = new List<PageSO>();
}
