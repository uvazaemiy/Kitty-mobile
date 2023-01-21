using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesItem : MonoBehaviour
{
    public static ClothesItem Instance;

    [SerializeField] private Transform _cat;

    private ItemClothesType[] _itemClothesTypes;
    private void Awake()
    {
        Instance = this;
        _itemClothesTypes = _cat.GetComponentsInChildren<ItemClothesType>();
        CheckClothes();
    }

    public void CheckClothes(ClothesType clothesType = ClothesType.empty)
    {
        ClothesPf[] temp = GetComponentsInChildren<ClothesPf>();

        if (temp.Length != 0)
            clothesType = temp[0].ClothesType;

        for (int i = 0; i < _itemClothesTypes.Length; i++)
            _itemClothesTypes[i].gameObject.SetActive(_itemClothesTypes[i].ClothesType == clothesType);
    }

    private void OnDestroy()
    {
        _itemClothesTypes = new ItemClothesType[0];
    }
}
