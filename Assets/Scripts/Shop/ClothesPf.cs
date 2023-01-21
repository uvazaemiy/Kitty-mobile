using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesPf : MonoBehaviour
{
    [SerializeField] private ClothesType _clothesType;

    public ClothesType ClothesType => _clothesType;

    private void OnEnable()
    {
        ClothesItem.Instance.CheckClothes(_clothesType);
    }

    private void OnDestroy()
    {
        ClothesItem.Instance.CheckClothes();
    }
}
