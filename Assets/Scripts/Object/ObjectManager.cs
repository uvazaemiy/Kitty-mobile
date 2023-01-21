using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotificationSamples;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private List<DataObject> _dataObjects = new List<DataObject>();

    private void Start()
    {
        UpdateObject();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && GameObject.Find("PopupBlock") == null && GameObject.Find("PopupPanel") == null)
            Application.Quit();
    }

    public void UpdateObject()
    {
        string saveData;

        for (int i = 0; i < _dataObjects.Count; i++)
        {
            for (int j = 0; j < _dataObjects[i].objectSO.ItemShopSO.Count; j++)
            {
                saveData = _dataObjects[i].objectSO.PageType.ToString() + _dataObjects[i].objectSO.ItemShopSO[j].Index.ToString();

                if (PlayerPrefs.GetInt(saveData, 0) == 2)
                {
                    if (_dataObjects[i].KeyValuePairs.ContainsKey(saveData) == false)
                    {
                        var obj = Instantiate(_dataObjects[i].objectSO.ItemShopSO[j].Template);
                        obj.transform.position = _dataObjects[i].objectSO.ItemShopSO[j].Position;
                        obj.transform.rotation = Quaternion.Euler(_dataObjects[i].objectSO.ItemShopSO[j].Rotation);
                        obj.transform.SetParent(_dataObjects[i].Parent, false);
                        _dataObjects[i].KeyValuePairs.Add(saveData, obj);
                    }
                }
                else
                {
                    if (_dataObjects[i].KeyValuePairs.ContainsKey(saveData))
                    {
                        Destroy(_dataObjects[i].KeyValuePairs[saveData]);
                        _dataObjects[i].KeyValuePairs.Remove(saveData);
                    }
                }
            }
        }
    }
}

[System.Serializable]
public class DataObject
{
    public Transform Parent;
    public ObjectSO objectSO;
    [HideInInspector] public Dictionary<string, GameObject> KeyValuePairs = new Dictionary<string, GameObject>();
}