#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScaleState
{
    Small,
    Large
}

public class Scalable : MonoBehaviour
{
    public float smallScale;
    public float largeScale;
    public ScaleState stateOnStart;

    public float scaleDuration;
    ScaleState target;
    public DateTime? transitionStart;
    public float CurrentScale { get => transform.localScale.x; set {
            transform.localScale = new Vector3(value, value, value);
            material.SetFloat("_CurrentScale", value);
        } }
    float scaleWhenStartingTransition;
    public ScaleState Target { get => target; set {
            if (value == target) return;
            target = value;
            scaleWhenStartingTransition = CurrentScale;
            transitionStart = DateTime.Now;
        } }
    float TargetScale { get => (target) switch { ScaleState.Small => smallScale, ScaleState.Large => largeScale }; }

    Material material;

    // Start is called before the first frame update
    void Start()
    {
        target = stateOnStart;
        transitionStart = null;
        material = GetComponent<Renderer>().material;
        material.SetFloat("_SmallScale", smallScale);
        material.SetFloat("_LargeScale", largeScale);
        CurrentScale = TargetScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (transitionStart == null) return;
        DateTime now = DateTime.Now;
        double? percentage = (now - transitionStart)?.TotalMilliseconds / scaleDuration;
        if (percentage == null || percentage > 1)
        {
            CurrentScale = TargetScale;
            transitionStart = null;
            return;
        }
        Func<float, float> ease = x => x * x * (3f - 2f * x);
        CurrentScale = Mathf.Lerp(scaleWhenStartingTransition, TargetScale, ease((float)percentage));
    }

    public void Toggle()
    {
        Target = (Target) switch { ScaleState.Small => ScaleState.Large, ScaleState.Large => ScaleState.Small };
    }
}
