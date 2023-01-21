using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CodeDataSO", menuName = "Code/CodeData")]
public class CodeDataSO : ScriptableObject
{
    public List<CodeData> CodeDatas = new List<CodeData>();
}

[Serializable]
public class CodeData
{
    [Header("Code")]
    public string Code;
    [Header("Valid From")]
    public DateInput From;
    [Header("Valid To")]
    public DateInput To;
}

[Serializable]
public class DateInput
{
    public int Day;
    public int Month;
    public int Year;
}


//private void OnValidate()
//{
//    for (int i = 0; i < CodeDatas.Count; i++)
//    {
//        if (CodeDatas[i].From.Excel != "")
//        {
//            string[] temp = CodeDatas[i].From.Excel.Split('/');
//            CodeDatas[i].From.Month = Convert.ToInt32(temp[0]);
//            CodeDatas[i].From.Day = Convert.ToInt32(temp[1]);
//            CodeDatas[i].From.Year = Convert.ToInt32("20" + temp[2]);
//        }

//        if (CodeDatas[i].To.Excel != "")
//        {
//            string[] temp = CodeDatas[i].To.Excel.Split('/');
//            CodeDatas[i].To.Month = Convert.ToInt32(temp[0]);
//            CodeDatas[i].To.Day = Convert.ToInt32(temp[1]);
//            CodeDatas[i].To.Year = Convert.ToInt32("20" + temp[2]);
//        }
//    }
//}