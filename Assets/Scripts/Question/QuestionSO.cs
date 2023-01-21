using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionSO", menuName = "Question/QuestionSO")]
public class QuestionSO : ScriptableObject
{
    public TypePopup TypePopup;

    public string Question;

    public List<Answer> Answers = new List<Answer>();
}
