using System;
using UnityEngine;

[Serializable]
public class Answer
{
    public string Text;
    public int SizeText;
    [Range(0, 100)]
    public int RewardCoefficient;
    public bool RightAnswer;
    public string Message;
    public Sprite Sprite;
}
