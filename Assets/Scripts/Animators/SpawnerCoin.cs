using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerCoin : MonoBehaviour
{
    public event UnityAction MoneyAdd;

    [SerializeField] private UICoin _pfCoin;
    [SerializeField] private Transform _parent;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _endPoint;

    public void SpawnCoin(int value)
    {
        UICoin temp = Instantiate(_pfCoin, _parent);
        temp.transform.position = _spawnPoint.position;
        temp.SetValue(value);
        temp.SetTargetPosition(_endPoint);
        temp.StartAnimation();
        temp.MoneyAdd = OnMoneyAdd;
    }

    private void OnMoneyAdd()
    {
        MoneyAdd?.Invoke();
    }
}
