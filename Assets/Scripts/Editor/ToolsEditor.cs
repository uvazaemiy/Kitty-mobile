using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ToolsEditor : MonoBehaviour
{
    [MenuItem("ToolsEditor/Clear Saves")]
    public static void ClearAllSaves()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("ToolsEditor/Add Money")]
    public static void AddMoney()
    {
        Wallet.Add(3000);
    }
}
