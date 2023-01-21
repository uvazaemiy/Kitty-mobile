using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeedsController : MonoBehaviour
{
    [Serializable]
    public class NeedReference
    {
        public GameObject Root;
        public SmoothFillAmount Progress;
        public GameObject Warning;
    }

    public List<NeedReference> NeedReferences;

    private void Start()
    {
        Needs needs = Needs.Load();

        TimeService.Interval(1, () => Update());

        NeedReferences.ForEach(needReference =>
        {
            needReference.Root.GetComponent<Button>().onClick.AddListener(() =>
            {
                needs.needs.Find(x => x.name == needReference.Root.name).Reduce(0.2f);
                needs.Save();

                Update();
            });
        });

        void Update()
        {
            needs.needs.ForEach(need =>
            {
                NeedReferences.Find(x => x.Root.name == need.name).Progress.Value = need.Value;
                NeedReferences.Find(x => x.Root.name == need.name).Warning.SetActive(need.Value == 1);
            });
        }
    }
}

public class Needs : Model<Needs>
{
    public List<Need> needs;

    public Needs() { }

    public override Needs Create()
    {
        this.needs = new List<Need>()
        {
            new Need() {
                name = "Eat",
                startTime = DateTime.Now,
                duration = 30
            },
            new Need() {
                name = "Sleep",
                startTime = DateTime.Now,
                duration = 30
            },
            new Need() {
                name = "Play",
                startTime = DateTime.Now,
                duration = 30
            },
            new Need() {
                name = "Toilet",
                startTime = DateTime.Now,
                duration = 30
            }
        };

        return this;
    }
}

public class Need
{
    public string name;
    public DateTime startTime;
    public float duration;

    public void Reduce(float value)
    {
        if ((DateTime.Now - startTime).TotalSeconds > duration)
            startTime = DateTime.Now.AddSeconds(-duration);

        startTime = startTime.AddSeconds(duration * value);

        if (startTime > DateTime.Now)
            startTime = DateTime.Now;
    }

    public float Value
    {
        get
        {
            return Mathf.Clamp((float)(DateTime.Now - startTime).TotalSeconds / duration, 0, 1);
        }
    }
}
