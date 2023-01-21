using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScaleAnimator : MonoBehaviour
{
    [Header("Parameters")]
    public string AnimationName = "ScaleShowFast";
    public float Delay = 0.1f;

    void Start()
    {
        List<Animation> animations = GetComponentsInChildren<Animation>().ToList().FindAll(x => x.GetClip(AnimationName) != null);

        animations.ForEach(x => x.gameObject.transform.localScale = Vector3.zero);
        animations.Shuffle().ToList().AsyncForeach((animation, dispatcher) =>
        {
            animation.Play(AnimationName);
            TimeService.Delay(Delay, () => dispatcher.Continue());
        });
    }
}
